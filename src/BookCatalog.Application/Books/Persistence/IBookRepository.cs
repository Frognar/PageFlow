using BookCatalog.Domain.Models.Books;
using DotResult;
using Mediator;

namespace BookCatalog.Application.Books.Persistence;

public interface IBookRepository
{
    public Task<Result<Unit>> Add(BookType book);
}
