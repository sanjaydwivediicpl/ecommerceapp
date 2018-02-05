using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using MobileDAL.cs;
using DapperProject.Models;

namespace DapperProject.Controllers
{
    public class HomeController : Controller
    {
        MobiledetailDAL _mdal = new MobiledetailDAL();
        DataTable dt;
        

        public ActionResult Index(int ? CategoryId)
        {
            string mycmd = string.Empty;
            if (CategoryId != null)
            {
                mycmd ="select * from Product Where CategoryId=" + CategoryId;
            }
            else
            {
                mycmd = "select Top 4 * from Product Where CategoryId=1 "
                + " union all "
                + " select Top 4 * from Product Where CategoryId = 2 "
                + " union all "
                + "  select Top 4 * from Product Where CategoryId = 3 "
                + " union all "
                + " select Top 4 * from Product Where CategoryId = 4";
            }
             
            dt = new DataTable();

            dt = _mdal.SelactAll(mycmd);


            List<Products> list = new List<Products>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Products mob = new Products
                {
                    ProductId = Convert.ToInt32(dt.Rows[i]["ProductId"]),
                    ProductName = Convert.ToString(dt.Rows[i]["ProductName"]),
                    Price = Convert.ToDouble(dt.Rows[i]["Price"]),
                    Url = Convert.ToString(dt.Rows[i]["Url"]),
                    ZoomUrl = Convert.ToString(dt.Rows[i]["ZoomUrl"]),
                    CategoryId = Convert.ToInt32(dt.Rows[i]["CategoryId"])
                };
                list.Add(mob);
            }
            return View(list);

        }

        public ActionResult AboutUs()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult EachProductDetails(Products m)
        {
            
            string mycmd = "select m.ProductId,m.ProductName,m.price,m.url,md.Features,md.model,md.color,md.SimType from Product m inner join ProductDetail md on m.ProductId=md.ProductId where m.ProductId=" + m.ProductId+"";
            dt = new DataTable();

            dt = _mdal.SelactAll(mycmd);


            List<ProductDetails> list = new List<ProductDetails>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ProductDetails mob = new ProductDetails();
                mob.ProductId = Convert.ToInt32(dt.Rows[i]["ProductId"]);
                mob.ProductName = dt.Rows[i]["ProductName"].ToString();
                mob.Price = Convert.ToDouble(dt.Rows[i]["Price"]);
                mob.Url = dt.Rows[i]["Url"].ToString();
                mob.Features= dt.Rows[i]["Features"].ToString();
                mob.color = dt.Rows[i]["color"].ToString();
                mob.SimType = dt.Rows[i]["SimType"].ToString();

                list.Add(mob);
            }
           
          
            return View(list);

           

        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}