using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Tingle_WebForms.Models
{
    public class EmailAddress
    {
        [ScaffoldColumn(false)]
        [Key]
        public int EmailAddressID { get; set; }

        public DateTime Timestamp { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string Company { get; set; }

        public Int16 Status { get; set; }

        public virtual TForm TForm { get; set; }

    }
}