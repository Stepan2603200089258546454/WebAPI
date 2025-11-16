using DataContext.Abstractions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataContext.Abstractions.Configurations
{
    public class HavingsConfiguration : IEntityTypeConfiguration<Havings>
    {
        public void Configure(EntityTypeBuilder<Havings> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Position).WithMany(x => x.Havings).HasForeignKey(x => x.IdPosition);
        }
    }
}
