using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Tingle_WebForms.Models
{
    public class SystemUsers
    {
        [Key]
        public int SystemUserID { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string DisplayName { get; set; }

        [MaxLength(100)]
        public string EmailAddress { get; set; }

        public virtual UserRoles UserRole { get; set; }

        public Int16 Status { get; set; }

        [MaxLength(50)]
        public String Greeting { get; set; }

        public Int32 Points { get; set; }

        public virtual UserStatus UserStatus { get; set; }

        public Nullable<Boolean> InventoryApprovalUser { get; set; }

        public SystemUsers()
        {
            Status = 1;
        }

    }
}