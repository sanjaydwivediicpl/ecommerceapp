namespace DapperProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        public int CategoryId { get; set; }

        [StringLength(255)]
        public string CategoryName { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDelete { get; set; }
    }
}
