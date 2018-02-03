namespace DapperProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserRole
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
