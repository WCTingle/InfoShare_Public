using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tingle_WebForms.Models
{
    public class Vendor
    {
        [Key]
        public int RecordId { get; set; }

        public DateTime Timestamp { get; set; }

        [MaxLength(100)]
        public String VendorName { get; set; }

        [MaxLength(100)]
        public String VendorAddress { get; set; }

        [MaxLength(50)]
        public String VendorCity { get; set; }

        [MaxLength(20)]
        public String VendorState { get; set; }

        [MaxLength(15)]
        public String VendorZip { get; set; }

        [MaxLength(20)]
        public String VendorPhone { get; set; }

        [MaxLength(20)]
        public String VendorFax { get; set; }

        public Decimal CurrentStock { get; set; }
    }
}