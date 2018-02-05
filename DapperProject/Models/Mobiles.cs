using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DapperProject.Models
{
    public class Products
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Url { get; set; }
        public string ZoomUrl { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}