using Microsoft.VisualStudio.TestTools.UnitTesting;
using Staples.DAL.Interfaces;
using Staples.DAL.Models;
using Staples.DAL.Repositories;
using Staples.GUI.Controllers;
using Staples.GUI.ViewModels;
using Staples.SL.Interfaces;
using Staples.SL.Services;
using System.Linq;
using System.Web.Mvc;

namespace Staples.Tests.IntegrationTests
{
    [TestClass]
    public class PeopleControllerTest
    {
        private static string _testPersonFirstName = "TestPersonFirstName";
        private static string _testPersonLastName = "TestPersonLasteName";

        private static IPeopleService _peopleService;
        private static IPersonRepository _personRepository;

        private static PeopleController _peopleController;

        [ClassInitialize]
        public static void TestSetup(TestContext context)
        {
            GUI.Infrastructure.AutoMapperSetup.SetupAutoMapper();
            _personRepository = new PersonRepository();
            _peopleService = new PeopleService(_personRepository);
            _peopleController = new PeopleController(_peopleService);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            DeleteTestPersonFromDatabase();
        }

        [TestMethod]
        public void CreateNewPerson_PersonAddedToDatabase()
        {
            _peopleController = new PeopleController(_peopleService);
            var inputViewModel = new PersonDetailsViewModel()
            {
                PersonDetails = new PersonDetails()
                {
                    FirstName = _testPersonFirstName,
                    LastName = _testPersonLastName
                }
            };
            var result = _peopleController.Create(inputViewModel).Result;

            if (result is ViewResult)
            {
                var responseViewModel = ((result as ViewResult).Model as PersonDetailsViewModel);
                Assert.IsFalse(responseViewModel.Errors.Any());
            }

            Assert.IsTrue(TestPersonExistsInTheDatabase());
        }

        [TestMethod]
        public void CreateExistingPerson_ViewModelReturnsWithErrors()
        {
            InsertTestPersonIntoDatabase();
            _peopleController = new PeopleController(_peopleService);

            var inputViewModel = new PersonDetailsViewModel()
            {
                PersonDetails = new PersonDetails()
                {
                    FirstName = _testPersonFirstName,
                    LastName = _testPersonLastName
                }
            };

            var result = _peopleController.Create(inputViewModel).Result;
            var responseViewModel = ((result as ViewResult).Model as PersonDetailsViewModel);

            Assert.IsTrue(responseViewModel.Errors.Any());
        }

        [TestCleanup]
        public void TestMethodCleanup()
        {
            DeleteTestPersonFromDatabase();
        }

        [ClassCleanup]
        public static void TestTeardown()
        {
            DeleteTestPersonFromDatabase();
        }

        private static void InsertTestPersonIntoDatabase()
        {
            using (var context = new StaplesDBContext())
            {
                context.People.Add(new Person()
                {
                    FirstName = _testPersonFirstName,
                    LastName = _testPersonLastName
                });
                context.SaveChanges();
            }
        }

        private static void DeleteTestPersonFromDatabase()
        {
            using (var context = new StaplesDBContext())
            {
                var peopleToRemove = context
                    .People
                    .Where(x => x.FirstName == _testPersonFirstName && x.LastName == _testPersonLastName)
                    .ToList();

                context.People.RemoveRange(peopleToRemove);
                context.SaveChanges();
            }
        }

        private static bool TestPersonExistsInTheDatabase()
        {
            using (var context = new StaplesDBContext())
            {
                var testPerson = context
                    .People
                    .Where(x => x.FirstName == _testPersonFirstName && x.LastName == _testPersonLastName)
                    .ToList();

                return testPerson.Any();
            }
        }
    }
}
