using System.Runtime.Serialization;

namespace Pafetser.Yarf.Exceptions;

[Serializable]
public class ArgumentEmptyException : ArgumentException
{
    public ArgumentEmptyException() : base()
    {
    }

    public ArgumentEmptyException(string? paramName)
        : base($"{paramName} cannot be empty", paramName)
    {
    }

    public ArgumentEmptyException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    public ArgumentEmptyException(string? paramName, string? message)
        : base(message, paramName)
    {
    }

    protected ArgumentEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}