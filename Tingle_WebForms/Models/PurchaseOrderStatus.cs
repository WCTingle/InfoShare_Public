using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tingle_WebForms.Models
{
    public class PurchaseOrderStatus
    {
        [Key]
        public int RecordId { get; set; }

        [MaxLength(20)]
        public string Status { get; set; }

        [MaxLength(1)]
        public string StatusCode { get; set; }
    }
}