using DataContext.Abstractions.Interfaces;
using DataContext.Abstractions.Models;
using DataContext.Context;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repositories
{
    internal class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        public PositionRepository(ApplicationDbContext context) : base(context)
        {

        }

        protected override IQueryable<Position> Include(IQueryable<Position> query)
        {
            return query
                .Include(x => x.DrivingSchool)
                .Include(x => x.RefPosition)
                .Include(x => x.User)
                .Include(x => x.Havings);
        }
    }
}
