using Bogsi.Ceridwen.Exceptions;

namespace Bogsi.Ceridwen.Utilities;

public readonly struct Result<TValue>
{
    private Result(bool isSuccess, TValue? value, CeridwenException error)
    {
        if (isSuccess && error != GeneralErrors.None || !isSuccess && error == GeneralErrors.None) 
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        Value = value;
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; init; }

    public bool IsFailure => !IsSuccess;

    public readonly TValue? Value { get; init; }

    public readonly CeridwenException? Error { get; init; }

    public static Result<TValue> Success(TValue value) => new (true, value, GeneralErrors.None);

    public static Result<TValue> Failure(CeridwenException error) => new (false, default, error);
}
