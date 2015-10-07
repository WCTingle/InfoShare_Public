using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Tingle_WebForms.Models
{
    public class FavoriteForm
    {
        [Key]
        public int RecordId { get; set; }

        public virtual SystemUsers User { get; set; }

        public virtual TForm Form { get; set; }
    }
}