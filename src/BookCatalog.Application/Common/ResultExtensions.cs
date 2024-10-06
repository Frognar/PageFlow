using DotResult;

namespace BookCatalog.Application.Common;

public static class ValidationResult
{
    public static Result<TResult> Combine<T, U, TResult>(
        Result<T> first,
        Result<U> second,
        Func<T, U, TResult> onSuccess)
    {
        return first
            .Match(
                f1 => second.Match(
                    f2 => Fail.OfType<TResult>(Failure.Validation(metadata: f1.Metadata.Concat(f2.Metadata).ToDictionary())),
                    _ => Fail.OfType<TResult>(f1)),
                s1 => second.Match(
                    Fail.OfType<TResult>,
                    s2 => Success.From(onSuccess(s1, s2))));
    }
}
