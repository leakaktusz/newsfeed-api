using Microsoft.EntityFrameworkCore;
using NewsFeed.Api.Models;

namespace NewsFeed.Api.Data;

public class NewsFeedDbContext : DbContext
{
    public NewsFeedDbContext(DbContextOptions<NewsFeedDbContext> options) : base(options)
    {
    }
    public DbSet<Article> Articles => Set<Article>();
}