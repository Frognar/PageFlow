using DotMaybe;
using DotResult;

namespace BookCatalog.Application.Common;

public static class MaybeToResult
{
    public static Result<T> ToResult<T>(this Maybe<T> source, Func<Failure> onNoneFailure)
    {
        return source.Match(() => Fail.OfType<T>(onNoneFailure()), Success.From);
    }
}
