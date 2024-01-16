using Microsoft.EntityFrameworkCore;

namespace dotnetWebService
{
    public class MyDbContext : DbContext
    {
        // DbContext class for interacting with the database
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }
        // DbSet representing the "shoppingSummary" table in the database
        public DbSet<ShoppingSummary> shoppingSummary { get; set; }
    }
}
