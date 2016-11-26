using AuditedRepository.DA.EntityFramework.Interfaces.Contexts;
using AuditedRepository.Interfaces.Repositories;
using AuditedRepository.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.DA.EntityFramework.Models;

namespace Tests.DA.EntityFramework
{
    [TestClass]
    public class RepositoryTests
    {
        private static IRepository<Entity> _repository;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // Set up context
            var data = new List<Entity>()
            {
                #region // Test seed data
                new Entity()
                {
                    Id = "A",
                    CreatedDate = DateTime.UtcNow.Date
                },
                new Entity()
                {
                    Id = "B",
                    CreatedDate = DateTime.UtcNow.Date
                },
                new Entity()
                {
                    Id = "C",
                    CreatedDate = DateTime.UtcNow.Date
                },
                new Entity()
                {
                    Id = "D",
                    CreatedDate = DateTime.UtcNow.Date,
                    IsArchived = true
                }
                #endregion
            }.AsQueryable<Entity>();

            var mockEntities = new Mock<DbSet<Entity>>();
            mockEntities.As<IQueryable<Entity>>().Setup(m => m.Provider).Returns(data.Provider);
            mockEntities.As<IQueryable<Entity>>().Setup(m => m.Expression).Returns(data.Expression);
            mockEntities.As<IQueryable<Entity>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockEntities.As<IQueryable<Entity>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<MockDbContext>();
            mockContext.Setup(m => m.Set<Entity>()).Returns(mockEntities.Object);

            // Set up repository
            _repository = new Models.MockRepository(mockContext.Object);
        }

        #region Any Tests
        [TestMethod]
        public void Any_ValidEntity()
        {
            // Assign
            var id = "A";

            // Act
            var result = _repository.Any(e => e.Id == id);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Any_InvalidEntity()
        {
            // Assign
            var id = "0";

            // Act
            var result = _repository.Any(e => e.Id == id);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Any_ArchivedEntity()
        {
            // Assign
            var id = "D";

            // Act
            var result = _repository.Any(e => e.Id == id);

            // Assert
            Assert.IsFalse(result);
        }
        #endregion

        #region Find Tests
        [TestMethod]
        public void FindById_ValidEntity()
        {
            // Assign
            var id = "A";

            // Act
            var result = _repository.FindById(id);

            // Assert
            Assert.AreEqual(id, result.Id);
        }

        [TestMethod]
        public void FindById_InvalidEntity()
        {
            // Assign
            var id = "0";

            // Act
            var result = _repository.FindById(id);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void FindById_ArchivedEntity()
        {
            // Assign
            var id = "D";

            // Act
            var result = _repository.FindById(id);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void FindById_BypassArchivedEntity()
        {
            // Assign
            var id = "D";

            // Act
            var result = _repository.FindById(id, true);

            // Assert
            Assert.AreEqual(id, result.Id);
        }

        [TestMethod]
        public void Find_ValidEntity()
        {
            // Assign
            var id = "A";

            // Act
            var result = _repository.Find(e => e.Id == id);

            // Assert
            Assert.AreEqual(result.Count(), 1);
            Assert.AreEqual(id, result.First().Id);
        }

        [TestMethod]
        public void Find_InvalidEntity()
        {
            // Assign
            var id = "0";

            // Act
            var result = _repository.Find(e => e.Id == id);

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void Find_ArchivedEntity()
        {
            // Assign
            var id = "D";

            // Act
            var result = _repository.Find(e => e.Id == id);

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void Find_AllEntities()
        {
            // Assign

            // Act
            var result = _repository.Find();

            // Assert
            Assert.AreEqual(3, result.Count());
            Assert.IsFalse(result.Any(r => r.IsArchived));
        }

        [TestMethod]
        public void Find_AllNonArchivedEntities()
        {
            // Assign

            // Act
            var result = _repository.Find(e => !e.IsArchived);

            // Assert
            Assert.AreEqual(3, result.Count());
            Assert.IsFalse(result.Any(r => r.IsArchived));
        }

        [TestMethod]
        public void Find_AllEntities_TakeSingle()
        {
            // Assign

            // Act
            var result = _repository.Find(null, null, 0, 1);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.IsFalse(result.Any(r => r.IsArchived));
        }
        #endregion
    }
}
