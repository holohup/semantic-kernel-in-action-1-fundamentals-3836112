using Microsoft.SemanticKernel;

namespace _03_03e;

public class PromptFunction
{
  private static string someText = "Effective prompt design is essential to achieving desired outcomes with LLM AI models. Prompt engineering, also known as prompt design, is an emerging field that requires creativity and attention to detail. It involves selecting the right words, phrases, symbols, and formats that guide the model in generating high-quality and relevant texts.\r\n\r\nIf you've already experimented with ChatGPT, you can see how the model's behavior changes dramatically based on the inputs you provide. For example, the following prompts produce very different outputs:";

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
    var kernel = builder.Build();

    await CreateAndExecuteAPrompt(kernel);
    await ImportPluginFromFolderAndExecuteIt(kernel);

    Console.ReadLine();
  }

  public static async Task CreateAndExecuteAPrompt(Kernel kernel)
  {
    var prompt = "What is the meaning of life?";
    var kernelFunction = kernel.CreateFunctionFromPrompt(prompt);
    var result = await kernelFunction.InvokeAsync(kernel);

    Console.WriteLine(result);
  }

  public static async Task ImportPluginFromFolderAndExecuteIt(Kernel kernel)
  {
    // Import a plugin from a prompt directory
    var SummarizePluginDirectory = Path.Combine(
        System.IO.Directory.GetCurrentDirectory(),
        "plugins",
        "SummarizePlugin");
    kernel.ImportPluginFromPromptDirectory(SummarizePluginDirectory);

    var summarizeResult =
        await kernel.InvokeAsync(
          "SummarizePlugin",
          "Summarize",
          new() {
            { "input", someText }
          });

    Console.WriteLine($"Result:  {summarizeResult}");
  }

}