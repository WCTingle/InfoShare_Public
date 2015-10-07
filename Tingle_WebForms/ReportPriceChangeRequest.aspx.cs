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

namespace Tingle_WebForms
{
    public partial class ReportPriceChangeRequest : System.Web.UI.Page
    {
        FormContext ctx = new FormContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            UserLogic newLogic = new UserLogic();

            System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            SystemUsers currentUser = newLogic.GetCurrentUser(user);

            if (String.IsNullOrEmpty(gvReport.SortExpression)) gvReport.Sort("Timestamp", SortDirection.Descending);

            if (newLogic.HasAccess(currentUser, "Price Change Request"))
            {
                if (pnlDetails.Visible == true)
                {
                    RadComboBox ddlNotifyOther = (RadComboBox)fvReport.FindControl("ddlNotifyOther");
                    ddlNotifyOther.OpenDropDownOnLoad = false;
                }

                if (Page.IsPostBack && gvReport.Visible == true)
                {
                    gvReport.DataBind();
                }
                else
                {
                    if (Request.QueryString["formId"] != null)
                    {
                        ddlCompany.SelectedIndex = 0;
                        ddlGlobalStatus.SelectedIndex = -1;
                        pnlFilter.Visible = false;
                        pnlReport.Visible = false;
                        pnlDetails.Visible = true;
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

        public Tingle_WebForms.Models.PriceChangeRequestForm GetFormDetails([Control("gvReport")] int? RecordId, [QueryString("formId")] Nullable<Int32> formId)
        {
            var myForm = ctx.PriceChangeRequestForms.FirstOrDefault();

            if (RecordId == null)
            {
                if (formId != null)
                {
                    myForm = ctx.PriceChangeRequestForms.FirstOrDefault(f => f.RecordId == formId);
                }
            }
            else
            {
                myForm = ctx.PriceChangeRequestForms.FirstOrDefault(f => f.RecordId == RecordId);
            }

            return myForm;
        }

        public IEnumerable<Status> GetStatuses()
        {
            FormContext ctx = new FormContext();
            var StatusList = ctx.Statuses.ToList();

            return StatusList;
        }

        public IEnumerable<Status> GetStatusesEdit()
        {
            FormContext ctx = new FormContext();
            var StatusList = ctx.Statuses.ToList();

            return StatusList;
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

        public IEnumerable<PriceChangeRequestProducts> GetProducts()
        {
            using (FormContext ctx = new FormContext())
            {
                var prodList = ctx.PriceChangeRequestProducts.ToList();

                return prodList;
            }

        }

        public void FillEmailAddressLabels()
        {
            Label lblNotifyAssigneeValue = (Label)fvReport.FindControl("lblNotifyAssigneeValue");
            Label lblNotifyRequesterValue = (Label)fvReport.FindControl("lblNotifyRequesterValue");
            Label lblNotifyStandardValue = (Label)fvReport.FindControl("lblNotifyStandardValue");
            Label lblEmailsSentTo = (Label)fvReport.FindControl("lblEmailsSentTo");
            RadComboBox ddlAssignedToEdit = (RadComboBox)fvReport.FindControl("ddlAssignedToEdit");
            RadComboBox ddlRequestedByEdit = (RadComboBox)fvReport.FindControl("ddlRequestedByEdit");
            RadDropDownList ddlCompanyEdit = (RadDropDownList)fvReport.FindControl("ddlCompanyEdit");
            RadComboBox ddlNotifyOther = (RadComboBox)fvReport.FindControl("ddlNotifyOther");
            CheckBox cbNotifyStandard = (CheckBox)fvReport.FindControl("cbNotifyStandard");
            CheckBox cbNotifyAssignee = (CheckBox)fvReport.FindControl("cbNotifyAssignee");
            CheckBox cbNotifyRequester = (CheckBox)fvReport.FindControl("cbNotifyRequester");
            CheckBox cbNotifyOther = (CheckBox)fvReport.FindControl("cbNotifyOther");

            lblNotifyAssigneeValue.Text = ddlAssignedToEdit.SelectedIndex != -1 ? ddlAssignedToEdit.SelectedItem.Text : "";
            lblNotifyRequesterValue.Text = ddlRequestedByEdit.SelectedIndex != -1 ? ddlRequestedByEdit.SelectedItem.Text : "";
            lblNotifyStandardValue.Text = ddlCompanyEdit.SelectedText;

            List<String> listEmails = new List<String>();

            if (cbNotifyStandard.Checked)
            {
                using (FormContext ctx = new FormContext())
                {
                    if (ctx.EmailAddresses.Any(x => x.Status == 1 && x.TForm.FormName == "Price Change Request" && x.Company == ddlCompanyEdit.SelectedText))
                    {
                        ICollection<EmailAddress> emailAddresses = ctx.EmailAddresses.Where(x => x.Status == 1 && x.TForm.FormName == "Price Change Request" && x.Company == ddlCompanyEdit.SelectedText).ToList();

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

        public IQueryable<Tingle_WebForms.Models.PriceChangeRequestForm> GetPriceChangeRequestForms(
            [Control("txtFromDate")] string FromDate,
            [Control("txtToDate")] string ToDate,
            [Control("txtDueByDateFrom")] string DueByFrom,
            [Control("txtDueByDateTo")] string DueByTo,
            [Control("ddlCompany")] string Company,
            [Control("txtCustomer")] string Customer,
            [Control("txtLineNumber")] string LineNumber,
            [Control("txtAccountNumber")] string AccountNumber,
            [Control("txtQuantity")] string Quantity,
            [Control("txtSKU")] string Sku,
            [Control("txtSalesRep")] string SalesRep,
            [Control("txtMaterialSku")] string MaterialSku,
            [Control("ddlProduct")] Nullable<Int32> ProductId,
            [Control("txtOrderNumber")] string OrderNumber,
            [Control("txtPrice")] string Price,
            [Control("txtCrossRefOldOrderNumber")] string CrossReferenceOldOrderNumber,
            [Control("txtAdditionalInfo")] string AdditionalInfo,
            [Control("txtRequestHandler")] string RequestHandler,
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

            IQueryable<Tingle_WebForms.Models.PriceChangeRequestForm> PriceChangeRequestFormList = ctx.PriceChangeRequestForms.OrderByDescending(forms => forms.Timestamp);

            if (GlobalStatus != null)
            {
                if (GlobalStatus == "Active")
                {
                    PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.Status.StatusText != "Completed");
                }
                else if (GlobalStatus == "Archive")
                {
                    PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.Status.StatusText == "Completed");
                }
            }


            if (currentUser.UserRole.RoleName == "ReportsUser")
            {
                var reqHistory = ctx.UserRequests.Where(x => x.User.SystemUserID == currentUser.SystemUserID && x.Form.FormName == "Price Change Request");
                var assHistory = ctx.UserAssignments.Where(x => x.User.SystemUserID == currentUser.SystemUserID && x.Form.FormName == "Price Change Request");

                PriceChangeRequestFormList = PriceChangeRequestFormList
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
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.RecordId.Equals(id));
            }

            if (!String.IsNullOrWhiteSpace(FromDate))
            {
                DateTime.TryParse(FromDate, out dtFromDate);
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.Timestamp >= dtFromDate);
            }
            if (!String.IsNullOrWhiteSpace(ToDate))
            {
                DateTime.TryParse(ToDate, out dtToDate);
                dtToDate = dtToDate.AddDays(1);
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.Timestamp <= dtToDate);
            }
            if (!String.IsNullOrWhiteSpace(Company) && Company != "Any Company")
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.Company == Company);
            }
            if (!String.IsNullOrWhiteSpace(Customer))
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.Customer.Contains(Customer.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(LineNumber))
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.LineNumber.Contains(LineNumber.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(AccountNumber))
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.AccountNumber.Contains(AccountNumber.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(Quantity))
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.Quantity.Contains(Quantity.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(Sku))
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.SKU.Contains(Sku.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(SalesRep))
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.SalesRep.Contains(SalesRep.Trim()));
            }
            if (ProductId != null)
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.Product.RecordId == ProductId);
            }
            if (!String.IsNullOrWhiteSpace(OrderNumber))
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.OrderNumber.Contains(OrderNumber.Trim()));
            }
            if (!String.IsNullOrWhiteSpace(Price))
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.Price.Contains(Price));
            }
            if (!String.IsNullOrWhiteSpace(CrossReferenceOldOrderNumber))
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.CrossReferenceOldOrderNumber.Contains(CrossReferenceOldOrderNumber.Trim()));
            }
            if (StatusId != null)
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.Status.StatusId == StatusId);
            }
            if (!String.IsNullOrWhiteSpace(DueByFrom))
            {
                DateTime.TryParse(DueByFrom, out dtDueByFrom);
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.DueDate >= dtDueByFrom);
            }
            if (!String.IsNullOrWhiteSpace(DueByTo))
            {
                DateTime.TryParse(DueByTo, out dtDueByTo);
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.DueDate <= dtDueByTo);
            }
            if (RequestedById != null)
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.RequestedUser.SystemUserID == RequestedById);
            }
            if (AssignedToId != null)
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.AssignedUser.SystemUserID == AssignedToId);
            }
            if (PriorityId != null)
            {
                PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.Priority.RecordId == PriorityId);
            }

            if (Session["MyForms"] != null)
            {
                if (Session["MyForms"].ToString() == "Requested")
                {
                    PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.RequestedUser.SystemUserID == currentUser.SystemUserID);
                }
                else if (Session["MyForms"].ToString() == "Assigned")
                {
                    PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.AssignedUser.SystemUserID == currentUser.SystemUserID);
                }
                else if (Session["MyForms"].ToString() == "Created")
                {
                    PriceChangeRequestFormList = PriceChangeRequestFormList.Where(forms => forms.SubmittedUser.SystemUserID == currentUser.SystemUserID);
                }
            }

            return PriceChangeRequestFormList;
        }

        protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                pnlDetails.Visible = true;
                pnlReport.Visible = false;
                pnlFilter.Visible = false;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            pnlDetails.Visible = false;
            pnlReport.Visible = true;
            pnlFilter.Visible = true;
            gvReport.DataBind();
        }

        protected void fvReport_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            if (IsValid)
            {
                int id = Convert.ToInt32(((Label)fvReport.FindControl("lblRecordId")).Text);

                Tingle_WebForms.Models.PriceChangeRequestForm myForm = ctx.PriceChangeRequestForms.FirstOrDefault(eof => eof.RecordId == id);

                RadDropDownList ddlCompanyEdit = (RadDropDownList)fvReport.FindControl("ddlCompanyEdit");
                TextBox txtCustomer = (TextBox)fvReport.FindControl("txtCustomerEdit");
                TextBox txtLineNumber = (TextBox)fvReport.FindControl("txtLineNumberEdit");
                TextBox txtAccountNumber = (TextBox)fvReport.FindControl("txtAccountNumberEdit");
                TextBox txtQuantity = (TextBox)fvReport.FindControl("txtQuantityEdit");
                TextBox txtSKU = (TextBox)fvReport.FindControl("txtSKUEdit");
                TextBox txtSalesRep = (TextBox)fvReport.FindControl("txtSalesRepEdit");
                RadDropDownList ddlProduct = (RadDropDownList)fvReport.FindControl("ddlProductEdit");
                TextBox txtOrderNumber = (TextBox)fvReport.FindControl("txtOrderNumberEdit");
                TextBox txtPrice = (TextBox)fvReport.FindControl("txtPriceEdit");
                TextBox txtCrossRefOldOrderNumber = (TextBox)fvReport.FindControl("txtCrossRefOldOrderNumberEdit");
                HtmlInputText txtDueByDate = (HtmlInputText)fvReport.FindControl("txtDueByDateEdit");
                RadDropDownList ddlStatus = (RadDropDownList)fvReport.FindControl("ddlStatusEdit");
                int statusId = Convert.ToInt32(ddlStatus.SelectedValue);
                RadComboBox ddlRequestedByEdit = (RadComboBox)fvReport.FindControl("ddlRequestedByEdit");
                int requestedById = Convert.ToInt32(ddlRequestedByEdit.SelectedValue);
                RadComboBox ddlAssignedToEdit = (RadComboBox)fvReport.FindControl("ddlAssignedToEdit");
                int assignedToId = 0;
                if (ddlAssignedToEdit.SelectedIndex != -1)
                {
                    assignedToId = Convert.ToInt32(ddlAssignedToEdit.SelectedValue);
                }
                RadDropDownList ddlPriorityEdit = (RadDropDownList)fvReport.FindControl("ddlPriorityEdit");
                int priorityId = Convert.ToInt32(ddlPriorityEdit.SelectedValue);
                CheckBox cbSendComments = (CheckBox)fvReport.FindControl("cbSendComments");
                CheckBox cbShowSystemComments = (CheckBox)fvReport.FindControl("cbShowSystemComments");
                CheckBox cbNotifyStandard = (CheckBox)fvReport.FindControl("cbNotifyStandard");
                CheckBox cbNotifyAssignee = (CheckBox)fvReport.FindControl("cbNotifyAssignee");
                CheckBox cbNotifyOther = (CheckBox)fvReport.FindControl("cbNotifyOther");
                CheckBox cbNotifyRequester = (CheckBox)fvReport.FindControl("cbNotifyRequester");
                RadComboBox ddlNotifyOther = (RadComboBox)fvReport.FindControl("ddlNotifyOther");

                Label lblEmailsSentTo = (Label)fvReport.FindControl("lblEmailsSentTo");
                Label lblFVMessage = (Label)fvReport.FindControl("lblFVMessage");

                DateTime tryDateDue;
                Nullable<DateTime> dateDue = null;

                try
                {
                    if (myForm.RequestedUser.SystemUserID.ToString() != ddlRequestedByEdit.SelectedValue)
                    {
                        Comments newRequesterComment = new Comments
                        {
                            Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Price Change Request"),
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
                            Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Price Change Request"),
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
                                Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Price Change Request"),
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
                            Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Price Change Request"),
                            Note = "Request Assignment Removed From: " + ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == myForm.AssignedUser.SystemUserID).DisplayName,
                            RelatedFormId = myForm.RecordId,
                            SystemComment = true,
                            Timestamp = DateTime.Now
                        };

                        ctx.Comments.Add(newAssignedComment);
                    }

                    string accountNumber = txtAccountNumber.Text;
                    int accountNumberLength = txtAccountNumber.Text.Length;

                    while (accountNumberLength < 6)
                    {
                        accountNumber = "0" + accountNumber;
                        accountNumberLength++;
                    }

                    if (txtDueByDate.Value != null)
                    {
                        DateTime.TryParse(txtDueByDate.Value.ToString(), out tryDateDue);
                        if (tryDateDue.Year > 0001)
                        {
                            dateDue = tryDateDue;
                        }
                    }

                    var statusCode = ctx.Statuses.FirstOrDefault(s => s.StatusId == statusId);

                    myForm.Company = ddlCompanyEdit.SelectedValue;
                    myForm.Customer = txtCustomer.Text;
                    myForm.LineNumber = txtLineNumber.Text;
                    myForm.AccountNumber = txtAccountNumber.Text;
                    myForm.Quantity = txtQuantity.Text;
                    myForm.SKU = txtSKU.Text;
                    myForm.SalesRep = txtSalesRep.Text;
                    myForm.Product = ctx.PriceChangeRequestProducts.FirstOrDefault(x => x.ProductText == ddlProduct.SelectedText);
                    myForm.OrderNumber = txtOrderNumber.Text;
                    myForm.Price = txtPrice.Text;
                    myForm.CrossReferenceOldOrderNumber = txtCrossRefOldOrderNumber.Text;
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

                    ctx.PriceChangeRequestForms.Attach(myForm);
                    ctx.Entry(myForm).State = EntityState.Modified;

                    ctx.SaveChanges();

                    if (myForm.AssignedUser != null && !newLogic.HasBeenAssigned(myForm.AssignedUser, myForm.RecordId, "Price Change Request"))
                    {
                        UserAssignmentAssociation uA = new UserAssignmentAssociation
                        {
                            Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Price Change Request"),
                            RelatedFormId = myForm.RecordId,
                            User = myForm.AssignedUser
                        };

                        ctx.UserAssignments.Add(uA);
                    }

                    if (myForm.RequestedUser != null && !newLogic.HasBeenRequested(myForm.RequestedUser, myForm.RecordId, "Price Change Request"))
                    {
                        UserRequestAssociation uR = new UserRequestAssociation
                        {
                            Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Price Change Request"),
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
                            Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Price Change Request"),
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
                            Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Price Change Request"),
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
                            Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Price Change Request"),
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
                    ddlNotifyOther.ClearSelection();
                    ddlNotifyOther.Text = "";

                    gvReport.DataBind();
                    gvReport.SelectedIndex = -1;
                    pnlDetails.Visible = false;
                    pnlReport.Visible = true;
                    pnlFilter.Visible = true;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public void SendUpdateEmail(Int32 FormId, string EmailList, Boolean SendComments, Boolean IncludeSystemComments)
        {
            Tingle_WebForms.Models.PriceChangeRequestForm myForm = ctx.PriceChangeRequestForms.FirstOrDefault(eof => eof.RecordId == FormId);
            TForm submittedForm = ctx.TForms.FirstOrDefault(tf => tf.FormName == "Price Change Request");
            Boolean anyComments = false;
            IEnumerable<Comments> finalComments = null;

            string dueDateString = "";

            if (myForm.DueDate != null)
            {
                dueDateString = myForm.DueDate.Value.ToShortDateString();
            }

            if (IncludeSystemComments)
            {
                if (ctx.Comments.Any(x => x.Form.FormName == "Price Change Request" && x.RelatedFormId == myForm.RecordId))
                {
                    anyComments = true;

                    IEnumerable<Comments> commentsList = ctx.Comments
                        .Where(x => x.Form.FormName == "Price Change Request" && x.RelatedFormId == myForm.RecordId)
                        .Include(x => x.User)
                        .OrderByDescending(x => x.RecordId)
                        .ToList();

                    finalComments = commentsList;
                }
            }
            else
            {
                if (ctx.Comments.Any(x => x.Form.FormName == "Price Change Request" && x.RelatedFormId == myForm.RecordId && x.SystemComment == false))
                {
                    anyComments = true;

                    IEnumerable<Comments> commentsList = ctx.Comments
                        .Where(x => x.Form.FormName == "Price Change Request" && x.RelatedFormId == myForm.RecordId && x.SystemComment == false)
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
                .AppendLine("        <td colspan=\"4\" style=\"text-align: center;vertical-align: middle;font-weight: bold;font-size: 20px;border: 4px solid #d0604c; color:#FFF; background-color:#bc4445;\">Price Change Request Update</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Company:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Company).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;\"></td>")
                .AppendLine("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;\"></td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Customer:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Customer).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Line #:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.LineNumber).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Account Number:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.AccountNumber).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Product:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Product.ProductText).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Sales Rep:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.SalesRep).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Material SKU:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.SKU).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Reference / Order #:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.OrderNumber).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Quantity:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Quantity).AppendLine("</td>")
                .AppendLine("    </tr>")
                .AppendLine("    <tr>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Cross Reference Old Order #:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.CrossReferenceOldOrderNumber).AppendLine("</td>")
                .AppendLine("        <td style=\"text-align:right;font-size:16px;font-weight:bold;width:25%;color:#bc4445;\">Price:</td>")
                .Append("        <td style=\"text-align:left;font-size:16px;font-weight:bold;width:25%;color:#000;\">").Append(myForm.Price).AppendLine("</td>")
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

            bool result = msg.SendMail("InfoShare@wctingle.com", emailList, "Price Change Request Update", bodyHtml.ToString(), submittedForm, myForm.RecordId, currentUser);

        }

        public void UpdateForm(int RecordId)
        {



        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDetails.Visible = false;
            pnlReport.Visible = true;
            pnlFilter.Visible = true;
            gvReport.DataBind();
            gvReport.SelectedIndex = -1;
        }

        protected void fvReport_DataBound(object sender, EventArgs e)
        {

        }

        protected void fvReport_DataBinding(object sender, EventArgs e)
        {
        }

        protected void fvReport_PreRender(object sender, EventArgs e)
        {
            Button btnUpdate = (Button)fvReport.FindControl("btnUpdate");
            Button btnCancel = (Button)fvReport.FindControl("btnCancel");
            Button btnBack = (Button)fvReport.FindControl("btnDetailsBack");

            System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            UserLogic uLogic = new UserLogic();
            SystemUsers currentUser = uLogic.GetCurrentUser(user);

            RadDropDownList ddlCompanyEdit = (RadDropDownList)fvReport.FindControl("ddlCompanyEdit");
            Label lblCompanyEdit = (Label)fvReport.FindControl("lblCompanyEdit");
            TextBox txtCustomerEdit = (TextBox)fvReport.FindControl("txtCustomerEdit");
            TextBox txtLineNumberEdit = (TextBox)fvReport.FindControl("txtLineNumberEdit");
            TextBox txtAccountNumberEdit = (TextBox)fvReport.FindControl("txtAccountNumberEdit");
            TextBox txtQuantityEdit = (TextBox)fvReport.FindControl("txtQuantityEdit");
            TextBox txtSKUEdit = (TextBox)fvReport.FindControl("txtSKUEdit");
            TextBox txtSalesRepEdit = (TextBox)fvReport.FindControl("txtSalesRepEdit");
            RadDropDownList ddlProductEdit = (RadDropDownList)fvReport.FindControl("ddlProductEdit");
            TextBox txtOrderNumberEdit = (TextBox)fvReport.FindControl("txtOrderNumberEdit");
            TextBox txtPriceEdit = (TextBox)fvReport.FindControl("txtPriceEdit");
            TextBox txtCrossRefOldOrderNumberEdit = (TextBox)fvReport.FindControl("txtCrossRefOldOrderNumberEdit");
            TextBox txtAdditionalInfoEdit = (TextBox)fvReport.FindControl("txtAdditionalInfoEdit");
            TextBox txtRequestHandlerEdit = (TextBox)fvReport.FindControl("txtRequestHandlerEdit");
            TextBox txtCompletionNotes = (TextBox)fvReport.FindControl("txtCompletionNotes");
            TextBox txtCCCompletedFormToEmail = (TextBox)fvReport.FindControl("txtCCCompletedFormToEmail");
            RadDropDownList ddlStatusEdit = (RadDropDownList)fvReport.FindControl("ddlStatusEdit");
            RadComboBox ddlRequestedByEdit = (RadComboBox)fvReport.FindControl("ddlRequestedByEdit");
            RadComboBox ddlAssignedToEdit = (RadComboBox)fvReport.FindControl("ddlAssignedToEdit");
            RadDropDownList ddlPriorityEdit = (RadDropDownList)fvReport.FindControl("ddlPriorityEdit");
            HtmlInputText txtDueByDateEdit = (HtmlInputText)fvReport.FindControl("txtDueByDateEdit");
            CheckBox cbNotifyStandard = (CheckBox)fvReport.FindControl("cbNotifyStandard");
            CheckBox cbNotifyAssignee = (CheckBox)fvReport.FindControl("cbNotifyAssignee");
            CheckBox cbSendComments = (CheckBox)fvReport.FindControl("cbSendComments");
            CheckBox cbNotifyOther = (CheckBox)fvReport.FindControl("cbNotifyOther");
            CheckBox cbNotifyRequester = (CheckBox)fvReport.FindControl("cbNotifyRequester");
            RadComboBox ddlNotifyOther = (RadComboBox)fvReport.FindControl("ddlNotifyOther");
            RadButton btnAddNewEmail = (RadButton)fvReport.FindControl("btnAddNewEmail");
            Button btnAddComment = (Button)fvReport.FindControl("btnAddComment");

            FillEmailAddressLabels();

            Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
            int recordId;
            Int32.TryParse(lblRecordId.Text, out recordId);
            Boolean isComplete;

            using (var ctx = new FormContext())
            {
                var thisForm = ctx.PriceChangeRequestForms.FirstOrDefault(eof => eof.RecordId == recordId);

                isComplete = thisForm.Status.StatusText == "Completed" ? true : false;
            }

            if (currentUser.UserRole.RoleName == "ReportsUser" && isComplete)
            {
                ddlCompanyEdit.Enabled = false;
                txtCustomerEdit.Enabled = false;
                txtLineNumberEdit.Enabled = false;
                txtAccountNumberEdit.Enabled = false;
                txtQuantityEdit.Enabled = false;
                txtSKUEdit.Enabled = false;
                txtSalesRepEdit.Enabled = false;
                ddlProductEdit.Enabled = false;
                txtOrderNumberEdit.Enabled = false;
                txtPriceEdit.Enabled = false;
                txtCrossRefOldOrderNumberEdit.Enabled = false;
                ddlStatusEdit.Enabled = false;
                ddlRequestedByEdit.Enabled = false;
                ddlAssignedToEdit.Enabled = false;
                ddlPriorityEdit.Enabled = false;
                txtDueByDateEdit.Disabled = true;
                cbNotifyStandard.Enabled = false;
                cbNotifyAssignee.Enabled = false;
                cbSendComments.Enabled = false;
                cbNotifyOther.Enabled = false;
                cbNotifyRequester.Enabled = false;
                ddlNotifyOther.Enabled = false;
                btnAddNewEmail.Enabled = false;
                btnAddComment.Enabled = false;
                btnUpdate.Enabled = false;
            }

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
            tr4.Visible = true;
            tr5.Visible = true;
            btnAdvancedSearch.Visible = false;
            btnBasicSearch.Visible = true;
        }

        public void HideAdvanced()
        {
            tr1.Visible = false;
            tr2.Visible = false;
            tr3.Visible = false;
            tr4.Visible = false;
            tr5.Visible = false;
            btnAdvancedSearch.Visible = true;
            btnBasicSearch.Visible = false;
        }

        protected void btnCancel2_Click(object sender, EventArgs e)
        {
            PlaceHolder phFVCompleted = (PlaceHolder)fvReport.FindControl("phFVCompleted");
            PlaceHolder phFVDetails = (PlaceHolder)fvReport.FindControl("phFVDetails");
            HiddenField hfCompleted = (HiddenField)fvReport.FindControl("hfCompleted");

            phFVCompleted.Visible = false;
            phFVDetails.Visible = true;
            hfCompleted.Value = "0";
        }

        protected void ddlStatusEdit_DataBound(object sender, EventArgs e)
        {
            try
            {
                RadDropDownList ddlStatus = (RadDropDownList)sender;
                Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
                int recordId;
                Int32.TryParse(lblRecordId.Text, out recordId);

                using (var ctx = new FormContext())
                {
                    var thisForm = ctx.PriceChangeRequestForms.FirstOrDefault(eof => eof.RecordId == recordId);

                    ddlStatus.SelectedValue = thisForm.Status.StatusId.ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnInsertEmail_Click(object sender, EventArgs e)
        {
            RadTextBox txtNameInsert = (RadTextBox)fvReport.FindControl("txtNameInsert");
            RadTextBox txtAddressInsert = (RadTextBox)fvReport.FindControl("txtAddressInsert");
            RadComboBox ddlNotifyOther = (RadComboBox)fvReport.FindControl("ddlNotifyOther");
            RadioButtonList rblNotificationEmailStatusInsert = (RadioButtonList)fvReport.FindControl("rblNotificationEmailStatusInsert");
            Label lblInsertEmailMessage = (Label)fvReport.FindControl("lblInsertEmailMessage");

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

        protected void cbNotifyRequester_CheckedChanged(object sender, EventArgs e)
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
            RadComboBox ddlNotifyOther = (RadComboBox)sender;
            ddlNotifyOther.OpenDropDownOnLoad = true;
        }

        protected void ddlNotifyOther_CheckAllCheck(object sender, Telerik.Web.UI.RadComboBoxCheckAllCheckEventArgs e)
        {
            FillEmailAddressLabels();
            RadComboBox ddlNotifyOther = (RadComboBox)sender;
            ddlNotifyOther.OpenDropDownOnLoad = true;

        }

        protected void ddlCompanyEdit_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            FillEmailAddressLabels();
        }

        protected void ddlCompanyEdit_DataBinding(object sender, EventArgs e)
        {
            try
            {
                RadDropDownList ddlCompanyEdit = (RadDropDownList)sender;
                Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
                int recordId;
                Int32.TryParse(lblRecordId.Text, out recordId);

                using (var ctx = new FormContext())
                {
                    var thisForm = ctx.PriceChangeRequestForms.FirstOrDefault(eof => eof.RecordId == recordId);

                    ddlCompanyEdit.SelectedValue = thisForm.Company;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnAddComment_Click(object sender, EventArgs e)
        {
            try
            {
                Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
                int recordId;
                Int32.TryParse(lblRecordId.Text, out recordId);
                RadTextBox txtNewComment = (RadTextBox)fvReport.FindControl("txtNewComment");
                Repeater rptrComments = (Repeater)fvReport.FindControl("rptrComments");

                UserLogic newLogic = new UserLogic();

                System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                SystemUsers currentUser = newLogic.GetCurrentUser(user);

                using (var ctx = new FormContext())
                {
                    var thisForm = ctx.PriceChangeRequestForms.FirstOrDefault(eof => eof.RecordId == recordId);

                    Comments newComment = new Comments
                    {
                        Form = ctx.TForms.FirstOrDefault(x => x.FormName == "Price Change Request"),
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
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Comments> rptrComments_GetData()
        {
            try
            {
                Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
                int recordId;
                Int32.TryParse(lblRecordId.Text, out recordId);

                Repeater rptrComments = (Repeater)fvReport.FindControl("rptrComments");

                using (FormContext ctx = new FormContext())
                {
                    if (ctx.Comments.Any(x => x.Form.FormName == "Price Change Request" && x.RelatedFormId == recordId))
                    {
                        IEnumerable<Comments> commentsList = ctx.Comments
                            .Where(x => x.Form.FormName == "Price Change Request" && x.RelatedFormId == recordId)
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

        protected void ddlPriority_DataBound(object sender, EventArgs e)
        {
            try
            {
                RadDropDownList ddlPriority = (RadDropDownList)sender;
                Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
                int recordId;
                Int32.TryParse(lblRecordId.Text, out recordId);

                ddlPriority.SelectedText = "Normal";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void ddlPriorityEdit_DataBound(object sender, EventArgs e)
        {
            try
            {
                RadDropDownList ddlPriorityEdit = (RadDropDownList)sender;
                Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
                int recordId;
                Int32.TryParse(lblRecordId.Text, out recordId);

                using (var ctx = new FormContext())
                {
                    var thisForm = ctx.PriceChangeRequestForms.FirstOrDefault(eof => eof.RecordId == recordId);

                    ddlPriorityEdit.SelectedValue = thisForm.Priority.RecordId.ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void ddlAssignedToEdit_DataBound(object sender, EventArgs e)
        {
            try
            {
                RadComboBox ddlAssignedToEdit = (RadComboBox)sender;
                Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
                int recordId;
                Int32.TryParse(lblRecordId.Text, out recordId);

                using (var ctx = new FormContext())
                {
                    var thisForm = ctx.PriceChangeRequestForms.FirstOrDefault(eof => eof.RecordId == recordId);

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

        protected void ddlRequestedByEdit_DataBound(object sender, EventArgs e)
        {
            try
            {
                RadComboBox ddlRequestedByEdit = (RadComboBox)sender;
                Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
                int recordId;
                Int32.TryParse(lblRecordId.Text, out recordId);

                using (var ctx = new FormContext())
                {
                    var thisForm = ctx.PriceChangeRequestForms.FirstOrDefault(eof => eof.RecordId == recordId);

                    ddlRequestedByEdit.SelectedValue = thisForm.RequestedUser.SystemUserID.ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void ddlProductEdit_DataBinding(object sender, EventArgs e)
        {
            try
            {
                RadDropDownList ddlProductEdit = (RadDropDownList)sender;
                Label lblRecordId = (Label)fvReport.FindControl("lblRecordId");
                int recordId;
                Int32.TryParse(lblRecordId.Text, out recordId);

                using (var ctx = new FormContext())
                {
                    var thisForm = ctx.PriceChangeRequestForms.FirstOrDefault(eof => eof.RecordId == recordId);

                    ddlProductEdit.SelectedValue = thisForm.Product.RecordId.ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnResetFilters_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect(Request.RawUrl);
        }

        protected void cvRequestedBy_ServerValidate(object source, ServerValidateEventArgs args)
        {
            RadComboBox ddlRequestedByEdit = (RadComboBox)fvReport.FindControl("ddlRequestedByEdit");

            if (ddlRequestedByEdit.FindItemByText(ddlRequestedByEdit.Text) != null)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void cvAssignedTo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            RadComboBox ddlAssignedToEdit = (RadComboBox)fvReport.FindControl("ddlAssignedToEdit");

            if (ddlAssignedToEdit.FindItemByText(ddlAssignedToEdit.Text) != null)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblRecordId = (Label)e.Row.FindControl("lblRecordId");
                    RadToolTip ttCustomer = (RadToolTip)e.Row.FindControl("ttCustomer");

                    string ltlCustomerHtml = "";

                    int recordId = Convert.ToInt32(lblRecordId.Text);

                    using (FormContext ctx = new FormContext())
                    {
                        var thisForm = ctx.PriceChangeRequestForms.FirstOrDefault(x => x.RecordId == recordId);

                        ltlCustomerHtml += "<div style=\"border: 4px solid #d0604c; background-color: #FFF;\">Account Number: " + thisForm.AccountNumber + "<br /> Sales Rep: " + thisForm.SalesRep
                            + "<br />Quantity: " + thisForm.Quantity + "<br />Price: " + thisForm.Price + "</div>";

                        ttCustomer.Text = ltlCustomerHtml;
                    }
                }
            }
            catch
            {

            }
        }

        public string LoadCommentLiteral(Boolean SystemComment, String Note, String DisplayName, String Timestamp)
        {
            try
            {
                if (SystemComment == false)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("<div style=\"width: 80%; max-width: 800px; background-color: #bc4445; margin: 0 auto; text-align: left; font-size: 14px; color: white; overflow: hidden; word-wrap: break-word;\">")
                        .AppendLine("<div style=\"width: 95%; padding: 5px;\">")
                        .Append(Note).AppendLine("<br /><br /><span style=\"padding-right: 20px\">by")
                        .Append(DisplayName).Append("</span>").Append(Timestamp).AppendLine("</div></div><br />");

                    return sb.ToString();
                }
                else
                {
                    StringBuilder sb = new StringBuilder();

                    CheckBox cbShowSystemComments = (CheckBox)fvReport.FindControl("cbShowSystemComments");

                    if (cbShowSystemComments.Checked)
                    {
                        sb.AppendLine("<div style=\"width: 80%; max-width: 800px; background-color: #FFF; margin: 0 auto; text-align: right; font-size: 12px; color: #bc4445; overflow: hidden; word-wrap: break-word;\">")
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

        protected void cbShowSystemComments_CheckedChanged(object sender, EventArgs e)
        {
            Repeater rptrComments = (Repeater)fvReport.FindControl("rptrComments");

            rptrComments.DataBind();
        }

    }
}