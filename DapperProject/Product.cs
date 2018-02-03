namespace DapperProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }

        public int ProductId { get; set; }

        [StringLength(100)]
        public string ProductName { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        [StringLength(350)]
        public string url { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [StringLength(350)]
        public string ZoomUrl { get; set; }

        public int? CategoryId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
