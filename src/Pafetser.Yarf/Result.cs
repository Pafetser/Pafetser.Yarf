namespace Pafetser.Yarf;

/// <summary>
/// Describes an item that contains extra information about the result of executing an operation
/// </summary>
public interface IResult
{
    /// <summary>
    /// All messages that are generated when executing an operation
    /// </summary>
    IEnumerable<Message> Messages { get; }
    /// <summary>
    /// All informational messages that are generated when executing an operation
    /// </summary>
    IEnumerable<Information> Informations { get; }
    /// <summary>
    /// All warnings that are generated when executing an operation
    /// </summary>
    IEnumerable<Warning> Warnings { get; }
    /// <summary>
    /// All errors that are generated when executing an operation
    /// </summary>
    IEnumerable<Error> Errors { get; }
    /// <summary>
    /// Flag indicating wether executing the operation was succesful
    /// </summary>
    bool IsSuccess { get; }
    /// <summary>
    /// Flag indicating there were any warnings generated when executing the operation
    /// </summary>
    bool HasWarnings { get; }
}
/// <summary>
/// Default implementation of <see cref="IResult"/>
/// </summary>
public record Result: IResult
{
    internal readonly List<Message> _messages = new List<Message>();
    /// <inheritdoc/>
    public IEnumerable<Message> Messages => _messages.AsReadOnly();
    /// <inheritdoc/>
    public IEnumerable<Information> Informations => Messages.OfType<Information>();
    /// <inheritdoc/>
    public IEnumerable<Warning> Warnings => Messages.OfType<Warning>();
    /// <inheritdoc/>
    public IEnumerable<Error> Errors => Messages.OfType<Error>();
    /// <inheritdoc/>
    public bool IsSuccess { get; internal set; } = true;
    /// <inheritdoc/>
    public bool HasWarnings { get; internal set; } = false;
}