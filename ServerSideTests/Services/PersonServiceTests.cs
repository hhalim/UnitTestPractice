using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ServerSide.Controllers;
using ServerSide.Models;
using ServerSide.Services;
using NUnit.Framework;
using Moq;
using ServerSide.Repositories;
using System.Linq;

namespace Tests
{
    public class PersonServiceTests
    {
        private Mock<IPersonRepository> _personRepository = new Mock<IPersonRepository>();
        private ServerSide.Services.PersonRepository _personService;

        [SetUp]
        public void Setup()
        {
            _personService = new ServerSide.Services.PersonRepository(_personRepository.Object);
        }

        [Test]
        public void FindAllReturnsListOfPerson()
        {
            //Arrange
            List<Person> personList = new List<Person> { new Person { Id = 1 }, new Person { Id = 2 } };
            _personRepository.Setup(p => p.FindAll()).Returns(personList);

            //Act
            var returnedList = _personService.FindAll().ToList();

            //Assert
            Assert.AreEqual(2, returnedList.Count);
        }

        [Test]
        public void GetPersonByIdReturnsAPerson()
        {
            //Arrange
            var personId = 1;
            Person person = new Person { Id = 1 };
            _personRepository.Setup(p => p.Get(personId)).Returns(person);

            //Act
            var returnedObject = _personService.Get(personId);

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
            _personRepository.Setup(p => p.Get(personId)).Returns((Person)null);

            //Act
            var returnedObject = _personService.Get(personId);

            //Assert
            Assert.IsNull(returnedObject);
        }
    }
}