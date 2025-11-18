using DataContext.Abstractions.Interfaces;
using DataContext.Abstractions.Models;
using DataContext.Context;
using Domain.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataContext.Repositories
{
    internal class HavingsRepository : BaseRepository<Havings>, IHavingsRepository
    {
        public HavingsRepository(ApplicationDbContext context, IOptions<DataBaseOptions> options) : base(context, options)
        {

        }

        protected override IQueryable<Havings> Include(IQueryable<Havings> query)
        {
            return query.Include(x => x.Position);
        }
    }
}
