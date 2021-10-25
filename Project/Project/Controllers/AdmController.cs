using Project.EF;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    [HandleError]
    [Authorize(Roles = SecurityRole.Admin)]
    public class AdmController : Controller
    {
        // GET: Adm

        public void Get()
        {
            var user = User.Identity;
            ViewBag.Name = user.Name;
        }

        public ActionResult AdminIndex()
        {
            Get();
            return View();
        }


        public ActionResult TrainerAcc()
        {
            Get();
            using (CustomIdentityDbContext context = new CustomIdentityDbContext())
            {
                var usersWithRoles = (from user in context.Users
                                      select new
                                      {
                                          UserId = user.Id,
                                          Username = user.UserName,
                                          Email = user.Email,
                                          Name = user.name,
                                          Type = user.type,
                                          Workplace = user.workplace,
                                          Phone = user.PhoneNumber,
                                          Toeic = user.toeic,
                                          Education = user.edu,
                                          Language = user.language,
                                          //More Propety

                                          RoleNames = (from userRole in user.Roles
                                                       join role in context.Roles on userRole.RoleId
                                                       equals role.Id
                                                       select role.Name).ToList()
                                      }).ToList().Where(p => string.Join(",", p.RoleNames) == "trainer").Select(p => new CustomUser()

                                      {
                                          Id = p.UserId,
                                          name = p.Name, 
                                          UserName = p.Username,
                                          toeic = p.Toeic,
                                          edu = p.Education,
                                          language = p.Language,
                                          type = p.Type,
                                          workplace = p.Workplace,
                                          PhoneNumber = p.Phone,
                                          Email = p.Email
                                      });
                return View(usersWithRoles);
            }
        }



        [Authorize(Roles = SecurityRole.Admin)]
        [HttpGet]
        public ActionResult EditTrainerAcc(string id)
        {
            Get();
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
                TempData["message"] = $"Edit successfully a trainer with name: {t.name}";
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

                TempData["message"] = $"Delete successfully a trainer with name: {t.name}";
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

            else if (!string.IsNullOrEmpty(t.PhoneNumber) && t.PhoneNumber[0] != '0' && t.PhoneNumber.Length <10)
            {
                ModelState.AddModelError("Name", "Phone number must start with 0 and include 10 numbers");
            }
        }





        //-----------------------------------
        [Authorize(Roles = SecurityRole.Admin)]
        public ActionResult StaffAcc()
        {
            Get();
            using (CustomIdentityDbContext context = new CustomIdentityDbContext())
            {
                var usersWithRoles = (from user in context.Users
                                      select new
                                      {
                                          UserId = user.Id,
                                          Username = user.UserName,
                                          Email = user.Email,
                                          Name = user.name,
                                         
                                          //More Propety

                                          RoleNames = (from userRole in user.Roles
                                                       join role in context.Roles on userRole.RoleId
                                                       equals role.Id
                                                       select role.Name).ToList()
                                      }).ToList().Where(p => string.Join(",", p.RoleNames) == "staff").Select(p => new CustomUser()

                                      {
                                          Id = p.UserId,
                                          name = p.Name,
                                          UserName = p.Username,
                                          Email = p.Email
                                      });
                return View(usersWithRoles);
            }
        }


        [Authorize(Roles = SecurityRole.Admin)]
        [HttpGet]
        public ActionResult EditStaffAcc(string id)
        {
            Get();
            using (var TNCT = new EF.CustomIdentityDbContext())
            {
                var staffs = TNCT.Users.FirstOrDefault(s => s.Id == id);
                if (staffs == null)
                {
                    return Content("failue");
                }
                else
                {
                    return View(staffs);
                }
            }
        }
        [HttpPost]
        public ActionResult EditStaffAcc(CustomUser s)
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
                TempData["message"] = $"Edit successfully a staff with name: {s.name}";
                return RedirectToAction("StaffAcc");
            }
        }
        [Authorize(Roles = SecurityRole.Admin)]
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

                TempData["message"] = $"Delete successfully a staff with name: {staff.name}";
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
        }

        
    }
}