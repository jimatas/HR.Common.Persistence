using HR.Common.Persistence.InMemory;
using HR.Common.Persistence.Tests.Fixture;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Threading.Tasks;

namespace HR.Common.Persistence.Tests
{
    [TestClass]
    public class UnitOfWorkTests
    {
        private IServiceProvider serviceProvider;
        private IUnitOfWork uow;

        [TestInitialize]
        public void Initialize()
        {
            var services = new ServiceCollection()
                .AddLogging(logging => logging.AddConsole())
                .AddUnitOfWork();

            serviceProvider = services.BuildServiceProvider();
            uow = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        [TestCleanup]
        public void CleanUp() => (serviceProvider as IDisposable)?.Dispose();

        [TestMethod]
        public void EnsureUnitOfWorkRegistered()
        {
            // Arrange

            // Act

            // Assert
            Assert.IsNotNull(uow);
        }

        [TestMethod]
        public void Repository_GivenValidGenericType_ReturnsRepository()
        {
            // Arrange

            // Act
            var personRepository = uow.Repository<Person>();

            // Assert
            Assert.IsNotNull(personRepository);
        }

        [TestMethod]
        public void Repository_CalledTwiceForSameGenericType_ReturnsSameRepository()
        {
            // Arrange

            // Act
            var personRepository = uow.Repository<Person>();
            var anotherPersonRepository = uow.Repository<Person>();

            // Assert
            Assert.AreEqual(personRepository, anotherPersonRepository);
        }

        [TestMethod]
        public void Repository_CalledTwiceForDifferentGenericTypes_ReturnsTwoRepositories()
        {
            // Arrange

            // Act
            var bookRepository = uow.Repository<Book>();
            var personRepository = uow.Repository<Person>();

            // Assert
            Assert.AreNotEqual(bookRepository, personRepository);
        }

        [TestMethod]
        public void Complete_ByDefault_FiresCompletedEvent()
        {
            // Arrange
            var isCompleted = false;
            uow.Completed += (sender, e) => isCompleted = true;

            // Act
            uow.Complete();

            // Assert
            Assert.IsTrue(isCompleted);
        }

        [TestMethod]
        public async Task CompleteAsync_ByDefault_FiresCompletedEvent()
        {
            // Arrange
            var isCompleted = false;
            uow.Completed += (sender, e) => isCompleted = true;

            // Act
            await uow.CompleteAsync();

            // Assert
            Assert.IsTrue(isCompleted);
        }
    }
}
