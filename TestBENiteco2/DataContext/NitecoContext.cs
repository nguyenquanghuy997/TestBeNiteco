using Microsoft.EntityFrameworkCore;
using TestBENiteco2.Models;

namespace TestBENiteco2.DataContext;

public class NitecoContext : DbContext
{
    public NitecoContext(DbContextOptions<NitecoContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}