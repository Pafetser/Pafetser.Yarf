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

public record Information : Message
{
    public Information() : base()
    {
    }
    [SetsRequiredMembers]
    public Information(string information, string? code = default) : base(information, code)
    {
    }
    public static implicit operator Information(string description) => new Information(description);
    public static implicit operator string(Information information) => information.ToString();
    public override string ToString()
    {
        return base.ToString();
    }
}

public record Warning : Message
{
    public Warning() : base()
    {
    }
    [SetsRequiredMembers]
    public Warning(string warning) : base(warning)
    {
    }

    public static implicit operator Warning(string value) => new Warning(value);
    public static implicit operator string(Warning warning) => warning.Description;
}

public record Error : Message
{
    public Error() : base()
    {
    }
    [SetsRequiredMembers]
    public Error(string error) : base(error)
    {
    }
    public static implicit operator Error(string value) => new Error(value);
    public static implicit operator string(Error error) => error.Description;
}

public record ValidationError : Error
{
    public ValidationError() : base()
    {
    }
    [SetsRequiredMembers]
    public ValidationError(string source, string error) : base(error)
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
        return $"[{Source}] {base.ToString()}";
    }
    public static implicit operator ValidationError((string source, string value) item) => new ValidationError(item.source, item.value);
    public static implicit operator string(ValidationError validationError) => validationError.ToString();
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