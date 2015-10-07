using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tingle_WebForms.Models
{
    public class Comments
    {
        [Key]
        public int RecordId { get; set; }

        public DateTime Timestamp { get; set; }

        [MaxLength(300)]
        public String Note { get; set; }

        public virtual TForm Form { get; set; }

        public virtual SystemUsers User { get; set; }

        public Int32 RelatedFormId { get; set; }

        public bool SystemComment { get; set; }
    }
}