namespace DapperProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ShippingDetail")]
    public partial class ShippingDetail
    {
        public int ShippingDetailId { get; set; }

        public int? MemberId { get; set; }

        [StringLength(255)]
        public string AddressLine { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(50)]
        public string ZipCode { get; set; }

        public string OrderId { get; set; }

        public decimal? TotalPrice { get; set; }

        [StringLength(50)]
        public string PaymentType { get; set; }
    }
}
