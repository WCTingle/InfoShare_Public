using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tingle_WebForms.Models
{
    public class PriceChangeRequestProducts
    {
        [Key]
        public int RecordId { get; set; }

        [MaxLength(20)]
        public string ProductText { get; set; }
    }
}