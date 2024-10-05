namespace BookCatalog.Domain.Models.Books;

public sealed record BookType(IsbnType Isbn, TitleType Title, IEnumerable<NameType> Authors);

public static class Book
{
    public static BookType Create(IsbnType isbn, TitleType title, IEnumerable<NameType> authors)
        => new(isbn, title, authors);
}
