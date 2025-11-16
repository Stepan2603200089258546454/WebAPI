using DataContext.Abstractions.Interfaces;
using DataContext.Abstractions.Models;
using DataContext.Context;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repositories
{
    internal class HavingsRepository : BaseRepository<Havings>, IHavingsRepository
    {
        public HavingsRepository(ApplicationDbContext context) : base(context)
        {

        }

        protected override IQueryable<Havings> Include(IQueryable<Havings> query)
        {
            return query.Include(x => x.Position);
        }
    }
}
