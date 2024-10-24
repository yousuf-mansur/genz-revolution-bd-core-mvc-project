using Microsoft.EntityFrameworkCore;

namespace GenZRevolutionBD.Models
{
    public class SuperHeroDbContext:DbContext
    {
        public SuperHeroDbContext(DbContextOptions<SuperHeroDbContext>options):base(options)
        {
            
        }

        public DbSet<Power> Powers { get; set; }
        public DbSet<SuperHero> SuperHeroes { get; set; }
        public DbSet<SuperPower> SuperPoweres { get; set; }

    }
}
