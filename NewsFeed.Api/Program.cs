using NewsFeed.Api.Models;
using NewsFeed.Api.Data;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<NewsFeedDbContext>(options=>
options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));


var app = builder.Build();

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();



app.MapGet("/articles", async (NewsFeedDbContext db) =>
await db.Articles.OrderByDescending(a => a.PublishedAt).ToListAsync());

app.MapGet("/articles/{id:guid}", async (Guid id, NewsFeedDbContext db) =>
{
    var article = await db.Articles.FindAsync(id);
    return article is null ? Results.NotFound() : Results.Ok(article);
}
);
app.MapPost("/articles", async (CreateArticleRequestData request,  NewsFeedDbContext db) =>
{
    var article = new Article(
    Guid.NewGuid(),
    request.Title,
    request.Body,
    request.Author,
    DateTimeOffset.UtcNow,
    request.Tags ?? Array.Empty<string>()
);
    db.Articles.Add(article);
    await db.SaveChangesAsync();
    return Results.Created($"/articles/{article.Id}", article);

});
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}





app.Run();


