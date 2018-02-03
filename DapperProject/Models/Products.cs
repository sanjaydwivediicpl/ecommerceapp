using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DapperProject.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDetail { get; set; }
        public int Price { get; set; }
    }
}