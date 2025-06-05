using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Configurations;

/// <summary>
/// Entity Framework configuration for the Cart entity.
/// Defines relationships, constraints, and mappings.
/// </summary>
public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.User)
               .WithMany(u => u.Carts)
               .HasForeignKey(c => c.UserId)
               .OnDelete(DeleteBehavior.Cascade); // ou Restrict, se preferir

        builder.Property(c => c.Status)
               .HasConversion<string>(); // se estiver usando enum como string

        builder.Property(c => c.CreatedAt).IsRequired();
        builder.Property(c => c.UpdatedAt);
    }
}
