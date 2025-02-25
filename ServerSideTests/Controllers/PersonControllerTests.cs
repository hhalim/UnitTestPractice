using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ServerSide.Controllers;
using ServerSide.Models;
using ServerSide.Services;
using NUnit.Framework;
using Moq;

namespace Tests
{
    public class PersonControllerTests
    {
        private Mock<IPersonService> _personService = new Mock<IPersonService>();
        private PersonController _personController;

        [SetUp]
        public void Setup()
        {
            _personController = new PersonController(_personService.Object);
        }

        [Test]
        public void FindAllReturnsListOfPerson()
        {
            //Arrange
            List<Person> personList = new List<Person> { new Person { Id = 1 }, new Person { Id = 2 } };
            _personService.Setup(p => p.FindAll()).Returns(personList);

            //Act
            var controllerReturn = _personController.FindAll().Result as ObjectResult;
            var returnedList = controllerReturn.Value as List<Person>;

            //Assert
            Assert.AreEqual(200, controllerReturn.StatusCode.Value);
            Assert.AreEqual(2, returnedList.Count);
        }

        [Test]
        public void GetPersonByIdReturnsAPerson()
        {
            //Arrange
            var personId = 1;
            Person person = new Person { Id = 1 };
            _personService.Setup(p => p.Get(personId)).Returns(person);

            //Act
            var controllerReturn = _personController.Get(personId).Result as ObjectResult;
            var returnedObject = controllerReturn.Value as Person;

            //Assert
            Assert.AreEqual(200, controllerReturn.StatusCode.Value);
            Assert.IsInstanceOf<Person>(returnedObject);
            Assert.AreEqual(1, returnedObject.Id);
        }

        [Test]
        public void GetPersonByIdReturnsNotFound()
        {
            //Arrange
            var personId = 2;
            Person person = new Person { Id = 1 };
            _personService.Setup(p => p.Get(personId)).Returns((Person)null);

            //Act
            var controllerReturn = _personController.Get(personId).Result as ObjectResult;
            var returnedObject = controllerReturn.Value as Person;

            //Assert
            Assert.AreEqual(404, controllerReturn.StatusCode.Value);
            Assert.IsNull(returnedObject);
        }
    }
}