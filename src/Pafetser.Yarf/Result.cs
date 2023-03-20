namespace Pafetser.Yarf;

public interface IResult
{
    IEnumerable<Message> Messages { get; }
    IEnumerable<Information> Informations { get; }
    IEnumerable<Warning> Warnings { get; }
    IEnumerable<Error> Errors { get; }
    bool IsSuccess { get; }
    bool HasWarnings { get; }
}
public record Result: IResult
{
    internal readonly List<Message> _messages = new List<Message>();
    public IEnumerable<Message> Messages => _messages.AsReadOnly();
    public IEnumerable<Information> Informations => Messages.OfType<Information>();
    public IEnumerable<Warning> Warnings => Messages.OfType<Warning>();
    public IEnumerable<Error> Errors => Messages.OfType<Error>();
    public bool IsSuccess { get; internal set; } = true;
    public bool HasWarnings { get; internal set; } = false;
}