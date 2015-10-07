using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Tingle_WebForms.Models
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }

        [MaxLength(20)]
        public string StatusText {get;set;}

    }
}