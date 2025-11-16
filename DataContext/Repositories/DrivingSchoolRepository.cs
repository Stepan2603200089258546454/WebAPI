using DataContext.Abstractions.Interfaces;
using DataContext.Abstractions.Models;
using DataContext.Context;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repositories
{
    internal class DrivingSchoolRepository : BaseRepository<DrivingSchool>, IDrivingSchoolRepository
    {
        public DrivingSchoolRepository(ApplicationDbContext context) : base(context)
        {

        }

        protected override IQueryable<DrivingSchool> Include(IQueryable<DrivingSchool> query)
        {
            return query.Include(x => x.Positions);
        }
    }
}
