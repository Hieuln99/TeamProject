using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Project.EF;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    [HandleError]
    public class TraineeController : Controller
    {
        // GET: Trainee
        public ActionResult Index()
        {
            Get();
            using (var bwCtx = new CustomIdentityDbContext())
            {
                var s = User.Identity.GetUserId();
                var t = bwCtx.Users.FirstOrDefault(f => f.Id == s);
                return View(t);
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
        public void Get()
        {
            var user = User.Identity;
            ViewBag.Name = user.Name;
        }

        [HttpGet]
        public async Task<ActionResult> UpdateTraineeAcc()
        {
            Get();
            var context = new CustomIdentityDbContext();
            var store = new UserStore<CustomUser>(context);
            var manager = new UserManager<CustomUser>(store);

            var trainee = await manager.FindByIdAsync(User.Identity.GetUserId());

            if (trainee != null)
            {

                return View(trainee);
            }

            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: /Trainer/UpdateProfile
        [HttpPost]
        public async Task<ActionResult> UpdateTraineeAcc(CustomUser trainee)
        {
            Get();
            validation(trainee.toeic);
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var context = new CustomIdentityDbContext();
                var store = new UserStore<CustomUser>(context);
                var manager = new UserManager<CustomUser>(store);

                var user = await manager.FindByIdAsync(User.Identity.GetUserId());
                
                if (user != null)
                {
                        user.name = trainee.name;
                        user.age = trainee.age;
                        user.dob = trainee.dob;
                        user.edu = trainee.edu;
                        user.language = trainee.language;
                        user.toeic = trainee.toeic;
                        user.exp = trainee.exp;
                        user.department = trainee.department;
                        user.location = trainee.location;
                        await manager.UpdateAsync(user);
                    
                    return RedirectToAction("Index");

                }
                return RedirectToAction("Index");
            }
        }
        private void validation(int t)
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

        public ActionResult TraineeCourse()
        {
            Get();
            var id = User.Identity.GetUserId();
            using (var bwCtx = new CustomIdentityDbContext())
            {

                var t = bwCtx.Users.Include((b => b.courses)).FirstOrDefault(f => f.Id == id);
                if (t != null)
                {
                    ViewBag.a = t.courses;
                    return View();
                }
                return RedirectToAction("Index");
            }

        }
        [HttpGet]
        public ActionResult ChangePassS()
        {
            Get();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ChangePassS(ChangePass form, string id)
        {
            var context = new CustomIdentityDbContext();
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<CustomUser>(context);
            var userManager = new UserManager<CustomUser>(userStore);

            if (User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var result = await userManager.ChangePasswordAsync(User.Identity.GetUserId(), form.currentpass, form.newpass);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login", "Home");
                    }
                }
                return View(form);
            }
        }
    }
}