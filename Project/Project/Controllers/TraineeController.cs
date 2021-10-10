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
            using (var TNCT = new EF.TrainingContext())
            {
                var trainees = TNCT.trainees.OrderBy(t => t.id).ToList();
                return View(trainees);
            }
        }

        public ActionResult TraineeAdd()
        {
            return View();
        }


         public ActionResult TraineeEdit()
        {
            return View();
        }
    }
}