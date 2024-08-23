using System.Diagnostics.CodeAnalysis;

namespace Bogsi.Ceridwen.Exceptions;

public record CeridwenException
{
    [SetsRequiredMembers]
    public CeridwenException(string code, string? description = null) 
        => (Code, Description) = (code, description);

    public required string Code { get; init; }
    public string? Description { get; init; }
}

