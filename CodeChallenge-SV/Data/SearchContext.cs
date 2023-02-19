using CodeChallenge_SV.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge_SV.Data
{
    public class SearchContext : DbContext
    {
        public SearchContext(DbContextOptions<SearchContext> options)
        : base(options)
        {
        }

        public DbSet<Building> Buildings { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Lock> Locks { get; set; } = null!;
        public DbSet<Medium> Media { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Search");
        }
    }
}
