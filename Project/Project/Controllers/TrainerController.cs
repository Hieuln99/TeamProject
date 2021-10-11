using Project.Models;
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

        [HttpGet]
        public ActionResult TrainerAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TrainerAdd(Trainer t)
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
                    TNCT.trainers.Add(t);
                    TNCT.SaveChanges();
                }
            }
            TempData["message"] = $"Add Successfully a trainer with id: {t.id}";
            return RedirectToAction("TrainerIndex");
        }

        [HttpGet]
         public ActionResult TrainerEdit(int id)
        {
            using(var TNCT=new EF.TrainingContext())
            {
                var trainer = TNCT.trainers.FirstOrDefault(t => t.id == id);
                if(trainer == null)
                {
                    return RedirectToAction("TrainerIndex");
                }
                else
                {
                    return View(trainer);
                }
            }
        }
        [HttpPost]
        public ActionResult TrainerEdit(int id, Trainer t)
        {
            using(var TNCT = new EF.TrainingContext())
            {
                TNCT.Entry<Trainer>(t).State = System.Data.Entity.EntityState.Modified;
                TNCT.SaveChanges();
            }
            TempData["message"] = $"Edit successfully a trainer with id: {t.id}";
            return RedirectToAction("TrainerIndex");
        }
        
        public ActionResult TrainerDelete(int id)
        {
            using(var TNCT = new EF.TrainingContext())
            {
                var t = TNCT.trainers.FirstOrDefault(u => u.id == id);
                if (t == null)
                {
                    TempData["message"] = $"Delete failed";
                }
                TNCT.trainers.Remove(t);
                TNCT.SaveChanges();

                TempData["message"] = $"Delete successfully a trainer with id: {t.id}";
            }
            return RedirectToAction("TrainerIndex");
        }

        private void validation(Trainer t)
        {
            if(!string.IsNullOrEmpty(t.phonenumber)&&t.phonenumber[0] != '0')
            {
                ModelState.AddModelError("Name", "Phone number must start with 0");
            }
        }

    }
}