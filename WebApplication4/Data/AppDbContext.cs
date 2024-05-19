using WebApplication4.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<DocumentHeader> DocumentHeaders { get; set; }
        public DbSet<DocumentLine> DocumentLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.DocumentHeaders)
                .WithOne(dh => dh.User)
                .HasForeignKey(dh => dh.UserId);

            modelBuilder.Entity<DocumentHeader>()
                .HasMany(dh => dh.DocumentLines)
                .WithOne(dl => dl.DocumentHeader)
                .HasForeignKey(dl => dl.DocumentHeaderId);
        }
    }
}