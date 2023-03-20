namespace Pafetser.Yarf;

public static class CollectionExtensions
{
    public static TResults WithItem<TResults, T>(this TResults result, T? item) where TResults : Results<T?>
    {
        return result.WithItems(item);
    }

    public static TResults WithItems<TResults, T>(this TResults result, IEnumerable<T?> items) where TResults : Results<T?>
    {
        if (result == null) throw new ArgumentNullException(nameof(result));
        if (items == null) throw new ArgumentNullException(nameof(items));
        if (result._value is null) result._value = new List<T>();
        foreach (var item in items)
            result._value = result._value.Append(item);
        return result;
    }

    public static TResults WithItems<TResults, T>(this TResults result, params T?[] items) where TResults : Results<T?>
    {
        return result.WithItems((IEnumerable<T?>)items);
    }
}