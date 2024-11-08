using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Troas.Customer.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Domain.Customer> Customers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        // Define indexes
        builder.Entity<Domain.Customer>()
            .HasIndex(c => c.EmailAddress)
            .IsUnique();
        
        builder.Entity<Domain.Customer>()
            .HasIndex(c => c.PhoneNumber)
            .IsUnique();

        builder.Entity<Domain.Customer>()
            .HasIndex(c => c.FirstName);
        
        builder.Entity<Domain.Customer>()
            .HasIndex(c => c.LastName);
        
        base.OnModelCreating(builder);
    }
}