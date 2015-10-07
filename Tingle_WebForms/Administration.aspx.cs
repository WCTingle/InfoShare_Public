using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tingle_WebForms.Models;
using Tingle_WebForms.Logic;
using Tingle_WebForms.Forms;
using Tingle_WebForms;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Web.ModelBinding;
using System.Data;
using System.Globalization;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Data.Common;
using Telerik.Web.UI;

namespace Tingle_WebForms
{
    public partial class Administration : System.Web.UI.Page
    {
        FormContext _ctx = new FormContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFormPermissions();
            }
        }


        public IQueryable<Tingle_WebForms.Models.EmailAddress> gvEmailList_GetData()
        {
            try
            {
                int id = Convert.ToInt32(ddlFormName.SelectedValue);

                if (id != 0)
                {
                    IQueryable<Tingle_WebForms.Models.EmailAddress> EmailAddresses = _ctx.EmailAddresses.Where(ea => ea.TForm.FormID == id);

                    lblUserMessage.Text = "";

                    return EmailAddresses;
                }
                else
                {
                    lblEmailMessage.Text = "Unable to load Email List.  Please contact your system administrator.";
                    return null;
                }
            }
            catch (Exception ex)
            {
                lblEmailMessage.Text = "Unable to load Email List.  Please contact your system administrator.";
                return null;
            }

        }



        public void gvEmailList_DeleteItem(int EmailAddressID)
        {
            try
            {
                EmailAddress EmailAddressToDelete = _ctx.EmailAddresses.Where(ea => ea.EmailAddressID == EmailAddressID).FirstOrDefault();

                if (EmailAddressToDelete != null)
                {
                    _ctx.EmailAddresses.Remove(EmailAddressToDelete);
                    _ctx.SaveChanges();
                }

                lblUserMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblEmailMessage.Text = "Unable to delete Email Address.  Please contact your system administrator.";
            }
        }

        public void gvEmailList_UpdateItem(int EmailAddressID){}

        protected void gvEmailList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                GridViewRow gvr = (GridViewRow)gv.Rows[e.RowIndex];
                Label lblEmailAddressID = (Label)gvr.FindControl("lblEmailAddressIDEdit");
                int id = Convert.ToInt32(lblEmailAddressID.Text);

                using (FormContext ctx = new FormContext())
                {
                    var emailAddress = ctx.EmailAddresses.Where(ea => ea.EmailAddressID == id).FirstOrDefault();

                    emailAddress.Name = ((TextBox)gvr.FindControl("txtNameEdit")).Text;
                    emailAddress.Address = ((TextBox)gvr.FindControl("txtAddressEdit")).Text;
                    emailAddress.Status = Convert.ToInt16(((RadioButtonList)gvr.FindControl("rblStatusEdit")).SelectedValue);
                    emailAddress.Company = ((DropDownList)gvr.FindControl("ddlCompanyEdit")).SelectedValue;

                    ctx.EmailAddresses.Attach(emailAddress);
                    ctx.Entry(emailAddress).State = EntityState.Modified;

                    ctx.SaveChanges();
                    gvEmailList.DataBind();

                    lblEmailMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblEmailMessage.Text = "Unable to update Email Address.  Please contact your system administrator.";
            }
        }

        protected void gvEmailList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow && gvEmailList.EditIndex == e.Row.RowIndex)
                {
                    DropDownList ddlCompanyEdit = (DropDownList)e.Row.FindControl("ddlCompanyEdit");
                    ddlCompanyEdit.Items.FindByValue((e.Row.FindControl("lblCompanyEdit") as Label).Text).Selected = true;

                    RadioButtonList rblStatusEdit = (RadioButtonList)e.Row.FindControl("rblStatusEdit");
                    int recordId = Convert.ToInt32(((Label)e.Row.FindControl("lblEmailAddressIDEdit")).Text);
                    EmailAddress eAddress = _ctx.EmailAddresses.FirstOrDefault(x => x.EmailAddressID == recordId);

                    rblStatusEdit.SelectedValue = eAddress.Status.ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void fvEmailInsert_InsertItem()
        {
            try
            {
                TextBox txtNameInsert = (TextBox)fvEmailInsert.FindControl("txtNameInsert");
                TextBox txtAddressInsert = (TextBox)fvEmailInsert.FindControl("txtAddressInsert");
                RadioButtonList rblCompanyInsert = (RadioButtonList)fvEmailInsert.FindControl("rblCompanyInsert");
                RadioButtonList rblStatusInsert = (RadioButtonList)fvEmailInsert.FindControl("rblStatusInsert");
                Int16 status = Convert.ToInt16(rblStatusInsert.SelectedValue);
                int id = Convert.ToInt32(ddlFormName.SelectedValue);

                using (FormContext ctx = new FormContext())
                {
                    var tForm = ctx.TForms.Where(f => f.FormID == id).FirstOrDefault();

                    EmailAddress newEmail = new EmailAddress();

                    newEmail.Name = txtNameInsert.Text;
                    newEmail.Address = txtAddressInsert.Text;
                    newEmail.Company = rblCompanyInsert.SelectedValue;
                    newEmail.Status = status;
                    newEmail.TForm = tForm;
                    newEmail.Timestamp = DateTime.Now;

                    ctx.EmailAddresses.Add(newEmail);

                    if (ModelState.IsValid)
                    {
                        ctx.SaveChanges();
                        gvEmailList.DataBind();
                    }

                    lblEmailMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblEmailMessage.Text = "Unable to insert new Email Address.  Please contact your system administrator.";
            }
        }


        public IQueryable<Tingle_WebForms.Models.SystemUsers> gvUsers_GetData()
        {
            try
            {
                IQueryable<Tingle_WebForms.Models.SystemUsers> SystemUsers = _ctx.SystemUsers;

                if (SystemUsers != null)
                {
                    lblUserMessage.Text = "";
                    return SystemUsers;
                }
                else
                {
                    lblUserMessage.Text = "Unable to load User List.  Please contact your system administrator.";
                    return null;
                }
            }
            catch (Exception ex)
            {
                lblUserMessage.Text = "Unable to load User List.  Please contact your system administrator.";
                return null;
            }
        }

        public void gvUsers_DeleteItem(int SystemUserID)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    SystemUsers UserToDelete = ctx.SystemUsers.Where(su => su.SystemUserID == SystemUserID).FirstOrDefault();

                    if (UserToDelete != null)
                    {
                        ctx.SystemUsers.Remove(UserToDelete);
                        ctx.SaveChanges();
                    }

                    lblUserMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblUserMessage.Text = "Unable to delete User.  Please contact your system administrator.";
            }
        }

        public void gvUsers_UpdateItem(int SystemUserID){}

        protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                GridViewRow gvr = (GridViewRow)gv.Rows[e.RowIndex];
                Label lblSystemUserID = (Label)gvr.FindControl("lblSystemUserIDEdit");
                int id = Convert.ToInt32(lblSystemUserID.Text);
                int userRoleID = Convert.ToInt32(((DropDownList)gvr.FindControl("ddlUserRoleEdit")).SelectedValue);
                Boolean invMgmt = Convert.ToBoolean(((CheckBox)gvr.FindControl("cbInventoryManagementEdit")).Checked);

                using (FormContext ctx = new FormContext())
                {
                    var systemUser = ctx.SystemUsers.Where(su => su.SystemUserID == id).FirstOrDefault();

                    systemUser.DisplayName = ((TextBox)gvr.FindControl("txtDisplayNameEdit")).Text;
                    systemUser.EmailAddress = ((TextBox)gvr.FindControl("txtEmailAddressEdit")).Text;
                    systemUser.Status = Convert.ToInt16(((RadioButtonList)gvr.FindControl("rblUserStatusEdit")).SelectedValue);
                    systemUser.UserRole = ctx.UserRoles.Where(ur => ur.UserRoleId == userRoleID).FirstOrDefault();
                    systemUser.InventoryApprovalUser = invMgmt;

                    ctx.SystemUsers.Attach(systemUser);
                    ctx.Entry(systemUser).State = EntityState.Modified;

                    ctx.SaveChanges();
                    gvUsers.DataBind();
                    lblUserMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblUserMessage.Text = "Unable to update user.  Please contact your system administrator.";
            }

        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlUserRoleEdit = (DropDownList)e.Row.FindControl("ddlUserRoleEdit");
                    int userId = Convert.ToInt32(((Label)e.Row.FindControl("lblSystemUserIDEdit")).Text);
                    SystemUsers sUser = _ctx.SystemUsers.Where(su => su.SystemUserID == userId).FirstOrDefault();

                    ddlUserRoleEdit.SelectedValue = sUser.UserRole.UserRoleId.ToString();

                    RadioButtonList rblUserStatusEdit = (RadioButtonList)e.Row.FindControl("rblUserStatusEdit");
                    int recordId = Convert.ToInt32(((Label)e.Row.FindControl("lblSystemUserIDEdit")).Text);

                    rblUserStatusEdit.SelectedValue = sUser.Status.ToString();

                }
            }
        }

        public void fvUserInsert_InsertItem()
        {
            try
            {
                TextBox txtUserNameInsert = (TextBox)fvUserInsert.FindControl("txtUserNameInsert");
                TextBox txtDisplayNameInsert = (TextBox)fvUserInsert.FindControl("txtDisplayNameInsert");
                TextBox txtEmailAddressInsert = (TextBox)fvUserInsert.FindControl("txtEmailAddressInsert");
                RadioButtonList rblUserStatusInsert = (RadioButtonList)fvUserInsert.FindControl("rblUserStatusInsert");
                CheckBox cbInventoryApproval = (CheckBox)fvUserInsert.FindControl("cbInventoryApproval");
                int roleId = Convert.ToInt32(((DropDownList)fvUserInsert.FindControl("ddlUserRoleInsert")).SelectedValue);
                Int16 status = Convert.ToInt16(rblUserStatusInsert.SelectedValue);

                using (FormContext ctx = new FormContext())
                {
                    if (ctx.SystemUsers.Where(su => su.UserName == txtUserNameInsert.Text).FirstOrDefault() == null)
                    {
                        SystemUsers newUser = new SystemUsers();
                        UserRoles newRole = ctx.UserRoles.Where(ur => ur.UserRoleId == roleId).FirstOrDefault();

                        newUser.UserName = txtUserNameInsert.Text;
                        newUser.DisplayName = txtDisplayNameInsert.Text;
                        newUser.EmailAddress = txtEmailAddressInsert.Text;
                        newUser.Status = status;
                        newUser.UserRole = newRole;
                        newUser.InventoryApprovalUser = cbInventoryApproval.Checked;

                        ctx.SystemUsers.Add(newUser);

                        if (ModelState.IsValid)
                        {
                            ctx.SaveChanges();
                            gvUsers.DataBind();
                        }
                        lblUserMessage.Text = "";
                    }
                    else
                    {
                        lblUserMessage.Text = "A User with the username: " + txtUserNameInsert.Text + " already exists.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblUserMessage.Text = "Unable to add new User.  Please contact your system administrator.";
            }
        }


        protected void btnClearForm_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox txtNameInsert = (TextBox)fvEmailInsert.FindControl("txtNameInsert");
                TextBox txtAddressInsert = (TextBox)fvEmailInsert.FindControl("txtAddressInsert");
                RadioButtonList rblStatusInsert = (RadioButtonList)fvEmailInsert.FindControl("rblStatusInsert");
                int status = Convert.ToInt32(rblStatusInsert.SelectedValue);

                txtNameInsert.Text = "";
                txtAddressInsert.Text = "";
                rblStatusInsert.SelectedValue = "1";

                lblUserMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblEmailMessage.Text = "Clear form failed.  Please contact your system admistrator.";
            }
        }

        protected void btnClearUserForm_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox txtUserNameInsert = (TextBox)fvUserInsert.FindControl("txtUserNameInsert");
                TextBox txtDisplayNameInsert = (TextBox)fvUserInsert.FindControl("txtDisplayNameInsert");
                TextBox txtEmailAddressInsert = (TextBox)fvUserInsert.FindControl("txtEmailAddressInsert");
                RadioButtonList rblUserStatusInsert = (RadioButtonList)fvUserInsert.FindControl("rblUserStatusInsert");
                DropDownList ddlUserRoleInsert = (DropDownList)fvUserInsert.FindControl("ddlUserRoleInsert");
                int status = Convert.ToInt32(rblUserStatusInsert.SelectedValue);

                txtUserNameInsert.Text = "";
                txtDisplayNameInsert.Text = "";
                txtEmailAddressInsert.Text = "";
                rblUserStatusInsert.SelectedValue = "1";
                ddlUserRoleInsert.SelectedIndex = 0;
                lblUserMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblUserMessage.Text = "Clear form failed.  Please contact your system admistrator.";
            }
        }

        protected void btnClearCodeForm_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox txtCodeInsert = (TextBox)fvUserInsert.FindControl("txtCodeInsert");
                TextBox txtDescriptionInsert = (TextBox)fvUserInsert.FindControl("txtDescriptionInsert");
                RadioButtonList rblCodeStatusInsert = (RadioButtonList)fvUserInsert.FindControl("rblCodeStatusInsert");

                txtCodeInsert.Text = "";
                txtDescriptionInsert.Text = "";
                rblCodeStatusInsert.SelectedValue = "1";
                lblVariableMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblVariableMessage.Text = "Clear form failed.  Please contact your system admistrator.";
            }
        }

        protected void btnClearStatusForm_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox txtStatusInsert = (TextBox)fvPOStatus.FindControl("txtStatusInsert");
                TextBox txtStatusCodeInsert = (TextBox)fvPOStatus.FindControl("txtStatusCodeInsert");

                txtStatusInsert.Text = "";
                txtStatusCodeInsert.Text = "";
                lblVariableMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblVariableMessage.Text = "Clear form failed.  Please contact your system admistrator.";
            }
        }

        protected void btnClearNotificationEmailForm_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox txtNameInsert = (TextBox)fvNotificationEmails.FindControl("txtNameInsert");
                TextBox txtAddressInsert = (TextBox)fvNotificationEmails.FindControl("txtAddressInsert");
                RadioButtonList rblNotificationEmailStatusInsert = (RadioButtonList)fvNotificationEmails.FindControl("rblNotificationEmailStatusInsert");

                txtNameInsert.Text = "";
                txtAddressInsert.Text = "";
                rblNotificationEmailStatusInsert.SelectedValue = "1";
                lblNotificationEmailMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblNotificationEmailMessage.Text = "Clear form failed.  Please contact your system admistrator.";
            }
        }

        protected void btnClearVendorForm_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox txtVendorNameInsert = (TextBox)fvNotificationEmails.FindControl("txtVendorNameInsert");
                TextBox txtVendorAddressInsert = (TextBox)fvNotificationEmails.FindControl("txtVendorAddressInsert");
                TextBox txtVendorCityInsert = (TextBox)fvNotificationEmails.FindControl("txtVendorCityInsert");
                TextBox txtVendorStateInsert = (TextBox)fvNotificationEmails.FindControl("txtVendorStateInsert");
                TextBox txtVendorZipInsert = (TextBox)fvNotificationEmails.FindControl("txtVendorZipInsert");
                TextBox txtVendorPhoneInsert = (TextBox)fvNotificationEmails.FindControl("txtVendorPhoneInsert");
                TextBox txtVendorFaxInsert = (TextBox)fvNotificationEmails.FindControl("txtVendorFaxInsert");

                txtVendorNameInsert.Text = "";
                txtVendorAddressInsert.Text = "";
                txtVendorCityInsert.Text = "";
                txtVendorStateInsert.Text = "";
                txtVendorZipInsert.Text = "";
                txtVendorPhoneInsert.Text = "";
                txtVendorFaxInsert.Text = "";
                
                lblNotificationEmailMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblNotificationEmailMessage.Text = "Clear form failed.  Please contact your system admistrator.";
            }
        }

        protected void lbManageEmailList_Click(object sender, EventArgs e)
        {
            pnlFormPopup.Visible = false;
            pnlEmailList.Visible = true;

        }


        public Int32 GetListID(TForm tForm)
        {
            if (!String.IsNullOrEmpty(tForm.FormID.ToString()))
            {
            }

            return 0;
        }

        public IEnumerable<TForm> SelectForms()
        {
            try
            {
                var FormsList = _ctx.TForms.Where(f => f.Status == 1).OrderBy(f => f.FormName).ToList();

                lblUserMessage.Text = "";

                return FormsList;
            }
            catch (Exception ex)
            {
                lblEmailMessage.Text = "Unable to load forms.  Please contact your system administrator.";
                return null;
            }
        }

        public IEnumerable<UserRoles> GetUserRoles()
        {
            try
            {
                var UserRoleList = _ctx.UserRoles.OrderBy(ur => ur.UserRoleId).ToList();

                lblUserMessage.Text = "";

                return UserRoleList;
            }
            catch (Exception ex)
            {
                lblUserMessage.Text = "Unable to load User Roles.  Please contact your system administrator.";
                return null;
            }
        }


        protected void ddlFormName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(ddlFormName.SelectedValue);

                if (id != 0)
                {
                    using (FormContext ctx = new FormContext())
                    {
                        if (ctx.TForms.Any(t => t.FormID == id))
                        {
                            var thisForm = ctx.TForms.FirstOrDefault(f => f.FormID == id);

                            lblFormId.Text = thisForm.FormID.ToString();
                            lblFormName.Text = thisForm.FormName;
                            lblFormCreator.Text = thisForm.FormCreator;
                            lblTimestamp.Text = thisForm.Timestamp.ToString();
                            lblNotes.Text = thisForm.Notes;

                            gvEmailList.DataBind();

                            switch (ddlTab.SelectedValue)
                            {
                                case "FormSummary":
                                    pnlFormPopup.Visible = true;
                                    pnlEmailList.Visible = false;
                                    break;
                                case "EmailList":
                                    pnlFormPopup.Visible = false;
                                    pnlEmailList.Visible = true;
                                    break;
                                default:
                                    pnlFormPopup.Visible = true;
                                    pnlEmailList.Visible = false;
                                    break;
                            }

                            lblUserMessage.Text = "";
                        }
                        else
                        {
                            lblUserMessage.Text = "Form Not Found.";
                        }
                    }
                }
                else
                {
                    pnlFormPopup.Visible = false;
                    pnlEmailList.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblEmailMessage.Text = "Unabled to load form summary.  Please contact your system administrator.";
            }

        }

        protected void ddlUserRoleEdit_DataBound(object sender, EventArgs e) { }

        protected void ddlVariable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVariable.SelectedValue == "E")
            {
                pnlExpediteCodes.Visible = true;
                pnlPOStatuses.Visible = false;
                pnlVendors.Visible = false;
            }
            else if (ddlVariable.SelectedValue == "P")
            {
                pnlExpediteCodes.Visible = false;
                pnlPOStatuses.Visible = true;
                pnlVendors.Visible = false;
            }
            else if (ddlVariable.SelectedValue == "V")
            {
                pnlExpediteCodes.Visible = false;
                pnlPOStatuses.Visible = false;
                pnlVendors.Visible = true;
            }
        }

        protected void ddlTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFormName.SelectedValue != "0")
            {
                switch (ddlTab.SelectedValue)
                {
                    case "FormSummary":
                        pnlFormPopup.Visible = true;
                        pnlEmailList.Visible = false;
                        break;
                    case "EmailList":
                        pnlFormPopup.Visible = false;
                        pnlEmailList.Visible = true;
                        break;
                    default:
                        pnlFormPopup.Visible = true;
                        pnlEmailList.Visible = false;
                        break;
                }
            }
        }


        protected void rblStatusEdit_DataBound(object sender, EventArgs e) { }

        protected void rblUserStatusEdit_DataBound(object sender, EventArgs e) { }

        protected void rblCodeStatusEdit_DataBound(object sender, EventArgs e) { }

        protected void rblNotificationEmailStatusEdit_DataBound(object sender, EventArgs e) { }



        public IQueryable<Tingle_WebForms.Models.ExpediteCode> gvExpediteCodes_GetData()
        {
            try
            {
                IQueryable<Tingle_WebForms.Models.ExpediteCode> expediteCodes = _ctx.ExpediteCodes.OrderBy(x => x.Code);

                return expediteCodes;
            }
            catch (Exception ex)
            {
                lblVariableMessage.Text = "Unable to load Expedite Codes.  Please contact your system administrator.";
                return null;
            }
        }

        public void gvExpediteCodes_DeleteItem(int ExpediteCodeID)
        {
            try
            {
                ExpediteCode ExpediteCodeToDelete = _ctx.ExpediteCodes.FirstOrDefault(x => x.ExpediteCodeID == ExpediteCodeID);

                if (ExpediteCodeToDelete != null)
                {
                    _ctx.ExpediteCodes.Remove(ExpediteCodeToDelete);
                    _ctx.SaveChanges();
                }

                lblVariableMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblVariableMessage.Text = "Unable to delete Email Address.  Please contact your system administrator.";
            }
        }

        public void gvExpediteCodes_UpdateItem(int ExpediteCodeID) { }

        protected void gvExpediteCodes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                GridViewRow gvr = (GridViewRow)gv.Rows[e.RowIndex];
                Label lblExpediteCodeID = (Label)gvr.FindControl("lblExpediteCodeIDEdit");
                int id = Convert.ToInt32(lblExpediteCodeID.Text);

                using (FormContext ctx = new FormContext())
                {
                    var expCode = ctx.ExpediteCodes.FirstOrDefault(x => x.ExpediteCodeID == id);

                    expCode.Code = ((TextBox)gvr.FindControl("txtCodeEdit")).Text;
                    expCode.Description = ((TextBox)gvr.FindControl("txtDescriptionEdit")).Text;
                    expCode.Status = Convert.ToInt16(((RadioButtonList)gvr.FindControl("rblCodeStatusEdit")).SelectedValue);

                    ctx.SaveChanges();
                    gvExpediteCodes.DataBind();

                    lblVariableMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblVariableMessage.Text = "Unable to update Expedite Code.  Please contact your system administrator.";
            }
        }

        protected void gvExpediteCodes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    RadioButtonList rblCodeStatusEdit = (RadioButtonList)e.Row.FindControl("rblCodeStatusEdit");
                    int recordId = Convert.ToInt32(((Label)e.Row.FindControl("lblExpediteCodeIDEdit")).Text);
                    ExpediteCode eCode = _ctx.ExpediteCodes.FirstOrDefault(x => x.ExpediteCodeID == recordId);

                    rblCodeStatusEdit.SelectedValue = eCode.Status.ToString();

                }
            }
        }

        public void fvExpediteCodes_InsertItem()
        {
            try
            {
                TextBox txtCodeInsert = (TextBox)fvExpediteCodes.FindControl("txtCodeInsert");
                TextBox txtDescriptionInsert = (TextBox)fvExpediteCodes.FindControl("txtDescriptionInsert");
                RadioButtonList rblCodeStatusInsert = (RadioButtonList)fvExpediteCodes.FindControl("rblCodeStatusInsert");
                Int16 statusId = Convert.ToInt16(rblCodeStatusInsert.SelectedValue);

                using (FormContext ctx = new FormContext())
                {
                    if (!ctx.ExpediteCodes.Any(x => x.Code == txtCodeInsert.Text))
                    {
                        ExpediteCode newCode = new ExpediteCode();

                        newCode.Timestamp = DateTime.Now;
                        newCode.Code = txtCodeInsert.Text;
                        newCode.Description = txtDescriptionInsert.Text;
                        newCode.Status = statusId;

                        ctx.ExpediteCodes.Add(newCode);

                        ctx.SaveChanges();
                        gvExpediteCodes.DataBind();

                        lblVariableMessage.Text = "";
                    }
                    else
                    {
                        lblVariableMessage.Text = "An Expedite Code named: " + txtCodeInsert.Text + " already exists.  Please try again.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblUserMessage.Text = "Unable to add this Expedite Code.  Please contact your system administrator.";
            }
        }



        public IQueryable<Tingle_WebForms.Models.NotificationEmailAddress> gvNotificationEmails_GetData()
        {
            try
            {
                IQueryable<Tingle_WebForms.Models.NotificationEmailAddress> notificationEmails = _ctx.NotificationEmailAddresses.OrderBy(x => x.Name);

                return notificationEmails;
            }
            catch (Exception ex)
            {
                lblNotificationEmailMessage.Text = "Unable to load Notification Emails.  Please contact your system administrator.";
                return null;
            }
        }

        public void gvNotificationEmails_DeleteItem(int RecordId)
        {
            try
            {
                NotificationEmailAddress EmailToDelete = _ctx.NotificationEmailAddresses.FirstOrDefault(x => x.RecordId == RecordId);

                if (EmailToDelete != null)
                {
                    _ctx.NotificationEmailAddresses.Remove(EmailToDelete);
                    _ctx.SaveChanges();
                }

                lblNotificationEmailMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblNotificationEmailMessage.Text = "Unable to delete Notification Email Address.  Please contact your system administrator.";
            }
        }

        public void gvNotificationEmails_UpdateItem(int RecordId) { }

        protected void gvNotificationEmails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                GridViewRow gvr = (GridViewRow)gv.Rows[e.RowIndex];
                Label lblRecorIdEdit = (Label)gvr.FindControl("lblRecorIdEdit");
                int id = Convert.ToInt32(lblRecorIdEdit.Text);

                using (FormContext ctx = new FormContext())
                {
                    var notiEmail = ctx.NotificationEmailAddresses.FirstOrDefault(x => x.RecordId == id);

                    notiEmail.Name = ((TextBox)gvr.FindControl("txtNameEdit")).Text;
                    notiEmail.Address = ((TextBox)gvr.FindControl("txtAddressEdit")).Text;
                    notiEmail.Status = Convert.ToInt16(((RadioButtonList)gvr.FindControl("rblNotificationEmailStatusEdit")).SelectedValue);

                    ctx.SaveChanges();
                    gvNotificationEmails.DataBind();

                    lblNotificationEmailMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblNotificationEmailMessage.Text = "Unable to update Notification Email Address.  Please contact your system administrator.";
            }
        }

        protected void gvNotificationEmails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    RadioButtonList rblNotificationEmailStatusEdit = (RadioButtonList)e.Row.FindControl("rblNotificationEmailStatusEdit");
                    int recordId = Convert.ToInt32(((Label)e.Row.FindControl("lblRecorIdEdit")).Text);
                    NotificationEmailAddress notiEmail = _ctx.NotificationEmailAddresses.FirstOrDefault(x => x.RecordId == recordId);

                    rblNotificationEmailStatusEdit.SelectedValue = notiEmail.Status.ToString();

                }
            }
        }





        public void fvNotificationEmails_InsertItem()
        {
            try
            {
                TextBox txtNameInsert = (TextBox)fvNotificationEmails.FindControl("txtNameInsert");
                TextBox txtAddressInsert = (TextBox)fvNotificationEmails.FindControl("txtAddressInsert");
                RadioButtonList rblNotificationEmailStatusInsert = (RadioButtonList)fvNotificationEmails.FindControl("rblNotificationEmailStatusInsert");
                Int16 statusId = Convert.ToInt16(rblNotificationEmailStatusInsert.SelectedValue);

                using (FormContext ctx = new FormContext())
                {
                    if (!ctx.NotificationEmailAddresses.Any(x => x.Name == txtNameInsert.Text || x.Address == txtAddressInsert.Text))
                    {
                        if (!ctx.SystemUsers.Any(x => x.EmailAddress == txtAddressInsert.Text))
                        {
                            NotificationEmailAddress newEmail = new NotificationEmailAddress();

                            newEmail.Timestamp = DateTime.Now;
                            newEmail.Name = txtNameInsert.Text;
                            newEmail.Address = txtAddressInsert.Text;
                            newEmail.Status = statusId;

                            ctx.NotificationEmailAddresses.Add(newEmail);

                            ctx.SaveChanges();
                            gvNotificationEmails.DataBind();

                            lblNotificationEmailMessage.Text = "";
                        }
                        else
                        {
                            lblNotificationEmailMessage.Text = "A System User already exists with this Email Address.  Please enter a unique Email Address.";
                        }
                    }
                    else
                    {
                        if (ctx.NotificationEmailAddresses.Any(x => x.Name == txtNameInsert.Text && x.Address == txtAddressInsert.Text))
                        {
                            lblNotificationEmailMessage.Text = "A Notification Email already exists with this Name and Email Address.  Please enter a unique Name and Email Address.";
                        }
                        else if (ctx.NotificationEmailAddresses.Any(x => x.Name == txtNameInsert.Text))
                        {
                            lblNotificationEmailMessage.Text = "A Notification Email already exists with this Name.  Please enter a unique Name.";
                        }
                        else if (ctx.NotificationEmailAddresses.Any(x => x.Address == txtAddressInsert.Text))
                        {
                            lblNotificationEmailMessage.Text = "A Notification Email already exists with this Email Address.  Please enter a unique Email Address.";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lblNotificationEmailMessage.Text = "Unable to add this Email Address.  Please contact your system administrator.";
            }
        }


        public IQueryable<Tingle_WebForms.Models.PurchaseOrderStatus> gvPOStatus_GetData()
        {
            try
            {
                IQueryable<Tingle_WebForms.Models.PurchaseOrderStatus> poStatuses = _ctx.POStatuses.OrderBy(x => x.Status);

                return poStatuses;
            }
            catch (Exception ex)
            {
                lblVariableMessage.Text = "Unable to load PO Statuses.  Please contact your system administrator.";
                return null;
            }
        }

        public void gvPOStatus_UpdateItem(int RecordId) { }

        public void gvPOStatus_DeleteItem(int RecordId)
        {
            try
            {
                PurchaseOrderStatus StatusToDelete = _ctx.POStatuses.FirstOrDefault(x => x.RecordId == RecordId);

                if (StatusToDelete != null)
                {
                    _ctx.POStatuses.Remove(StatusToDelete);
                    _ctx.SaveChanges();
                }

                lblVariableMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblVariableMessage.Text = "Unable to delete PO Status.  Please contact your system administrator.";
            }
        }

        protected void gvPOStatus_RowDataBound(object sender, GridViewRowEventArgs e) { }

        protected void gvPOStatus_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                GridViewRow gvr = (GridViewRow)gv.Rows[e.RowIndex];
                Label lblRecordId = (Label)gvr.FindControl("lblRecordIdEdit");
                int id = Convert.ToInt32(lblRecordId.Text);

                using (FormContext ctx = new FormContext())
                {
                    var status = ctx.POStatuses.FirstOrDefault(x => x.RecordId == id);

                    status.Status = ((TextBox)gvr.FindControl("lblStatusEdit")).Text;
                    status.StatusCode = ((TextBox)gvr.FindControl("lblStatusCodeEdit")).Text;

                    ctx.SaveChanges();
                    gvPOStatus.DataBind();

                    lblVariableMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblVariableMessage.Text = "Unable to update PO Status.  Please contact your system administrator.";
            }
        }

        public void fvPOStatus_InsertItem()
        {
            try
            {
                TextBox txtStatusInsert = (TextBox)fvPOStatus.FindControl("txtStatusInsert");
                TextBox txtStatusCodeInsert = (TextBox)fvPOStatus.FindControl("txtStatusCodeInsert");

                using (FormContext ctx = new FormContext())
                {
                    if (!ctx.Statuses.Any(x => x.StatusText == txtStatusInsert.Text))
                    {
                        PurchaseOrderStatus newStatus = new PurchaseOrderStatus();

                        newStatus.Status = txtStatusInsert.Text;
                        newStatus.StatusCode = txtStatusCodeInsert.Text;

                        ctx.POStatuses.Add(newStatus);

                        ctx.SaveChanges();
                        gvPOStatus.DataBind();

                        lblVariableMessage.Text = "";
                    }
                    else
                    {
                        lblVariableMessage.Text = "A PO Status named: " + txtStatusInsert.Text + " already exists.  Please try again.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblVariableMessage.Text = "Unable to add this PO Status.  Please contact your system administrator.";
            }
        }



        public IQueryable<Tingle_WebForms.Models.Vendor> gvVendors_GetData()
        {
            try
            {
                IQueryable<Tingle_WebForms.Models.Vendor> vendors = _ctx.Vendors.OrderBy(x => x.VendorName);

                return vendors;
            }
            catch (Exception ex)
            {
                lblVariableMessage.Text = "Unable to load Vendors.  Please contact your system administrator.";
                return null;
            }
        }

        public void gvVendors_UpdateItem(int RecordId) { }

        public void gvVendors_DeleteItem(int RecordId)
        {
            try
            {
                Vendor VendorToDelete = _ctx.Vendors.FirstOrDefault(x => x.RecordId == RecordId);

                if (VendorToDelete != null)
                {
                    _ctx.Vendors.Remove(VendorToDelete);
                    _ctx.SaveChanges();
                }

                lblVariableMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblVariableMessage.Text = "Unable to delete Vendor.  Please contact your system administrator.";
            }
        }

        protected void gvVendors_RowDataBound(object sender, GridViewRowEventArgs e) { }

        protected void gvVendors_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                GridViewRow gvr = (GridViewRow)gv.Rows[e.RowIndex];
                Label lblRecordId = (Label)gvr.FindControl("lblRecordIdEdit");
                int id = Convert.ToInt32(lblRecordId.Text);

                using (FormContext ctx = new FormContext())
                {
                    var vendor = ctx.Vendors.FirstOrDefault(x => x.RecordId == id);

                    vendor.VendorName = ((TextBox)gvr.FindControl("lblVendorNameEdit")).Text;
                    vendor.VendorPhone = ((TextBox)gvr.FindControl("lblVendorPhoneEdit")).Text;
                    vendor.VendorFax = ((TextBox)gvr.FindControl("lblVendorFaxEdit")).Text;

                    ctx.SaveChanges();
                    gvVendors.DataBind();

                    lblVariableMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblVariableMessage.Text = "Unable to update Vendor.  Please contact your system administrator.";
            }
        }

        public void fvVendors_InsertItem()
        {
            try
            {
                TextBox txtVendorNameInsert = (TextBox)fvVendors.FindControl("txtVendorNameInsert");
                TextBox txtVendorPhoneInsert = (TextBox)fvVendors.FindControl("txtVendorPhoneInsert");
                TextBox txtVendorFaxInsert = (TextBox)fvVendors.FindControl("txtVendorFaxInsert");

                using (FormContext ctx = new FormContext())
                {
                    if (!ctx.Vendors.Any(x => x.VendorName == txtVendorNameInsert.Text.Trim()))
                    {
                        Vendor newVendor = new Vendor();

                        newVendor.VendorName = txtVendorNameInsert.Text;
                        newVendor.VendorPhone = txtVendorPhoneInsert.Text;
                        newVendor.VendorPhone = txtVendorFaxInsert.Text;
                        newVendor.CurrentStock = 0;
                        newVendor.Timestamp = DateTime.Now;

                        ctx.Vendors.Add(newVendor);

                        ctx.SaveChanges();
                        gvVendors.DataBind();

                        lblVariableMessage.Text = "";
                    }
                    else
                    {
                        lblVariableMessage.Text = "A Vendor named: " + txtVendorNameInsert.Text + " already exists.  Please try again.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblVariableMessage.Text = "Unable to add this Vendor.  Please contact your system administrator.";
            }
        }


        public bool UpdateFormPermissions(string formName, string roleName, bool status)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    if (ctx.FormPermissions.Any(x => x.FormName == formName && x.UserRole.RoleName == roleName))
                    {
                        var formPermission = ctx.FormPermissions.First(x => x.FormName == formName && x.UserRole.RoleName == roleName);
                        formPermission.Enabled = status;
                    }
                    else
                    {
                        FormPermissions formPermission = new FormPermissions();
                        formPermission.FormName = formName;
                        formPermission.UserRole = ctx.UserRoles.First(x => x.RoleName == roleName);
                        formPermission.Enabled = status;

                        ctx.FormPermissions.Add(formPermission);
                    }

                    ctx.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public void LoadFormPermissions()
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    var formPermissions = ctx.FormPermissions;

                    foreach (var formPermission in formPermissions)
                    {
                        if (formPermission.FormName == "Order Cancellation")
                        {
                            if (formPermission.UserRole.RoleName == "User")
                            {
                                cbOrderCancellationUser.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "ReportsUser")
                            {
                                cbOrderCancellationReportsUser.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "ReportsAdmin")
                            {
                                cbOrderCancellationReportsAdmin.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "SuperUser")
                            {
                                cbOrderCancellationSuperUser.Checked = formPermission.Enabled;
                            }
                        }
                        else if (formPermission.FormName == "Expedited Order")
                        {
                            if (formPermission.UserRole.RoleName == "User")
                            {
                                cbExpeditedOrderUser.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "ReportsUser")
                            {
                                cbExpeditedOrderReportsUser.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "ReportsAdmin")
                            {
                                cbExpeditedOrderReportsAdmin.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "SuperUser")
                            {
                                cbExpeditedOrderSuperUser.Checked = formPermission.Enabled;
                            }
                        }
                        else if (formPermission.FormName == "Price Change Request")
                        {
                            if (formPermission.UserRole.RoleName == "User")
                            {
                                cbPriceChangeRequestUser.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "ReportsUser")
                            {
                                cbPriceChangeRequestReportsUser.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "ReportsAdmin")
                            {
                                cbPriceChangeRequestReportsAdmin.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "SuperUser")
                            {
                                cbPriceChangeRequestSuperUser.Checked = formPermission.Enabled;
                            }
                        }
                        else if (formPermission.FormName == "Hot Rush")
                        {
                            if (formPermission.UserRole.RoleName == "User")
                            {
                                cbHotRushUser.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "ReportsUser")
                            {
                                cbHotRushReportsUser.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "ReportsAdmin")
                            {
                                cbHotRushReportsAdmin.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "SuperUser")
                            {
                                cbHotRushSuperUser.Checked = formPermission.Enabled;
                            }
                        }
                        else if (formPermission.FormName == "Low Inventory")
                        {
                            if (formPermission.UserRole.RoleName == "User")
                            {
                                cbLowInventoryUser.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "ReportsUser")
                            {
                                cbLowInventoryReportsUser.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "ReportsAdmin")
                            {
                                cbLowInventoryReportsAdmin.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "SuperUser")
                            {
                                cbLowInventorySuperUser.Checked = formPermission.Enabled;
                            }
                        }
                        else if (formPermission.FormName == "Sample Request")
                        {
                            if (formPermission.UserRole.RoleName == "User")
                            {
                                cbSampleRequestUser.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "ReportsUser")
                            {
                                cbSampleRequestReportsUser.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "ReportsAdmin")
                            {
                                cbSampleRequestReportsAdmin.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "SuperUser")
                            {
                                cbSampleRequestSuperUser.Checked = formPermission.Enabled;
                            }
                        }
                        else if (formPermission.FormName == "Direct Order")
                        {
                            if (formPermission.UserRole.RoleName == "User")
                            {
                                cbDirectOrderUser.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "ReportsUser")
                            {
                                cbDirectOrderReportsUser.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "ReportsAdmin")
                            {
                                cbDirectOrderReportsAdmin.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "SuperUser")
                            {
                                cbDirectOrderSuperUser.Checked = formPermission.Enabled;
                            }
                        }
                        else if (formPermission.FormName == "Request For Check")
                        {
                            if (formPermission.UserRole.RoleName == "User")
                            {
                                cbRequestForCheckUser.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "ReportsUser")
                            {
                                cbRequestForCheckReportsUser.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "ReportsAdmin")
                            {
                                cbRequestForCheckReportsAdmin.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "SuperUser")
                            {
                                cbRequestForCheckSuperUser.Checked = formPermission.Enabled;
                            }
                        }
                        else if (formPermission.FormName == "Must Include")
                        {
                            if (formPermission.UserRole.RoleName == "User")
                            {
                                cbMustIncludeUser.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "ReportsUser")
                            {
                                cbMustIncludeReportsUser.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "ReportsAdmin")
                            {
                                cbMustIncludeReportsAdmin.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "SuperUser")
                            {
                                cbMustIncludeSuperUser.Checked = formPermission.Enabled;
                            }
                        }
                        else if (formPermission.FormName == "Cannot Wait For Container")
                        {
                            if (formPermission.UserRole.RoleName == "User")
                            {
                                cbCannotWaitForContainerUser.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "ReportsUser")
                            {
                                cbCannotWaitForContainerReportsUser.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "ReportsAdmin")
                            {
                                cbCannotWaitForContainerReportsAdmin.Checked = formPermission.Enabled;
                            }
                            else if (formPermission.UserRole.RoleName == "SuperUser")
                            {
                                cbCannotWaitForContainerSuperUser.Checked = formPermission.Enabled;
                            }
                        }
                    }
                }
            }
            catch
            {
                lblFormPermissions.Text = "Unable to Load initial Form Permissions.  Before making any Form Permission changes, please contact your System Administrator.";
            }

        }






        protected void cbOrderCancellationUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Order Cancellation", "User", cbOrderCancellationUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }

        }

        protected void cbOrderCancellationReportsUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Order Cancellation", "ReportsUser", cbOrderCancellationReportsUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbOrderCancellationReportsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Order Cancellation", "ReportsAdmin", cbOrderCancellationReportsAdmin.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbOrderCancellationSuperUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Order Cancellation", "SuperUser", cbOrderCancellationSuperUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbExpeditedOrderUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Expedited Order", "User", cbExpeditedOrderUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbExpeditedOrderReportsUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Expedited Order", "ReportsUser", cbExpeditedOrderReportsUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbExpeditedOrderReportsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Expedited Order", "ReportsAdmin", cbExpeditedOrderReportsAdmin.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbExpeditedOrderSuperUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Expedited Order", "SuperUser", cbExpeditedOrderSuperUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbPriceChangeRequestUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Price Change Request", "User", cbPriceChangeRequestUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbPriceChangeRequestReportsUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Price Change Request", "ReportsUser", cbPriceChangeRequestReportsUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbPriceChangeRequestReportsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Price Change Request", "ReportsAdmin", cbPriceChangeRequestReportsAdmin.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbPriceChangeRequestSuperUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Price Change Request", "SuperUser", cbPriceChangeRequestSuperUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbHotRushUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Hot Rush", "User", cbHotRushUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbHotRushReportsUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Hot Rush", "ReportsUser", cbHotRushReportsUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbHotRushReportsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Hot Rush", "ReportsAdmin", cbHotRushReportsAdmin.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbHotRushSuperUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Hot Rush", "SuperUser", cbHotRushSuperUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbLowInventoryUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Low Inventory", "User", cbLowInventoryUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbLowInventoryReportsUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Low Inventory", "ReportsUser", cbLowInventoryReportsUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbLowInventoryReportsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Low Inventory", "ReportsAdmin", cbLowInventoryReportsAdmin.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbLowInventorySuperUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Low Inventory", "SuperUser", cbLowInventorySuperUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbSampleRequestUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Sample Request", "User", cbSampleRequestUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbSampleRequestReportsUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Sample Request", "ReportsUser", cbSampleRequestReportsUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbSampleRequestReportsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Sample Request", "ReportsAdmin", cbSampleRequestReportsAdmin.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbSampleRequestSuperUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Sample Request", "SuperUser", cbSampleRequestSuperUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbDirectOrderUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Direct Order", "User", cbDirectOrderUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbDirectOrderReportsUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Direct Order", "ReportsUser", cbDirectOrderReportsUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbDirectOrderReportsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Direct Order", "ReportsAdmin", cbDirectOrderReportsAdmin.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbDirectOrderSuperUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Direct Order", "SuperUser", cbDirectOrderSuperUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbRequestForCheckUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Request For Check", "User", cbRequestForCheckUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbRequestForCheckReportsUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Request For Check", "ReportsUser", cbRequestForCheckReportsUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbRequestForCheckReportsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Request For Check", "ReportsAdmin", cbRequestForCheckReportsAdmin.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbRequestForCheckSuperUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Request For Check", "SuperUser", cbRequestForCheckSuperUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbMustIncludeUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Must Include", "User", cbMustIncludeUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbMustIncludeReportsUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Must Include", "ReportsUser", cbMustIncludeReportsUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbMustIncludeReportsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Must Include", "ReportsAdmin", cbMustIncludeReportsAdmin.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbMustIncludeSuperUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Must Include", "SuperUser", cbRequestForCheckSuperUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbCannotWaitForContainerUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Cannot Wait For Container", "User", cbCannotWaitForContainerUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbCannotWaitForContainerReportsUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Cannot Wait For Container", "ReportsUser", cbCannotWaitForContainerReportsUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbCannotWaitForContainerReportsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Cannot Wait For Container", "ReportsAdmin", cbCannotWaitForContainerReportsAdmin.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }

        protected void cbCannotWaitForContainerSuperUser_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!UpdateFormPermissions("Cannot Wait For Container", "SuperUser", cbCannotWaitForContainerSuperUser.Checked))
                {
                    lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
                }
            }
            catch (Exception ex)
            {
                lblFormPermissions.Text = "Unable to update Form Permissions.  Please contact your system administrator.";
            }
        }




        public IQueryable<Tingle_WebForms.Models.NotificationEmailAddress> gridInventoryNotificationList_GetData()
        {
            try
            {
                IQueryable<Tingle_WebForms.Models.NotificationEmailAddress> emails = _ctx.NotificationEmailAddresses;

                return emails;
            }
            catch (Exception ex)
            {
                lblUserMessage.Text = "Unable to load User List.  Please contact your system administrator.";
                return null;
            }
        }

        protected void gridInventoryNotificationList_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                var deleteItem = ((GridDataItem)e.Item);

                using (FormContext ctx = new FormContext())
                {
                    int recordId = Convert.ToInt32(deleteItem.GetDataKeyValue("RecordId"));
                    InventoryNotificationEmails email = ctx.InventoryNotificationEmailAddresses.FirstOrDefault(x => x.RecordId == recordId);

                    ctx.InventoryNotificationEmailAddresses.Remove(email);
                    ctx.SaveChanges();

                }

                gridInventoryNotificationList.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gridInventoryNotificationList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GetInventoryNotificationList();
        }

        protected void gridInventoryNotificationList_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                var editableItem = ((GridEditableItem)e.Item);

                using (FormContext ctx = new FormContext())
                {
                    int recordId = Convert.ToInt32(editableItem.GetDataKeyValue("RecordId"));
                    InventoryNotificationEmails email = ctx.InventoryNotificationEmailAddresses.FirstOrDefault(x => x.RecordId == recordId);

                    if (email != null)
                    {
                        email.Name = ((RadTextBox)editableItem.FindControl("txtNameEdit")).Text;
                        email.Address = ((RadTextBox)editableItem.FindControl("txtAddressEdit")).Text;
                        email.Status = Convert.ToInt16(((RadioButtonList)editableItem.FindControl("rblStatusEdit")).SelectedValue);

                        ctx.SaveChanges();
                    }

                    gridInventoryNotificationList.Rebind();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gridInventoryNotificationList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
           
        }

        public void gridInventoryNotificationList_Insert(){}
        public void gridInventoryNotificationList_Update(){}
        public void gridInventoryNotificationList_Delete() { }

        public IQueryable<Tingle_WebForms.Models.InventoryNotificationEmails> GetInventoryNotificationList()
        {
            try
            {
                IQueryable<InventoryNotificationEmails> emails = _ctx.InventoryNotificationEmailAddresses.OrderBy(x => x.Status);

                if (emails != null)
                {
                    return emails;
                }
                
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        protected void gridInventoryNotificationList_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                var insertItem = ((GridDataInsertItem)e.Item);


                using (FormContext ctx = new FormContext())
                {
                    InventoryNotificationEmails email = new InventoryNotificationEmails();

                    email.Name = ((RadTextBox)insertItem.FindControl("txtNameInsert")).Text;
                    email.Address = ((RadTextBox)insertItem.FindControl("txtAddressInsert")).Text;
                    email.Status = 1;

                    ctx.InventoryNotificationEmailAddresses.Add(email);
                    ctx.SaveChanges();

                    gridInventoryNotificationList.Rebind();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rblStatusEdit_DataBound1(object sender, EventArgs e)
        {
            try
            {
                GridEditableItem editedItem = (sender as RadioButtonList).NamingContainer as GridEditableItem;
                RadioButtonList rblStatusEdit = sender as RadioButtonList;
                int formId = Convert.ToInt32(editedItem.GetDataKeyValue("RecordId").ToString());

                using (FormContext ctx = new FormContext())
                {
                    InventoryNotificationEmails email = ctx.InventoryNotificationEmailAddresses.FirstOrDefault(x => x.RecordId == formId);

                    if (email != null)
                    {
                        rblStatusEdit.SelectedValue = email.Status.ToString();

                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}