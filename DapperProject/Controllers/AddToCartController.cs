using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DapperProject.Models;
using System.Data;
using System.Data.SqlClient;
using MobileDAL.cs;

namespace DapperProject.Controllers
{
    public class AddToCartController : Controller
    {
        DataTable dt;
        MobiledetailDAL _mdal = new MobiledetailDAL();
        // GET: AddToCart
        public ActionResult Add(Products mo)
        {
            
            if (Session["cart"]==null)
            {
                List<Products> li = new List<Products>();

                li.Add(mo);
                Session["cart"] = li;
                ViewBag.cart = li.Count();

               
                    Session["count"] = 1;


            }
            else
            {
                List<Products> li = (List<Products>)Session["cart"];
                li.Add(mo);
                Session["cart"] = li;
                ViewBag.cart = li.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;

            }
            return RedirectToAction("Index", "Home");

           
        }

        public ActionResult Myorder()
        {
            if (Session["cart"] != null)
            {
               return View((List<Products>)Session["cart"]);
            }
            else
            {
                return View();
            }
           

        }

        public ActionResult Remove(Products mob)
        {
            List<Products> li = (List<Products>)Session["cart"];
            li.RemoveAll(x=>x.ProductId==mob.ProductId);
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return RedirectToAction("Myorder", "AddToCart");
            //return View();
        }
    }
}