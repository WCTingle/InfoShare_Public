using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Tingle_WebForms.Models
{
    public class NotifyOtherList
    {
        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
    }
}