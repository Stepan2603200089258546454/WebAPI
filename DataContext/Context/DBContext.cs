using DataContext.Abstractions.Configurations;
using DataContext.Abstractions.Models;
using DataContext.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<DrivingSchool> DrivingSchools { get; set; }
        public DbSet<Havings> Havings { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<RefPosition> RefPositions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DrivingSchoolConfiguration());
            builder.ApplyConfiguration(new HavingsConfiguration());
            builder.ApplyConfiguration(new PositionConfiguration());
            builder.ApplyConfiguration(new RefPositionConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
