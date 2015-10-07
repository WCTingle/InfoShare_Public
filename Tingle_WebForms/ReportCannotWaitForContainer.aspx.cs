using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
    public partial class ReportCannotWaitForContainer : System.Web.UI.Page
    {
        FormContext ctx = new FormContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            UserLogic newLogic = new UserLogic();

            System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            SystemUsers currentUser = newLogic.GetCurrentUser(user);

            if (newLogic.HasAccess(currentUser, "Cannot Wait For Container"))
            {
                if (Page.IsPostBack && gvReport.EditItems.Count == 0)
                {
                    gvReport.Rebind();
                }
                else
                {
                    if (Request.QueryString["formId"] != null)
                    {
                        ddlCompany.SelectedIndex = 0;
                        ddlGlobalStatus.SelectedIndex = -1;
                    }
                    else
                    {
                        if (Session["Company"] != null)
                        {
                            if (Session["Company"].ToString() == "Tingle")
                            {
                                ddlCompany.SelectedValue = "Tingle";
                            }
                            else if (Session["Company"].ToString() == "Summit")
                            {
                                ddlCompany.SelectedValue = "Summit";
                            }
                            else if (Session["Company"].ToString() == "Any")
                            {
                                ddlCompany.SelectedIndex = 0;
                            }
                        }

                        if (Session["GlobalStatus"] != null)
                        {
                            if (Session["GlobalStatus"].ToString() == "Active")
                            {
                                ddlGlobalStatus.SelectedValue = "Active";
                            }
                            else if (Session["GlobalStatus"].ToString() == "Archive")
                            {
                                ddlGlobalStatus.SelectedValue = "Archive";
                            }
                            else if (Session["GlobalStatus"].ToString() == "All")
                            {
                                ddlGlobalStatus.SelectedValue = "All";
                            }
                        }

                        if (Session["MyForms"] != null)
                        {
                            if (Session["MyForms"].ToString() == "Assigned")
                            {
                                ddlAssignedTo.SelectedValue = currentUser.SystemUserID.ToString();
                            }
                            else if (Session["MyForms"].ToString() == "Requested")
                            {
                                ddlRequestedBy.SelectedValue = currentUser.SystemUserID.ToString();
                            }
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("/Default");
            }
        }

        public Tingle_WebForms.Models.CannotWaitForContainerForm GetFormDetails([Control("gvReport")] int? RecordId, [QueryString("formId")] Nullable<Int32> formId)
        {
            var myForm = ctx.CannotWaitForContainerForms.FirstOrDefault();

            if (RecordId == null)
            {
                if (formId != null)
                {
                    myForm = ctx.CannotWaitForContainerForms.FirstOrDefault(f => f.RecordId == formId);
                }
            }
            else
            {
                myForm = ctx.CannotWaitForContainerForms.FirstOrDefault(f => f.RecordId == RecordId);
            }

            return myForm;
        }

        public IEnumerable<Status> GetStatuses()
        {
            using (FormContext ctx = new FormContext())
            {
                var StatusList = ctx.Statuses.ToList();

                return StatusList;
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

        public IEnumerable<Priority> GetPriorities()
        {
            using (FormContext ctx = new FormContext())
            {
                var priList = ctx.Priorities.ToList();

                return priList;
            }

        }

        public IEnumerable<Plant> GetPlants()
        {
            using (FormContext ctx = new FormContext())
            {
                var plantList = ctx.Plants.ToList();

                return plantList;
            }

        }

        public void FillEmailAddressLabels(GridEditableItem editItem)
        {
            UserControl userControl = (UserControl)editItem.FindControl(GridEditFormItem.EditFormUserControlID);


            Label lblNotifyAssigneeValue = (Label)userControl.FindControl("lblNotifyAssigneeValue");
            Label lblNotifyRequesterValue = (Label)userControl.FindControl("lblNotifyRequesterValue");
            Label lblNotifyStandardValue = (Label)userControl.FindControl("lblNotifyStandardValue");
            Label lblEmailsSentTo = (Label)userControl.FindControl("lblEmailsSentTo");
            RadComboBox ddlAssignedToEdit = (RadComboBox)userControl.FindControl("ddlAssignedToEdit");
            RadComboBox ddlRequestedByEdit = (RadComboBox)userControl.FindControl("ddlRequestedByEdit");
            RadDropDownList ddlCompanyEdit = (RadDropDownList)userControl.FindControl("ddlCompanyEdit");
            RadComboBox ddlNotifyOther = (RadComboBox)userControl.FindControl("ddlNotifyOther");
            CheckBox cbNotifyStandard = (CheckBox)userControl.FindControl("cbNotifyStandard");
            CheckBox cbNotifyAssignee = (CheckBox)userControl.FindControl("cbNotifyAssignee");
            CheckBox cbNotifyRequester = (CheckBox)userControl.FindControl("cbNotifyRequester");
            CheckBox cbNotifyOther = (CheckBox)userControl.FindControl("cbNotifyOther");

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

        public IQueryable<Tingle_WebForms.Models.CannotWaitForContainerForm> GetReportData(
            [Control("txtFromDate")] string FromDate,
            [Control("txtToDate")] string ToDate,
            [Control("txtDueByDateFrom")] string DueByFrom,
            [Control("txtDueByDateTo")] string DueByTo,
            [Control("ddlCompany")] string Company,
            [Control("txtOrderNumber")] string OrderNumber,
            [Control("ddlPlants")] Nullable<Int32> PlantId,
            [Control("txtLine")] string Line,
            [Control("txtQuantity")] string Quantity,
            [Control("txtSKU")] string SKU,
            [Control("ddlStatus")] Nullable<Int32> StatusId,
            [Control("ddlGlobalStatus")] string GlobalStatus,
            [Control("ddlRequestedBy")] Nullable<Int32> RequestedById,
            [Control("ddlAssignedTo")] Nullable<Int32> AssignedToId,
            [Control("ddlPriority")] Nullable<Int32> PriorityId,
            [QueryString("formId")] Nullable<Int32> formId
            )
        {
            DateTime dtFromDate;
            DateTime dtToDate;
            DateTime dtDueByFrom;
            DateTime dtDueByTo;

            System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            UserLogic uLogic = new UserLogic();
            SystemUsers currentUser = uLogic.GetCurrentUser(user);

            IQueryable<Tingle_WebForms.Models.CannotWaitForContainerForm> CannotWaitForContainerFormList = ctx.CannotWaitForContainerForms.OrderByDescending(forms => forms.Timestamp);

            if (GlobalStatus != null)
            {
                if (GlobalStatus == "Active")
                {
                    CannotWaitForContainerFormList = CannotWaitForContainerFormList.Where(forms => forms.Status.StatusText != "Completed");
                }
                else if (GlobalStatus == "Archive")
                {
                    CannotWaitForContainerFormList = CannotWaitForContainerFormList.Where(forms => forms.Status.StatusText == "Completed");
                }
            }

            if (currentUser.UserRole.RoleName == "ReportsUser")
            {
                var reqHistory = ctx.UserRequests.Where(x => x.User.SystemUserID == currentUser.SystemUserID && x.Form.FormName == "Cannot Wait For Container");
                var assHistory = ctx.UserAssignments.Where(x => x.User.SystemUserID == currentUser.SystemUserID && x.Form.FormName == "Cannot Wait For Container");

                CannotWaitForContainerFormList = CannotWaitForContainerFormList
                                        .Where(eofl => eofl.SubmittedUser.SystemUserID == currentUser.SystemUserID
                                        || eofl.AssignedUser.SystemUserID == currentUser.SystemUserID
                                        || eofl.RequestedUser.SystemUserID == currentUser.SystemUserID
                                        || reqHistory.Any(y => y.RelatedFormId == eofl.RecordId)
                                        || assHistory.Any(y => y.RelatedFormId == eofl.RecordId)
                                        );
            }

            if (formId != null)
            {
                Int32 id = Convert.ToInt32(formId);
                CannotWaitForContainerFormList = CannotWaitForContainerFormList.Where(forms => forms.RecordId.Equals(id));
            }

            if (!String.IsNullOrWhiteSpace(FromDate))
            {
                DateTime.TryParse(FromDate, out dtFromDate);
                CannotWaitForContainerFormList = CannotWaitForContainerFormList.Where(forms => forms.Timestamp >= dtFromDate);
            }
            if (!String.IsNullOrWhiteSpace(ToDate))
            {
                DateTime.TryParse(ToDate, out dtToDate);
                dtToDate = dtToDate.AddDays(1);
                CannotWaitForContainerFormList = CannotWaitForContainerFormList.Where(forms => forms.Timestamp <= dtToDate);
            }
            if (!String.IsNullOrWhiteSpace(Company) && Company != "Any Company")
            {
                CannotWaitForContainerFormList = CannotWaitForContainerFormList.Where(forms => forms.Company == Company);
            }
            if (!String.IsNullOrWhiteSpace(OrderNumber))
            {
                CannotWaitForContainerFormList = CannotWaitForContainerFormList.Where(forms => forms.OrderNumber.Contains(OrderNumber.Trim()));
            }
            if (PlantId != null)
            {
                CannotWaitForContainerFormList = CannotWaitForContainerFormList.Where(forms => forms.Plant.RecordId == PlantId);
            }
            if (!String.IsNullOrWhiteSpace(Line))
            {
                CannotWaitForContainerFormList = CannotWaitForContainerFormList.Where(forms => forms.Line.Contains(Line.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(Quantity))
            {
                CannotWaitForContainerFormList = CannotWaitForContainerFormList.Where(forms => forms.Quantity.Contains(Quantity.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(SKU))
            {
                CannotWaitForContainerFormList = CannotWaitForContainerFormList.Where(forms => forms.SKU.Contains(SKU.Trim()));
            }
            if (StatusId != null)
            {
                CannotWaitForContainerFormList = CannotWaitForContainerFormList.Where(forms => forms.Status.StatusId == StatusId);
            }

            if (!String.IsNullOrWhiteSpace(DueByFrom))
            {
                DateTime.TryParse(DueByFrom, out dtDueByFrom);
                CannotWaitForContainerFormList = CannotWaitForContainerFormList.Where(forms => forms.DueDate >= dtDueByFrom);
            }
            if (!String.IsNullOrWhiteSpace(DueByTo))
            {
                DateTime.TryParse(DueByTo, out dtDueByTo);
                CannotWaitForContainerFormList = CannotWaitForContainerFormList.Where(forms => forms.DueDate <= dtDueByTo);
            }
            if (RequestedById != null)
            {
                CannotWaitForContainerFormList = CannotWaitForContainerFormList.Where(forms => forms.RequestedUser.SystemUserID == RequestedById);
            }
            if (AssignedToId != null)
            {
                CannotWaitForContainerFormList = CannotWaitForContainerFormList.Where(forms => forms.AssignedUser.SystemUserID == AssignedToId);
            }
            if (PriorityId != null)
            {
                CannotWaitForContainerFormList = CannotWaitForContainerFormList.Where(forms => forms.Priority.RecordId == PriorityId);
            }

            if (Session["MyForms"] != null)
            {
                if (Session["MyForms"].ToString() == "Requested")
                {
                    CannotWaitForContainerFormList = CannotWaitForContainerFormList.Where(forms => forms.RequestedUser.SystemUserID == currentUser.SystemUserID);
                }
                else if (Session["MyForms"].ToString() == "Assigned")
                {
                    CannotWaitForContainerFormList = CannotWaitForContainerFormList.Where(forms => forms.AssignedUser.SystemUserID == currentUser.SystemUserID);
                }
                else if (Session["MyForms"].ToString() == "Created")
                {
                    CannotWaitForContainerFormList = CannotWaitForContainerFormList.Where(forms => forms.SubmittedUser.SystemUserID == currentUser.SystemUserID);
                }
            }

            return CannotWaitForContainerFormList;
        }

        protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e){}

        protected void btnBack_Click(object sender, EventArgs e)
        {
            gvReport.DataBind();
        }

        protected void fvReport_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            //if (IsValid)
            //{
            //    int id = Convert.ToInt32(((Label)fvReport.FindControl("lblRecordId")).Text);

            //    Tingle_WebForms.Models.CannotWaitForContainerForm myForm = ctx.CannotWaitForContainerForms.FirstOrDefault(eof => eof.RecordId == id);

            //    RadDropDownList ddlCompanyEdit = (RadDropDownList)fvReport.FindControl("ddlCompanyEdit");
            //    TextBox txtOrderNumber = (TextBox)fvReport.FindControl("txtOrderNumberEdit");
            //    RadDropDownList ddlPlantsEdit = (RadDropDownList)fvReport.FindControl("ddlPlantsEdit");
            //    TextBox txtLine = (TextBox)fvReport.FindControl("txtLineEdit");
            //    TextBox txtQuantity = (TextBox)fvReport.FindControl("txtQuantityEdit");
            //    TextBox txtSKU = (TextBox)fvReport.FindControl("txtSKUEdit");
            //    HtmlInputText txtDueByDate = (HtmlInputText)fvReport.FindControl("txtDueByDateEdit");
            //    RadDropDownList ddlStatus = (RadDropDownList)fvReport.FindControl("ddlStatusEdit");
            //    int statusId = Convert.ToInt32(ddlStatus.SelectedValue);
            //    RadComboBox ddlRequestedByEdit = (RadComboBox)fvReport.FindControl("ddlRequestedByEdit");
            //    int requestedById = Convert.ToInt32(ddlRequestedByEdit.SelectedValue);
            //    RadComboBox ddlAssignedToEdit = (RadComboBox)fvReport.FindControl("ddlAssignedToEdit");
            //    int assignedToId = 0;
            //    if (ddlAssignedToEdit.SelectedIndex != -1)
            //    {
            //        assignedToId = Convert.ToInt32(ddlAssignedToEdit.SelectedValue);
            //    }
            //    RadDropDownList ddlPriorityEdit = (RadDropDownList)fvReport.FindControl("ddlPriorityEdit");
            //    int priorityId = Convert.ToInt32(ddlPriorityEdit.SelectedValue);
            //    CheckBox cbSendComments = (CheckBox)fvReport.FindControl("cbSendComments");
            //    CheckBox cbShowSystemComments = (CheckBox)fvReport.FindControl("cbShowSystemComments");
            //    CheckBox cbNotifyStandard = (CheckBox)fvReport.FindControl("cbNotifyStandard");
            //    CheckBox cbNotifyAssignee = (CheckBox)fvReport.FindControl("cbNotifyAssignee");
            //    CheckBox cbNotifyOther = (CheckBox)fvReport.FindControl("cbNotifyOther");
            //    CheckBox cbNotifyRequester = (CheckBox)fvReport.FindControl("cbNotifyRequester");
            //    RadComboBox ddlNotifyOther = (RadComboBox)fvReport.FindControl("ddlNotifyOther");

            //    Label lblEmailsSentTo = (Label)fvReport.FindControl("lblEmailsSentTo");
            //    Label lblFVMessage = (Label)fvReport.FindControl("lblFVMessage");

            //    DateTime tryDateDue;
            //    Nullable<DateTime> dateDue = null;

            //    try
            //    {
            //        if (myForm.RequestedUser.SystemUserID.ToString() != ddlRequestedByEdit.SelectedValue)
            //        {
            //            Comments newRequesterComment = new Comments
            //            {
            //                Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Cannot Wait For Container"),
            //                Note = "Requester Changed To: " + ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == requestedById).DisplayName,
            //                RelatedFormId = myForm.RecordId,
            //                SystemComment = true,
            //                Timestamp = DateTime.Now
            //            };

            //            ctx.Comments.Add(newRequesterComment);
            //        }

            //        if (myForm.AssignedUser == null && ddlAssignedToEdit.SelectedIndex != -1) // (myForm.AssignedUser != null && ddlAssignedToEdit.SelectedIndex != -1 && Convert.ToString(myForm.AssignedUser.SystemUserID) != ddlAssignedToEdit.SelectedValue))
            //        {
            //            Comments newAssignedComment = new Comments
            //            {
            //                Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Cannot Wait For Container"),
            //                Note = "Request Assigned To: " + ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == assignedToId).DisplayName,
            //                RelatedFormId = myForm.RecordId,
            //                SystemComment = true,
            //                Timestamp = DateTime.Now
            //            };

            //            ctx.Comments.Add(newAssignedComment);
            //        }
            //        else if (myForm.AssignedUser != null && ddlAssignedToEdit.SelectedIndex != -1)
            //        {
            //            if (myForm.AssignedUser.SystemUserID.ToString() != ddlAssignedToEdit.SelectedValue)
            //            {
            //                Comments newAssignedComment = new Comments
            //                {
            //                    Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Cannot Wait For Container"),
            //                    Note = "Request Assignee Changed To: " + ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == assignedToId).DisplayName,
            //                    RelatedFormId = myForm.RecordId,
            //                    SystemComment = true,
            //                    Timestamp = DateTime.Now
            //                };

            //                ctx.Comments.Add(newAssignedComment);
            //            }
            //        }
            //        else if (myForm.AssignedUser != null && ddlAssignedToEdit.SelectedIndex == -1)
            //        {
            //            Comments newAssignedComment = new Comments
            //            {
            //                Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Cannot Wait For Container"),
            //                Note = "Request Assignment Removed From: " + ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == myForm.AssignedUser.SystemUserID).DisplayName,
            //                RelatedFormId = myForm.RecordId,
            //                SystemComment = true,
            //                Timestamp = DateTime.Now
            //            };

            //            ctx.Comments.Add(newAssignedComment);
            //        }

            //        if (txtDueByDate.Value != null)
            //        {
            //            DateTime.TryParse(txtDueByDate.Value.ToString(), out tryDateDue);
            //            if (tryDateDue.Year > 0001)
            //            {
            //                dateDue = tryDateDue;
            //            }
            //        }

            //        var statusCode = ctx.Statuses.FirstOrDefault(s => s.StatusId == statusId);

            //        myForm.Company = ddlCompanyEdit.SelectedValue;
            //        myForm.OrderNumber = txtOrderNumber.Text;
            //        myForm.Plant = ctx.Plants.FirstOrDefault(x => x.PlantText == ddlPlantsEdit.SelectedText);
            //        myForm.Line = txtLine.Text;
            //        myForm.Quantity = txtQuantity.Text;
            //        myForm.SKU = txtSKU.Text;
            //        myForm.Status = statusCode;
            //        myForm.RequestedUser = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == requestedById);
            //        myForm.Priority = ctx.Priorities.FirstOrDefault(x => x.RecordId == priorityId);
            //        myForm.DueDate = dateDue;

            //        if (ddlAssignedToEdit.SelectedIndex != -1)
            //        {
            //            myForm.AssignedUser = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == assignedToId);
            //        }
            //        else
            //        {
            //            myForm.AssignedUser = null;
            //        }

            //        myForm.LastModifiedTimestamp = DateTime.Now;
            //        UserLogic newLogic = new UserLogic();
            //        System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            //        SystemUsers currentUser = newLogic.GetCurrentUser(user);
            //        myForm.LastModifiedUser = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == currentUser.SystemUserID);

            //        ctx.CannotWaitForContainerForms.Attach(myForm);
            //        ctx.Entry(myForm).State = EntityState.Modified;

            //        ctx.SaveChanges();

            //        if (myForm.AssignedUser != null && !newLogic.HasBeenAssigned(myForm.AssignedUser, myForm.RecordId, "Cannot Wait For Container"))
            //        {
            //            UserAssignmentAssociation uA = new UserAssignmentAssociation
            //            {
            //                Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Cannot Wait For Container"),
            //                RelatedFormId = myForm.RecordId,
            //                User = myForm.AssignedUser
            //            };

            //            ctx.UserAssignments.Add(uA);
            //        }

            //        if (myForm.RequestedUser != null && !newLogic.HasBeenRequested(myForm.RequestedUser, myForm.RecordId, "Cannot Wait For Container"))
            //        {
            //            UserRequestAssociation uR = new UserRequestAssociation
            //            {
            //                Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Cannot Wait For Container"),
            //                RelatedFormId = myForm.RecordId,
            //                User = myForm.RequestedUser
            //            };

            //            ctx.UserRequests.Add(uR);
            //        }

            //        ctx.SaveChanges();

            //        if (myForm.Status.StatusText == "Completed")
            //        {
            //            Comments updateComment = new Comments
            //            {
            //                Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Cannot Wait For Container"),
            //                Note = "Request Completed By: " + currentUser.DisplayName,
            //                RelatedFormId = myForm.RecordId,
            //                SystemComment = true,
            //                Timestamp = DateTime.Now
            //            };

            //            ctx.Comments.Add(updateComment);

            //        }
            //        else
            //        {
            //            Comments updateComment = new Comments
            //            {
            //                Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Cannot Wait For Container"),
            //                Note = "Request Updated By: " + currentUser.DisplayName + " --- Status: " + myForm.Status.StatusText + " --- Priority: " + myForm.Priority.PriorityText,
            //                RelatedFormId = myForm.RecordId,
            //                SystemComment = true,
            //                Timestamp = DateTime.Now
            //            };

            //            ctx.Comments.Add(updateComment);
            //        }


            //        if (lblEmailsSentTo.Text != "")
            //        {
            //            SendUpdateEmail(myForm.RecordId, lblEmailsSentTo.Text, cbSendComments.Checked, cbShowSystemComments.Checked);

            //            Comments notificationComment = new Comments
            //            {
            //                Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Cannot Wait For Container"),
            //                Note = "Request Notifications Sent To: " + lblEmailsSentTo.Text,
            //                RelatedFormId = myForm.RecordId,
            //                SystemComment = true,
            //                Timestamp = DateTime.Now
            //            };

            //            ctx.Comments.Add(notificationComment);
            //        }

            //        ctx.SaveChanges();

            //        cbNotifyAssignee.Checked = false;
            //        cbNotifyOther.Checked = false;
            //        cbNotifyRequester.Checked = false;
            //        cbNotifyStandard.Checked = false;
            //        cbSendComments.Checked = false;
            //        ddlNotifyOther.Text = "";

            //        gvReport.DataBind();
            //        gvReport.SelectedIndex = -1;
            //        pnlDetails.Visible = false;
            //        pnlReport.Visible = true;
            //        pnlFilter.Visible = true;
            //    }
            //    catch (Exception ex)
            //    {
            //        throw;
            //    }
            //}

        }

        public void SendUpdateEmail(Int32 FormId, string EmailList, Boolean SendComments, Boolean IncludeSystemComments)
        {
            Tingle_WebForms.Models.CannotWaitForContainerForm myForm = ctx.CannotWaitForContainerForms.FirstOrDefault(eof => eof.RecordId == FormId);
            TForm submittedForm = ctx.TForms.FirstOrDefault(tf => tf.FormName == "Cannot Wait For Container");
            Boolean anyComments = false;
            IEnumerable<Comments> finalComments = null;

            string dueDateString = "";

            if (myForm.DueDate != null)
            {
                dueDateString = myForm.DueDate.Value.ToShortDateString();
            }

            if (IncludeSystemComments)
            {
                if (ctx.Comments.Any(x => x.Form.FormName == "Cannot Wait For Container" && x.RelatedFormId == myForm.RecordId))
                {
                    anyComments = true;

                    IEnumerable<Comments> commentsList = ctx.Comments
                        .Where(x => x.Form.FormName == "Cannot Wait For Container" && x.RelatedFormId == myForm.RecordId)
                        .Include(x => x.User)
                        .OrderByDescending(x => x.RecordId)
                        .ToList();

                    finalComments = commentsList;
                }
            }
            else
            {
                if (ctx.Comments.Any(x => x.Form.FormName == "Cannot Wait For Container" && x.RelatedFormId == myForm.RecordId && x.SystemComment == false))
                {
                    anyComments = true;

                    IEnumerable<Comments> commentsList = ctx.Comments
                        .Where(x => x.Form.FormName == "Cannot Wait For Container" && x.RelatedFormId == myForm.RecordId && x.SystemComment == false)
                        .Include(x => x.User)
                        .OrderByDescending(x => x.RecordId)
                        .ToList();

                    finalComments = commentsList;
                }
            }

            string emailListString = EmailList.Replace(" ", "");
            List<string> emailList = emailListString.Split(',').ToList<string>();

            System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            UserLogic uLogic = new UserLogic();
            SystemUsers currentUser = uLogic.GetCurrentUser(user);

            SendEmail msg = new SendEmail();
            StringBuilder bodyHtml = new StringBuilder();

            bodyHtml.AppendLine("<div style=\"width:50%; text-align:center;\"><img src=\"http://www.wctingle.com/img/Logo.jpg\" /><br /><br />")
                .AppendLine("<table style=\"border: 4px solid #d0604c;background-color:#FFF;width:100%;margin-lefT:auto; margin-right:auto;\">")
                .AppendLine("    <tr>")
                .AppendLine("        <td colspan=\"4\" style=\"text-align: center;vertical-align: middle;font-weight: bold;font-size: 20px;border: 4px solid #d0604c; color:#FFF; background-color:#bc4445;\">Cannot Wait For Container Request Update</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Company:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Company).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;\"></td>")
                .AppendLine("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;\"></td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Order #:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.OrderNumber).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Plant:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Plant).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Line:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Line).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Quantity:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Quantity).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">SKU:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.SKU).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\"></td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\"></td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("       <td style=\"width:100%;\" colspan=\"4\">")
                .AppendLine("        <table style=\"border:none; width:100%\">")
                .AppendLine("            <tr>")
                .AppendLine("                <td colspan=\"4\" style=\"text-align:center\">")
                .AppendLine("                    <span style=\"font-weight:bold; color:#bc4445; text-decoration:underline\">Assignment and Request Details:</span>")
                .AppendLine("                </td>")
                .AppendLine("            </tr>")
                .AppendLine("            <tr>")
                .AppendLine("                <td style=\"width:25%; text-align:right\">Requested By:</td>")
                .Append("                    <td style=\"width:25%; text-align:left\">").AppendLine(myForm.RequestedUser.DisplayName)
                .AppendLine("                </td>")
                .AppendLine("                <td style=\"width:25%; text-align:right\">Assigned To:</td>")
                .Append("                   <td style=\"width:25%; text-align:left\">");

            if (myForm.AssignedUser != null) { bodyHtml.AppendLine(myForm.AssignedUser.DisplayName); } else { bodyHtml.AppendLine("N/A"); }

            bodyHtml.AppendLine("            </td>")
                .AppendLine("            </tr>")
                .AppendLine("            <tr>")
                .AppendLine("                <td style=\"width:25%; text-align:right\">Date Created:</td>")
                .AppendLine("                <td style=\"width:25%; text-align:left\">").Append(myForm.Timestamp.ToShortDateString())
                .AppendLine("                </td>")
                .AppendLine("                <td style=\"width:25%; text-align:right\">Due By:</td>")
                .Append("                    <td style=\"width:25%; text-align:left\">").Append(dueDateString).AppendLine("</td>")
                .AppendLine("            </tr>")
                .AppendLine("            <tr>")
                .AppendLine("                <td style=\"width:25%; text-align:right\">Status:</td>")
                .Append("                    <td style=\"width:25%; text-align:left\">").AppendLine(myForm.Status.StatusText)
                .AppendLine("                </td>")
                .AppendLine("                <td style=\"width:25%; text-align:right\">Priority:</td>")
                .Append("                    <td style=\"width:25%; text-align:left\">").AppendLine(myForm.Priority.PriorityText)
                .AppendLine("                </td>")
                .AppendLine("            </tr>")
                .AppendLine("        </table>")
                .AppendLine("       </td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .Append("           <td style=\"width:25%; text-align:right\">Created By:</td>")
                .Append("           <td style=\"width:25%; text-align:left\">").AppendLine(myForm.SubmittedUser.DisplayName).Append("</td>")
                .Append("           <td style=\"width:25%; text-align:right\">Updated By:</td>")
                .Append("           <td style=\"width:25%; text-align:left\">").AppendLine(myForm.LastModifiedUser.DisplayName).Append("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .Append("           <td style=\"width:25%; text-align:right\"></td>")
                .Append("           <td style=\"width:25%; text-align:left\"></td>")
                .Append("           <td style=\"width:25%; text-align:right\">Last Update:</td>")
                .Append("           <td style=\"width:25%; text-align:left\">").AppendLine(myForm.LastModifiedTimestamp.ToShortDateString()).Append("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .Append("           <td style=\"width:100%; text-align:center\" colspan=\"4\"><span style=\"color:#bc4445; font-weight:bold\">Request Notifications Sent To:</span> <br />")
                .AppendLine(EmailList)
                .AppendLine("       </td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("           <td style=\"width:100%; text-align:center\" colspan=\"4\"><br /><br /></td>")
                .AppendLine("    </tr>")
                .AppendLine("</table><br /><br />");

            if (SendComments && anyComments)
            {
                bodyHtml.AppendLine("<div style=\"width:100%; padding-top:10px\">");

                foreach (var comment in finalComments)
                {
                    if (comment.SystemComment == false)
                    {
                        bodyHtml.AppendLine("<div style=\"width:80%; background-color:#bc4445; margin: 0 auto; text-align:left; font-size:12px; color:white;word-wrap: break-word;\">")
                            .AppendLine("<div style=\"width:100%; margin:10px\">").AppendLine(comment.Note).AppendLine("<br /><br />")
                            .AppendLine("<span style=\"padding-right:20px\">by ").AppendLine(comment.User.DisplayName).AppendLine("</span>")
                            .AppendLine(comment.Timestamp.ToString())
                            .AppendLine("</div></div>");
                    }
                    else
                    {
                        bodyHtml.AppendLine("<div style=\"width:80%; background-color:#FFF; margin: 0 auto; text-align:right; font-size:12px; color:#bc4445; word-wrap: break-word;\">")
                            .AppendLine("<div style=\"width:95%; padding: 5px;\">").AppendLine(comment.Note).AppendLine("<br /><br />")
                            .AppendLine(comment.Timestamp.ToString())
                            .AppendLine("</div></div>");
                    }
                }
            }

            bodyHtml.AppendLine("</div><br /><br />");

            bool result = msg.SendMail("InfoShare@wctingle.com", emailList, "Cannot Wait For Container Request Update", bodyHtml.ToString(), submittedForm, myForm.RecordId, currentUser);

        }

        public void UpdateForm(int RecordId){}

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            gvReport.DataBind();
        }

        protected void fvReport_DataBound(object sender, EventArgs e){}

        protected void fvReport_DataBinding(object sender, EventArgs e){}

        protected void fvReport_PreRender(object sender, EventArgs e)
        {
            //Button btnUpdate = (Button)fvReport.FindControl("btnUpdate");
            //Button btnCancel = (Button)fvReport.FindControl("btnCancel");
            //Button btnBack = (Button)fvReport.FindControl("btnDetailsBack");

            //System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            //UserLogic uLogic = new UserLogic();
            //SystemUsers currentUser = uLogic.GetCurrentUser(user);

            //RadDropDownList ddlCompanyEdit = (RadDropDownList)fvReport.FindControl("ddlCompanyEdit");
            //Label lblCompanyEdit = (Label)fvReport.FindControl("lblCompanyEdit");
            //TextBox txtOrderNumberEdit = (TextBox)fvReport.FindControl("txtOrderNumberEdit");
            //TextBox txtPlantEdit = (TextBox)fvReport.FindControl("txtPlantEdit");
            //TextBox txtLineEdit = (TextBox)fvReport.FindControl("txtLineEdit");
            //TextBox txtQuantityEdit = (TextBox)fvReport.FindControl("txtQuantityEdit");
            //TextBox txtSKUEdit = (TextBox)fvReport.FindControl("txtSKUEdit");
            //TextBox txtAdditionalInfoEdit = (TextBox)fvReport.FindControl("txtAdditionalInfoEdit");
            //TextBox txtRequestHandlerEdit = (TextBox)fvReport.FindControl("txtRequestHandlerEdit");
            //TextBox txtCompletionNotes = (TextBox)fvReport.FindControl("txtCompletionNotes");
            //TextBox txtCCCompletedFormToEmail = (TextBox)fvReport.FindControl("txtCCCompletedFormToEmail");
            //RadDropDownList ddlStatusEdit = (RadDropDownList)fvReport.FindControl("ddlStatusEdit");
            //RadComboBox ddlRequestedByEdit = (RadComboBox)fvReport.FindControl("ddlRequestedByEdit");
            //RadComboBox ddlAssignedToEdit = (RadComboBox)fvReport.FindControl("ddlAssignedToEdit");
            //RadDropDownList ddlPriorityEdit = (RadDropDownList)fvReport.FindControl("ddlPriorityEdit");
            //HtmlInputText txtDueByDateEdit = (HtmlInputText)fvReport.FindControl("txtDueByDateEdit");
            //CheckBox cbNotifyStandard = (CheckBox)fvReport.FindControl("cbNotifyStandard");
            //CheckBox cbNotifyAssignee = (CheckBox)fvReport.FindControl("cbNotifyAssignee");
            //CheckBox cbSendComments = (CheckBox)fvReport.FindControl("cbSendComments");
            //CheckBox cbNotifyOther = (CheckBox)fvReport.FindControl("cbNotifyOther");
            //CheckBox cbNotifyRequester = (CheckBox)fvReport.FindControl("cbNotifyRequester");
            //RadComboBox ddlNotifyOther = (RadComboBox)fvReport.FindControl("ddlNotifyOther");
            //RadButton btnAddNewEmail = (RadButton)fvReport.FindControl("btnAddNewEmail");
            //Button btnAddComment = (Button)fvReport.FindControl("btnAddComment");

            //FillEmailAddressLabels();

            //Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
            //int recordId;
            //Int32.TryParse(lblRecordId.Text, out recordId);
            //Boolean isComplete;

            //using (var ctx = new FormContext())
            //{
            //    var thisForm = ctx.CannotWaitForContainerForms.FirstOrDefault(eof => eof.RecordId == recordId);

            //    isComplete = thisForm.Status.StatusText == "Completed" ? true : false;
            //}

            //if (currentUser.UserRole.RoleName == "ReportsUser" && isComplete)
            //{
            //    ddlCompanyEdit.Enabled = false;
            //    txtOrderNumberEdit.Enabled = false;
            //    txtPlantEdit.Enabled = false;
            //    txtLineEdit.Enabled = false;
            //    txtQuantityEdit.Enabled = false;
            //    txtSKUEdit.Enabled = false;
            //    ddlStatusEdit.Enabled = false;
            //    ddlRequestedByEdit.Enabled = false;
            //    ddlAssignedToEdit.Enabled = false;
            //    ddlPriorityEdit.Enabled = false;
            //    txtDueByDateEdit.Disabled = true;
            //    cbNotifyStandard.Enabled = false;
            //    cbNotifyAssignee.Enabled = false;
            //    cbSendComments.Enabled = false;
            //    cbNotifyOther.Enabled = false;
            //    cbNotifyRequester.Enabled = false;
            //    ddlNotifyOther.Enabled = false;
            //    btnAddNewEmail.Enabled = false;
            //    btnAddComment.Enabled = false;
            //    btnUpdate.Enabled = false;
            //}

        }

        protected void btnAdvancedSearch_Click(object sender, EventArgs e)
        {
            ShowAdvanced();
        }

        protected void btnBasicSearch_Click(object sender, EventArgs e)
        {
            HideAdvanced();
        }

        public void ShowAdvanced()
        {
            tr1.Visible = true;
            tr2.Visible = true;
            tr3.Visible = true;
            btnAdvancedSearch.Visible = false;
            btnBasicSearch.Visible = true;
        }

        public void HideAdvanced()
        {
            tr1.Visible = false;
            tr2.Visible = false;
            tr3.Visible = false;
            btnAdvancedSearch.Visible = true;
            btnBasicSearch.Visible = false;
        }

        protected void btnCancel2_Click(object sender, EventArgs e)
        {
            //PlaceHolder phFVCompleted = (PlaceHolder)fvReport.FindControl("phFVCompleted");
            //PlaceHolder phFVDetails = (PlaceHolder)fvReport.FindControl("phFVDetails");
            //HiddenField hfCompleted = (HiddenField)fvReport.FindControl("hfCompleted");

            //phFVCompleted.Visible = false;
            //phFVDetails.Visible = true;
            //hfCompleted.Value = "0";
        }

        protected void ddlStatusEdit_DataBound(object sender, EventArgs e)
        {
            //try
            //{
            //    RadDropDownList ddlStatus = (RadDropDownList)sender;
            //    Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
            //    int recordId;
            //    Int32.TryParse(lblRecordId.Text, out recordId);

            //    using (var ctx = new FormContext())
            //    {
            //        var thisForm = ctx.CannotWaitForContainerForms.FirstOrDefault(eof => eof.RecordId == recordId);

            //        ddlStatus.SelectedValue = thisForm.Status.StatusId.ToString();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }

        protected void btnInsertEmail_Click(object sender, EventArgs e)
        {
            //RadTextBox txtNameInsert = (RadTextBox)fvReport.FindControl("txtNameInsert");
            //RadTextBox txtAddressInsert = (RadTextBox)fvReport.FindControl("txtAddressInsert");
            //RadComboBox ddlNotifyOther = (RadComboBox)fvReport.FindControl("ddlNotifyOther");
            //RadioButtonList rblNotificationEmailStatusInsert = (RadioButtonList)fvReport.FindControl("rblNotificationEmailStatusInsert");
            //Label lblInsertEmailMessage = (Label)fvReport.FindControl("lblInsertEmailMessage");

            //try
            //{
            //    using (FormContext ctx = new FormContext())
            //    {
            //        if (!ctx.NotificationEmailAddresses.Any(x => x.Name == txtNameInsert.Text || x.Address == txtAddressInsert.Text))
            //        {
            //            if (!ctx.SystemUsers.Any(x => x.EmailAddress == txtAddressInsert.Text))
            //            {
            //                NotificationEmailAddress newEmail = new NotificationEmailAddress();

            //                newEmail.Timestamp = DateTime.Now;
            //                newEmail.Name = txtNameInsert.Text;
            //                newEmail.Address = txtAddressInsert.Text;
            //                newEmail.Status = Convert.ToInt16(rblNotificationEmailStatusInsert.SelectedValue);

            //                ctx.NotificationEmailAddresses.Add(newEmail);

            //                ctx.SaveChanges();

            //                lblInsertEmailMessage.Text = "";
            //                txtAddressInsert.Text = "";
            //                txtNameInsert.Text = "";
            //                rblNotificationEmailStatusInsert.SelectedIndex = 0;
            //                ddlNotifyOther.DataBind();
            //            }
            //            else
            //            {
            //                lblInsertEmailMessage.Text = "A System User already exists with this Email Address.  Please enter a unique Email Address.";
            //            }
            //        }
            //        else
            //        {
            //            if (ctx.NotificationEmailAddresses.Any(x => x.Name == txtNameInsert.Text && x.Address == txtAddressInsert.Text))
            //            {
            //                lblInsertEmailMessage.Text = "A Notification Email already exists with this Name and Email Address.  Please enter a unique Name and Email Address.";
            //            }
            //            else if (ctx.NotificationEmailAddresses.Any(x => x.Name == txtNameInsert.Text))
            //            {
            //                lblInsertEmailMessage.Text = "A Notification Email already exists with this Name.  Please enter a unique Name.";
            //            }
            //            else if (ctx.NotificationEmailAddresses.Any(x => x.Address == txtAddressInsert.Text))
            //            {
            //                lblInsertEmailMessage.Text = "A Notification Email already exists with this Email Address.  Please enter a unique Email Address.";
            //            }

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    lblInsertEmailMessage.Text = "Unable to add this Email Address.  Please contact your system administrator.";
            //}
        }

        protected void ddlRequestedByEdit_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //FillEmailAddressLabels();
        }

        protected void ddlAssignedToEdit_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //FillEmailAddressLabels();
        }

        protected void ddlNotifyOtherEdit_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //FillEmailAddressLabels();
        }

        protected void cbNotifyStandard_CheckedChanged(object sender, EventArgs e)
        {
            //FillEmailAddressLabels();
        }

        protected void cbNotifyAssignee_CheckedChanged(object sender, EventArgs e)
        {
            //FillEmailAddressLabels();
        }

        protected void cbNotifyOther_CheckedChanged(object sender, EventArgs e)
        {
            //FillEmailAddressLabels();
        }

        protected void cbNotifyRequester_CheckedChanged(object sender, EventArgs e)
        {
            //FillEmailAddressLabels();
        }

        protected void ddlNotifyOther_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //FillEmailAddressLabels();
        }

        protected void ddlNotifyOther_ItemChecked(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            //FillEmailAddressLabels();
            RadComboBox ddlNotifyOther = (RadComboBox)sender;
            ddlNotifyOther.OpenDropDownOnLoad = true;
        }

        protected void ddlNotifyOther_CheckAllCheck(object sender, Telerik.Web.UI.RadComboBoxCheckAllCheckEventArgs e)
        {
            //FillEmailAddressLabels();
            RadComboBox ddlNotifyOther = (RadComboBox)sender;
            ddlNotifyOther.OpenDropDownOnLoad = true;

        }

        protected void ddlCompanyEdit_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            //FillEmailAddressLabels();
        }

        protected void ddlCompanyEdit_DataBinding(object sender, EventArgs e)
        {
            //try
            //{
            //    RadDropDownList ddlCompanyEdit = (RadDropDownList)sender;
            //    Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
            //    int recordId;
            //    Int32.TryParse(lblRecordId.Text, out recordId);

            //    using (var ctx = new FormContext())
            //    {
            //        var thisForm = ctx.CannotWaitForContainerForms.FirstOrDefault(eof => eof.RecordId == recordId);

            //        ddlCompanyEdit.SelectedValue = thisForm.Company;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }

        protected void btnAddComment_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
            //    int recordId;
            //    Int32.TryParse(lblRecordId.Text, out recordId);
            //    RadTextBox txtNewComment = (RadTextBox)fvReport.FindControl("txtNewComment");
            //    Repeater rptrComments = (Repeater)fvReport.FindControl("rptrComments");

            //    UserLogic newLogic = new UserLogic();

            //    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            //    SystemUsers currentUser = newLogic.GetCurrentUser(user);

            //    using (var ctx = new FormContext())
            //    {
            //        var thisForm = ctx.CannotWaitForContainerForms.FirstOrDefault(eof => eof.RecordId == recordId);

            //        Comments newComment = new Comments
            //        {
            //            Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Cannot Wait For Container"),
            //            Note = txtNewComment.Text,
            //            RelatedFormId = thisForm.RecordId,
            //            SystemComment = false,
            //            Timestamp = DateTime.Now,
            //            User = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == currentUser.SystemUserID)
            //        };

            //        ctx.Comments.Add(newComment);
            //        ctx.SaveChanges();

            //        txtNewComment.Text = "";
            //        txtNewComment.Invalid = false;

            //        rptrComments.DataBind();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }

        protected void ddlPriority_DataBound(object sender, EventArgs e)
        {
            //try
            //{
            //    RadDropDownList ddlPriority = (RadDropDownList)sender;
            //    Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
            //    int recordId;
            //    Int32.TryParse(lblRecordId.Text, out recordId);

            //    ddlPriority.SelectedText = "Normal";
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }

        protected void ddlPriorityEdit_DataBound(object sender, EventArgs e)
        {
            //try
            //{
            //    RadDropDownList ddlPriorityEdit = (RadDropDownList)sender;
            //    Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
            //    int recordId;
            //    Int32.TryParse(lblRecordId.Text, out recordId);

            //    using (var ctx = new FormContext())
            //    {
            //        var thisForm = ctx.CannotWaitForContainerForms.FirstOrDefault(eof => eof.RecordId == recordId);

            //        ddlPriorityEdit.SelectedValue = thisForm.Priority.RecordId.ToString();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }

        protected void ddlAssignedToEdit_DataBound(object sender, EventArgs e)
        {
            //try
            //{
            //    RadComboBox ddlAssignedToEdit = (RadComboBox)sender;
            //    Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
            //    int recordId;
            //    Int32.TryParse(lblRecordId.Text, out recordId);

            //    using (var ctx = new FormContext())
            //    {
            //        var thisForm = ctx.CannotWaitForContainerForms.FirstOrDefault(eof => eof.RecordId == recordId);

            //        if (thisForm.AssignedUser != null)
            //        {
            //            ddlAssignedToEdit.SelectedValue = thisForm.AssignedUser.SystemUserID.ToString();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }

        protected void ddlRequestedByEdit_DataBound(object sender, EventArgs e)
        {
            //try
            //{
            //    RadComboBox ddlRequestedByEdit = (RadComboBox)sender;
            //    Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
            //    int recordId;
            //    Int32.TryParse(lblRecordId.Text, out recordId);

            //    using (var ctx = new FormContext())
            //    {
            //        var thisForm = ctx.CannotWaitForContainerForms.FirstOrDefault(eof => eof.RecordId == recordId);

            //        ddlRequestedByEdit.SelectedValue = thisForm.RequestedUser.SystemUserID.ToString();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }

        protected void ddlPlantsEdit_DataBound(object sender, EventArgs e)
        {
            //try
            //{
            //    RadDropDownList ddlPlantsEdit = (RadDropDownList)sender;
            //    Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
            //    int recordId;
            //    Int32.TryParse(lblRecordId.Text, out recordId);

            //    using (var ctx = new FormContext())
            //    {
            //        var thisForm = ctx.CannotWaitForContainerForms.FirstOrDefault(eof => eof.RecordId == recordId);

            //        ddlPlantsEdit.SelectedValue = thisForm.Plant.RecordId.ToString();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }

        protected void btnResetFilters_Click(object sender, EventArgs e)
        {
            Session.Clear();
            PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            isreadonly.SetValue(this.Request.QueryString, false, null);
            this.Request.QueryString.Clear();
            Response.Redirect(Request.Url.AbsolutePath);
        }

        protected void cvRequestedBy_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //RadComboBox ddlRequestedByEdit = (RadComboBox)fvReport.FindControl("ddlRequestedByEdit");

            //if (ddlRequestedByEdit.FindItemByText(ddlRequestedByEdit.Text) != null)
            //{
            //    args.IsValid = true;
            //}
            //else
            //{
            //    args.IsValid = false;
            //}
        }

        protected void cvAssignedTo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //RadComboBox ddlAssignedToEdit = (RadComboBox)fvReport.FindControl("ddlAssignedToEdit");

            //if (ddlAssignedToEdit.FindItemByText(ddlAssignedToEdit.Text) != null)
            //{
            //    args.IsValid = true;
            //}
            //else
            //{
            //    args.IsValid = false;
            //}
        }

        protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblRecordId = (Label)e.Row.FindControl("lblRecordId");
                    RadToolTip ttOrderNumber = (RadToolTip)e.Row.FindControl("ttOrderNumber");

                    string ltlOrderNumberHtml = "";

                    int recordId = Convert.ToInt32(lblRecordId.Text);

                    using (FormContext ctx = new FormContext())
                    {
                        var thisForm = ctx.CannotWaitForContainerForms.FirstOrDefault(x => x.RecordId == recordId);

                        ltlOrderNumberHtml += "<div style=\"border: 4px solid #d0604c; background-color: #FFF;\">SKU: " + thisForm.SKU + "<br /> Quantity: " + thisForm.Quantity + "</div>";

                        ttOrderNumber.Text = ltlOrderNumberHtml;
                    }
                }
            }
            catch
            {

            }
        }

        protected void cbShowSystemComments_CheckedChanged(object sender, EventArgs e)
        {
            //Repeater rptrComments = (Repeater)fvReport.FindControl("rptrComments");

            //rptrComments.DataBind();
        }

        protected void gvReport_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            if (IsValid)
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

                int id = Convert.ToInt32(((HiddenField)userControl.FindControl("hRecordId")).Value);

                Tingle_WebForms.Models.CannotWaitForContainerForm myForm = ctx.CannotWaitForContainerForms.FirstOrDefault(eof => eof.RecordId == id);

                RadDropDownList ddlCompanyEdit = (RadDropDownList)userControl.FindControl("ddlCompanyEdit");
                RadTextBox txtOrderNumber = (RadTextBox)userControl.FindControl("txtOrderNumberEdit");
                RadDropDownList ddlPlantsEdit = (RadDropDownList)userControl.FindControl("ddlPlantsEdit");
                RadTextBox txtLine = (RadTextBox)userControl.FindControl("txtLineEdit");
                RadTextBox txtQuantity = (RadTextBox)userControl.FindControl("txtQuantityEdit");
                RadTextBox txtSKU = (RadTextBox)userControl.FindControl("txtSKUEdit");
                RadDatePicker txtDueByDate = (RadDatePicker)userControl.FindControl("txtDueByDateEdit");
                RadDropDownList ddlStatus = (RadDropDownList)userControl.FindControl("ddlStatusEdit");
                int statusId = Convert.ToInt32(ddlStatus.SelectedValue);
                RadComboBox ddlRequestedByEdit = (RadComboBox)userControl.FindControl("ddlRequestedByEdit");
                int requestedById = Convert.ToInt32(ddlRequestedByEdit.SelectedValue);
                RadComboBox ddlAssignedToEdit = (RadComboBox)userControl.FindControl("ddlAssignedToEdit");
                int assignedToId = 0;
                if (ddlAssignedToEdit.SelectedIndex != -1)
                {
                    assignedToId = Convert.ToInt32(ddlAssignedToEdit.SelectedValue);
                }
                RadDropDownList ddlPriorityEdit = (RadDropDownList)userControl.FindControl("ddlPriorityEdit");
                int priorityId = Convert.ToInt32(ddlPriorityEdit.SelectedValue);
                CheckBox cbSendComments = (CheckBox)userControl.FindControl("cbSendComments");
                CheckBox cbShowSystemComments = (CheckBox)userControl.FindControl("cbShowSystemComments");
                CheckBox cbNotifyStandard = (CheckBox)userControl.FindControl("cbNotifyStandard");
                CheckBox cbNotifyAssignee = (CheckBox)userControl.FindControl("cbNotifyAssignee");
                CheckBox cbNotifyOther = (CheckBox)userControl.FindControl("cbNotifyOther");
                CheckBox cbNotifyRequester = (CheckBox)userControl.FindControl("cbNotifyRequester");
                RadComboBox ddlNotifyOther = (RadComboBox)userControl.FindControl("ddlNotifyOther");

                Label lblEmailsSentTo = (Label)userControl.FindControl("lblEmailsSentTo");
                Label lblFVMessage = (Label)userControl.FindControl("lblFVMessage");

                DateTime tryDateDue;
                Nullable<DateTime> dateDue = null;

                try
                {
                    if (myForm.RequestedUser.SystemUserID.ToString() != ddlRequestedByEdit.SelectedValue)
                    {
                        Comments newRequesterComment = new Comments
                        {
                            Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Cannot Wait For Container"),
                            Note = "Requester Changed To: " + ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == requestedById).DisplayName,
                            RelatedFormId = myForm.RecordId,
                            SystemComment = true,
                            Timestamp = DateTime.Now
                        };

                        ctx.Comments.Add(newRequesterComment);
                    }

                    if (myForm.AssignedUser == null && ddlAssignedToEdit.SelectedIndex != -1) // (myForm.AssignedUser != null && ddlAssignedToEdit.SelectedIndex != -1 && Convert.ToString(myForm.AssignedUser.SystemUserID) != ddlAssignedToEdit.SelectedValue))
                    {
                        Comments newAssignedComment = new Comments
                        {
                            Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Cannot Wait For Container"),
                            Note = "Request Assigned To: " + ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == assignedToId).DisplayName,
                            RelatedFormId = myForm.RecordId,
                            SystemComment = true,
                            Timestamp = DateTime.Now
                        };

                        ctx.Comments.Add(newAssignedComment);
                    }
                    else if (myForm.AssignedUser != null && ddlAssignedToEdit.SelectedIndex != -1)
                    {
                        if (myForm.AssignedUser.SystemUserID.ToString() != ddlAssignedToEdit.SelectedValue)
                        {
                            Comments newAssignedComment = new Comments
                            {
                                Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Cannot Wait For Container"),
                                Note = "Request Assignee Changed To: " + ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == assignedToId).DisplayName,
                                RelatedFormId = myForm.RecordId,
                                SystemComment = true,
                                Timestamp = DateTime.Now
                            };

                            ctx.Comments.Add(newAssignedComment);
                        }
                    }
                    else if (myForm.AssignedUser != null && ddlAssignedToEdit.SelectedIndex == -1)
                    {
                        Comments newAssignedComment = new Comments
                        {
                            Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Cannot Wait For Container"),
                            Note = "Request Assignment Removed From: " + ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == myForm.AssignedUser.SystemUserID).DisplayName,
                            RelatedFormId = myForm.RecordId,
                            SystemComment = true,
                            Timestamp = DateTime.Now
                        };

                        ctx.Comments.Add(newAssignedComment);
                    }

                    if (txtDueByDate.SelectedDate != null)
                    {
                        DateTime.TryParse(txtDueByDate.SelectedDate.Value.ToString(), out tryDateDue);
                        if (tryDateDue.Year > 0001)
                        {
                            dateDue = tryDateDue;
                        }
                    }

                    var statusCode = ctx.Statuses.FirstOrDefault(s => s.StatusId == statusId);

                    myForm.Company = ddlCompanyEdit.SelectedValue;
                    myForm.OrderNumber = txtOrderNumber.Text;
                    myForm.Plant = ctx.Plants.FirstOrDefault(x => x.PlantText == ddlPlantsEdit.SelectedText);
                    myForm.Line = txtLine.Text;
                    myForm.Quantity = txtQuantity.Text;
                    myForm.SKU = txtSKU.Text;
                    myForm.Status = statusCode;
                    myForm.RequestedUser = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == requestedById);
                    myForm.Priority = ctx.Priorities.FirstOrDefault(x => x.RecordId == priorityId);
                    myForm.DueDate = dateDue;

                    if (ddlAssignedToEdit.SelectedIndex != -1)
                    {
                        myForm.AssignedUser = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == assignedToId);
                    }
                    else
                    {
                        myForm.AssignedUser = null;
                    }

                    myForm.LastModifiedTimestamp = DateTime.Now;
                    UserLogic newLogic = new UserLogic();
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    SystemUsers currentUser = newLogic.GetCurrentUser(user);
                    myForm.LastModifiedUser = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == currentUser.SystemUserID);

                    ctx.SaveChanges();

                    if (myForm.AssignedUser != null && !newLogic.HasBeenAssigned(myForm.AssignedUser, myForm.RecordId, "Cannot Wait For Container"))
                    {
                        UserAssignmentAssociation uA = new UserAssignmentAssociation
                        {
                            Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Cannot Wait For Container"),
                            RelatedFormId = myForm.RecordId,
                            User = myForm.AssignedUser
                        };

                        ctx.UserAssignments.Add(uA);
                    }

                    if (myForm.RequestedUser != null && !newLogic.HasBeenRequested(myForm.RequestedUser, myForm.RecordId, "Cannot Wait For Container"))
                    {
                        UserRequestAssociation uR = new UserRequestAssociation
                        {
                            Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Cannot Wait For Container"),
                            RelatedFormId = myForm.RecordId,
                            User = myForm.RequestedUser
                        };

                        ctx.UserRequests.Add(uR);
                    }

                    ctx.SaveChanges();

                    if (myForm.Status.StatusText == "Completed")
                    {
                        Comments updateComment = new Comments
                        {
                            Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Cannot Wait For Container"),
                            Note = "Request Completed By: " + currentUser.DisplayName,
                            RelatedFormId = myForm.RecordId,
                            SystemComment = true,
                            Timestamp = DateTime.Now
                        };

                        ctx.Comments.Add(updateComment);

                    }
                    else
                    {
                        Comments updateComment = new Comments
                        {
                            Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Cannot Wait For Container"),
                            Note = "Request Updated By: " + currentUser.DisplayName + " --- Status: " + myForm.Status.StatusText + " --- Priority: " + myForm.Priority.PriorityText,
                            RelatedFormId = myForm.RecordId,
                            SystemComment = true,
                            Timestamp = DateTime.Now
                        };

                        ctx.Comments.Add(updateComment);
                    }


                    if (lblEmailsSentTo.Text != "")
                    {
                        SendUpdateEmail(myForm.RecordId, lblEmailsSentTo.Text, cbSendComments.Checked, cbShowSystemComments.Checked);

                        Comments notificationComment = new Comments
                        {
                            Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Cannot Wait For Container"),
                            Note = "Request Notifications Sent To: " + lblEmailsSentTo.Text,
                            RelatedFormId = myForm.RecordId,
                            SystemComment = true,
                            Timestamp = DateTime.Now
                        };

                        ctx.Comments.Add(notificationComment);
                    }

                    ctx.SaveChanges();

                    cbNotifyAssignee.Checked = false;
                    cbNotifyOther.Checked = false;
                    cbNotifyRequester.Checked = false;
                    cbNotifyStandard.Checked = false;
                    cbSendComments.Checked = false;
                    ddlNotifyOther.Text = "";

                    gvReport.DataBind();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        protected void gvReport_NeedDataSource(object sender, GridNeedDataSourceEventArgs e){}

        protected void gvReport_PreRender(object sender, EventArgs e){}

        protected void gvReport_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

                RadComboBox ddlNotifyOther = userControl.FindControl("ddlNotifyOther") as RadComboBox;
                ddlNotifyOther.OpenDropDownOnLoad = false;

                FillEmailAddressLabels(editedItem);
            }
        }

        protected void gvReport_ItemCreated(object sender, GridItemEventArgs e){}

        protected void gvReport_ItemCommand(object sender, GridCommandEventArgs e){}

        public void UpdateReport() { }
    }
}