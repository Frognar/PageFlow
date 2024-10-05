using System.Diagnostics;
using BookCatalog.Domain.Models.Books;

namespace BookCatalog.Domain.Processes.Books;

public delegate string FormatName(NameType name);

public delegate string FormatNameList(IEnumerable<NameType> names);

public delegate string FormatNameListExt(FormatName nameFormatter, IEnumerable<NameType> names);

public static class FormatNamesListExtensions
{
    public static FormatNameList Apply(this FormatNameListExt formatter, FormatName nameFormatter)
        => names => formatter(nameFormatter, names);
}

public static class FormatNamesListDefaults
{
    public static FormatNameListExt CommaSeparatedNames
        => (nameFormatter, names)
            => string.Join(", ", names.Select(nameFormatter.Invoke));
}

public static class FormatNameDefaults
{
    public static FormatName FormatFullName
        => name
            => name.Match((firstName, lastName) => $"{firstName} {lastName}", mononym => mononym);
}
