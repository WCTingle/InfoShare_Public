using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Tingle_WebForms.Models
{
    public class TForm
    {
        [ScaffoldColumn(false)]
        [Key]
        public int FormID { get; set; }

        public DateTime Timestamp { get; set; }

        [MaxLength(200)]
        public string FormName { get; set; }

        [MaxLength(400)]
        public string FormNameHtml { get; set; }

        [MaxLength(200)]
        public string FormCreator { get; set; }

        [MaxLength(200)]
        public string Notes { get; set; }

        [MaxLength(200)]
        public string FormUrl { get; set; }

        public Int16 Status { get; set; }

    }
}