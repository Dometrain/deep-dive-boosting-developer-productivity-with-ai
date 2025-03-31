using Dometrain.DemoAgent;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using System.Text;

const string gitHubAppName = "kevindockx-dometrain-demo-agent";
const string guidelineRepositoryOwner = "KevinDockx";
const string guidelineRepositoryName = "DometrainRAGDemo";
const string guidelineRepositoryBranch = "main";
const string guidelineFilePath = "CodingGuidelinesByExample.txt";
const string githubCopilotApiBaseAddress = "https://api.githubcopilot.com/";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped(sp =>
{
    var productInformation = new ProductHeaderValue(gitHubAppName);
    return new GitHubClient(productInformation);
});
builder.Services.AddHttpClient("GitHubCopilotApiClient", client =>
{
    client.BaseAddress = new Uri(githubCopilotApiBaseAddress);
});

var app = builder.Build();

app.MapGet("/hello", 
    () => "Hi there, I'm your friendly neighbourhood Copilot Agent.");

app.MapGet("/authentication-callback",
    () => "Lookin' good.");

app.MapPost("/incoming-copilot-message",
    async ([FromHeader(Name = "X-GitHub-Token")] string gitHubApiToken,    
    [FromServices] GitHubClient gitHubClient,
    [FromServices] IHttpClientFactory httpClientFactory,
    HttpRequest request) 
        => { 
            gitHubClient.Credentials = new Credentials(gitHubApiToken);

            string incomingCopilotPayloadAsString = string.Empty;
            IncomingCopilotPayload incomingCopilotPayload = new();
            request.EnableBuffering();
            using (var reader = new StreamReader(request.Body, 
                Encoding.UTF8, 
                leaveOpen: true))
            {
                incomingCopilotPayloadAsString = await reader.ReadToEndAsync();
                // Be a good citizen and reset the position for whatever may be next in the pipeline
                request.Body.Position = 0;
                incomingCopilotPayload = System.Text.Json.
                    JsonSerializer.Deserialize<IncomingCopilotPayload>(incomingCopilotPayloadAsString,
                        new System.Text.Json.JsonSerializerOptions(System.Text.Json.JsonSerializerDefaults.Web))
                    ?? throw new ArgumentException("Incoming payload is empty.");
            }


            var guidelineContents = await gitHubClient
                .Repository.Content.GetAllContentsByRef(
                    guidelineRepositoryOwner, 
                    guidelineRepositoryName, 
                    guidelineFilePath, 
                    guidelineRepositoryBranch);

            var guidelineContent = guidelineContents.Count > 0 ? 
                guidelineContents[0].Content : string.Empty;

            var outgoingCopilotPayload = new OutgoingCopilotPayload
            {
                Messages = incomingCopilotPayload.Messages.Concat(
                    new[]
                    {
                        new CopilotMessage
                        {
                            Role = "system",
                            Content = guidelineContent
                        }
                    }
                    ).ToList(),
                Stream = true
            };

            var httpClient = httpClientFactory
                .CreateClient("GitHubCopilotApiClient");
            httpClient.DefaultRequestHeaders.Authorization = 
                new("Bearer", gitHubApiToken);

            var copilotResponseMessage = await httpClient.PostAsJsonAsync(
                 "chat/completions",
                 outgoingCopilotPayload);

            copilotResponseMessage.EnsureSuccessStatusCode();

            return Results.Stream(
                await copilotResponseMessage.Content.ReadAsStreamAsync(),
                "application/json"); });

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();
