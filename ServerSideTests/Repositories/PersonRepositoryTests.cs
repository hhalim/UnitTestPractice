using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ServerSide.Repositories;
using ServerSide.Models;
using NUnit.Framework;
using System.Linq;
using Moq;

namespace Tests
{
    public class PersonRepositoryTests
    {
        private Mock<PersonDbContext> _personDbContext = new Mock<PersonDbContext>(MockBehavior.Default, new DbContextOptions<PersonDbContext>());
        private PersonRepository _personRepository;

        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }

        [SetUp]
        public void Setup()
        {
            var person1 = new Person()
            {
                Id = 1,
                address = "123 Main Street",
                firstName = "John",
                lastName = "Doe"
            };

            DbSet<Person> myDbSet = GetQueryableMockDbSet(new List<Person>() { person1 });

            _personDbContext.Setup(p => p.People).Returns(myDbSet);
            //_personDbContext.Setup(p => p.Initialize).Returns(void);

            _personRepository = new PersonRepository(_personDbContext.Object);
        }

        [Test]
        public void FindAllReturnsListOfPerson()
        {
            //Arrange
            List<Person> personList = new List<Person> { new Person { Id = 1 }, new Person { Id = 2 } };
            DbSet<Person> myDbSet = GetQueryableMockDbSet(personList);
            _personDbContext.Setup(p => p.People).Returns(myDbSet);

            //Act
            var returnedList = _personRepository.FindAll().ToList();

            //Assert
            Assert.AreEqual(2, returnedList.Count);
        }

        [Test]
        public void GetPersonByIdReturnsAPerson()
        {
            //Arrange
            var personId = 1;
            Person person = new Person { Id = 1 };
            DbSet<Person> myDbSet = GetQueryableMockDbSet(new List<Person>() { person });
            _personDbContext.Setup(p => p.People).Returns(myDbSet);

            //Act
            var returnedObject = _personRepository.Get(personId);

            //Assert
            Assert.IsInstanceOf<Person>(returnedObject);
            Assert.AreEqual(1, returnedObject.Id);
        }

        [Test]
        public void GetPersonByIdReturnsNotFound()
        {
            //Arrange
            var personId = 2;
            Person person = new Person { Id = 1 };
            DbSet<Person> myDbSet = GetQueryableMockDbSet(new List<Person>() { person });
            _personDbContext.Setup(p => p.People).Returns(myDbSet);

            //Act
            var returnedObject = _personRepository.Get(personId);

            //Assert
            Assert.IsNull(returnedObject);
        }
    }
}