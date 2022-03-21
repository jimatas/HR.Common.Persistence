using Microsoft.EntityFrameworkCore;

namespace HR.Common.Persistence.EntityFrameworkCore
{
    public interface IUnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext : DbContext
    {
        TDbContext DbContext { get; }
    }
}
