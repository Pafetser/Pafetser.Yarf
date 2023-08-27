namespace Pafetser.Yarf;

public static class ToExtensions
{
    public static TTarget To<TSource, TTarget>(this TSource from)
        where TSource : Result
        where TTarget : Result, new()
    {
        if (from is null) throw new ArgumentNullException(nameof(from));
        return new TTarget().WithMessages(from.Messages.Select(m => m with { }));
    }
}