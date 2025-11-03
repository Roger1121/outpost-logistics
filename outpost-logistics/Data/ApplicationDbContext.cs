using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using outpost_logistics.Models;

namespace outpost_logistics.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public ApplicationDbContext() : base(new DbContextOptions<ApplicationDbContext>()) { }
        public virtual DbSet<Course>? Courses { get; set; }
        public virtual DbSet<Vehicle>? Vehicles { get; set; }
    }
}
