using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class AdmController : Controller
    {
        // GET: Adm
        public ActionResult AdminIndex()
        {
            return View();
        }


        public ActionResult TrainerAcc()
        {
            using(var TNCT = new EF.TrainingContext())
            {
                var trainers = TNCT.trainers.OrderBy(t => t.id).ToList();
                return View(trainers);
            }
        }


        [HttpGet]
        public ActionResult AddTrainerAcc()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTrainerAcc(Trainer t)
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
            return RedirectToAction("TrainerAcc");
        }


        [HttpGet]
        public ActionResult EditTrainerAcc(int id)
        {
            using (var TNCT = new EF.TrainingContext())
            {
                var trainers = TNCT.trainers.FirstOrDefault(t => t.id == id);
                if (trainers == null)
                {
                    return RedirectToAction("TrainerAcc");
                }
                else
                {
                    return View(trainers);
                }
            }
        }
        [HttpPost]
        public ActionResult EditTrainerAcc(int id, Trainer t)
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
                    TNCT.Entry<Trainer>(t).State = System.Data.Entity.EntityState.Modified;
                    TNCT.SaveChanges();
                }
                TempData["message"] = $"Edit successfully a trainer with id: {t.id}";
                return RedirectToAction("TrainerAcc");
            }
        }


        public ActionResult DeleteTrainerAcc(int id)
        {
            using (var TNCT = new EF.TrainingContext())
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
            return RedirectToAction("TrainerAcc");
        }

        private void validation(Trainer t)
        {
            if (!string.IsNullOrEmpty(t.phonenumber) && t.phonenumber[0] != '0')
            {
                ModelState.AddModelError("Name", "Phone number must start with 0");
            }
        }





        //-----------------------------------
        public ActionResult StaffAcc()
        {
            using(var TNCT = new EF.TrainingContext())
            {
                var staffs = TNCT.staffs.OrderBy(s => s.id).ToList();
                return View(staffs);
            }
        }

        [HttpGet]
        public ActionResult AddStaffAcc()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddStaffAcc(Staff s)
        {
            
                using (var TNCT = new EF.TrainingContext())
                {
                    TNCT.staffs.Add(s);
                    TNCT.SaveChanges();
                }
            
            TempData["message"] = $"Add Successfully a staff with id: {s.id}";
            return RedirectToAction("TrainerAcc");
        }

        [HttpGet]
        public ActionResult EditStaffAcc(int id)
        {
            using (var TNCT = new EF.TrainingContext())
            {
                var staffs = TNCT.staffs.FirstOrDefault(s => s.id == id);
                if (staffs == null)
                {
                    return RedirectToAction("StaffAcc");
                }
                else
                {
                    return View(staffs);
                }
            }
        }
        [HttpPost]
        public ActionResult EditStaffAcc(int id, Staff s)
        {
            validation1(s);
            if (!ModelState.IsValid)
            {
                return View(s);
            }
            else
            {
                using (var TNCT = new EF.TrainingContext())
                {
                    TNCT.Entry<Staff>(s).State = System.Data.Entity.EntityState.Modified;
                    TNCT.SaveChanges();
                }
                TempData["message"] = $"Edit successfully a staff with id: {s.id}";
                return RedirectToAction("StaffAcc");
            }
        }

        public ActionResult DeleteStaffAcc(int id)
        {
            using (var TNCT = new EF.TrainingContext())
            {
                var staff = TNCT.staffs.FirstOrDefault(s => s.id == id);
                if (staff == null)
                {
                    TempData["message"] = $"Delete failed";
                }
                TNCT.staffs.Remove(staff);
                TNCT.SaveChanges();

                TempData["message"] = $"Delete successfully a staff with id: {staff.id}";
            }
            return RedirectToAction("StaffAcc");
        }

        private void validation1(Staff s)
        {

        }

    }
}