using HR.Common.Utilities;

using System;

namespace HR.Common.Persistence
{
    public class UnitOfWorkCompletedEventArgs : EventArgs
    {
        public UnitOfWorkCompletedEventArgs(IUnitOfWork uow)
            => UnitOfWork = Ensure.Argument.NotNull(() => uow);

        public IUnitOfWork UnitOfWork { get; }
    }
}
