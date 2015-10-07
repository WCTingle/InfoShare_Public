using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tingle_WebForms.Logic;
using Tingle_WebForms.Models;

namespace Tingle_WebForms
{
    public partial class TingleChat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IEnumerable<User> GetChatters()
        {
            return ChatHub._CurrentChatters.Select(k => new User
            {
                Name = k.Value,
                ConnectionId = k.Key
            }).ToArray();
        }
    }
}