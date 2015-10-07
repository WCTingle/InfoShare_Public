using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Tingle_WebForms.Models
{
    public class FormPermissions
    {
        [Key]
        public int RecordId { get; set; }

        [MaxLength(100)]
        public string FormName { get; set; }

        public virtual UserRoles UserRole { get; set; }

        public bool Enabled { get; set; }
    }
}