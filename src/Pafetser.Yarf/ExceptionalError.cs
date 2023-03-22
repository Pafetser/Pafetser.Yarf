using System.Diagnostics.CodeAnalysis;

namespace Pafetser.Yarf;

public record ExceptionalError : Error
{

    [SetsRequiredMembers]
    public ExceptionalError(Exception ex) : base(ex?.ToString()!)
    {
        Exception = ex ?? throw new ArgumentNullException(nameof(ex));
    }

    public Exception Exception
    {
        get;
    }
    public override string ToString()
    {
        return Exception.ToString();
    }

    public static implicit operator ExceptionalError(Exception ex) => new ExceptionalError(ex);
    public static implicit operator Exception(ExceptionalError exceptionalError) => exceptionalError.Exception;
}