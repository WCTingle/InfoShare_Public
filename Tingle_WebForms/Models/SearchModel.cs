using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tingle_WebForms.Models;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Web.ModelBinding;
using System.Data;
using System.Text;
using System.Globalization;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Data.Common;
using Tingle_WebForms.Logic;
using System.Web.UI.HtmlControls;

namespace Tingle_WebForms.Models
{
    public class SearchModel
    {
        public string FormName { get; set; }

        public string FormId { get; set; }

        public string PostBackUrl { get; set; }

        public string SubmittedDate { get; set; }

        public string SubmittedBy { get; set; }

        public string Status { get; set; }

        public string MatchedFieldsHtml { get; set; }

        public int ResultIndex { get; set; }
    }
}