
using System.Net;
using System.Net.Http.Json;
using NewsFeed.Api.Models;

namespace NewsFeed.Api.Tests;

public class ArticlesEndpointsTests : IClassFixture<NewsFeedApiFactory>
{
    private readonly HttpClient _client;

    public ArticlesEndpointsTests(NewsFeedApiFactory factory)
    {
        _client = factory.CreateClient();
    }
    [Fact]
    public async Task GetArticles_ReturnsOk()
    {
        var response = await _client.GetAsync("/articles");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    [Fact]
    public async Task GetArticle_UnknownId_ReturnsNotFound()
    {
        var response = await _client.GetAsync($"/articles/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    [Fact]
    public async Task PostArticle_ThenGetById_ReturnsSameArticle()
    {
        var request = new CreateArticleRequestData("Test title", "Test body", "Tester", ["ci"]);

        var postResponse = await _client.PostAsJsonAsync("/articles", request);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
        var created = await postResponse.Content.ReadFromJsonAsync<Article>();
        Assert.NotNull(created);

        var getResponse = await _client.GetAsync($"/articles/{created.Id}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        var fetched = await getResponse.Content.ReadFromJsonAsync<Article>();
        Assert.NotNull(fetched);
        Assert.Equal("Test title", fetched.Title);
        Assert.Equal("Tester", fetched.Author);
        Assert.Equal(["ci"], fetched.Tags);
    }
}