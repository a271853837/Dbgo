using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTest.Framework;
using WebTest.Models;

namespace WebTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new MyContext())
            {
                User model = new Models.User();
                model.CreateTime = DateTime.Now;
                model.IsDel = false;
                model.Pwd = "123";
                model.UserName = "jerry";
                db.Users.Add(model);
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}