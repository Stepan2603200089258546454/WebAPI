using DataContext.Abstractions.Interfaces;
using DataContext.Abstractions.Models;
using DataContext.Context;
using Domain.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataContext.Repositories
{
    internal class DrivingSchoolRepository : BaseRepository<DrivingSchool>, IDrivingSchoolRepository
    {
        public DrivingSchoolRepository(ApplicationDbContext context, IOptions<DataBaseOptions> options) : base(context, options)
        {

        }

        protected override IQueryable<DrivingSchool> Include(IQueryable<DrivingSchool> query)
        {
            return query.Include(x => x.Positions);
        }
    }
}
