namespace DapperProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AppUser")]
    public partial class AppUser
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ModifiedDate { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public int EditStatus { get; set; }

        [StringLength(50)]
        public string Role { get; set; }
    }
}
