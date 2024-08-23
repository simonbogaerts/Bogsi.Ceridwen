using System;

using OllamaSharp;

namespace Bogsi.Ceridwen.Utilities;

public class OllamaQuestion(IOllamaApiClient client) : IDisposable
{
    private ConversationContext? _context = null;
    private readonly IOllamaApiClient _client = client;

    public async Task AskOllama(string question) 
    {
        await foreach (var stream in _client.StreamCompletion(question, _context))
        {
            Console.Write(stream?.Response);
        }

        Console.WriteLine();
    }

    public void Dispose()
    {
        _context = null;
    }
}
