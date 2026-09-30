namespace NewsFeed.Api.Models;

public record CreateArticleRequestData(string Title, string Body, string Author, string[] Tags);