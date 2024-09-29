using PageFlow.WebApi.Attributes;

namespace PageFlow.WebApi.Features.Books;

[AutoEndpoint]
public static class GetBook
{
    public static void RegisterEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/books/{isbn}", GetBookByIsbn);
    }

    private static async Task<IResult> GetBookByIsbn(string isbn)
    {
        return await Task.FromResult(Results.Ok($"ISBN: {isbn}"));
    }
}
