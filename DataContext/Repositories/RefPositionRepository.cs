using DataContext.Abstractions.Interfaces;
using DataContext.Abstractions.Models;
using DataContext.Context;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repositories
{
    internal class RefPositionRepository : BaseRepository<RefPosition>, IRefPositionRepository
    {
        public RefPositionRepository(ApplicationDbContext context) : base(context)
        {

        }

        protected override IQueryable<RefPosition> Include(IQueryable<RefPosition> query)
        {
            return query.Include(x => x.Positions);
        }
    }
}
