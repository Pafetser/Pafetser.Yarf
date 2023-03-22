using Pafetser.Yarf.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Pafetser.Yarf;

public record ValidationError : Error
{
    public ValidationError() : base()
    {
    }
    [SetsRequiredMembers]
    public ValidationError(string source, string description, string? code = default) : base(description, code)
    {
        Source = source;
    }

    private readonly string _source = default!;
    public required string Source
    {
        get => _source;
        init
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentEmptyException(nameof(Source));
            _source = value;
        }
    }
    public override string ToString()
    {
        return $"{base.ToString()} ({Source})";
    }

    public static implicit operator string(ValidationError validationError) => validationError.ToString();
}