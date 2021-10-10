using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class TrainerController : Controller
    {
        // GET: Trainer
        public ActionResult TrainerIndex()
        {
            return View();
        }

        public ActionResult TrainerAdd()
        {
            return View();
        }

         public ActionResult TrainerEdit()
        {
            return View();
        }
    }
}