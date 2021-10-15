using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    [Authorize(Roles = SecurityRole.Admin)]
    public class AdmController : Controller
    {
        // GET: Adm
        public ActionResult AdminIndex()
        {
            return View();
        }


        public ActionResult TrainerAcc()
        {
            using(var TNCT = new EF.CustomIdentityDbContext())
            {
                var trainers = TNCT.Users.OrderBy(t => t.Id).ToList();
                return View(trainers);
            }
        }


        [HttpGet]
        public ActionResult AddTrainerAcc()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTrainerAcc(CustomUser t)
        {
            validation(t);
            if (!ModelState.IsValid)
            {
                return View(t);
            }
            else
            {
                using (var TNCT = new EF.CustomIdentityDbContext())
                {
                    TNCT.Users.Add(t);
                    TNCT.SaveChanges();
                }
            }
            TempData["message"] = $"Add Successfully a trainer with id: {t.Id}";
            return RedirectToAction("TrainerAcc");
        }


        [HttpGet]
        public ActionResult EditTrainerAcc(string id)
        {
            using (var TNCT = new EF.CustomIdentityDbContext())
            {
                var trainers = TNCT.Users.FirstOrDefault(t => t.Id == id);
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
        public ActionResult EditTrainerAcc(string id, CustomUser t)
        {
            validation(t);
            if (!ModelState.IsValid)
            {
                return View(t);
            }
            else
            {
                using (var TNCT = new EF.CustomIdentityDbContext())
                {
                    TNCT.Entry<CustomUser>(t).State = System.Data.Entity.EntityState.Modified;
                    TNCT.SaveChanges();
                }
                TempData["message"] = $"Edit successfully a trainer with id: {t.Id}";
                return RedirectToAction("TrainerAcc");
            }
        }


        public ActionResult DeleteTrainerAcc(string id)
        {
            using (var TNCT = new EF.CustomIdentityDbContext())
            {
                var t = TNCT.Users.FirstOrDefault(u => u.Id == id);
                if (t == null)
                {
                    TempData["message"] = $"Delete failed";
                }
                TNCT.Users.Remove(t);
                TNCT.SaveChanges();

                TempData["message"] = $"Delete successfully a trainer with id: {t.Id}";
            }
            return RedirectToAction("TrainerAcc");
        }

        private void validation(CustomUser t)
        {
            
            if(!string.IsNullOrEmpty(t.name) && t.name.Length < 6)
            {
                ModelState.AddModelError("Name", "Trainers's name must be more than 5 characters");
            }
            else if(!string.IsNullOrEmpty(t.UserName) && t.UserName.Length < 7)
            {
                ModelState.AddModelError("Name", "User name must be more than 6");
            }
            //else if (!string.IsNullOrEmpty(t.password) && t.password.Length <= 7)
            //{
            //    ModelState.AddModelError("Name", "Password must be more than 7");
            //}

            else if (!string.IsNullOrEmpty(t.PhoneNumber) && t.PhoneNumber[0] != '0' && t.PhoneNumber.Length <10)
            {
                ModelState.AddModelError("Name", "Phone number must start with 0 and include 10 numbers");
            }
        }





        //-----------------------------------
        public ActionResult StaffAcc()
        {
            using(var TNCT = new EF.CustomIdentityDbContext())
            {
                var staffs = TNCT.Users.OrderBy(s => s.Id).ToList();
                return View(staffs);
            }
        }

        [HttpGet]
        public ActionResult AddStaffAcc()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddStaffAcc(CustomUser s)
        {
            validation1(s);
            if (!ModelState.IsValid)
            {
                return View(s);
            }
            else
            {
                using (var TNCT = new EF.CustomIdentityDbContext())
                {
                    TNCT.Users.Add(s);
                    TNCT.SaveChanges();
                }
                TempData["message"] = $"Add Successfully a staff with id: {s.Id}";
                return RedirectToAction("TrainerAcc");
            }
        }

        [HttpGet]
        public ActionResult EditStaffAcc(string id)
        {
            using (var TNCT = new EF.CustomIdentityDbContext())
            {
                var staffs = TNCT.Users.FirstOrDefault(s => s.Id == id);
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
        public ActionResult EditStaffAcc(int id,CustomUser s)
        {
            validation1(s);
            if (!ModelState.IsValid)
            {
                return View(s);
            }
            else
            {
                using (var TNCT = new EF.CustomIdentityDbContext())
                {
                    TNCT.Entry<CustomUser>(s).State = System.Data.Entity.EntityState.Modified;
                    TNCT.SaveChanges();
                }
                TempData["message"] = $"Edit successfully a staff with id: {s.Id}";
                return RedirectToAction("StaffAcc");
            }
        }

        public ActionResult DeleteStaffAcc(string id)
        {
            using (var TNCT = new EF.CustomIdentityDbContext())
            {
                var staff = TNCT.Users.FirstOrDefault(s => s.Id == id);
                if (staff == null)
                {
                    TempData["message"] = $"Delete failed";
                }
                TNCT.Users.Remove(staff);
                TNCT.SaveChanges();

                TempData["message"] = $"Delete successfully a staff with id: {staff.Id}";
            }
            return RedirectToAction("StaffAcc");
        }

        private void validation1(CustomUser s)
        {
            if (!string.IsNullOrEmpty(s.name) && s.name.Length < 6)
            {
                ModelState.AddModelError("Name", "Staff's name must be more than 5 characters");
            }
            else if (!string.IsNullOrEmpty(s.UserName) && s.UserName.Length < 7)
            {
                ModelState.AddModelError("Name", "User name must be more than 6");
            }
            //else if (!string.IsNullOrEmpty(s.password) && s.password.Length <= 7)
            //{
            //    ModelState.AddModelError("Name", "Password must be more than 7");
            //}
        }

    }
}