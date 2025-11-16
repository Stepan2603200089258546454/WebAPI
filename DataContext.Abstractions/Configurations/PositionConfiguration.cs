using DataContext.Abstractions.Models;
using DataContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataContext.Abstractions.Configurations
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.DrivingSchool).WithMany(x => x.Positions).HasForeignKey(x => x.IdDrivingSchool);
            builder.HasOne(x => x.RefPosition).WithMany(x => x.Positions).HasForeignKey(x => x.IdRefPosition);
            builder.HasOne(x => x.User).WithOne(x => x.Position).HasForeignKey<ApplicationUser>(x => x.IdPosition);
            builder.HasMany(x => x.Havings).WithOne(x => x.Position).HasForeignKey(x => x.IdPosition);
        }
    }
}
