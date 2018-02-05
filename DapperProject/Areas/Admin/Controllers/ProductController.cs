using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DapperProject.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        ShopingCart objShopingCart = new ShopingCart();
        // GET: Admin/Product
        public ActionResult Index()
        {
            var result = objShopingCart.Products.ToList();
            return View(result);
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int id)
        {
            var result = objShopingCart.Products.Find(id);
            return View(result);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            Product objProduct = new Product();
            return View(objProduct);
        }

        // POST: Admin/Product/Create
        [HttpPost]
        public ActionResult Create(Product model, HttpPostedFileBase file)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string _fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/images"), _fileName);
                        file.SaveAs(_path);
                        model.url = _fileName;
                    }

                    var result = objShopingCart.Products.Add(model);
                    objShopingCart.SaveChanges();
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

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int id)
        {
            var result = objShopingCart.Products.Find(id);
            return View(result);
        }

        // POST: Admin/Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product model)
        {
            try
            {
                // TODO: Add update logic here
                var result = objShopingCart.Products.Add(model);
                objShopingCart.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int id)
        {
            var result = objShopingCart.Products.Find(id);
            return View(result);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Product model)
        {
            try
            {
                // TODO: Add delete logic here
                var result = objShopingCart.Products.Remove(model);
                objShopingCart.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
