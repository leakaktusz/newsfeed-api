using NewsFeed.Api.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();



app.Run();


