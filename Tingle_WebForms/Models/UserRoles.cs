using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Tingle_WebForms.Models
{
    public class UserRoles
    {
        [Key]
        public int UserRoleId { get; set; }

        [MaxLength(100)]
        public string RoleName { get; set; }

        [MaxLength(500)]
        public string RoleDescription { get; set; }

    }
}