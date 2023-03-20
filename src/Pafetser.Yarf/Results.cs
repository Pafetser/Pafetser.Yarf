using System.Collections;

namespace Pafetser.Yarf;

public interface IResults<out T>: IResult<IEnumerable<T>>, IEnumerable<T>
{

}
public record Results<T> : Result<IEnumerable<T>>, IResults<T>
{
    public IEnumerator<T> GetEnumerator()
    {
        return (Value ?? Enumerable.Empty<T>()).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}