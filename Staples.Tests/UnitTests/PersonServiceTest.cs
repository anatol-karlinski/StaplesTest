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
        [TestMethod]
        public void AddNewPerson_ReturnSuccess()
        {
            var newPersonDetails = new PersonDetails()
            {
                FirstName = "TestPersonFirstName",
                MiddleName = "TestPersonMiddleName",
                LastName = "TestPersonFirstName",
                Age = 100,
                Email = "test@test.com",
                Gender = Gender.Male
            };

            var repoMock = new Mock<IPersonRepository>();

            repoMock.Setup(x => x.AddAsync(It.IsAny<Person>()))
                .Returns(Task.FromResult(1));

            repoMock.Setup(x => x.GetPeopleByFirstAndLastNameAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(new List<Person>()));

            var adapterMock = new Mock<IPersonAdapter>();
            adapterMock.Setup(x => x.Adapt(It.IsAny<PersonDetails>())).Returns(new Person()
            {
                FirstName = newPersonDetails.FirstName,
                LastName = newPersonDetails.LastName,
                ID = 1
            });

            var service = new PeopleService(repoMock.Object, adapterMock.Object);
            var serviceresponse = service.AddNewPerson(newPersonDetails).Result;

            Assert.IsTrue(serviceresponse.OperationSuccessful);
        }

        [TestMethod]
        public void AddExistingPerson_ReturnError()
        {
            var newPersonDetails = new PersonDetails()
            {
                FirstName = "TestPersonFirstName",
                MiddleName = "TestPersonMiddleName",
                LastName = "TestPersonFirstName",
                Age = 100,
                Email = "test@test.com",
                Gender = Gender.Male
            };

            var repoMock = new Mock<IPersonRepository>();

            repoMock.Setup(x => x.AddAsync(It.IsAny<Person>()))
                .Returns(Task.FromResult(0));

            repoMock.Setup(x => x.GetPeopleByFirstAndLastNameAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(new List<Person> {
                    new Person()
                }));

            var adapterMock = new Mock<IPersonAdapter>();
            adapterMock.Setup(x => x.Adapt(It.IsAny<PersonDetails>())).Returns(new Person()
            {
                FirstName = newPersonDetails.FirstName,
                LastName = newPersonDetails.LastName,
                ID = 1
            });

            var service = new PeopleService(repoMock.Object, adapterMock.Object);
            var serviceresponse = service.AddNewPerson(newPersonDetails).Result;

            Assert.IsFalse(serviceresponse.OperationSuccessful);
        }
    }
}
