using BookCatalog.Application.Common;
using BookCatalog.Domain.Models.Books;
using DotMaybe;
using DotResult;

namespace BookCatalog.Application.Books.Commands.Add;

public static class AddBookCommandValidator
{
    public static Result<BookType> Validate(AddBookCommand command)
    {
        return 
            ValidationResult.Combine(
                ValidationResult.Combine(
                    ValidateIsbn(command.Isbn),
                    ValidateTitle(command.Title),
                    (isbn, title) => (isbn, title)),
                ValidateAuthors(command.Authors),
                (x, authors) => Book.Create(x.isbn, x.title, authors));
    }

    private static Result<IsbnType> ValidateIsbn(string isbn)
        => Isbn.Create(isbn).ToResult(() => Failure.Validation(
            metadata: new Dictionary<string, object>{ { "isbn", "Invalid ISBN" }}));

    private static Result<TitleType> ValidateTitle(string title)
        => Title.Create(title).ToResult(() => Failure.Validation(
            metadata: new Dictionary<string, object>{ { "title", "Invalid title" }}));

    private static Result<IEnumerable<NameType>> ValidateAuthors(string[] authors)
        => Name.CreateMany(authors.Select(CreateAuthor).ToArray())
            .ToResult(() => Failure.Validation(
                metadata: new Dictionary<string, object>{ { "authors", "Invalid authors" }}));

    private static Maybe<NameType> CreateAuthor(string author)
    {
        return author.Split(' ') switch
        {
            [{ } name] => Name.Create(name),
            [{ } firstName, { } lastName] => Name.Create(firstName, lastName),
            _ => None.OfType<NameType>(),
        };
    }
}
