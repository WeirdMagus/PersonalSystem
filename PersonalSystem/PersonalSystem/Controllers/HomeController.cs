using PersonalSystem.Models;
using PersonalSystem.Repositories;
using PersonalSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PersonalSystem.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

        public ActionResult ApplicantStart()
        {
            return View();
        }

        public ActionResult CreateCompany()
        {
            return View();
        }

        public ActionResult CreateNews()
        {
            return View();
        }

        public ActionResult AdminStart()
        {
            return View();
        }

        public ActionResult Application()
        {
            return View( new ApplicationViewModel{ Name="Pelle Persson"} );
        }

        public ActionResult WorkerStart()
        {
            return View();
        }

        public ActionResult MinaSidor()
        {
            return View();
        }

        public ActionResult CreateSchedule()
        {
            return View();
        }

        public ActionResult ChefStart()
        {
            return View();
        }

        public ActionResult ShowCompanies()
        {
            return View();
        }

        public ActionResult ShowVacancies()
        {
            return View();
        }

		public ActionResult About()
		{
			ViewBag.Message = "Test av Angular & Bootstrap-komponenter";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";
			AddPhoneNumberViewModel vm = new AddPhoneNumberViewModel{
				Number = "123"
			};
			return View(vm);
		}

		public async Task<ActionResult> TESTVIEW()
		{
			CompanyRepository repo = new CompanyRepository();
			Company createTest = new Company { Name = "654321" };
			Company test = await repo.CreateCompany(createTest);
			return View(await repo.GetAllCompanies());
		}

        //public ActionResult Applications()
        //{
        //    return View();
        //}
	}
}