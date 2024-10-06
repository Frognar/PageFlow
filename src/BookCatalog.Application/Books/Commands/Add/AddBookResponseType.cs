using BookCatalog.Domain.Models.Books;

namespace BookCatalog.Application.Books.Commands.Add;

public sealed record AddBookResponseType(string Isbn);

public static class AddBookResponse
{
    public static AddBookResponseType From(BookType book) => new(book.Isbn.Value);
}
