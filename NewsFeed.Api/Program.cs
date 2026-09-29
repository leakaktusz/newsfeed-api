using NewsFeed.Api.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

var articles = new List<Article>
{
    new Article(
        Guid.NewGuid(),
        "Ritzau launches new API",
        "The Danish news agency published a new service for media customers.",
        "Lea Lestál",
        DateTimeOffset.UtcNow.AddDays(-1),
        new[] { "tech", "denmark" }
    ),
    new Article(
        Guid.NewGuid(),
        "Something happened",
        "Something new, and exciting happened here and now.",
        "Lea Lestál",
        DateTimeOffset.UtcNow.AddDays(-1),
        new[] { "thing", "denmark" }
    ),
    // add two more here
};

app.MapGet("/articles", () => articles);
app.MapGet("/articles/{id:guid}", (Guid id) =>
{
    var article =articles.FirstOrDefault(a=>a.Id ==id);
    return article is null ? Results.NotFound() : Results.Ok(article);
}
);
app.MapPost("/articles", (CreateArticleRequest request) =>
{
        var article = new Article(
        Guid.NewGuid(),
        request.Title,
        request.Body,
        request.Author,
        DateTimeOffset.UtcNow,
        request.Tags ?? Array.Empty<string>()
    );
articles.Add(article);
return Results.Created($"/articles/{article.Id}", article);

});
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();



app.Run();


