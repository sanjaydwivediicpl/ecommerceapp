namespace DapperProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductDetail")]
    public partial class ProductDetail
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }

        [StringLength(600)]
        public string Features { get; set; }

        [StringLength(50)]
        public string Model { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        [StringLength(50)]
        public string SimType { get; set; }

        public virtual Product Product { get; set; }
    }
}
