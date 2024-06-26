using Microsoft.EntityFrameworkCore;
using OCS.Applications.Domain.Entitites;

namespace OCS.Applications.Infrastructure.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Application> Applications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>().HasKey(a => a.Id);
        
        // Cringe
        modelBuilder.Entity<Application>()
            .HasIndex(a => new{a.AuthorId, a.Status})
            .HasFilter($"\"Status\" = '{ApplicationStatus.Draft}'")
            .IsUnique();
        
        modelBuilder.Entity<Application>().Property(a => a.Id).ValueGeneratedOnAdd();
        
        // Enums Activities, ApplicationStatus должны храниться в виде строк
        modelBuilder.Entity<Application>().Property(a => a.Activity).HasConversion<string>();
        modelBuilder.Entity<Application>().Property(a => a.Status).HasConversion<string>();
    }
}