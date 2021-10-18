using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Project.EF;
using Project.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class TrainerController : Controller
    {
        // GET: Trainer
        public ActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public ActionResult UpdatePass()
        {
            Get();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> UpdatePass(ChangePass p)
        {
            var context = new CustomIdentityDbContext();
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<CustomUser>(context);
            var userManager = new UserManager<CustomUser>(userStore);
            if (!ModelState.IsValid)
                {
                    return View(p);
                }
                var result = await userManager.ChangePasswordAsync(User.Identity.GetUserId(), p.currentpass, p.newpass);
                if (result.Succeeded)
                {
                    var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        return Content("Success");
                    }
                    return RedirectToAction("TrainerAcc","Adm");
                }
                AddErrors(result);
                return View(p);
            
        }

        public void Get()
        {
            var user = User.Identity;
            ViewBag.Name = user.Name;
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


        [HttpGet]
        public async Task<ActionResult> EditTrainerAcc()
        {
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
        public async Task<ActionResult> EditTrainerAcc(CustomUser trainer)
        {
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
                    user.edu = trainer.edu;
                    user.workplace = trainer.workplace;
                    user.PhoneNumber = trainer.PhoneNumber;
                    await manager.UpdateAsync(user);
                }
                return RedirectToAction("Index", "Trainer");
            }
        }
    }
}