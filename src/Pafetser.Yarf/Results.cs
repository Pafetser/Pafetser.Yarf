using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Pafetser.Yarf;

public interface IResults<out T>: IResult<IEnumerable<T?>>, IEnumerable<T?>
{

}
public record Results<T> : Result<IEnumerable<T?>>, IResults<T?>
{
    public Results() { }
    [SetsRequiredMembers]
    public Results(IEnumerable<T?> value):base(value) { }
    [SetsRequiredMembers]
    public Results(params T?[] value) : this((IEnumerable<T?>)value) { }

    public IEnumerator<T?> GetEnumerator()
    {
        return (Value ?? Enumerable.Empty<T?>()).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}