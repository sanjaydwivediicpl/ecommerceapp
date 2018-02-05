using DapperProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DapperProject.Controllers
{
    public class ShoppingController : Controller
    {
        ShopingCart objShopingCart = new ShopingCart();
        // GET: Shopping
        public ActionResult Index()
        {
            var result = objShopingCart.ShippingDetails.ToList();
            return View(result);
        }

        // GET: Shopping/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Shopping/Create
        public ActionResult Create()
        {
            if (Convert.ToInt32(Session["UserId"]) != 0)
            {
                ShippingDetail objShippingDetail = new ShippingDetail();
                objShippingDetail.TotalPrice = Convert.ToDecimal(Session["TotalPrice"]);
                return View(objShippingDetail);
            }
            else
            {
                return RedirectToRoute("~/Account/Login");
            }
        }

        // POST: Shopping/Create
        [HttpPost]
        public ActionResult Create(ShippingDetail model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ShippingDetail sd = new ShippingDetail();
                    sd.MemberId = Convert.ToInt32(Session["UserId"]);
                    sd.AddressLine = model.AddressLine;
                    sd.City = model.City;
                    sd.State = model.State;
                    sd.Country = model.Country;
                    sd.ZipCode = model.ZipCode;
                    sd.OrderId = Guid.NewGuid().ToString();
                    sd.TotalPrice = model.TotalPrice;
                    sd.PaymentType = model.PaymentType;
                    objShopingCart.ShippingDetails.Add(sd);
                    objShopingCart.SaveChanges();
                    Session["cart"] = null;
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

        // GET: Shopping/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Shopping/Edit/5
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

        // GET: Shopping/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Shopping/Delete/5
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
