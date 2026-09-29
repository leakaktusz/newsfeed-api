namespace NewsFeed.Api.Models;

public record CreateArticleRequest(string Title, string Body, string Author, string[] Tags);