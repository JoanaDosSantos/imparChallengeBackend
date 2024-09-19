using imparChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace imparChallenge.Models.Contexts
{
    public class ImparTestContext : DbContext
    {
        public ImparTestContext(DbContextOptions<ImparTestContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<Photo> Photo { get; set; } = null!;
    }
}
