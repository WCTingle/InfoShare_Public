using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;
using Tingle_WebForms.Models;
using Tingle_WebForms.Logic;
namespace Tingle_WebForms
{
    public partial class Site_Mobile : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;

                UserLogic uLogic = new UserLogic();

                SystemUsers currentUser = uLogic.GetCurrentUser(user);

                if (!String.IsNullOrEmpty(currentUser.DisplayName))
                {
                    lblUser.Text = currentUser.DisplayName;
                }

                if (currentUser.Status == 1)
                {
                    if (currentUser.UserRole.RoleName == "ReportsUser" || currentUser.UserRole.RoleName == "ReportsAdmin" || currentUser.UserRole.RoleName == "SuperUser")
                    {
                        miReports.Visible = true;
                    }
                    else
                    {
                        miReports.Visible = false;
                    }

                    if (currentUser.UserRole.RoleName == "SuperUser")
                    {
                        miAdmin.Visible = true;
                    }
                    else
                    {
                        miAdmin.Visible = false;
                    }
                }
                else
                {
                    miForms.Visible = false;
                    miReports.Visible = false;
                    miAdmin.Visible = false;
                    Response.Redirect("/Unauthorized");
                }


            }
            catch (Exception ex)
            {

            }
        }
    }
}