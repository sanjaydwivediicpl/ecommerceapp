using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DapperProject.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        ShopingCart objShopingCart = new ShopingCart();
        // GET: Admin/Category
        public ActionResult Index()
        {
            var result = objShopingCart.Categories.ToList();
            return View(result);
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int id)
        {
            var result = objShopingCart.Categories.Find(id);
            return View(result);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            Category objCategory = new Category();
            return View(objCategory);
        }

        // POST: Admin/Category/Create
        [HttpPost]
        public ActionResult Create(Category model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var result = objShopingCart.Categories.Add(model);
                  return RedirectToAction("Index");
                }
                else
                {
                    return View();
                } 
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int id)
        {
            var result = objShopingCart.Categories.Find(id);
            return View(result);
        }

        // POST: Admin/Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Category model)
        {
            try
            {
                // TODO: Add update logic here

                if (ModelState.IsValid)
                {
                    var result = objShopingCart.Categories.Add(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int id)
        {
            var result = objShopingCart.Categories.Find(id);
            return View(result);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Category model)
        {
            try
            {
                // TODO: Add delete logic here
                var result = objShopingCart.Categories.Remove(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
