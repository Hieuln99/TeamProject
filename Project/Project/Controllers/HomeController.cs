using Project.EF;
using Project.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public async Task<ActionResult> Index()
        {
            var context = new CustomIdentityDbContext();
            var store = new UserStore<CustomUser>(context);
            var manager = new UserManager<CustomUser>(store);
            var signInManager = new SignInManager<CustomUser, string>(manager,
                HttpContext.GetOwinContext().Authentication);


            var email = "hieu@bar.com";
            var password = "PassWord";
            var phone = "0909191919";
            var age = 0;
            var dob = DateTime.Now;
            var edu = "0";
            var language = "0";
            var toeic = 0;
            var exp = "0";
            var department = "0";
            var location = "0";
            var type = "0";
            var workplace = "0";

            var user = await manager.FindByEmailAsync(email);
            if(user == null)
            {
                user = new CustomUser
                {
                    UserName = email,
                    Email = email,
                    PhoneNumber = phone,
                    name = email,
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
                    PhoneNumberConfirmed= true,
                    TwoFactorEnabled =true,
                    LockoutEndDateUtc = dob,
                    LockoutEnabled = false,
                    AccessFailedCount = 0

                };
                await manager.CreateAsync(user, password);
                return Content($"Welcom {user.UserName}");
            }
            else
            {
                var result = await signInManager.PasswordSignInAsync(
                    userName : user.UserName,
                    password : password,
                    isPersistent : false,
                    shouldLockout : false
                    );
                return Content($"Welcom back {user.UserName}, your acc is {result}");
            }
        }

        public ActionResult Auth()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Content("you are not authentication");
            }
            else
            {
                return Content("welcom");
            }
        }

        private async Task Prepare()
        {
            var context = new CustomIdentityDbContext();
            var userStore = new UserStore<CustomUser>(context);
            var userManager = new UserManager<CustomUser>(userStore);

            var aemail = "adm@bar.com";
            var semail = "stff@bar.com";
            var temail = "tr@bar.com";


            
            var phone = "0909191919";
            var age = 0;
            var dob = DateTime.Now;
            var edu = "0";
            var language = "0";
            var toeic = 0;
            var exp = "0";
            var department = "0";
            var location = "0";
            var type = "0";
            var workplace = "0";


            var adm = await userManager.FindByEmailAsync(aemail);
            var stff = await userManager.FindByEmailAsync(semail);
            var tr = await userManager.FindByEmailAsync(temail);

            if(adm == null)
            {
                await userManager.CreateAsync(
                    new CustomUser
                    {
                        UserName = aemail,
                        Email = aemail,
                        PhoneNumber = phone,
                        name = aemail.Split('@')[0],
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
                    "Pass123"
                    );
            }
            if (stff == null)
            {
                await userManager.CreateAsync(
                    new CustomUser
                    {
                        UserName = semail,
                        Email = semail,
                        PhoneNumber = phone,
                        name = semail.Split('@')[0],
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
                    "1Aa@211"
                    );
            }
            if (tr == null)
            {
                await userManager.CreateAsync(
                    new CustomUser
                    {
                        UserName = temail,
                        Email = temail,
                        PhoneNumber = phone,
                        name = temail.Split('@')[0],
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
                    "pA1411"
                    );
            }

        }

        public async Task<ActionResult> Init()
        {
            var context = new CustomIdentityDbContext();
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<CustomUser>(context);
            var userManager = new UserManager<CustomUser>(userStore);

            if(!await roleManager.RoleExistsAsync(SecurityRole.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = SecurityRole.Admin });
            }
            if (!await roleManager.RoleExistsAsync(SecurityRole.Staff))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = SecurityRole.Staff });
            }
            if (!await roleManager.RoleExistsAsync(SecurityRole.Trainer))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = SecurityRole.Trainer });
            }
            if (!await roleManager.RoleExistsAsync(SecurityRole.Trainee))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = SecurityRole.Trainee });
            }
            if (!await roleManager.RoleExistsAsync(SecurityRole.User))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = SecurityRole.User });
            }


            await Prepare();

            var aUser = await userManager.FindByEmailAsync("adm@bar.com");
            var sUser = await userManager.FindByEmailAsync("stff@bar.com");
            var tUser = await userManager.FindByEmailAsync("tr@bar.com");

            if (!await userManager.IsInRoleAsync(aUser.Id,SecurityRole.Admin))
            {
                userManager.AddToRole(aUser.Id, SecurityRole.Admin);
            }

            if (!await userManager.IsInRoleAsync(aUser.Id, SecurityRole.Staff))
            {
                userManager.AddToRole(aUser.Id, SecurityRole.Staff);
            }

            if (!await userManager.IsInRoleAsync(sUser.Id, SecurityRole.Staff))
            {
                userManager.AddToRole(sUser.Id, SecurityRole.Staff);
            }
            if (!await userManager.IsInRoleAsync(tUser.Id, SecurityRole.Trainer))
            {
                userManager.AddToRole(tUser.Id, SecurityRole.Trainer);
            }
            //passs must be include capital-letter and number
            return Content("admin are set up");
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterForm form)
        {
            var context = new CustomIdentityDbContext();
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<CustomUser>(context);
            var userManager = new UserManager<CustomUser>(userStore);

            if (!await roleManager.RoleExistsAsync(SecurityRole.Staff))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = SecurityRole.Staff });
            }

            var email =form.UserName;

            var phone = "0909191919";
            var age = 0;
            var dob = DateTime.Now;
            var edu = "0";
            var language = "0";
            var toeic = 0;
            var exp = "0";
            var department = "0";
            var location = "0";
            var type = "0";
            var workplace = "0";

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
                            Role = "Staff",
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
                        return Content("Success");
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
                var User = await userManager.FindByEmailAsync(form.UserName);

                if (!await userManager.IsInRoleAsync(User.Id, SecurityRole.Staff))
                {
                    userManager.AddToRole(User.Id, SecurityRole.Staff);
                }
            }
            //passs must be include capital-letter and number
            return View(form);
        }

    

[HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginForm form)
        {
            var context = new CustomIdentityDbContext();
            var store = new UserStore<CustomUser>(context);
            var manager = new UserManager<CustomUser>(store);

            var signInManager = new SignInManager<CustomUser, string>(manager, HttpContext.GetOwinContext().Authentication);

            var user = await manager.FindByEmailAsync(form.UserName);
            var result = await signInManager.PasswordSignInAsync(
              userName: form.UserName,
              password: form.Password,
              isPersistent: false,
              shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("AdminIndex", "Adm");
                default:
                    ModelState.AddModelError("", "Your account is not correct try again!");
                    return View(form);
            }
            
        }


        public ActionResult home()
        {
            return View();
        }
    }
}