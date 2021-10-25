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
    [Authorize]
    public class TrainerController : Controller
    {
        // GET: Trainer
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
        public async Task<ActionResult> UpdateTrainerAcc()
        {
            Get();
            var context = new CustomIdentityDbContext();
            var store = new UserStore<CustomUser>(context);
            var manager = new UserManager<CustomUser>(store);

            var trainer = await manager.FindByIdAsync(User.Identity.GetUserId());

            if (trainer != null)
            {

                return View(trainer);
            }

            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: /Trainer/UpdateProfile
        [HttpPost]
        public async Task<ActionResult> UpdateTrainerAcc(CustomUser trainer)
        {
            Get();
            validation3(trainer.PhoneNumber);
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
                    user.name = trainer.name;
                    user.type = trainer.type;
                    user.workplace = trainer.workplace;
                    user.PhoneNumber = trainer.PhoneNumber;
                    await manager.UpdateAsync(user);
                }
                return RedirectToAction("Index", "Trainer");
            }
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
        public ActionResult TrainerCourse()
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
        public ActionResult ChangePassT()
        {
            Get();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ChangePassT(ChangePass form, string id)
        {
            var context = new CustomIdentityDbContext();
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<CustomUser>(context);
            var userManager = new UserManager<CustomUser>(userStore);

            if (User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Login");
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