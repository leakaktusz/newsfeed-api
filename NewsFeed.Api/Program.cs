using NewsFeed.Api.Data;
using NewsFeed.Api.Endpoints;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<NewsFeedDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));


var app = builder.Build();

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.MapArticleEndpoints();




app.Run();


