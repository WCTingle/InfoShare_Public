using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Tingle_WebForms.Models
{
    public class UserStatus
    {
        [Key]
        public int RecordId { get; set; }

        public String StatusText { get; set; }
    }
}