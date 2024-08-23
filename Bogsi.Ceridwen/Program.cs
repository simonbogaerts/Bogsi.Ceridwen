using Bogsi.Ceridwen.Services;
using Bogsi.Ceridwen.Utilities;

using OllamaSharp;

internal class Program
{
    private static IOllamaApiClient? _client;

    private static async Task Main()
    {
        Console.WriteLine("> Welcome to Ceridwen by BOGsi...");

        var validatedClient = await new OllamaApiClientProvider().GetClient();

        if (validatedClient.IsFailure) 
        {
            Console.WriteLine($"Something went wrong = {validatedClient.Error!.Code} which means: {validatedClient.Error!.Description}");
            Console.WriteLine("> Closing Ceridwen...");

            return;
        }

        _client = validatedClient.Value!;

        var running = true; 

        do {

            Console.WriteLine("> What is your question? Say /bye to exit...");
            Console.Write("> ");
            var question = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(question) || question.ToLowerInvariant().Equals("/bye")) 
            {
                running = false;
            }
            else 
            {
                await new OllamaQuestion(_client).AskOllama(question);
            }

        } while (running);

        Console.WriteLine("> Closing Ceridwen...");
    }
}