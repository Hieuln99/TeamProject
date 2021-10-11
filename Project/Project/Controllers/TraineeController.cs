using Project.Models;
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

        [HttpGet]
        public ActionResult TraineeAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TraineeAdd(Trainee t)
        {
             using(var TNCT = new EF.TrainingContext())
            {
                TNCT.trainees.Add(t);
                TNCT.SaveChanges();
            }
            TempData["message"] = $"Add Successfully a trainee with id: {t.id}";
            return RedirectToAction("TraineeIndex");
        }

        [HttpGet]
         public ActionResult TraineeEdit(int id)
        {
            using (var TNCT = new EF.TrainingContext())
            {
                var trainee = TNCT.trainees.FirstOrDefault(t => t.id == id);
                if (trainee == null)
                {
                    return RedirectToAction("TraineeIndex");
                }
                else
                {
                    return View(trainee);
                }
            }
        }
        [HttpPost]
        public ActionResult TraineeEdit(int id, Trainee t)
        {
            using (var TNCT = new EF.TrainingContext())
            {
                TNCT.Entry<Trainee>(t).State = System.Data.Entity.EntityState.Modified;
                TNCT.SaveChanges();
            }
            TempData["message"] = $"Edit successfully a trainee with id: {t.id}";
            return RedirectToAction("TraineeIndex");
        }

        public ActionResult TraineeDelete(int id)
        {
            using (var TNCT = new EF.TrainingContext())
            {
                var t = TNCT.trainees.FirstOrDefault(u => u.id == id);
                if (t == null)
                {
                    TempData["message"] = $"Delete failed";
                }
                TNCT.trainees.Remove(t);
                TNCT.SaveChanges();

                TempData["message"] = $"Delete successfully a trainee with id: {t.id}";
            }
            return RedirectToAction("TraineeIndex");
        }
    }
}