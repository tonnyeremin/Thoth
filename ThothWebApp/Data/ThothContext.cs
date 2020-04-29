using Microsoft.EntityFrameworkCore;

namespace Thoth.Data
{
    public class ThothContext : DbContext
    {
        public ThothContext(DbContextOptions<ThothContext> options) : base(options){}

        public DbSet<QuoteItem> QuoteItems{get; set;}
    }
}