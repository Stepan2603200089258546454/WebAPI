using DataContext.Abstractions.Interfaces;
using DataContext.Abstractions.Models;
using DataContext.Context;
using Domain.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataContext.Repositories
{
    internal class RefPositionRepository : BaseRepository<RefPosition>, IRefPositionRepository
    {
        public RefPositionRepository(ApplicationDbContext context, IOptions<DataBaseOptions> options) : base(context, options)
        {

        }

        protected override IQueryable<RefPosition> Include(IQueryable<RefPosition> query)
        {
            return query.Include(x => x.Positions);
        }
    }
}
