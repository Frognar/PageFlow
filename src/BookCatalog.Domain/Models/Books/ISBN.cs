using DotMaybe;

namespace BookCatalog.Domain.Models.Books;

public sealed record IsbnType(string Value);

public static class Isbn
{
    public static Maybe<IsbnType> Create(string value)
        => string.IsNullOrWhiteSpace(value) || value.Trim().Length != 13
            ? None.OfType<IsbnType>()
            : Some.With(new IsbnType(value.Trim()));
}
