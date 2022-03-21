using System.Threading;
using System.Threading.Tasks;

namespace HR.Common.Persistence.InMemory
{
    public class UnitOfWork : UnitOfWorkBase
    {
        public UnitOfWork() : this(new RepositoryFactory()) { }
        public UnitOfWork(IRepositoryFactory repositoryFactory) : base(repositoryFactory) { }

        public override bool IsTransactional => false;

        public override void BeginTransaction() { }

        public override void Complete()
            => OnCompleted(new UnitOfWorkCompletedEventArgs(this));

        public override Task CompleteAsync(CancellationToken cancellationToken = default)
        {
            Complete();
            return Task.CompletedTask;
        }
    }
}
