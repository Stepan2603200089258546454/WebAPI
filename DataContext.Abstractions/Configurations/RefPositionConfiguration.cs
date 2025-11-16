using DataContext.Abstractions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataContext.Abstractions.Configurations
{
    public class RefPositionConfiguration : IEntityTypeConfiguration<RefPosition>
    {
        public void Configure(EntityTypeBuilder<RefPosition> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Positions).WithOne(x => x.RefPosition).HasForeignKey(x => x.IdRefPosition);
        }
    }
}
