using Pafetser.Yarf.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Pafetser.Yarf;

/// <summary>
/// Describes any message that provides more additional information about an <see cref="IResult"/>
/// </summary>
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