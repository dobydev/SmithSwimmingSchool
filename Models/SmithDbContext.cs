using Microsoft.EntityFrameworkCore;

namespace SmithSwimmingSchool.Models
{
    public class SmithDbContext : DbContext
    {
        // Constructor that accepts DbContextOptions
        public SmithDbContext(DbContextOptions<SmithDbContext> options)
            : base(options)
        {

        }

        // Add DbSet<T> properties here as needed
    }
}