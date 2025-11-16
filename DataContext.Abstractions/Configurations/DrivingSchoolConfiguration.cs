using DataContext.Abstractions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Abstractions.Configurations
{
    public class DrivingSchoolConfiguration : IEntityTypeConfiguration<DrivingSchool>
    {
        public void Configure(EntityTypeBuilder<DrivingSchool> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Positions).WithOne(x => x.DrivingSchool).HasForeignKey(x => x.IdDrivingSchool);
        }
    }
}
