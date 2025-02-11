using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    internal class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<ShortedUrl> ShortedUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ShortedUrl>()
                .HasKey(x => x.Guid);

            modelBuilder.Entity<ShortedUrl>()
                .HasIndex(x => x.Code)
                .IsUnique();
        }
    }
}
