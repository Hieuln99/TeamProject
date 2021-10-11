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
            validation(t);
            if (!ModelState.IsValid)
            {
                return View(t);
            }
            else
            {
                using (var TNCT = new EF.TrainingContext())
                {
                    TNCT.trainees.Add(t);
                    TNCT.SaveChanges();
                }
                TempData["message"] = $"Add Successfully a trainee with id: {t.id}";
                return RedirectToAction("TraineeIndex");
            }
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
            validation(t);
            if (!ModelState.IsValid)
            {
                return View(t);
            }
            else
            {
                using (var TNCT = new EF.TrainingContext())
                {
                    TNCT.Entry<Trainee>(t).State = System.Data.Entity.EntityState.Modified;
                    TNCT.SaveChanges();
                }
                TempData["message"] = $"Edit successfully a trainee with id: {t.id}";
                return RedirectToAction("TraineeIndex");
            }
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

        private void validation(Trainee t)
        {
            DateTime t1 = t.dob;
            DateTime t2 = new DateTime(2003, 01, 01);
            string a = Convert.ToString(t.age);
            string toe = Convert.ToString(t.toeic);

            if (!string.IsNullOrEmpty(a) && t.age <= 17)
            {
                ModelState.AddModelError("Name", "Trainees's age must be more than 18");
            }
            else if(!string.IsNullOrEmpty(t.name) && t.name.Length < 6)
            {
                ModelState.AddModelError("Name", "Trainees's name must be more than 5 characters");
            }
            else if(!string.IsNullOrEmpty(t.username) && t.username.Length < 7)
            {
                ModelState.AddModelError("Name", "User name must be more than 6");
            }
            else if (!string.IsNullOrEmpty(t.password) && t.password.Length <= 7)
            {
                ModelState.AddModelError("Name", "Password must be more than 8");
            }
            else if (!string.IsNullOrEmpty(toe) && t.toeic % 5 == 1 || !string.IsNullOrEmpty(toe) && t.toeic > 990)
            {
                ModelState.AddModelError("Name", "TOEIC score must be divisible by 5 and less than 990");
            }
            else if (t1 > t2)
            {
                ModelState.AddModelError("Name", "Date of birth must be less than 01/01/2003");
            }
        }
    }
}