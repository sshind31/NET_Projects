using LoginRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginRegister.Controllers
{
    public class UserController : Controller
    {
        User user = new User();
        
        public ActionResult Regiser()
        {
            List<SelectListItem> list = user.GetListOfCity();
            ViewBag.listofCity = list;
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Regiser(User u)
        {
            try
            {
                bool check = user.InsertUserDataToDB(u);

                return RedirectToAction("Index");
            }
            catch
            {
                List<SelectListItem> list = user.GetListOfCity();
                ViewBag.listofCity = list;
                return View();
            }
        }
        // GET: User/Create
        public ActionResult Login()
        {
            HttpCookie objCookie1 = Request.Cookies["LoginName"];
            HttpCookie objCookie2 = Request.Cookies["Password"];

            if(objCookie1 != null && objCookie2 != null)
            {
                LoginUser lu = new LoginUser 
                { 
                    LoginName=objCookie1.Value.ToString(), 
                    Password=objCookie2.Value.ToString(), 
                    RemeberMe = false 
                };
                bool check = user.validateUser(lu);
                return RedirectToAction("Default");
            }
            
            return View();
        }
        [ChildActionOnly]
        public ActionResult card()
        {
            return View("~/Views/_card.cshtml");
        }


        // POST: User/Create
        [HttpPost]
        public ActionResult Login(LoginUser lu)
        {
            try
            {
                bool check = user.validateUser(lu);
                if(check)
                {
                    Session["key"] = lu.LoginName;
                    Session["pas"] = lu.Password;
                }
                if(check && lu.RemeberMe)
                {
                    Response.Cookies["LoginName"].Value = lu.LoginName;
                    Response.Cookies["Password"].Value = lu.Password;
                }
                return RedirectToAction("Default");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Update(string id)
        {
            LoginUser lu = new LoginUser 
            { 
                LoginName=Session["key"].ToString(),
                Password = Session["pas"].ToString(),
                RemeberMe=false
            };
            User u = user.GetuserByLoginName(lu);

            return View(u);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Update(User u)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Default()
        {
            LoginUser lu = new LoginUser();
            lu = null;
            return View(lu);
        }
        public ActionResult Logout()
        {
            HttpCookie objCookie1 = Request.Cookies["LoginName"];
            HttpCookie objCookie2 = Request.Cookies["Password"];
            Session.RemoveAll();
            if (objCookie1 != null && objCookie2 != null)
            {
                Response.Cookies["LoginName"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
            }
            return View();
        }
    }
}
