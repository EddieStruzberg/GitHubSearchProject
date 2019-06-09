using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GitHubSearchProject.Controllers
{
    public class HomeController : Controller
    {
        //Home Search repository controller
        public ActionResult Index()
        {
            return View();
        }
        //repository display controller
        public ActionResult SearchRepositories()
        {
            return View();
        }
        //BookMarks display controller
        public ActionResult BookMarks()
        {
            return View();
        }
    }
}