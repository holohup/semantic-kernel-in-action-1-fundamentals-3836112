using Microsoft.SemanticKernel;

namespace _02_05;

public class TryingOutTheKernel
{
    public static async Task Execute()
    {
        var modelDeploymentName = "gpt-4";
        var azureOpenAIEndpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
        var azureOpenAIApiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_KEY");

        var builder = Kernel.CreateBuilder();
        builder.Services.AddAzureOpenAIChatCompletion(
            modelDeploymentName,
            azureOpenAIEndpoint,
            azureOpenAIApiKey
        );
        var kernel = builder.Build();

        var topic = "The Semantic Kernel SDK has been born and is out to the world on December 19th, now all .NET developers are AI developers...";
        var prompt = $"Generate a very short funny poem about the given event. Be creative and be funny. Make the words ryhme together. Let your imagination run wild. Event:{topic}";
        var poemResult = await kernel.InvokePromptAsync(prompt);

        Console.WriteLine(poemResult);
        Console.ReadLine();
    }
}