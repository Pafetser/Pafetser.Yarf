using Pafetser.Yarf.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Pafetser.Yarf;

public abstract record Message
{
    private readonly string _description = default!;
    public Message()
    {
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="description"></param>
    /// <param name="code"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentEmptyException"></exception>
    [SetsRequiredMembers]
    public Message(string description, string? code = default)
    {
        Description = description;
        Code = code;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentEmptyException"></exception>
    public required string Description
    {
        get => _description;
        init
        {
            if (value is null) throw new ArgumentNullException(nameof(Description));
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentEmptyException(nameof(Description));
            _description = value;
        }
    }
    public string? Code { get; init; }
    public override string ToString()
    {
        return $"{(!string.IsNullOrWhiteSpace(Code) ? $"[{Code}] " : null)}{Description}";
    }
}








public record ExceptionalError : Error
{
    public ExceptionalError() : base()
    {
    }
    [SetsRequiredMembers]
    public ExceptionalError(Exception ex) : base(ex?.ToString()!)
    {
        Exception = ex!;
    }

    private readonly Exception _exception = default!;
    public required Exception Exception
    {
        get => _exception;
        init
        {
            if (value == default) throw new ArgumentNullException(nameof(Exception));
            _exception = value;
        }
    }
    public override string ToString()
    {
        return Exception.ToString();
    }

    public static implicit operator ExceptionalError(Exception ex) => new ExceptionalError(ex);
    public static implicit operator Exception(ExceptionalError exceptionalError) => exceptionalError.Exception;
}