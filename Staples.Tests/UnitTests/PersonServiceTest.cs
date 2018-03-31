using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Staples.Adapters.Interfaces;
using Staples.DAL.Enums;
using Staples.DAL.Interfaces;
using Staples.DAL.Models;
using Staples.SL.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Staples.Tests.UnitTests
{
    [TestClass]
    public class PersonServiceTest
    {

        private PersonDetails _newPersonDetails = new PersonDetails()
        {
            FirstName = "TestPersonFirstName",
            MiddleName = "TestPersonMiddleName",
            LastName = "TestPersonFirstName",
            Age = 100,
            Email = "test@test.com",
            Gender = Gender.Male
        };

        [TestMethod]
        public void AddNewPerson_ReturnSuccess()
        {
            var repoMock = new Mock<IPersonRepository>();

            repoMock.Setup(x => x.AddAsync(It.IsAny<Person>()))
                .Returns(Task.FromResult(1));

            repoMock.Setup(x => x.GetPeopleByFirstAndLastNameAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(new List<Person>()));

            var adapterMock = new Mock<IPersonAdapter>();
            adapterMock.Setup(x => x.Adapt(It.IsAny<PersonDetails>()))
                .Returns(new Person()
                {
                    FirstName = _newPersonDetails.FirstName,
                    LastName = _newPersonDetails.LastName,
                    ID = 1
                });

            var service = new PeopleService(repoMock.Object, adapterMock.Object);
            var serviceResponse = service.AddNewPerson(_newPersonDetails).Result;

            Assert.IsTrue(serviceResponse.OperationSuccessful);
        }

        [TestMethod]
        public void AddExistingPerson_ReturnError()
        {
            var repoMock = new Mock<IPersonRepository>();

            repoMock.Setup(x => x.AddAsync(It.IsAny<Person>()))
                .Returns(Task.FromResult(0));

            repoMock.Setup(x => x.GetPeopleByFirstAndLastNameAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(new List<Person> {
                    new Person()
                }));

            var adapterMock = new Mock<IPersonAdapter>();
            adapterMock.Setup(x => x.Adapt(It.IsAny<PersonDetails>()))
                .Returns(new Person()
                {
                    FirstName = _newPersonDetails.FirstName,
                    LastName = _newPersonDetails.LastName,
                    ID = 1
                });

            var service = new PeopleService(repoMock.Object, adapterMock.Object);
            var serviceResponse = service.AddNewPerson(_newPersonDetails).Result;

            Assert.IsFalse(serviceResponse.OperationSuccessful);
        }
    }
}
