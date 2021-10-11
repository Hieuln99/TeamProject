using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class AdmController : Controller
    {
        // GET: Adm
        public ActionResult AdminIndex()
        {
            return View();
        }

        public ActionResult AddTrainerAcc()
        {
            return View();
        }
        public ActionResult EditTrainerAcc()
        {
            return View();
        }
        public ActionResult DeleteTrainerAcc()
        {
            return View();
        }



        //-----------------------------------
        public ActionResult AddStaffAcc()
        {
            return View();
        }
        public ActionResult EditStaffAcc()
        {
            return View();
        }
        public ActionResult DeleteStaffAcc()
        {
            return View();
        }


    }
}