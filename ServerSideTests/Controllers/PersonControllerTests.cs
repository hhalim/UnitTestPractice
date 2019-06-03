using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ServerSide.Controllers;
using ServerSide.Models;
using ServerSide.Services;

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
        public void FindAllReturnsListofPerson()
        {
            //Arrange
            List<Person> personList = new List<Person> { new Person { Id = 1 } };
            _personService.Setup(p => p.findAll()).Returns(personList);

            //Act
            var controllerReturn = _personController.findAll().Result as ObjectResult;
            var returnedList = controllerReturn.Value as List<Person>;

            //Assert
            Assert.AreEqual(200, controllerReturn.StatusCode.Value);
            Assert.AreEqual(1, returnedList.Count);
        }
    }
}