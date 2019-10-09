using Microsoft.EntityFrameworkCore;

namespace CRUDelicious.Models
{
    public class CRUDContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public CRUDContext(DbContextOptions options) : base(options) { }

        public DbSet<Dish> Dishes { get; set; }
    }
}