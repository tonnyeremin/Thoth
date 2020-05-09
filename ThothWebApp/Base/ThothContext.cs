using System;
using Microsoft.EntityFrameworkCore;

namespace ThothBase
{
    public class ThothContext : DbContext
    {
        public ThothContext(DbContextOptions<ThothContext> options) : base(options){}

        public DbSet<QuoteItem> QuoteItems{get; set;}

    }
}