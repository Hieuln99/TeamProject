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
    [HandleError]
    [Authorize(Roles = SecurityRole.Staff)]
    public class StaffController : Controller
    {
        // GET: Staff

        public void Get()
        {
            var user = User.Identity;
            ViewBag.Name = user.Name;
        }

        [HttpGet]
        public ActionResult TrainerRegister()
        {
            Get();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> TrainerRegister(TrainerRegisterForm form)
        {
            Get();
            var context = new CustomIdentityDbContext();
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<CustomUser>(context);
            var userManager = new UserManager<CustomUser>(userStore);

            if (!await roleManager.RoleExistsAsync(SecurityRole.Trainer))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = SecurityRole.Trainer });
            }


            if (form.UserName != null && form.Password != null)
            {

                var email = form.UserName;
                var phone = form.PhoneNumber;
                var age = 0;
                var dob = DateTime.Now;
                var edu = "0";
                var language = "0";
                var toeic = 0;
                var exp = "0";
                var department = "0";
                var location = "0";
                var type = form.type;
                var workplace = form.workplace;

                validation3(phone);

                var stff = await userManager.FindByEmailAsync(email);
                if (ModelState.IsValid)
                {
                    if (stff == null)
                    {
                        var result = await userManager.CreateAsync(
                            new CustomUser
                            {
                                UserName = email,
                                Email = email,
                                PhoneNumber = phone,
                                name = email.Split('@')[0],
                                age = age,
                                dob = dob,
                                edu = edu,
                                language = language,
                                toeic = toeic,
                                exp = exp,
                                department = department,
                                location = location,
                                type = type,
                                workplace = workplace,
                                PhoneNumberConfirmed = true,
                                TwoFactorEnabled = true,
                                LockoutEndDateUtc = dob,
                                LockoutEnabled = false,
                                AccessFailedCount = 0
                            },
                           form.Password
                            );
                        if (result.Succeeded)
                        {
                            var Us = await userManager.FindByEmailAsync(form.UserName);

                            if (!await userManager.IsInRoleAsync(Us.Id, SecurityRole.Trainer))
                            {
                                userManager.AddToRole(Us.Id, SecurityRole.Trainer);
                            }
                            return RedirectToAction("TrainerAcc");
                        }
                        else
                        {
                            ModelState.AddModelError("", "failure!");
                            return View(form);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "username is exist!");
                        return View(form);
                    }

                }
                return View(form);
            }
            //passs must be include capital-letter and number
            return View(form);
        }


        private void validation3(string t)
        {
            if (string.IsNullOrEmpty(t))
            {
                ModelState.AddModelError("PhoneNumber", "Phone number can not be null");
            }
            else if (!string.IsNullOrEmpty(t) && t.Length < 10)
            {
                ModelState.AddModelError("PhoneNumber", "Phone number must start with 0 and include 10 numbers");
            }
            else if (!string.IsNullOrEmpty(t) && t.Length > 10)
            {
                ModelState.AddModelError("PhoneNumber", "Phone number must start with 0 and include 10 numbers");
            }
            else if (!IsNumber(t))
            {
                ModelState.AddModelError("PhoneNumber", "Phone number must be numbers");
            }
        }

        [HttpGet]
        public ActionResult TraineeRegister()
        {
            Get();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> TraineeRegister(TraineeRegisterForm form)
        {
            Get();
            var context = new CustomIdentityDbContext();
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<CustomUser>(context);
            var userManager = new UserManager<CustomUser>(userStore);

            if (!await roleManager.RoleExistsAsync(SecurityRole.Trainee))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = SecurityRole.Trainee });
            }


            var email = form.UserName;

            var phone = "0";
            var name = form.Name;
            var age = form.age;
            var dob = form.dob;
            var edu = form.edu;
            var language = form.language;
            var toeic = form.toeic;
            var exp = form.exp;
            var department = form.department;
            var location = form.location;
            var type = "0";
            var workplace = "0";
            
            validation2(toeic);
            if (form.UserName != null && form.Password != null)
            {
                var u = await userManager.FindByEmailAsync(email);
                if (ModelState.IsValid)
                {
                    if (u == null)
                    {
                        var result = await userManager.CreateAsync(
                            new CustomUser
                            {
                                UserName = email,
                                Email = email,
                                PhoneNumber = phone,
                                name = name,
                                age = age,
                                dob = dob,
                                edu = edu,
                                language = language,
                                toeic = toeic,
                                exp = exp,
                                department = department,
                                location = location,
                                type = type,
                                workplace = workplace,
                                PhoneNumberConfirmed = true,
                                TwoFactorEnabled = true,
                                LockoutEndDateUtc = dob,
                                LockoutEnabled = false,
                                AccessFailedCount = 0
                            },
                           form.Password
                            );
                        if (result.Succeeded)
                        {
                            var User = await userManager.FindByEmailAsync(form.UserName);

                            if (!await userManager.IsInRoleAsync(User.Id, SecurityRole.Trainee))
                            {
                                userManager.AddToRole(User.Id, SecurityRole.Trainee);
                            }
                            return RedirectToAction("TraineeAcc");
                        }
                        else
                        {
                            ModelState.AddModelError("", "failure!");
                            return View(form);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email is exist!");
                        return View(form);
                    }

                }
                return View(form);
            }
            //passs must be include capital-letter and number
            return View(form);
        }
        private void validation2(int t)
        {
            if (t > 950)
            {
                ModelState.AddModelError("Toeic", "TOEIC must be less than 950!");
            }
            else if (t % 5 != 0)
            {
                ModelState.AddModelError("Toeic", "TOEIC Score is invalid!");
            }
        }

        [Authorize(Roles = SecurityRole.Staff)]
        public ActionResult StaffIndex()
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
       

        [HttpGet]
        public ActionResult TrainerEdit(string id)
        {
            Get();
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
            Get();
            validation1(t.name);
            validation3(t.PhoneNumber);
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
        private void validation1(string t)
        {
            if (!string.IsNullOrEmpty(t) && t.Length < 6)
            {
                ModelState.AddModelError("Name", "Trainers's name must be more than 5 characters");
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

                TempData["message"] = $"Delete successfully a trainer: {trainer.name}";
            }
            return RedirectToAction("TrainerAcc");
        }


        public bool IsNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }

        //private void validation(CustomUser t)
        //{
        //    string phone = t.PhoneNumber;

        //    if (phone.Length < 10)
        //    {
        //        ModelState.AddModelError("PhoneNumber", "Phone number must start with 0 and include 10 numbers");
        //    }
        //    if (phone.Length > 10)
        //    {
        //        ModelState.AddModelError("PhoneNumber", "Phone number must start with 0 and include 10 numbers");
        //    }
        //    else if (!IsNumber(phone))
        //    {
        //        ModelState.AddModelError("PhoneNumber", "Phone number must be numbers");
        //    }
        //}





        //----------------------

        public ActionResult TraineeAcc()
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
                                          Age = user.age,
                                          Dob = user.dob,
                                          Toeic = user.toeic,
                                          Education = user.edu,
                                          Language = user.language,
                                          Exp = user.exp,
                                          Location = user.location,
                                          Department= user.department,
                                          //More Propety

                                          RoleNames = (from userRole in user.Roles
                                                       join role in context.Roles on userRole.RoleId
                                                       equals role.Id
                                                       select role.Name).ToList()
                                      }).ToList().Where(p => string.Join(",", p.RoleNames) == "trainee").Select(p => new CustomUser()

                                      {
                                          Id = p.UserId,
                                          name = p.Name,
                                          UserName = p.Username,
                                          toeic = p.Toeic,
                                          age = p.Age,
                                          dob = p.Dob,
                                          exp = p.Exp,
                                          location = p.Location,
                                          department = p.Department,
                                          edu = p.Education,
                                          language = p.Language,
                                          Email = p.Email
                                      });
                return View(usersWithRoles);
            }
        }
     

        [HttpGet]
        public ActionResult TraineeEdit(string id)
        {
            Get();
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
            
            validation1(t);
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
                TempData["message"] = $"Edit successfully a trainee with name: {t.name}";
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

                TempData["message"] = $"Delete successfully a trainee with name: {t.name}";
            }
            return RedirectToAction("TraineeAcc");
        }
        private void validation1(CustomUser t)
        {
            if (t.toeic > 950)
            {
                ModelState.AddModelError("Toeic", "TOEIC must be less than 950!");
            }
            else if (t.toeic % 5 != 0)
            {
                ModelState.AddModelError("Toeic", "TOEIC Score is invalid!");
            }
        }


        public ActionResult CourseIndex()
        {
            Get();
            using (var TNCT = new EF.CustomIdentityDbContext())
            {

                var course = TNCT.courses
                    .Include(c => c.CourseCategory)
                                 .OrderBy(b => b.id)
                                 .ToList();
                return View(course);                
            }
        }
        [HttpGet]
        public ActionResult CreateCourse()
        {
            Get();
            ViewBag.categories = GetCategoryDropDown();
            return View(); //show blank form
            // ko co data
            // thu thap data cua BookEntity
        }
        [HttpPost]
        public ActionResult CreateCourse(Course newCourse)
        {
            Get();
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

                TempData["message"] = $"Successfully insert a course: {newCourse.name}";
                return RedirectToAction("CourseIndex");
            }
        }

        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            Get();
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
            Get();
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
                TempData["message"] = $"Edit successfully a course: {t.name}";
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

                TempData["message"] = $"Delete successfully a course with name: {t.name}";
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
            Get();
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
            Get();
            return View();
        }
        [HttpPost]
        public ActionResult CreateCat(CourseCategory newCategories)
        {
            Get();
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

                TempData["message"] = $"Successfully insert a category: {newCategories.name}";
                return RedirectToAction("CategoryIndex");
            }
        }

        [HttpGet]
        public ActionResult EditCat(int id)
        {
            Get();
            using (var bwCtx = new EF.CustomIdentityDbContext())
            {
                var category = bwCtx.categories.FirstOrDefault(b => b.id == id);
                //ef method to select only one or null if not found

                if (category != null)
                {
                    return View(category);
                }
                else
                {
                    return RedirectToAction("CategoryIndex"); //redirect to action in the same controller
                }
            }
        }

        [HttpPost]
        public ActionResult EditCat(int id, CourseCategory category)
        {
            Get();
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

                    bwCtx.SaveChanges();
                }
                TempData["message"] = $"Successfully update category with name: {category.name}";
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

                TempData["message"] = $"Delete successfully a category: {t.name}";
            }
            return RedirectToAction("CategoryIndex");
        }



        //--------------------------------------------------------


        [HttpGet]
        public ActionResult AssignCourse(string id)
        {
            Get();
            using (var bwCtx = new CustomIdentityDbContext())
            {
                var Person = bwCtx.Users
                    .Include(b => b.courses)
                    .FirstOrDefault(b => b.Id == id);

                if (Person != null) 
                {

                    PrepareViewBag();
                    return View(Person);
                }
                else 
                {
                    return RedirectToAction("TraineeAcc"); //redirect to action in the same controller
                }
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
                TempData["message"] = $"Successfully assign course with trainee name: {newUser.name}";

                return RedirectToAction("TraineeAcc");
            }
        }

      

        //----------------------------------------------------
        [HttpGet]
        public ActionResult AssignCourseT(string id)
        {
            Get();
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
                    return RedirectToAction("TrainerAcc"); //redirect to action in the same controller
                }
            }
        }

        [HttpPost]
        public ActionResult AssignCourseT(string id, CustomUser newUser, FormCollection fc)
        {
            if (!ModelState.IsValid)
            {
                TempData["trainerIds"] = fc["trainerIds[]"];
                PrepareViewBag();
                return View(newUser);
            }
            else
            {

                using (var bwCtx = new CustomIdentityDbContext())
                {

                    if (fc["trainerIds[]"] != null)
                    {
                        bwCtx.Entry<CustomUser>(newUser).State
                           = System.Data.Entity.EntityState.Modified;

                        bwCtx.Entry<CustomUser>(newUser).Collection(b => b.courses).Load();
                        newUser.courses = LoadFormats(bwCtx, fc["trainerIds[]"]);

                        bwCtx.SaveChanges();
                    }
                    //add book to context and mark it as modified to do update, not insert


                }
                TempData["message"] = $"Successfully assign course with trainer name: {newUser.name}";

                return RedirectToAction("TrainerAcc");
            }
        }

        private void PrepareViewBag()
        {
            using (var bwCtx = new CustomIdentityDbContext())
            {
                ViewBag.Person = bwCtx.courses.ToList();
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