using Microsoft.EntityFrameworkCore;

namespace ecs_fargate.DbContexts
{
    public class HomeDbContext : DbContext
    {
        public HomeDbContext(DbContextOptions<HomeDbContext> options) : base(options)
        {
        }
    }
}
