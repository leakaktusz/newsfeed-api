using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewsFeed.Api.Data;

namespace NewsFeed.Api.Tests;

public class NewsFeedApiFactory : WebApplicationFactory<Program>
{
    private const string TestConnectionString =
    "Host=localhost;Port=5432;Database=newsfeed_test;Username=postgres;Password=dev";
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("ConnectionStrings:Default", TestConnectionString);
    }
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var host = base.CreateHost(builder);

        using var scope = host.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<NewsFeedDbContext>();
        db.Database.Migrate();

        return host;
    }

}