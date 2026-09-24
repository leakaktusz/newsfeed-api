namespace NewsFeed.Api.Models;

public record Article(
    Guid Id,
    string Title,
    string Body,
    string Author,
    DateTimeOffset PublishedAt,
    string[] Tags
);