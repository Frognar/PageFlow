using BookCatalog.Domain.Models.Books;

namespace BookCatalog.Domain.Processes.Books;

public delegate BookType AddAuthor(BookType book, NameType author);

public static class AddAuthorDefaults
{
    public static AddAuthor AddUniqueAuthor => (book, author)
        => book.Authors.Contains(author)
            ? book
            : book with { Authors = [..book.Authors, author] };
}
