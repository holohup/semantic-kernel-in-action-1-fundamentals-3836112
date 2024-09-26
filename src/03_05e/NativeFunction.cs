using Microsoft.SemanticKernel;

namespace _03_05e;

public class NativeFunction
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
        azureOpenAIApiKey,
        modelId: "gpt-4-32k"
    );
    builder.Plugins.AddFromType<MyMathPlugin>();
    var kernel = builder.Build();


    // Also able to add it after the kernel has been built
    // kernel.ImportPluginFromType<MyMathPlugin>();
    var NumberToSquareRoot = 81;
    var squareRootResult =
        await kernel.InvokeAsync(
          "MyMathPlugin",
          "Sqrt",
          new() {
            { "number1", NumberToSquareRoot }
          });

    Console.WriteLine($"The Square root of {NumberToSquareRoot} is:  {squareRootResult}");

    Console.ReadLine();
  }
}