using BookCatalog.Application.Books.Persistence;
using DotResult;
using Mediator;
using static BookCatalog.Application.Books.Commands.Add.AddBookCommandValidator;

namespace BookCatalog.Application.Books.Commands.Add;

public sealed record AddBookCommand(string Isbn, string Title, string[] Authors)
    : IRequest<Result<AddBookResponseType>>;

public sealed class AddBookCommandHandler(IBookRepository repository)
    : IRequestHandler<AddBookCommand, Result<AddBookResponseType>>
{
    public async ValueTask<Result<AddBookResponseType>> Handle(
        AddBookCommand command,
        CancellationToken cancellationToken)
    {
        return await (
            from b in Validate(command)
            from _ in repository.Add(b)
            select AddBookResponse.From(b));
    }
}
