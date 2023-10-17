using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductDetails.DAL;
using ProductDetails.Models;

namespace ProductDetails.Controllers
{
    public class ProductController : Controller
    {
        ProductDAL productDAL = new ProductDAL();

        // GET: Product
        public ActionResult Index()
        {
            var productList = productDAL.GetAllProducts();
            return View(productList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            Product product = productDAL.FetchByID(id);
            if (product == null)
            {
                TempData["InfoMessage"] = "Product is not in Database";
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            bool isInserted;

            try
            {
                if (ModelState.IsValid)
                {
                    isInserted = productDAL.InsertProductToDB(product);
                    if (isInserted)
                    {
                        TempData["InfoMessage"] = "Product inserted Successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Product not able to insert Something is wrong";
                    }
                }
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] =ex.Message;
                return View();
            }
            return View();
            
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = productDAL.FetchByID(id);
            if(product==null)
            {
                TempData["InfoMessage"] = "Product is not in Database";
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            bool isUpdated;

            try
            {
                if (ModelState.IsValid)
                {
                    isUpdated = productDAL.updateProductDetails(product);
                    if (isUpdated)
                    {
                        TempData["InfoMessage"] = "Product Updated Successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Product not able to update Something is wrong";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            bool isDeleted;
            try
            {
                isDeleted = productDAL.deleteProductFromDB(id);
                if(isDeleted)
                {
                    TempData["InfoMessage"] = "Product Deleted Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["InfoMessage"] = "Product not able to delete Something is wrong";
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                TempData["InfoMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // POST: Product/Delete/5
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
    }
}
