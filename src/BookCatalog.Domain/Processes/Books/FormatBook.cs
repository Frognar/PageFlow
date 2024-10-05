using BookCatalog.Domain.Models.Books;

namespace BookCatalog.Domain.Processes.Books;

public delegate string FormatBook(BookType book);

public delegate string FormatBookExt(FormatNameList namesFormatter, BookType book);

public static class FormatBookExtensions
{
    public static FormatBook Apply(this FormatBookExt formatter, FormatNameList namesFormatter)
        => book
            => formatter(namesFormatter, book);
}

public static class FormatBookDefaults
{
    public static FormatBookExt TitleThenNames
        => (namesFormatter, book)
            => $"{book.Title.Value} by {namesFormatter(book.Authors)}";
}
