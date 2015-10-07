using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tingle_WebForms.Models
{
    public class SkuQuantity
    {
        [Key]
        public int RecordId { get; set; }

        [MaxLength(100)]
        public string MaterialSku { get; set; }

        [MaxLength(100)]
        public string Quantity { get; set; }

        [MaxLength(100)]
        public string TempId { get; set; }

        public Boolean Completed { get; set; }

        public virtual ExpeditedOrderForm ExpeditedOrderForm { get; set; }

        public virtual DirectOrderForm DirectOrderForm { get; set; }

    }
}