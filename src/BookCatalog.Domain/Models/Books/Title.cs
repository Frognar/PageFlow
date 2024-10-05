using DotMaybe;

namespace BookCatalog.Domain.Models.Books;

public sealed record TitleType(string Value);

public static class Title
{
    public static Maybe<TitleType> Create(string value)
        => string.IsNullOrWhiteSpace(value)
            ? None.OfType<TitleType>()
            : Some.With(new TitleType(value.Trim()));
}
