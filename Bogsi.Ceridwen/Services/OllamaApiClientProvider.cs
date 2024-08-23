using Bogsi.Ceridwen.Exceptions;
using Bogsi.Ceridwen.Utilities;

using OllamaSharp;

namespace Bogsi.Ceridwen.Services;

public interface IOllamaApiClientProvider
{
    Task<Result<IOllamaApiClient>> GetClient();
}

public class OllamaApiClientProvider : IOllamaApiClientProvider
{
    private readonly string? _connection; 
    private readonly string? _model;

    public OllamaApiClientProvider()
    {
        _connection = Environment.GetEnvironmentVariable("OLLAMA_CONNECTION_STR");

        if (string.IsNullOrWhiteSpace(_connection))
        {
            throw new ArgumentException("No connection string provided for Ollama.", _connection);
        }

        _model = Environment.GetEnvironmentVariable("OLLAMA_MODEL");

        if (string.IsNullOrWhiteSpace(_model))
        {
            throw new ArgumentException("No default model provided for Ollama.", _model);
        }
    }

    public async Task<Result<IOllamaApiClient>> GetClient()
    {
        var client = await ConstructClient(); 

        if (client.IsFailure) 
        {
            return client;
        }

        await ValidateModel(client.Value);

        return client;
    }

    private async Task<Result<IOllamaApiClient>> ConstructClient()
    {
        var ollamaConfig = new OllamaApiClient.Configuration
        {
            Uri = new Uri(_connection!),
            Model = _model!
        };

        var apiClient = new OllamaApiClient(ollamaConfig);

        var isConnected = await apiClient.IsRunning();

        return isConnected
            ? Result<IOllamaApiClient>.Success(apiClient)
            : Result<IOllamaApiClient>.Failure(OllamaApiClientProviderExceptions.UnableToConnect);
    }

    private static async Task ValidateModel(IOllamaApiClient client) 
    {
        var isDownloaded = false; 

        var models = await client!.ListLocalModels();

        foreach(var model in models) 
        {
            isDownloaded = model.Name.Contains(client.SelectedModel);

            if (isDownloaded) 
            {
                Console.WriteLine($"> {client.SelectedModel} found.");

                break;
            }
        }

        if (!isDownloaded) 
        {
            Console.WriteLine($"> {client.SelectedModel} not found, download will ensue.");

            await client.PullModel(client.SelectedModel, status => Console.WriteLine($"> ({status.Percent}%) {status.Status}"));
        }
    }
}
