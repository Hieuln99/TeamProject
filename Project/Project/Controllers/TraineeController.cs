using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class TraineeController : Controller
    {
        // GET: Trainee
        public ActionResult TraineeIndex()
        {
            return View();
        }

        public ActionResult TraineeAdd()
        {
            return View();
        }
    }
}