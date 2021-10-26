using Project.EF;
using Project.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Microsoft.Owin.Security;

namespace Project.Controllers
{
    [HandleError]
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
                    "pA@1411"
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


        public async Task<ActionResult> init1()
        {
            var context = new CustomIdentityDbContext();
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<CustomUser>(context);
            var userManager = new UserManager<CustomUser>(userStore);

            if (!await roleManager.RoleExistsAsync(SecurityRole.Trainee))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = SecurityRole.Trainee });
            }



            for (int i = 0; i <= 100; i++)
            {
                string Num, Num1;
                Random rd = new Random();
                Num = rd.Next(200, 300).ToString();
                Num1 = rd.Next(100, 500).ToString();

                var email = $"abcde{i}@mail.com";
                var phone = $"09{Num1}91{Num}";
                var age = 20;
                var dob = DateTime.Now;
                var edu = "FPT";
                var language = "C#";
                var toeic = 500;
                var exp = "1 year";
                var department = "IT";
                var location = "Ha Noi";
                var type = "0";
                var workplace = "0";
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
                            "Pass123"
                            );
                        if (result.Succeeded)
                        {
                            var User = await userManager.FindByEmailAsync(email);
                            userManager.AddToRole(User.Id, SecurityRole.Trainee);
                        }
                    }
                    else
                    {
                        return Content("failure");
                    }

                }
            }
            return Content("Finished");
        }

        public async Task<ActionResult> init2()
        {
            var context = new CustomIdentityDbContext();
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<CustomUser>(context);
            var userManager = new UserManager<CustomUser>(userStore);

            if (!await roleManager.RoleExistsAsync(SecurityRole.Trainer))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = SecurityRole.Trainer });
            }            
            for (int i = 0; i <= 20; i++)
            {
                string Num, Num1;
                Random rd = new Random();
                Num = rd.Next(200, 300).ToString();
                Num1 = rd.Next(100, 500).ToString();

                var email = $"Trainer{i}@mail.com";
                var phone = $"09{Num1}91{Num}";
                var age = 0;
                var dob = DateTime.Now;
                var edu = "0";
                var language = "0";
                var toeic = 0;
                var exp = "0";
                var department = "0";
                var location = "0";
                var type = "External";
                var workplace = "FPT";
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
                            "1230123"
                            );
                        if (result.Succeeded)
                        {
                            var User = await userManager.FindByEmailAsync(email);
                            userManager.AddToRole(User.Id, SecurityRole.Trainer);
                        }
                    }
                    else
                    {
                        return Content("failure");
                    }

                }
            }
            return Content("Finished");
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

            if (form.UserName != null && form.Password != null)
            {
                var result = await signInManager.PasswordSignInAsync(
                  userName: form.UserName,
                  password: form.Password,
                  isPersistent: false,
                  shouldLockout: false);

                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToAction("Route");
                        /*return RedirectToAction("AdminIndex", "Adm");*/
                    default:
                        ModelState.AddModelError("", "Your account is not correct try again!");
                        return View(form);
                }
            }
            return View(form);
        }



        [Authorize]
        public ActionResult Route()
        {
            if (User.IsInRole(SecurityRole.Admin))
            {
                return RedirectToAction("AdminIndex", "Adm");
            }
            if (User.IsInRole(SecurityRole.Staff))
            {
                return RedirectToAction("StaffIndex", "Staff");
            }
            if (User.IsInRole(SecurityRole.Trainee))
            {
                return RedirectToAction("Index", "Trainee");
            }
            if (User.IsInRole(SecurityRole.Trainer))
            {
                return RedirectToAction("Index", "Trainer");
            }
            return RedirectToAction("Login");
        }

        [Authorize(Roles = SecurityRole.Admin)]

        //---------------------------------

        public void Get()
        {
            var user = User.Identity;
            ViewBag.Name = user.Name;
        }

        [HttpGet]
        public ActionResult Register()
        {
            Get();
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
            if (form.UserName != null && form.Password != null)
            {
                var email = form.UserName;

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

                            if (!await userManager.IsInRoleAsync(Us.Id, SecurityRole.Staff))
                            {
                                userManager.AddToRole(Us.Id, SecurityRole.Staff);
                            }
                            return RedirectToAction("AdminIndex","Adm");
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
            }
            //passs must be include capital-letter and number
            return View(form);
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChangePass()
        {
            Get();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ChangePass(ChangePass form, string id)
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
                        return RedirectToAction("Index", "Trainer");
                    }
                }
                return View(form);
            }
        }


        [Authorize(Roles = SecurityRole.Admin)]
        [HttpGet]
        public ActionResult TrainerRegister()
        {
            Get();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> TrainerRegister(RegisterForm form)
        {
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

                var phone = "0";
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
                            return RedirectToAction("AdminIndex","Adm");
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
            }
            //passs must be include capital-letter and number
            return View(form);
        }



        [Authorize(Roles = SecurityRole.Staff)]
        [HttpGet]
        public ActionResult TraineeRegister()
        {
            Get();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> TraineeRegister(TraineeRegisterForm form)
        {
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
            var type ="0";
            var workplace ="0";

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
                            return RedirectToAction("TraineeAcc","Staff");
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
            }
            //passs must be include capital-letter and number
            return View(form);
        }


        [HttpPost]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login","Home");
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}