namespace HR.Common.Persistence.Tests.Integration.Fixture
{
    public static class UnitOfWorkExtensions
    {
        public static IRepository<Person> People(this IUnitOfWork uow) => uow.Repository<Person>();
        public static IRepository<Message> Messages(this IUnitOfWork uow) => uow.Repository<Message>();
    }
}
