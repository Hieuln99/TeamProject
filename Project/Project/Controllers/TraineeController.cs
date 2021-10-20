using Microsoft.AspNet.Identity;
using Project.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class TraineeController : Controller
    {
        // GET: Trainee
        public ActionResult Index()
        {
            using (var bwCtx = new CustomIdentityDbContext())
            {
                var s = User.Identity.GetUserId();
                var t = bwCtx.Users.FirstOrDefault(f => f.Id == s);
                return View(t);
            }
                
        }
        public ActionResult TraineeCourse()
        {
            var id = User.Identity.GetUserId();
            using (var bwCtx = new CustomIdentityDbContext())
            {

                var t = bwCtx.Users.Include((b => b.courses)).FirstOrDefault(f => f.Id == id);
                if(t != null)
                {
                    ViewBag.a = t.courses;
                    return View();
                }
                return RedirectToAction("Index");
            }
            
        }
     }
}