using Microsoft.AspNet.Identity.EntityFramework;
using Project.EF;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Project.Controllers
{
    [Authorize(Roles = SecurityRole.Staff)]
    public class StaffController : Controller
    {
        // GET: Staff
        public ActionResult StaffIndex()
        {
            return View();
        }

        public ActionResult TrainerAcc()
        {
            using (var TNCT = new EF.CustomIdentityDbContext())
            {
                var l = new List<CustomUser>();
                var trainers = TNCT.Users.OrderBy(t => t.Id).ToList();
                foreach (var s in trainers)
                {
                    if (s.Role == "Trainer")
                    {
                        l.Add(s);
                    }
                }
                return View(l);
            }
        }
        /*[HttpGet]
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
*/

        [HttpGet]
        public ActionResult TrainerEdit(string id)
        {
            using (var TNCT = new EF.CustomIdentityDbContext())
            {
                var trainer = TNCT.Users.FirstOrDefault(t => t.Id == id);
                if (trainer == null)
                {
                    return RedirectToAction("TrainerAcc");
                }
                else
                {
                    return View(trainer);
                }
            }
        }
        [HttpPost]
        public ActionResult TrainerEdit(string id, CustomUser t)
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
                var trainer = TNCT.Users.FirstOrDefault(s => s.Id == id);
                if (trainer == null)
                {
                    TempData["message"] = $"Delete failed";
                }
                TNCT.Users.Remove(trainer);
                TNCT.SaveChanges();

                TempData["message"] = $"Delete successfully a staff with id: {trainer.name}";
            }
            return RedirectToAction("TrainerAcc");
        }
        private void validation(CustomUser t)
        {

            if (!string.IsNullOrEmpty(t.name) && t.name.Length < 6)
            {
                ModelState.AddModelError("Name", "Trainers's name must be more than 5 characters");
            }
            else if (!string.IsNullOrEmpty(t.UserName) && t.UserName.Length < 7)
            {
                ModelState.AddModelError("Name", "User name must be more than 6");
            }
            //else if (!string.IsNullOrEmpty(t.password) && t.password.Length <= 7)
            //{
            //    ModelState.AddModelError("Name", "Password must be more than 7");
            //}

            else if (!string.IsNullOrEmpty(t.PhoneNumber) && t.PhoneNumber[0] != '0' && t.PhoneNumber.Length < 10)
            {
                ModelState.AddModelError("Name", "Phone number must start with 0 and include 10 numbers");
            }
        }





        //----------------------
        
        public ActionResult TraineeAcc()
        {
            using (var TNCT = new EF.CustomIdentityDbContext())
            {
                var l = new List<CustomUser>();
                var trainers = TNCT.Users.OrderBy(t => t.Id).ToList();
                foreach (var s in trainers)
                {
                    if (s.Role == "Trainee")
                    {
                        l.Add(s);
                    }
                }
                return View(l);
            }
        }
        /*[HttpGet]
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
*/

        [HttpGet]
        public ActionResult TraineeEdit(string id)
        {
            using (var TNCT = new EF.CustomIdentityDbContext())
            {
                var trainee = TNCT.Users.FirstOrDefault(t => t.Id == id);
                if (trainee == null)
                {
                    return RedirectToAction("TraineeAcc");
                }
                else
                {
                    return View(trainee);
                }
            }
        }
        [HttpPost]
        public ActionResult TraineeEdit(string id, CustomUser t)
        {
            validation2(t);
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
                TempData["message"] = $"Edit successfully a trainee with id: {t.Id}";
                return RedirectToAction("TraineeAcc", "Staff");
            }
        }


        public ActionResult DeleteTraineeAcc(string id)
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

                TempData["message"] = $"Delete successfully a trainee with id: {t.Id}";
            }
            return RedirectToAction("TraineeAcc");
        }
        private void validation2(CustomUser t)
        {

            if (!string.IsNullOrEmpty(t.name) && t.name.Length < 6)
            {
                ModelState.AddModelError("Name", "Trainees's name must be more than 5 characters");
            }
            
        }

        
        public ActionResult CourseIndex()
        {

            using (var TNCT = new EF.CustomIdentityDbContext())
            {

                var course = TNCT.courses
                    .Include(c => c.categories)
                                 .OrderBy(b => b.id)
                                 .ToList();


                return View(course);

                
            }

        }

        [HttpGet]
        public ActionResult CreateCourse()
        {
            ViewBag.categories = GetCategoryDropDown();
            return View(); //show blank form
            // ko co data
            // thu thap data cua BookEntity
        }
        [HttpPost]
        public ActionResult CreateCourse(Course newCourse, FormCollection fs)
        {
            ViewBag.categories = GetCategoryDropDown();
            if (!ModelState.IsValid)
            {
                return View(newCourse);
            }
            else
            {
                using (var bwCtx = new EF.CustomIdentityDbContext())
                {


                    bwCtx.courses.Add(newCourse);
                    bwCtx.SaveChanges();
                }

                TempData["message"] = $"Successfully insert a new book with Id: {newCourse.id}";
                return RedirectToAction("CourseIndex");
            }
        }

        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            using (var TNCT = new EF.CustomIdentityDbContext())
            {
                ViewBag.categories = GetCategoryDropDown();
                var course = TNCT.courses.FirstOrDefault(t => t.id == id);
                if (course == null)
                {
                    return RedirectToAction("CourseIndex");
                }
                else
                {
                    return View(course);
                }
            }
        }
        [HttpPost]
        public ActionResult EditCourse(int id, Course t)
        {
            ViewBag.categories = GetCategoryDropDown();
            if (!ModelState.IsValid)
            {
                return View(t);
            }
            else
            {
                using (var TNCT = new EF.CustomIdentityDbContext())
                {
                    TNCT.Entry<Course>(t).State = System.Data.Entity.EntityState.Modified;
                    TNCT.SaveChanges();
                }
                TempData["message"] = $"Edit successfully a trainee with id: {t.id}";
                return RedirectToAction("CourseIndex");
            }
        }

        public ActionResult DeleteCourse(int Id)
        {
            using (var TNCT = new EF.CustomIdentityDbContext())
            {
                var t = TNCT.courses.FirstOrDefault(u => u.id == Id);
                if (t == null)
                {
                    TempData["message"] = $"Delete failed";
                }
                TNCT.courses.Remove(t);
                TNCT.SaveChanges();

                TempData["message"] = $"Delete successfully a course with id: {t.name}";
            }
            return RedirectToAction("CourseIndex");
        }

        private List<SelectListItem> GetCategoryDropDown()
        {
            using (var bwCtx = new CustomIdentityDbContext())
            {
                var categories = bwCtx.categories
                                 .Select(p => new SelectListItem
                                 {
                                     Text = p.name,
                                     Value = p.id.ToString()
                                 }).ToList();
                return categories;
            }
        }


        public ActionResult CategoryIndex()
        {
            using (var bwCtx = new EF.CustomIdentityDbContext())
            {
                var cats = bwCtx.categories
                                .OrderBy(b => b.id)
                                .ToList();
                return View(cats);
            }
        }

        [HttpGet]
        public ActionResult CreateCat()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCat(CourseCategory newCategories)
        {
            CustomIdentityDbContext context = new CustomIdentityDbContext();
            var roleManager = new Microsoft.AspNet.Identity.RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new Microsoft.AspNet.Identity.UserManager<CustomUser>(new UserStore<CustomUser>(context));
            if (!ModelState.IsValid)
            {
                return View(newCategories);
            }
            else
            {
                using (var bwCtx = new CustomIdentityDbContext())
                {
                    bwCtx.categories.Add(newCategories);
                    bwCtx.SaveChanges();
                }

                TempData["message"] = $"Successfully insert a new book with Id: {newCategories.id}";
                return RedirectToAction("CategoryIndex");
            }
        }

        public ActionResult EditCat(int id)
        {
            using (var bwCtx = new EF.CustomIdentityDbContext())
            {
                var category = bwCtx.categories.FirstOrDefault(b => b.id == id);
                //ef method to select only one or null if not found

                if (category != null) // if a book is found, show edit view
                {
                    return View(category);
                }
                else // if no book is found, back to index
                {
                    return RedirectToAction("CategoryIndex"); //redirect to action in the same controller
                }
            }
        }

        [HttpPost]
        public ActionResult EditCat(int id, CourseCategory category)
        {

            

            if (!ModelState.IsValid)
            {
                return View(category);
            }
            else
            {

                using (var bwCtx = new EF.CustomIdentityDbContext())
                {
                    bwCtx.Entry<CourseCategory>(category).State
                        = System.Data.Entity.EntityState.Modified;
                    //add book to context and mark it as modified to do update, not insert

                    bwCtx.SaveChanges();
                }
                TempData["message"] = $"Successfully update book with Id: {category.id}";
                return RedirectToAction("CategoryIndex");
            }
        }
        public ActionResult DeleteCat(int Id)
        {
            using (var TNCT = new EF.CustomIdentityDbContext())
            {
                var t = TNCT.categories.FirstOrDefault(u => u.id == Id);
                if (t == null)
                {
                    TempData["message"] = $"Delete failed";
                }
                TNCT.categories.Remove(t);
                TNCT.SaveChanges();

                TempData["message"] = $"Delete successfully a trainee with id: {t.id}";
            }
            return RedirectToAction("CourseIndex");
        }
        
        //----------------------

        [HttpGet]
        public ActionResult AssignCourse(string id)
        {

            using (var bwCtx = new CustomIdentityDbContext())
            {



                var Person = bwCtx.Users
                    .Include(b => b.courses)
                    .FirstOrDefault(b => b.Id == id);

                if (Person != null) // if a book is found, show edit view
                {

                    PrepareViewBag();
                    return View(Person);
                }
                else // if no book is found, back to index
                {
                    return RedirectToAction("TraineeAcc"); //redirect to action in the same controller
                }
            }
        }

        private void PrepareViewBag()
        {
            using (var bwCtx = new CustomIdentityDbContext())
            {

                ViewBag.Person = bwCtx.courses.ToList();
            }
        }

        [HttpPost]
        public ActionResult AssignCourse(string id, CustomUser newUser, FormCollection fc)
        {


            if (!ModelState.IsValid)
            {

                TempData["traineeIds"] = fc["traineeIds[]"];
                PrepareViewBag();

                return View(newUser);
            }
            else
            {

                using (var bwCtx = new CustomIdentityDbContext())
                {

                    if (fc["traineeIds[]"] != null)
                    {
                        bwCtx.Entry<CustomUser>(newUser).State
                           = System.Data.Entity.EntityState.Modified;

                        bwCtx.Entry<CustomUser>(newUser).Collection(b => b.courses).Load();
                        newUser.courses = LoadFormats(bwCtx, fc["traineeIds[]"]);

                        bwCtx.SaveChanges();
                    }
                    //add book to context and mark it as modified to do update, not insert


                }
                TempData["message"] = $"Successfully update book with Id: {newUser.Id}";

                return RedirectToAction("TraineeAcc");
            }
        }

        private List<Course> LoadFormats(CustomIdentityDbContext bwCtx, string v)
        {
            var selectedSIds = v.Split(',')
                                        .Select(id => Int32.Parse(id))
                                        .ToArray();
            return bwCtx.courses.Where(f => selectedSIds.Contains(f.id)).ToList();
        }
    }
}