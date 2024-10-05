using System.Diagnostics;
using DotMaybe;

namespace BookCatalog.Domain.Models.Books;

public abstract record NameType;

internal record FullNameType(string FirstName, string LastName) : NameType;

internal record MononymType(string Name) : NameType;

public static class Name
{
    public static Maybe<NameType> Create(string firstName, string lastName)
        => string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName)
            ? None.OfType<NameType>()
            : Some.With<NameType>(new FullNameType(firstName.Trim(), lastName.Trim()));

    public static Maybe<NameType> Create(string name)
        => string.IsNullOrWhiteSpace(name)
            ? None.OfType<NameType>()
            : Some.With<NameType>(new MononymType(name.Trim()));

    public static Maybe<IEnumerable<NameType>> CreateMany(params Maybe<NameType>[] names)
        => names.Aggregate(
            Some.With<IEnumerable<NameType>>(new List<NameType>()),
            (current, name) => Maybe.Map2(current, name, (c, n) => c.Append(n)));

    public static TResult Match<TResult>(
        this NameType name,
        Func<string, string, TResult> fullName,
        Func<string, TResult> mononym)
        => name switch
        {
            FullNameType fullNameType => fullName(fullNameType.FirstName, fullNameType.LastName),
            MononymType mononymType => mononym(mononymType.Name),
            _ => throw new UnreachableException(),
        };
}
