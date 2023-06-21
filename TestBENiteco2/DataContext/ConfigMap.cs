using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestBENiteco2.Models;

namespace TestBENiteco2.DataContext;

public class ConfigMap :
    IEntityTypeConfiguration<Category>,
    IEntityTypeConfiguration<Customer>,
    IEntityTypeConfiguration<Order>,
    IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(a => a.Id);
    }

    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(a => a.Id);
    }

    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(a => a.Id);
        builder.HasOne(x => x.Customer)
            .WithMany(x => x.Orders)
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(a => a.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(a => a.Id);
        builder.HasOne(a => a.Category)
            .WithMany(a => a.Products)
            .HasForeignKey(a => a.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}