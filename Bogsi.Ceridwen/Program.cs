using Bogsi.Ceridwen.Services;

using OllamaSharp;

internal class Program
{
    private static IOllamaApiClient? _client;

    private static async Task Main()
    {
        Console.WriteLine("Welcome to Ceridwen by BOGsi...");

        var validatedClient = await new OllamaApiClientProvider().GetClient();

        if (validatedClient.IsFailure) 
        {
            Console.WriteLine($"Something went wrong = {validatedClient.Error!.Code} which means: {validatedClient.Error!.Description}");
            Console.WriteLine("Closing Ceridwen...");

            return;
        }

        _client = validatedClient.Value!;


        ConversationContext context = null;
        await foreach (var stream in _client.StreamCompletion("How are you today?", context))
        {
            Console.Write(stream.Response);
        }
    }
}