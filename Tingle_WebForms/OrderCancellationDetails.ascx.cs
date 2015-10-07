using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
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
using System.Reflection;

namespace Tingle_WebForms
{
    public partial class OrderCancellationDetails : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rptrComments.DataBind();

            if (Session["NewComment"] != null && Session["NewComment"].ToString() == "true")
            {
                txtNewComment.Focus();
            }
        }

        protected void ddlCompanyEdit_DataBinding(object sender, EventArgs e)
        {
            try
            {
                int recordId;
                Int32.TryParse(lblRecordId.Text, out recordId);

                using (var ctx = new FormContext())
                {
                    var thisForm = ctx.OrderCancellationForms.FirstOrDefault(eof => eof.RecordId == recordId);

                    ddlCompanyEdit.SelectedValue = thisForm.Company;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void ddlPOStatusEdit_DataBound(object sender, EventArgs e)
        {
            try
            {
                int recordId;
                Int32.TryParse(hRecordId.Value, out recordId);

                using (var ctx = new FormContext())
                {
                    var thisForm = ctx.OrderCancellationForms.FirstOrDefault(eof => eof.RecordId == recordId);

                    string poListString = thisForm.POStatusList.Replace(" ", "");
                    List<string> poList = poListString.Split(',').ToList<string>();

                    foreach (string poCode in poList)
                    {
                        if (ddlPOStatusEdit.Items.Any(x => x.Text == poCode))
                        {
                            ddlPOStatusEdit.Items.FirstOrDefault(x => x.Text == poCode).Checked = true;
                        }
                    }

                    if (poListString != "")
                    {
                        ddlPOStatusEdit.Text = poListString;
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void ddlCompanyEdit_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            FillEmailAddressLabels();
        }

        protected void ddlRequestedByEdit_DataBound(object sender, EventArgs e)
        {
            try
            {
                int recordId;
                Int32.TryParse(lblRecordId.Text, out recordId);

                using (var ctx = new FormContext())
                {
                    var thisForm = ctx.OrderCancellationForms.FirstOrDefault(eof => eof.RecordId == recordId);

                    ddlRequestedByEdit.SelectedValue = thisForm.RequestedUser.SystemUserID.ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void ddlRequestedByEdit_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            FillEmailAddressLabels();
        }

        protected void ddlAssignedToEdit_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            FillEmailAddressLabels();
        }

        protected void ddlNotifyOtherEdit_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            FillEmailAddressLabels();
        }

        protected void cvRequestedBy_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ddlRequestedByEdit.FindItemByText(ddlRequestedByEdit.Text) != null)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void ddlStatusEdit_DataBound(object sender, EventArgs e)
        {
            try
            {
                int recordId;
                Int32.TryParse(lblRecordId.Text, out recordId);

                using (var ctx = new FormContext())
                {
                    var thisForm = ctx.OrderCancellationForms.FirstOrDefault(eof => eof.RecordId == recordId);

                    ddlStatusEdit.SelectedValue = thisForm.Status.StatusId.ToString();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        protected void ddlAssignedToEdit_DataBound(object sender, EventArgs e)
        {
            try
            {
                int recordId;
                Int32.TryParse(lblRecordId.Text, out recordId);

                using (var ctx = new FormContext())
                {
                    var thisForm = ctx.OrderCancellationForms.FirstOrDefault(eof => eof.RecordId == recordId);

                    if (thisForm.AssignedUser != null)
                    {
                        ddlAssignedToEdit.SelectedValue = thisForm.AssignedUser.SystemUserID.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void cvAssignedTo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ddlAssignedToEdit.FindItemByText(ddlAssignedToEdit.Text) != null)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void ddlPriorityEdit_DataBound(object sender, EventArgs e)
        {
            try
            {
                int recordId;
                Int32.TryParse(lblRecordId.Text, out recordId);

                using (var ctx = new FormContext())
                {
                    var thisForm = ctx.OrderCancellationForms.FirstOrDefault(eof => eof.RecordId == recordId);

                    ddlPriorityEdit.SelectedValue = thisForm.Priority.RecordId.ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void cbNotifyStandard_CheckedChanged(object sender, EventArgs e)
        {
            FillEmailAddressLabels();
        }

        protected void cbNotifyAssignee_CheckedChanged(object sender, EventArgs e)
        {
            FillEmailAddressLabels();
        }

        protected void cbNotifyOther_CheckedChanged(object sender, EventArgs e)
        {
            FillEmailAddressLabels();
        }

        protected void ddlNotifyOther_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            FillEmailAddressLabels();
        }

        protected void ddlNotifyOther_ItemChecked(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            FillEmailAddressLabels();
        }

        protected void ddlNotifyOther_CheckAllCheck(object sender, Telerik.Web.UI.RadComboBoxCheckAllCheckEventArgs e)
        {
            FillEmailAddressLabels();

        }

        protected void cbNotifyRequester_CheckedChanged(object sender, EventArgs e)
        {
            FillEmailAddressLabels();
        }

        protected void btnInsertEmail_Click(object sender, EventArgs e)
        {
            try
            {
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
                            newEmail.Status = Convert.ToInt16(rblNotificationEmailStatusInsert.SelectedValue);

                            ctx.NotificationEmailAddresses.Add(newEmail);

                            ctx.SaveChanges();

                            lblInsertEmailMessage.Text = "";
                            txtAddressInsert.Text = "";
                            txtNameInsert.Text = "";
                            rblNotificationEmailStatusInsert.SelectedIndex = 0;
                            ddlNotifyOther.DataBind();
                        }
                        else
                        {
                            lblInsertEmailMessage.Text = "A System User already exists with this Email Address.  Please enter a unique Email Address.";
                        }
                    }
                    else
                    {
                        if (ctx.NotificationEmailAddresses.Any(x => x.Name == txtNameInsert.Text && x.Address == txtAddressInsert.Text))
                        {
                            lblInsertEmailMessage.Text = "A Notification Email already exists with this Name and Email Address.  Please enter a unique Name and Email Address.";
                        }
                        else if (ctx.NotificationEmailAddresses.Any(x => x.Name == txtNameInsert.Text))
                        {
                            lblInsertEmailMessage.Text = "A Notification Email already exists with this Name.  Please enter a unique Name.";
                        }
                        else if (ctx.NotificationEmailAddresses.Any(x => x.Address == txtAddressInsert.Text))
                        {
                            lblInsertEmailMessage.Text = "A Notification Email already exists with this Email Address.  Please enter a unique Email Address.";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lblInsertEmailMessage.Text = "Unable to add this Email Address.  Please contact your system administrator.";
            }
        }

        protected void btnAddComment_Click(object sender, EventArgs e)
        {
            try
            {
                int recordId;
                Int32.TryParse(lblRecordId.Text, out recordId);

                UserLogic newLogic = new UserLogic();

                System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                SystemUsers currentUser = newLogic.GetCurrentUser(user);

                using (var ctx = new FormContext())
                {
                    var thisForm = ctx.OrderCancellationForms.FirstOrDefault(eof => eof.RecordId == recordId);

                    Comments newComment = new Comments
                    {
                        Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Cannot Wait For Container"),
                        Note = txtNewComment.Text,
                        RelatedFormId = thisForm.RecordId,
                        SystemComment = false,
                        Timestamp = DateTime.Now,
                        User = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == currentUser.SystemUserID)
                    };

                    ctx.Comments.Add(newComment);
                    ctx.SaveChanges();

                    txtNewComment.Text = "";
                    txtNewComment.Invalid = false;

                    rptrComments.DataBind();
                    Session["NewComment"] = "true";
                    
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void cbShowSystemComments_CheckedChanged(object sender, EventArgs e)
        {
            rptrComments.DataBind();
        }

        public string LoadCommentLiteral(Boolean SystemComment, String Note, String DisplayName, String Timestamp)
        {
            try
            {
                if (SystemComment == false)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("<div style=\"width: 80%; max-width: 800px; background-color: #bc4445; margin: 0 auto; text-align: left; font-size: 14px; color: white; border:1px black inset; border-radius:5px; overflow: hidden; word-wrap: break-word;\">")
                        .AppendLine("<div style=\"width: 95%; padding: 5px;\">")
                        .Append(Note).AppendLine("<br /><br /><span style=\"padding-right: 20px\">by")
                        .Append(DisplayName).Append("</span>").Append(Timestamp).AppendLine("</div></div><br />");

                    return sb.ToString();
                }
                else
                {
                    StringBuilder sb = new StringBuilder();

                    if (cbShowSystemComments.Checked)
                    {
                        sb.AppendLine("<div style=\"width: 80%; max-width: 800px; background-color: #FFF; margin: 0 auto; text-align: right; font-size: 12px; color: #bc4445; border:1px black inset; border-radius:5px; overflow: hidden; word-wrap: break-word;\">")
                            .AppendLine("<div style=\"width: 95%; padding: 5px; \">")
                            .Append(Note).AppendLine("<br /><br />")
                            .Append(Timestamp).AppendLine("</div></div><br />");
                    }
                    else
                    {
                        sb.AppendLine("");
                    }

                    return sb.ToString();
                }
            }
            catch
            {
                return "";
            }

        }

        public IEnumerable<PurchaseOrderStatus> GetPOStatuses()
        {
            using (FormContext ctx = new FormContext())
            {
                var StatusList = ctx.POStatuses.ToList();

                return StatusList;
            }
        }

        public IEnumerable<SystemUsers> GetUsers()
        {
            using (FormContext ctx = new FormContext())
            {
                if (ctx.SystemUsers.Any(x => x.Status == 1))
                {
                    var userList = ctx.SystemUsers.Where(x => x.Status == 1).ToList();

                    return userList.OrderBy(x => x.DisplayName);
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable<Status> GetStatusesEdit()
        {
            using (FormContext ctx = new FormContext())
            {
                var StatusList = ctx.Statuses.ToList();

                return StatusList;
            }
        }

        public IEnumerable<Priority> GetPriorities()
        {
            using (FormContext ctx = new FormContext())
            {
                var priList = ctx.Priorities.ToList();

                return priList;
            }

        }

        public IEnumerable<NotifyOtherList> GetOtherEmails()
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    var userList = from s in ctx.SystemUsers
                                   select new NotifyOtherList { Address = s.EmailAddress, Name = s.DisplayName };

                    var otherList = from n in ctx.NotificationEmailAddresses
                                    select new NotifyOtherList { Address = n.Address, Name = n.Name };

                    return userList.Union(otherList).ToList().OrderBy(x => x.Name);

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void FillEmailAddressLabels()
        {
            lblNotifyAssigneeValue.Text = ddlAssignedToEdit.SelectedIndex != -1 ? ddlAssignedToEdit.SelectedItem.Text : "";
            lblNotifyRequesterValue.Text = ddlRequestedByEdit.SelectedIndex != -1 ? ddlRequestedByEdit.SelectedItem.Text : "";
            lblNotifyStandardValue.Text = ddlCompanyEdit.SelectedText;

            List<String> listEmails = new List<String>();

            if (cbNotifyStandard.Checked)
            {
                using (FormContext ctx = new FormContext())
                {
                    if (ctx.EmailAddresses.Any(x => x.Status == 1 && x.TForm.FormName == "Order Cancellation" && x.Company == ddlCompanyEdit.SelectedText))
                    {
                        ICollection<EmailAddress> emailAddresses = ctx.EmailAddresses.Where(x => x.Status == 1 && x.TForm.FormName == "Order Cancellation" && x.Company == ddlCompanyEdit.SelectedText).ToList();

                        if (emailAddresses.Count() > 0)
                        {
                            foreach (EmailAddress email in emailAddresses)
                            {
                                listEmails.Add(email.Address);
                            }
                        }
                    }
                }
            }

            if (cbNotifyAssignee.Checked)
            {
                if (lblNotifyAssigneeValue.Text != "")
                {
                    listEmails.Add(lblNotifyAssigneeValue.Text);
                }
            }

            if (cbNotifyRequester.Checked)
            {
                if (lblNotifyRequesterValue.Text != "")
                {
                    listEmails.Add(lblNotifyRequesterValue.Text);
                }
            }

            if (cbNotifyOther.Checked)
            {
                var notifyOtherList = ddlNotifyOther.CheckedItems;

                if (notifyOtherList.Any())
                {
                    foreach (var item in notifyOtherList)
                    {
                        if (item.Text != null)
                        {
                            listEmails.Add(item.Text);
                        }
                    }
                }

            }


            string emailList = "";

            foreach (string email in listEmails)
            {
                emailList += email + ", ";
            }


            if (emailList.Length >= 2)
            {
                if (emailList.Substring(emailList.Length - 2, 2) == ", ")
                {
                    emailList = emailList.Substring(0, emailList.Length - 2);
                }
            }


            lblEmailsSentTo.Text = emailList;
        }

        public IEnumerable<Comments> rptrComments_GetData()
        {
            try
            {
                int recordId;
                Int32.TryParse(lblRecordId.Text, out recordId);

                using (FormContext ctx = new FormContext())
                {
                    if (ctx.Comments.Any(x => x.Form.FormName == "Order Cancellation" && x.RelatedFormId == recordId))
                    {
                        IEnumerable<Comments> commentsList = ctx.Comments
                            .Where(x => x.Form.FormName == "Order Cancellation" && x.RelatedFormId == recordId)
                            .Include(x => x.User)
                            .OrderByDescending(x => x.RecordId)
                            .ToList();

                        return commentsList;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DateTime? GetDueByDateSelectedDate(object dbDate)
        {
            if (dbDate != null)
            {
                return (DateTime?)dbDate;
            }

            return null;
        }
    }

}