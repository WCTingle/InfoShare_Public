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
using System.Text.RegularExpressions;

namespace Tingle_WebForms
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

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
                    if (currentUser.UserRole.RoleName == "ReportsUser" || currentUser.UserRole.RoleName == "ReportsAdmin" || currentUser.UserRole.RoleName == "SuperUser" ||
                        currentUser.UserRole.RoleName == "Developer")
                    {
                        miReports.Visible = true;
                        phSearch.Visible = true;
                    }
                    else
                    {
                        phSearch.Visible = false;
                        miReports.Visible = false;
                    }

                    if (currentUser.UserRole.RoleName == "SuperUser" || currentUser.UserRole.RoleName == "Developer")
                    {
                        miAdmin.Visible = true;
                    }
                    else
                    {
                        miAdmin.Visible = false;
                    }

                    if (currentUser.InventoryApprovalUser.HasValue && currentUser.InventoryApprovalUser.Value == true)
                    {
                        miInventoryApproval.Visible = true;
                    }
                    else
                    {
                        miInventoryApproval.Visible = false;
                    }
                }
                else
                {
                    miForms.Visible = false;
                    miReports.Visible = false;
                    miAdmin.Visible = false;
                    phSearch.Visible = false;
                    miInventoryApproval.Visible = false;
                    Response.Redirect("/Unauthorized");
                }


            }
            catch (Exception ex)
            {

            }


        }

        protected void txtSearchSubmit_Click(object sender, ImageClickEventArgs e)
        {
            if (txtSearch.Text != "")
            {
                Session["SearchValue"] = txtSearch.Text;
                Response.Redirect("/Search");
            }
        }

        public IEnumerable<UserStatus> GetUserStatuses()
        {
            using (FormContext ctx = new FormContext())
            {
                var StatusList = ctx.UserStatuses.ToList();

                return StatusList;
            }
        }

        protected void ddlUserStatus_DataBound(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                UserLogic uLogic = new UserLogic();
                System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                SystemUsers currentUser = uLogic.GetCurrentUser(user);

                if (currentUser != null)
                {
                    ddlUserStatus.SelectedText = currentUser.UserStatus.StatusText;
                }

            }

            
        }

        protected void txtGreeting_PreRender(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                UserLogic uLogic = new UserLogic();
                System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                SystemUsers currentUser = uLogic.GetCurrentUser(user);

                if (currentUser != null)
                {
                    txtGreeting.Text = currentUser.Greeting;
                }
            }
        }

        protected void btnSaveAccountSettings_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    UserLogic uLogic = new UserLogic();
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);

                    using (FormContext ctx = new FormContext())
                    {
                        var updateUser = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == currentUser.SystemUserID);

                        updateUser.Greeting = txtGreeting.Text;
                        updateUser.DisplayName = txtDisplayName.Text;
                        updateUser.UserStatus = ctx.UserStatuses.FirstOrDefault(x => x.StatusText == ddlUserStatus.SelectedText);

                        ctx.SaveChanges();

                        divSaveCancel.Visible = false;
                        divLabelEdit.Visible = true;
                        divDisplayNameEdit.Visible = false;
                        ddlUserStatus.Enabled = false;
                        txtGreeting.Enabled = false;

                        lblUser.Text = txtDisplayName.Text;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        protected void cvTxtDisplayName_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtDisplayName.Text != "")
            {
                string regexStr = @"^[A-Za-z ]+$";

                if (Regex.IsMatch(txtDisplayName.Text, regexStr))
                {
                    args.IsValid = true;
                    lblUpdateAccountSettings.Text = "";
                }
                else
                {
                    args.IsValid = false;
                    lblUpdateAccountSettings.Text = "Display Name: Letters and Spaces only";
                }
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void btnEditAccountSettings_Click(object sender, EventArgs e)
        {
            divSaveCancel.Visible = true;
            divLabelEdit.Visible = false;
            ddlUserStatus.Enabled = true;
            divDisplayNameEdit.Visible = true;
            txtGreeting.Enabled = true;

            if (Request.IsAuthenticated)
            {
                UserLogic uLogic = new UserLogic();
                System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                SystemUsers currentUser = uLogic.GetCurrentUser(user);

                if (currentUser != null)
                {
                    txtDisplayName.Text = currentUser.DisplayName;
                }
            }
        }

        protected void btnCancelAccountSettings_Click(object sender, EventArgs e)
        {
            divSaveCancel.Visible = false;
            divLabelEdit.Visible = true;
            divDisplayNameEdit.Visible = false;
            ddlUserStatus.Enabled = false;
            txtGreeting.Enabled = false;

            if (Request.IsAuthenticated)
            {
                UserLogic uLogic = new UserLogic();
                System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                SystemUsers currentUser = uLogic.GetCurrentUser(user);

                if (currentUser != null)
                {
                    txtGreeting.Text = currentUser.Greeting;
                    txtDisplayName.Text = currentUser.DisplayName;
                }
            }
        }

    }
}