namespace Bogsi.Ceridwen.Exceptions;

public sealed record GeneralErrors
{
    public static CeridwenException None => new(
        "CeridwenException.None",
        string.Empty);
}

public sealed record OllamaApiClientProviderExceptions 
{
    public static CeridwenException UnableToConnect => new(
        "OllamaApiClientProviderExceptions.UnableToConnect",
        "Unable to connect to Ollama instance.");
}
