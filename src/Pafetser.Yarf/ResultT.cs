using System.Diagnostics.CodeAnalysis;

namespace Pafetser.Yarf;

public interface IResult<out T>: IResult
{
    T? Value { get; }
}
public record Result<T>: Result, IResult<T>
{
    public Result() { }
    [SetsRequiredMembers]
    public Result(T? value) => Value = value;
    internal T? _value;
    public required T? Value 
    {
        get => _value;
        init => _value = value;
    }
    public static implicit operator T?(Result<T> result) => result.Value;
    public static implicit operator Result<T>(T? value) => new Result<T>(value);
}