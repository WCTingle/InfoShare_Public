using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tingle_WebForms.Models
{
    public class RequestNotifications
    {
        [Key]
        public int RecordId { get; set; }

        public DateTime Timestamp { get; set; }

        public virtual TForm Form { get; set; }

        public Int32 RequestedFormId { get; set; }

        public SystemUsers SentBy { get; set; }

        public string ToEmailAddress { get; set; }

        [Column(TypeName="nvarchar(MAX)")]
        public string BodyHtml { get; set; }

        public Int16 Status { get; set; }

    }
}