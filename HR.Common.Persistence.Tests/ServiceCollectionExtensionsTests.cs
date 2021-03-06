using HR.Common.Persistence.InMemory;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

namespace HR.Common.Persistence.Tests
{
    [TestClass]
    public class ServiceCollectionExtensionsTests
    {
        private readonly IServiceCollection services = new ServiceCollection();

        [TestMethod]
        public void AddUnitOfWork_ByDefault_RegistersInMemoryUnitOfWork()
        {
            // Arrange

            // Act
            services.AddUnitOfWork();
            var serviceProvider = services.BuildServiceProvider();

            IUnitOfWork uow = serviceProvider.GetService<IUnitOfWork>();

            // Assert
            Assert.IsInstanceOfType(uow, typeof(UnitOfWork));
        }

        [TestMethod]
        public void AddUnitOfWork_GivenInvalidRepositoryFactoryType_ThrowsArgumentException()
        {
            // Arrange
            var invalidRepositoryFactoryType = typeof(object);

            // Act
            void action() => services.AddUnitOfWork(invalidRepositoryFactoryType);

            // Assert
            Assert.ThrowsException<ArgumentException>(action);
        }

        [TestMethod]
        public void AddUnitOfWork_GivenRepositoryFactoryInterfaceType_ThrowsArgumentException()
        {
            // Arrange
            var repositoryFactoryInterfaceType = typeof(IRepositoryFactory);

            // Act
            void action() => services.AddUnitOfWork(repositoryFactoryInterfaceType);

            // Assert
            Assert.ThrowsException<ArgumentException>(action);
        }

        [TestMethod]
        public void AddUnitOfWork_GivenValidRepositoryFactoryType_DoesNotThrow()
        {
            // Arrange
            var validRepositoryFactoryType = typeof(RepositoryFactory);

            // Act
            void action() => services.AddUnitOfWork(validRepositoryFactoryType);

            // Assert
            try
            {
                action();
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}
