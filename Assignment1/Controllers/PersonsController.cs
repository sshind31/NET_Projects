using Assignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment1.Controllers
{
    public class PersonsController : Controller
    {
        // GET: Persons
        public ActionResult Index()
        {
            return View();
        }

        // GET: Persons/Details/5
        public ActionResult Details()
        {
            
            string name = (string)Session["value1"];
            Person obj = Person.GetDetails(name);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Details(string logout)
        {
            if (!string.IsNullOrEmpty(logout))
            {
                return RedirectToAction("Logout");
            }
            string name = (string)Session["value1"];
            Person obj = Person.GetDetails(name);
            return View(obj);

        }

        // GET: Persons/Create
        public ActionResult Create()
        {
            List < City > cities = City.GetAllCities();
            List<SelectListItem> cityList = new List<SelectListItem>();
            foreach (var item in cities)
            {
                cityList.Add( new SelectListItem { Text = item.CityName, Value = item.CityId.ToString() });
            }
            ViewBag.Cities = cityList;
            return View();
        }

        // POST: Persons/Create
        [HttpPost]
        public ActionResult Create(Person obj)
        {
            try
            {
                Person.InsertPerson(obj);
                return RedirectToAction("Login");
            }
            catch
            {
                return View();
            }
        }

        // GET: Persons/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Persons/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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

        // GET: Persons/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Persons/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //================Login=============================

        // GET: Persons/Login
        public ActionResult Login()
        {
            HttpCookie objCookie = Request.Cookies["PersonLogin"]; 
            if (objCookie==null)
            {
                return View();
            }
            else
            {
                string name = objCookie.Values["key1"];
                Session["value1"] = name;
                return RedirectToAction("Details");
            }
             
        }

        // POST: Persons/Login
        [HttpPost]
        public ActionResult Login(Person obj)
        {
            try
            {
                

                bool valid = Person.ValidPerson(obj);
                if (valid)
                {
                    Session["value1"] = obj.LoginName;
                    if (obj.isActive)
                    {
                        HttpCookie objCookie = new HttpCookie("PersonLogin");
                        objCookie.Expires = DateTime.Now.AddDays(1);
                        objCookie.Values["key1"] = obj.LoginName;
                        Response.Cookies.Add(objCookie);
                    }


                 
                    return RedirectToAction("Details");
                }
                else
                {
                    return RedirectToAction("Create");
                }
              
            }
            catch
            {
                return View();
            }
        }
        //======================logout====================
        public ActionResult Logout()
        {
            Session.Abandon();
            HttpCookie objCookie = new HttpCookie("PersonLogin");
            objCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(objCookie);

            return RedirectToAction("Login");
        }
    }
}
