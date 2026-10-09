using Microsoft.EntityFrameworkCore;
using NewsFeed.Api.Data;
using NewsFeed.Api.Models;

namespace NewsFeed.Api.Endpoints;

public static class ArticleEndpoints
{
    public static void MapArticleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/articles");

        group.MapGet("/", GetArticles);
        group.MapGet("/{id:guid}", GetArticle);
        group.MapPost("/", CreateArticle);
    }

    private static async Task<List<Article>> GetArticles(NewsFeedDbContext db) =>
        await db.Articles.OrderByDescending(a => a.PublishedAt).ToListAsync();

    private static async Task<IResult> GetArticle(Guid id, NewsFeedDbContext db)
    {
        var article = await db.Articles.FindAsync(id);
        return article is null ? Results.NotFound() : Results.Ok(article);
    }

    private static async Task<IResult> CreateArticle(CreateArticleRequestData request, NewsFeedDbContext db)
    {
        var article = new Article(
            Guid.NewGuid(),
            request.Title,
            request.Body,
            request.Author,
            DateTimeOffset.UtcNow,
            request.Tags ?? []);

        db.Articles.Add(article);
        await db.SaveChangesAsync();

        return Results.Created($"/articles/{article.Id}", article);
    }
}