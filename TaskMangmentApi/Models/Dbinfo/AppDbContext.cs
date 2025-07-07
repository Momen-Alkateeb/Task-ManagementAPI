using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace TaskMangmentApi.Models.Dbinfo
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<Tasks>Task { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options):
            base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(TaskConfig).Assembly);

        }
    }
}
