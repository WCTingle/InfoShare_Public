using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tingle_WebForms.Models
{
    public class ExpediteCode
    {
        [Key]
        public int ExpediteCodeID { get; set; }

        public DateTime Timestamp { get; set; }

        [MaxLength(20)]
        public string Code { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public Int16 Status { get; set; }

    }
}