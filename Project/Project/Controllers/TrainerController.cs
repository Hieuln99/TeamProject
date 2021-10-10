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
            using(var TNCT = new EF.TrainingContext())
            {
                var trainers = TNCT.trainers.OrderBy(t => t.id).ToList();
                return View(trainers);
            }
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