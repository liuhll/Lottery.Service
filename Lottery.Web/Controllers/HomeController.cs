using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lottery.Web.Models;
using Lottery.Web.Services;

namespace Lottery.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserNavigationManager _userNavigationManager;

        public HomeController()
        {
            _userNavigationManager = new UserNavigationManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public PartialViewResult Menu(string activeMenu = "")
        {
            var model = new TopMenuViewModel
            {
                MainMenu = _userNavigationManager.GetMenu(),
                ActiveMenuItemName = activeMenu
            };
            return PartialView("_Menu", model);
        }
    }
}