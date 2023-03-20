namespace Pafetser.Yarf;

public static class ToExtensions
{
    public static TTarget To<TSource, TTarget>(TSource from)
        where TSource : Result
        where TTarget : Result, new()
    {
        if (from is null) throw new ArgumentNullException(nameof(from));
        var target = new TTarget();
        foreach (var message in from.Messages)
            target.WithMessage(message with { });
        return target;
    }
}