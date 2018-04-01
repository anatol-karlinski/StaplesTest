using Staples.DAL.Models;
using Staples.GUI.ViewModels;
using Staples.SL.Interfaces;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Staples.GUI.Controllers
{
    public class PeopleController : Controller
    {
        private StaplesDBContext db = new StaplesDBContext();
        private readonly IPeopleService _peopleService;

        public PeopleController(IPeopleService peopleService)
        {

            _peopleService = peopleService;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _peopleService.GetAllPeople());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PersonDetailsViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var response = await _peopleService.AddNewPerson(viewModel.PersonDetails);

            if (response.OperationSuccessful)
                return RedirectToAction("Index");
            else
            {
                viewModel.Errors.AddRange(response.Errors);
                return View(viewModel);
            }
        }
    }
}
