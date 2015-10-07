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
using System.Collections;


namespace Tingle_WebForms
{
    public partial class InventoryApproval : System.Web.UI.Page
    {
        FormContext _ctx = new FormContext();
        Boolean isExport = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            LoadCurrentStock();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            UserLogic uLogic = new UserLogic();
            SystemUsers currentUser = uLogic.GetCurrentUser(user);

            if (currentUser.InventoryApprovalUser == null || currentUser.InventoryApprovalUser == false)
            {
                Response.Redirect("/UserDashboard");
            }
            else
            {
                LoadVendorStock();

                ddlNotifyOther.OpenDropDownOnLoad = false;

                if (!IsPostBack)
                {
                    FillEmailAddressLabels();

                    cbNotifyStandard.Checked = false;
                    cbNotifyOther.Checked = false;

                    ddlCompanyGlobal.SelectedIndex = 0;
                    ddlVendorsGlobal.SelectedValue = "All";
                    ddlOverLastGlobal.SelectedIndex = 0;
                    ddlCompany.SelectedIndex = 0;
                }
            }

        }

        public void FillEmailAddressLabels()
        {
            List<String> listEmails = new List<String>();

            if (cbNotifyStandard.Checked)
            {
                using (FormContext ctx = new FormContext())
                {
                    if (ctx.InventoryNotificationEmailAddresses.Any(x => x.Status == 1))
                    {
                        ICollection<InventoryNotificationEmails> emailAddresses = ctx.InventoryNotificationEmailAddresses.Where(x => x.Status == 1).ToList();

                        if (emailAddresses.Count() > 0)
                        {
                            foreach (InventoryNotificationEmails email in emailAddresses)
                            {
                                listEmails.Add(email.Address);
                            }
                        }
                    }
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

        protected void LoadCurrentStock()
        {
            try
            {
                phCurrentStock.Controls.AddAt(0, new LiteralControl("<div style=\"width:100%; text-align:center\"><p style=\"width:100%; text-align:center;\">Current Vendor Stock</p><p style=\"width:100%; text-align:center; font-size:8px; font-style: italic;\">(Numbers and Decimals only)</p>"));

                using (FormContext ctx = new FormContext())
                {
                    if (ctx.InventoryApprovalItems.Any(x => x.Status.StatusDescription == "Pending Approval"))
                    {
                        lblPendingTotalGrid.Text = "Pending Total: " + ctx.InventoryApprovalItems.Where(x => x.Status.StatusDescription == "Pending Approval").Sum(x => x.Cost).ToString("c2");
                    }
                    else
                    {
                        lblPendingTotalGrid.Text = "Pending Total: $0";
                    }

                    if (ctx.InventoryApprovalItems.Any(x => x.Status.StatusDescription == "Approved"))
                    {
                        lblApprovedTotalGrid.Text = "Approved Total: " + ctx.InventoryApprovalItems.Where(x => x.Status.StatusDescription == "Approved").Sum(x => x.Cost).ToString("c2");
                    }
                    else
                    {
                        lblPendingTotalGrid.Text = "Approved Total: $0";
                    }

                    if (ctx.InventoryApprovalItems.Any(x => x.Status.StatusDescription == "Ordered"))
                    {
                        lblOrderedTotalGrid.Text = "Ordered Total: " + ctx.InventoryApprovalItems.Where(x => x.Status.StatusDescription == "Ordered").Sum(x => x.Cost).ToString("c2");
                    }
                    else
                    {
                        lblPendingTotalGrid.Text = "Ordered Total: $0";
                    }

                    if (ctx.InventoryApprovalItems.Any(x => x.Status.StatusDescription == "Arrived"))
                    {
                        lblArrivedTotalGrid.Text = "Arrived Total: " + ctx.InventoryApprovalItems.Where(x => x.Status.StatusDescription == "Arrived").Sum(x => x.Cost).ToString("c2");
                    }
                    else
                    {
                        lblPendingTotalGrid.Text = "Arrived Total: $0";
                    }

                    if (ctx.InventoryApprovalItems.Any(x => x.Status.StatusDescription == "Invoiced"))
                    {
                        lblInvoicedTotalGrid.Text = "Invoiced Total: " + ctx.InventoryApprovalItems.Where(x => x.Status.StatusDescription == "Invoiced").Sum(x => x.Cost).ToString("c2");
                    }
                    else
                    {
                        lblPendingTotalGrid.Text = "Invoiced Total: $0";
                    }


                    if (ctx.Vendors.Any())
                    {
                        foreach (Vendor vendor in ctx.Vendors)
                        {
                            RadNumericTextBox txtBox = new RadNumericTextBox();
                            txtBox.ID = "txt" + vendor.VendorName.Replace(" ", "");
                            txtBox.Text = vendor.CurrentStock == null ? "0" : vendor.CurrentStock.ToString();
                            txtBox.MaxValue = 1000000000;
                            txtBox.Width = Unit.Pixel(100);
                            txtBox.SelectionOnFocus = SelectionOnFocus.None;
                            txtBox.Type = NumericType.Currency;
                            txtBox.NumberFormat.DecimalDigits = 0;
                            txtBox.NumberFormat.GroupSeparator = ",";

                            phCurrentStock.Controls.Add(new LiteralControl("<div style=\"width:40%; float:left\">"));
                            phCurrentStock.Controls.Add(new LiteralControl(vendor.VendorName));
                            phCurrentStock.Controls.Add(new LiteralControl("</div>"));
                            phCurrentStock.Controls.Add(new LiteralControl("<div style=\"width:40%; float:left;\">"));
                            phCurrentStock.Controls.Add(txtBox);
                            phCurrentStock.Controls.Add(new LiteralControl("</div>"));
                        }

                    }
                }

                phCurrentStock.Controls.Add(new LiteralControl("</div"));
            }
            catch(Exception ex)
            {
            }

            
        }

        public decimal GetCurrentStockValues(string statusDesc, FormContext context, string vendorName)
        {
            try
            {
                var list = context.InventoryApprovalItems
                    .Where(x => x.Status.StatusDescription == statusDesc && x.Vendor.VendorName == vendorName && x.Company == ddlCompanyGlobal.SelectedText);
                    
                if (ddlOverLastGlobal.SelectedIndex > 0)
                {
                    int overLast = Convert.ToInt32(ddlOverLastGlobal.SelectedValue);

                    if (statusDesc == "Pending Approval")
                    {
                        list = list.Where(x => x.Timestamp > DbFunctions.AddDays(DateTime.Now, -1 * overLast));
                    }
                    else if (statusDesc == "Approved")
                    {
                        list = list.Where(x => x.ApprovedDate > DbFunctions.AddDays(DateTime.Now, -1 * overLast));
                    }
                    else if (statusDesc == "Ordered")
                    {
                        list = list.Where(x => x.OrderDate > DbFunctions.AddDays(DateTime.Now, -1 * overLast));
                    }
                    else if (statusDesc == "Arrived")
                    {
                        list = list.Where(x => x.ArrivalDate > DbFunctions.AddDays(DateTime.Now, -1 * overLast));
                    }
                    else if (statusDesc == "Invoiced")
                    {
                        list = list.Where(x => x.InvoiceDate > DbFunctions.AddDays(DateTime.Now, -1 * overLast));
                    }
                }

                return list.Sum(x => x.Cost);
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        private IQueryable<InventoryApprovalForm> GetInventoryList(string statusDesc, FormContext context)
        {
            var list = context.InventoryApprovalItems.Where(x => x.Status.StatusDescription == statusDesc);

            if (ddlCompanyGlobal.SelectedIndex != -1)
            {
                list = list.Where(x => x.Company == ddlCompanyGlobal.SelectedText);
            }

            if (ddlVendorsGlobal.SelectedIndex > 0)
            {
                list = list.Where(x => x.Vendor.VendorName == ddlVendorsGlobal.SelectedText);
            }

            if (ddlOverLastGlobal.SelectedIndex > 0)
            {
                int overLast = Convert.ToInt32(ddlOverLastGlobal.SelectedValue);

                list = list.Where(x => x.Timestamp > DbFunctions.AddDays(DateTime.Now, -1 * overLast));
            }

            return list;
        }

        protected void lblPendingApprovalCount_PreRender(object sender, EventArgs e)
        {
            try
            {
                var list = GetInventoryList("Pending Approval", _ctx);

                lblPendingApprovalCount.Text = list.Count().ToString();
            }
            catch(Exception ex)
            {
                lblPendingApprovalCount.Text = "0";
            }
        }

        protected void lblPendingApprovalTotal_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (_ctx.InventoryApprovalItems.Where(x => x.Status.StatusDescription == "Pending Approval").Any())
                {
                    var list = GetInventoryList("Pending Approval", _ctx);

                    lblPendingApprovalTotal.Text = list.Sum(y => y.Cost).ToString("c0");
                }
                else
                {
                    lblPendingApprovalTotal.Text = "$0";
                }
            }
            catch (Exception ex)
            {
                lblPendingApprovalTotal.Text = "$0";
            }
        }

        protected void lblPendingApprovalOldest_PreRender(object sender, EventArgs e)
        {
            try
            {
                var list = GetInventoryList("Pending Approval", _ctx);

                lblPendingApprovalOldest.Text = list.OrderBy(x => x.Timestamp).FirstOrDefault().Timestamp.ToShortDateString();
            }
            catch (Exception ex)
            {
                lblPendingApprovalOldest.Text = "";
            }
        }

        protected void lblApprovedCount_PreRender(object sender, EventArgs e)
        {
            try
            {
                var list = GetInventoryList("Approved", _ctx);

                lblApprovedCount.Text = list.Count().ToString();
            }
            catch (Exception ex)
            {
                lblApprovedCount.Text = "0";
            }
        }

        protected void lblApprovedTotal_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (_ctx.InventoryApprovalItems.Where(x => x.Status.StatusDescription == "Approved").Any())
                {
                    var list = GetInventoryList("Approved", _ctx);

                    lblApprovedTotal.Text = list.Sum(y => y.Cost).ToString("c0");
                }
                else
                {
                    lblApprovedTotal.Text = "$0";
                }
            }
            catch (Exception ex)
            {
                lblApprovedTotal.Text = "$0";
            }
        }

        protected void lblApprovedOldest_PreRender(object sender, EventArgs e)
        {
            try
            {
                var list = GetInventoryList("Approved", _ctx);

                lblApprovedOldest.Text = list.OrderBy(x => x.Timestamp).FirstOrDefault().Timestamp.ToShortDateString();
            }
            catch (Exception ex)
            {
                lblApprovedOldest.Text = "";
            }
        }

        protected void lblOrderedCount_PreRender(object sender, EventArgs e)
        {
            try
            {
                var list = GetInventoryList("Ordered", _ctx);

                lblOrderedCount.Text = list.Count().ToString();
            }
            catch (Exception ex)
            {
                lblOrderedCount.Text = "0";
            }
        }

        protected void lblOrderedTotal_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (_ctx.InventoryApprovalItems.Where(x => x.Status.StatusDescription == "Ordered").Any())
                {
                    var list = GetInventoryList("Ordered", _ctx);

                    lblOrderedTotal.Text = list.Sum(y => y.Cost).ToString("c0");
                }
                else
                {
                    lblOrderedTotal.Text = "$0";
                }
            }
            catch (Exception ex)
            {
                lblOrderedTotal.Text = "$0";
            }
        }

        protected void lblOrderedOldest_PreRender(object sender, EventArgs e)
        {
            try
            {
                var list = GetInventoryList("Ordered", _ctx);

                lblOrderedOldest.Text = list.OrderBy(x => x.Timestamp).FirstOrDefault().Timestamp.ToShortDateString();
            }
            catch (Exception ex)
            {
                lblOrderedOldest.Text = "";
            }
        }

        protected void lblArrivedCount_PreRender(object sender, EventArgs e)
        {
            try
            {
                var list = GetInventoryList("Arrived", _ctx);

                lblArrivedCount.Text = list.Count().ToString();
            }
            catch (Exception ex)
            {
                lblArrivedCount.Text = "0";
            }
        }

        protected void lblArrivedTotal_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (_ctx.InventoryApprovalItems.Where(x => x.Status.StatusDescription == "Arrived").Any())
                {
                    var list = GetInventoryList("Arrived", _ctx);

                    lblArrivedTotal.Text = list.Sum(y => y.Cost).ToString("c0");
                }
                else
                {
                    lblArrivedTotal.Text = "$0";
                }
            }
            catch (Exception ex)
            {
                lblArrivedTotal.Text = "$0";
            }
        }

        protected void lblArrivedOldest_PreRender(object sender, EventArgs e)
        {
            try
            {
                var list = GetInventoryList("Arrived", _ctx);

                lblArrivedOldest.Text = list.OrderBy(x => x.Timestamp).FirstOrDefault().Timestamp.ToShortDateString();
            }
            catch (Exception ex)
            {
                lblArrivedOldest.Text = "";
            }
        }

        protected void lblInvoicedCount_PreRender(object sender, EventArgs e)
        {
            try
            {
                var list = GetInventoryList("Invoiced", _ctx);

                lblInvoicedCount.Text = list.Count().ToString();
            }
            catch (Exception ex)
            {
                lblInvoicedCount.Text = "0";
            }
        }

        protected void lblInvoicedTotal_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (_ctx.InventoryApprovalItems.Where(x => x.Status.StatusDescription == "Invoiced").Any())
                {
                    var list = GetInventoryList("Invoiced", _ctx);

                    lblInvoicedTotal.Text = list.Sum(y => y.Cost).ToString("c0");
                }
                else
                {
                    lblInvoicedTotal.Text = "$0";
                }
            }
            catch (Exception ex)
            {
                lblInvoicedTotal.Text = "$0";
            }
        }

        protected void lblInvoicedOldest_PreRender(object sender, EventArgs e)
        {
            try
            {
                var list = GetInventoryList("Invoiced", _ctx);

                lblInvoicedOldest.Text = list.OrderBy(x => x.Timestamp).FirstOrDefault().Timestamp.ToShortDateString();
            }
            catch (Exception ex)
            {
                lblInvoicedOldest.Text = "";
            }
        }

        protected void LoadVendorStock()
        {
            try
            {
                StringBuilder strVendorStock = new StringBuilder();

                strVendorStock.Append("<table style=\"border-spacing:0px; border:0; border-collapse: collapsel\">");

                using (FormContext ctx = new FormContext())
                {
                    if (ctx.Vendors.Any())
                    {
                        int width = Convert.ToInt32(90 / ctx.Vendors.Count());
                        int remainingWidth = 10 + (90 % ctx.Vendors.Count());

                        strVendorStock.Append("<tr style=\"background-color: #C3C3C3; border:1px solid black\"><td style=\"width:\"").Append(remainingWidth.ToString()).Append("%\"></td>");


                        foreach (Vendor vendor in ctx.Vendors)
                        {
                            strVendorStock.Append("<td style=\"width:").Append(width).Append("%; font-weight:bold; font-size:11px\">").Append(vendor.VendorName).Append("</td>");
                        }

                        strVendorStock.Append("</tr>");



                        strVendorStock.Append("<tr style=\"border:1px solid black\"><td style=\"width:").Append(remainingWidth.ToString()).Append("%\">Pending</td>");

                        if (ctx.InventoryApprovalItems.Any(x => x.Status.StatusDescription == "Pending Approval"))
                        {
                            foreach (Vendor vendor in ctx.Vendors)
                            {
                                strVendorStock.Append("<td style=\"width:").Append(width.ToString()).Append("%; font-size:11px\">");
                                
                                if (ctx.InventoryApprovalItems.Any(x => x.Status.StatusDescription == "Pending Approval" && x.Vendor.VendorName == vendor.VendorName))
                                {
                                    strVendorStock.Append(GetCurrentStockValues("Pending Approval", ctx, vendor.VendorName).ToString("c0"));
                                }
                                else
                                {
                                    strVendorStock.Append("$0");
                                }

                                strVendorStock.Append("</td>");
                            }
                        }
                        else
                        {
                            foreach (Vendor vendor in ctx.Vendors)
                            {
                                strVendorStock.Append("<td style=\"width:").Append(width.ToString()).Append("%; font-size:11px\">$0</td>");
                            }
                        }

                        strVendorStock.Append("</tr>");



                        strVendorStock.Append("<tr style=\"border:1px solid black\"><td style=\"width:").Append(remainingWidth.ToString()).Append("%\">Ordered</td>");

                        if (ctx.InventoryApprovalItems.Any(x => x.Status.StatusDescription == "Ordered"))
                        {
                            foreach (Vendor vendor in ctx.Vendors)
                            {
                                strVendorStock.Append("<td style=\"width:").Append(width.ToString()).Append("%; font-size:11px\">");

                                if (ctx.InventoryApprovalItems.Any(x => x.Status.StatusDescription == "Ordered" && x.Vendor.VendorName == vendor.VendorName))
                                {
                                    strVendorStock.Append(GetCurrentStockValues("Ordered", ctx, vendor.VendorName).ToString("c0"));
                                }
                                else
                                {
                                    strVendorStock.Append("$0");
                                }

                                strVendorStock.Append("</td>");
                            }
                        }
                        else
                        {
                            foreach (Vendor vendor in ctx.Vendors)
                            {
                                strVendorStock.Append("<td style=\"width:").Append(width.ToString()).Append("%; font-size:11px\">$0</td>");
                            }
                        }

                        strVendorStock.Append("</tr>");



                        strVendorStock.Append("<tr style=\"border:1px solid black\"><td style=\"width:").Append(remainingWidth.ToString()).Append("%\">Current Inventory</td>");

                        foreach (Vendor vendor in ctx.Vendors)
                        {
                            strVendorStock.Append("<td style=\"width:").Append(width.ToString()).Append("%; font-size:11px\">").Append(vendor.CurrentStock.ToString("c0"));
                        }

                        strVendorStock.Append("</tr>");



                        strVendorStock.Append("<tr><td style=\"width:").Append(remainingWidth.ToString()).Append("%; font-size:11px\"></td>");

                        foreach (Vendor vendor in ctx.Vendors)
                        {
                            decimal totalAmt = 0;

                            if (ctx.InventoryApprovalItems.Any(x => x.Status.StatusDescription == "Pending Approval" && x.Vendor.VendorName == vendor.VendorName))
                            {
                                totalAmt += GetCurrentStockValues("Pending Approval", ctx, vendor.VendorName);
                            }

                            if (ctx.InventoryApprovalItems.Any(x => x.Status.StatusDescription == "Ordered" && x.Vendor.VendorName == vendor.VendorName))
                            {
                                totalAmt += GetCurrentStockValues("Ordered", ctx, vendor.VendorName);
                            }

                            totalAmt = vendor.CurrentStock == null ? totalAmt : vendor.CurrentStock + totalAmt;

                            strVendorStock.Append("<td style=\"font-weight:bold; width:").Append(width.ToString()).Append("%; font-size:11px\">")
                                .Append(totalAmt.ToString("c0"))
                                .Append("</td>");
                        }

                        strVendorStock.Append("</tr>");
                    }

                }

                strVendorStock.Append("</table");

                ltlVendorStock.Text = strVendorStock.ToString();
            }
            catch (Exception ex)
            {
                ltlVendorStock.Text = "An error has occurred while loading vendor stock.  Please contact your system administrator for more information.";
            }
        }

        protected void ltlVendorStock_PreRender(object sender, EventArgs e)
        {
            LoadVendorStock();
        }

        protected void btnUpdateCurrentStock_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    foreach (Vendor vendor in ctx.Vendors)
                    {
                        RadNumericTextBox txt = (RadNumericTextBox)phCurrentStock.FindControl("txt" + vendor.VendorName.Replace(" ", ""));
                        txt.Text = vendor.CurrentStock.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            divUdpateCurrentStock.Visible = true;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    foreach (Vendor vendor in ctx.Vendors)
                    {
                        RadNumericTextBox txt = (RadNumericTextBox)phCurrentStock.FindControl("txt" + vendor.VendorName.Replace(" ", ""));
                        vendor.CurrentStock = Convert.ToDecimal(txt.Text);
                    }

                    ctx.SaveChanges();

                    divUdpateCurrentStock.Visible = false; 
                }
            }
            catch(Exception ex)
            {
                throw;
            }

        }

        public IEnumerable<Vendor> GetVendors()
        {
            using (FormContext ctx = new FormContext())
            {
                var vList = ctx.Vendors.ToList();

                return vList;
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

        public IEnumerable<InventoryApprovalStatus> GetStatuses()
        {
            using (FormContext ctx = new FormContext())
            {
                var list = ctx.InventoryApprovalStatuses.ToList();

                return list;
            }

        }

        public IQueryable<InventoryApprovalForm> GetPendingApproval()
        {
            var iaList = _ctx.InventoryApprovalItems.Where(x => x.Status.StatusDescription == "Pending Approval");

            if (ddlCompanyGlobal.SelectedIndex != -1)
            {
                iaList = iaList.Where(x => x.Company == ddlCompanyGlobal.SelectedText);
            }

            if (ddlVendorsGlobal.SelectedIndex > 0)
            {
                iaList = iaList.Where(x => x.Vendor.VendorName == ddlVendorsGlobal.SelectedText);
            }

            if (ddlOverLastGlobal.SelectedIndex > 0)
            {
                int overLast = Convert.ToInt32(ddlOverLastGlobal.SelectedValue);

                iaList = iaList.Where(x => x.Timestamp > DbFunctions.AddDays(DateTime.Now, -1 * overLast));
            }

            return iaList.OrderBy(x => x.RecordId);
        }

        public IQueryable<InventoryApprovalForm> GetApproved()
        {
            var iaList = _ctx.InventoryApprovalItems.Where(x => x.Status.StatusDescription == "Approved");

            if (ddlCompanyGlobal.SelectedIndex != -1)
            {
                iaList = iaList.Where(x => x.Company == ddlCompanyGlobal.SelectedText);
            }

            if (ddlVendorsGlobal.SelectedIndex > 0)
            {
                iaList = iaList.Where(x => x.Vendor.VendorName == ddlVendorsGlobal.SelectedText);
            }

            if (ddlOverLastGlobal.SelectedIndex > 0)
            {
                int overLast = Convert.ToInt32(ddlOverLastGlobal.SelectedValue);

                iaList = iaList.Where(x => x.Timestamp > DbFunctions.AddDays(DateTime.Now, -1 * overLast));
            }

            return iaList.OrderBy(x => x.RecordId);
        }

        public IQueryable<InventoryApprovalForm> GetOrdered()
        {
            var iaList = _ctx.InventoryApprovalItems.Where(x => x.Status.StatusDescription == "Ordered");

            if (ddlCompanyGlobal.SelectedIndex != -1)
            {
                iaList = iaList.Where(x => x.Company == ddlCompanyGlobal.SelectedText);
            }

            if (ddlVendorsGlobal.SelectedIndex > 0)
            {
                iaList = iaList.Where(x => x.Vendor.VendorName == ddlVendorsGlobal.SelectedText);
            }

            if (ddlOverLastGlobal.SelectedIndex > 0)
            {
                int overLast = Convert.ToInt32(ddlOverLastGlobal.SelectedValue);

                iaList = iaList.Where(x => x.Timestamp > DbFunctions.AddDays(DateTime.Now, -1 * overLast));
            }

            return iaList.OrderBy(x => x.RecordId);
        }

        public IQueryable<InventoryApprovalForm> GetArrived()
        {
            var iaList = _ctx.InventoryApprovalItems.Where(x => x.Status.StatusDescription == "Arrived");

            if (ddlCompanyGlobal.SelectedIndex != -1)
            {
                iaList = iaList.Where(x => x.Company == ddlCompanyGlobal.SelectedText);
            }

            if (ddlVendorsGlobal.SelectedIndex > 0)
            {
                iaList = iaList.Where(x => x.Vendor.VendorName == ddlVendorsGlobal.SelectedText);
            }

            if (ddlOverLastGlobal.SelectedIndex > 0)
            {
                int overLast = Convert.ToInt32(ddlOverLastGlobal.SelectedValue);

                iaList = iaList.Where(x => x.Timestamp > DbFunctions.AddDays(DateTime.Now, -1 * overLast));
            }

            return iaList.OrderBy(x => x.RecordId);
        }

        public IQueryable<InventoryApprovalForm> GetInvoiced()
        {
            var iaList = _ctx.InventoryApprovalItems.Where(x => x.Status.StatusDescription == "Invoiced");

            if (ddlCompanyGlobal.SelectedIndex != -1)
            {
                iaList = iaList.Where(x => x.Company == ddlCompanyGlobal.SelectedText);
            }

            if (ddlVendorsGlobal.SelectedIndex > 0)
            {
                iaList = iaList.Where(x => x.Vendor.VendorName == ddlVendorsGlobal.SelectedText);
            }

            if (ddlOverLastGlobal.SelectedIndex > 0)
            {
                int overLast = Convert.ToInt32(ddlOverLastGlobal.SelectedValue);

                iaList = iaList.Where(x => x.Timestamp > DbFunctions.AddDays(DateTime.Now, -1 * overLast));
            }

            return iaList.OrderBy(x => x.RecordId);
        }

        public IQueryable<InventoryApprovalForm> GetCancelled()
        {
            var iaList = _ctx.InventoryApprovalItems.Where(x => x.Status.StatusDescription == "Cancelled");

            if (ddlCompanyGlobal.SelectedIndex != -1)
            {
                iaList = iaList.Where(x => x.Company == ddlCompanyGlobal.SelectedText);
            }

            if (ddlVendorsGlobal.SelectedIndex > 0)
            {
                iaList = iaList.Where(x => x.Vendor.VendorName == ddlVendorsGlobal.SelectedText);
            }

            if (ddlOverLastGlobal.SelectedIndex > 0)
            {
                int overLast = Convert.ToInt32(ddlOverLastGlobal.SelectedValue);

                iaList = iaList.Where(x => x.Timestamp > DbFunctions.AddDays(DateTime.Now, -1 * overLast));
            }

            return iaList.OrderBy(x => x.RecordId);
        }
        
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                    divUdpateCurrentStock.Visible = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void ddlPriority_DataBound(object sender, EventArgs e)
        {
            ddlPriority.SelectedText = "Normal";
        }

        protected void btnAddNewItem_Click(object sender, EventArgs e)
        {
            try
            {
                int vendorId = Convert.ToInt32(ddlVendor.SelectedValue);
                int priorityId = Convert.ToInt32(ddlPriority.SelectedValue);
                System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                UserLogic uLogic = new UserLogic();
                SystemUsers currentUser = uLogic.GetCurrentUser(user);
                int userId = currentUser.SystemUserID;
                DateTime tryESD;
                Nullable<DateTime> esd = null;
                DateTime tryETA;
                Nullable<DateTime> eta = null;

                DateTime.TryParse(txtEstShipDate.Value, out tryESD);

                if (tryESD.Year > 0001)
                {
                    esd = tryESD;
                }

                DateTime.TryParse(txtETA.Value, out tryETA);

                if (tryETA.Year > 0001)
                {
                    eta = tryETA;
                }

                using (FormContext ctx = new FormContext())
                {
                    InventoryApprovalForm newForm = new InventoryApprovalForm
                    {
                        Timestamp = DateTime.Now,
                        Company = ddlCompany.SelectedText,
                        Vendor = ctx.Vendors.FirstOrDefault(x => x.RecordId == vendorId),
                        PurchaseOrderNumber = txtPO.Text,
                        MaterialGroup = txtMaterialGroup.Text,
                        Cost = Convert.ToDecimal(txtCost.Value),
                        ContainerNumber = txtContainer.Text,
                        Priority = ctx.Priorities.FirstOrDefault(x => x.RecordId == priorityId),
                        EstimatedShipDate = esd,
                        EstimatedTimeOfArrival = eta,
                        LastModifiedTimestamp = DateTime.Now,
                        LastModifiedUser = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId),
                        Status = ctx.InventoryApprovalStatuses.FirstOrDefault(x => x.StatusDescription == "Pending Approval"),
                        SubmittedUser = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId)
                    };

                    ctx.InventoryApprovalItems.Add(newForm);

                    ctx.SaveChanges();

                    gridPendingApproval.Rebind();

                    ddlCompany.SelectedIndex = 0;
                    ddlVendor.SelectedIndex = 0;
                    txtPO.Text = "";
                    txtMaterialGroup.Text = "";
                    txtCost.Text = "";
                    txtContainer.Text = "";
                    ddlPriority.SelectedValue = "Normal";
                    txtEstShipDate.Value = "";
                    txtETA.Value = "";


                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public void RebindGrids()
        {
            gridPendingApproval.Rebind();
            gridApproved.Rebind();
            gridOrdered.Rebind();
            gridArrived.Rebind();
            gridInvoiced.Rebind();
        }

        public void SetGridVisibility()
        {
            if (gridPendingApproval.Items.Count == 0)
            {
                spanPendingApprovalEmpty.Visible = true;
                gridPendingApproval.Visible = false;
            }
            else
            {
                spanPendingApprovalEmpty.Visible = false;
                gridPendingApproval.Visible = true;
            }

            if (gridApproved.Items.Count == 0)
            {
                spanApprovedEmpty.Visible = true;
                gridApproved.Visible = false;
            }
            else
            {
                spanApprovedEmpty.Visible = false;
                gridApproved.Visible = true;
            }

            if (gridOrdered.Items.Count == 0)
            {
                spanOrderedEmpty.Visible = true;
                gridOrdered.Visible = false;
            }
            else
            {
                spanOrderedEmpty.Visible = false;
                gridOrdered.Visible = true;
            }

            if (gridArrived.Items.Count == 0)
            {
                spanArrivalsEmpty.Visible = true;
                gridArrived.Visible = false;
            }
            else
            {
                spanArrivalsEmpty.Visible = false;
                gridArrived.Visible = true;
            }

            if (gridInvoiced.Items.Count == 0)
            {
                spanInvoicedEmpty.Visible = true;
                gridInvoiced.Visible = false;
            }
            else
            {
                spanInvoicedEmpty.Visible = false;
                gridInvoiced.Visible = true;
            }
        }

        public void SetPriorityColors(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem dataBoundItem = e.Item as GridDataItem;
                    TableCell priorityCell = dataBoundItem["Priority.PriorityText"];
                    Label lblPriority = (Label)dataBoundItem.FindControl("lblPriority");
                    string priorityDesc = "";
                    if (lblPriority != null)
                    {
                        priorityDesc = lblPriority.Text.ToLower();
                    }

                    switch (priorityDesc)
                    {
                        case "high":
                            priorityCell.BackColor = System.Drawing.Color.FromArgb(128, 0, 0);
                            lblPriority.ForeColor = System.Drawing.Color.White;
                            break;
                        case "low":
                            priorityCell.BackColor = System.Drawing.Color.FromArgb(230, 184, 0);
                            lblPriority.ForeColor = System.Drawing.Color.Black;
                            break;
                        default:
                            priorityCell.BackColor = System.Drawing.Color.White;
                            lblPriority.ForeColor = System.Drawing.Color.Black;
                            break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        

        protected void gridPendingApproval_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GetPendingApproval();
        }

        protected void gridPendingApproval_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    UserLogic uLogic = new UserLogic();
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);
                    int userId = currentUser.SystemUserID;

                    GridBatchEditingEventArgument argument = e.CommandArgument as GridBatchEditingEventArgument;
                    Hashtable oldValues = argument.OldValues;
                    Hashtable newValues = argument.NewValues;
                    int recordId = Convert.ToInt32(oldValues["RecordId"].ToString());
                    int vendorId = Convert.ToInt32(newValues["Vendor.VendorName"].ToString());
                    string po = newValues["PurchaseOrderNumber"].ToString();
                    string materialGroup = newValues["MaterialGroup"].ToString();
                    decimal cost = Convert.ToDecimal(newValues["Cost"].ToString());
                    string containerNumber = newValues["ContainerNumber"].ToString();
                    int priorityId = Convert.ToInt32(newValues["Priority.PriorityText"].ToString());
                    DateTime estimatedShipDate = Convert.ToDateTime(newValues["EstimatedShipDate"].ToString());
                    DateTime estimatedTimeOfArrival = Convert.ToDateTime(newValues["EstimatedTimeOfArrival"].ToString());

                    var updateForm = ctx.InventoryApprovalItems.FirstOrDefault(x => x.RecordId == recordId);
                    updateForm.Vendor = ctx.Vendors.FirstOrDefault(x => x.RecordId == vendorId);
                    updateForm.PurchaseOrderNumber = po;
                    updateForm.MaterialGroup = materialGroup;
                    updateForm.Cost = cost;
                    updateForm.ContainerNumber = containerNumber;
                    updateForm.Priority = ctx.Priorities.FirstOrDefault(x => x.RecordId == priorityId);
                    updateForm.LastModifiedUser = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId);
                    updateForm.LastModifiedTimestamp = DateTime.Now;
                    updateForm.EstimatedShipDate = estimatedShipDate;
                    updateForm.EstimatedTimeOfArrival = estimatedTimeOfArrival;

                    ctx.SaveChanges();



                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gridPendingApproval_ItemDataBound(object sender, GridItemEventArgs e)
        {
            SetPriorityColors(sender, e);
        }
            
        protected void gridPendingApproval_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem && isExport)
            {
                e.Item.Visible = false;
            }
        }

        protected void gridPendingApproval_PreRender(object sender, EventArgs e)
        {
        }

        public void UpdatePendingApproval()
        {
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                int formId = Convert.ToInt32(((RadButton)sender).CommandArgument);
                System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                UserLogic uLogic = new UserLogic();
                SystemUsers currentUser = uLogic.GetCurrentUser(user);
                int userId = currentUser.SystemUserID;

                using (FormContext ctx = new FormContext())
                {
                    if (ctx.InventoryApprovalItems.Any(x => x.RecordId == formId))
                    {
                        var editForm = ctx.InventoryApprovalItems.FirstOrDefault(x => x.RecordId == formId);
                        editForm.Status = ctx.InventoryApprovalStatuses.FirstOrDefault(x => x.StatusDescription == "Approved");
                        editForm.ApprovedBy = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId);
                        editForm.ApprovedDate = DateTime.Now;
                        editForm.LastModifiedUser = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId);
                        editForm.LastModifiedTimestamp = DateTime.Now;
                    }

                    ctx.SaveChanges();

                    RebindGrids();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        /*
         * Grid Approved
         * 
         * 
         * 
         * 
         */


        protected void gridApproved_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    UserLogic uLogic = new UserLogic();
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);
                    int userId = currentUser.SystemUserID;

                    GridBatchEditingEventArgument argument = e.CommandArgument as GridBatchEditingEventArgument;
                    Hashtable oldValues = argument.OldValues;
                    Hashtable newValues = argument.NewValues;
                    int recordId = Convert.ToInt32(oldValues["RecordId"].ToString());
                    int vendorId = Convert.ToInt32(newValues["Vendor.VendorName"].ToString());
                    string po = newValues["PurchaseOrderNumber"].ToString();
                    string materialGroup = newValues["MaterialGroup"].ToString();
                    decimal cost = Convert.ToDecimal(newValues["Cost"].ToString());
                    string containerNumber = newValues["ContainerNumber"].ToString();
                    int priorityId = Convert.ToInt32(newValues["Priority.PriorityText"].ToString());
                    DateTime estimatedShipDate = Convert.ToDateTime(newValues["EstimatedShipDate"].ToString());
                    DateTime estimatedTimeOfArrival = Convert.ToDateTime(newValues["EstimatedTimeOfArrival"].ToString());

                    var updateForm = ctx.InventoryApprovalItems.FirstOrDefault(x => x.RecordId == recordId);
                    updateForm.Vendor = ctx.Vendors.FirstOrDefault(x => x.RecordId == vendorId);
                    updateForm.PurchaseOrderNumber = po;
                    updateForm.MaterialGroup = materialGroup;
                    updateForm.Cost = cost;
                    updateForm.ContainerNumber = containerNumber;
                    updateForm.Priority = ctx.Priorities.FirstOrDefault(x => x.RecordId == priorityId);
                    updateForm.LastModifiedUser = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId);
                    updateForm.LastModifiedTimestamp = DateTime.Now;
                    updateForm.EstimatedShipDate = estimatedShipDate;
                    updateForm.EstimatedTimeOfArrival = estimatedTimeOfArrival;

                    ctx.SaveChanges();



                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gridApproved_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GetApproved();
        }

        protected void gridApproved_ItemDataBound(object sender, GridItemEventArgs e)
        {
            SetPriorityColors(sender, e);
        }

        protected void gridApproved_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem && isExport)
            {
                e.Item.Visible = false;
            }
        }

        protected void gridApproved_PreRender(object sender, EventArgs e)
        {
        }

        public void UpdateApproved()
        {
        }

        protected void ddlApprovedStatus_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = (sender as RadDropDownList).NamingContainer as GridEditableItem;
                RadDropDownList ddlApprovedStatus = sender as RadDropDownList;
                string statusDesc = ddlApprovedStatus.SelectedText;
                int formId = Convert.ToInt32(editedItem.GetDataKeyValue("RecordId"));
                System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                UserLogic uLogic = new UserLogic();
                SystemUsers currentUser = uLogic.GetCurrentUser(user);
                int userId = currentUser.SystemUserID;

                using (FormContext ctx = new FormContext())
                {
                    if (ctx.InventoryApprovalItems.Any(x => x.RecordId == formId))
                    {
                        var editForm = ctx.InventoryApprovalItems.FirstOrDefault(x => x.RecordId == formId);

                        editForm.Status = ctx.InventoryApprovalStatuses.FirstOrDefault(x => x.StatusDescription == statusDesc);
                        editForm.LastModifiedTimestamp = DateTime.Now;
                        editForm.LastModifiedUser = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId);

                        if (statusDesc == "Pending Approval")
                        {
                            editForm.ApprovedBy = null;
                            editForm.ApprovedDate = null;
                        }
                        else if (statusDesc == "Ordered")
                        {
                            editForm.OrderedBy = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId);
                            editForm.OrderDate = DateTime.Now;
                            editForm.ActualShipDate = editForm.EstimatedShipDate;
                        }
                        else if (statusDesc == "Arrived")
                        {
                            editForm.ReceivedBy = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId);
                            editForm.ArrivalDate = DateTime.Now;
                            editForm.ActualShipDate = editForm.EstimatedShipDate;
                            editForm.TimeToArrival = Math.Round((DateTime.Now - editForm.EstimatedShipDate.Value).TotalDays / 7, 1).ToString() + " weeks";
                        }
                        else if (statusDesc == "Invoiced")
                        {
                            editForm.InvoicedBy = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId);
                            editForm.InvoiceDate = DateTime.Now;
                            editForm.ActualShipDate = editForm.EstimatedShipDate;
                            editForm.TimeToArrival = Math.Round((DateTime.Now - editForm.EstimatedShipDate.Value).TotalDays / 7, 1).ToString() + " weeks";
                        }
                        else if (statusDesc == "Cancelled")
                        {
                            editForm.CancelledBy = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId);
                            editForm.CancelledDate = DateTime.Now;
                        }

                    }

                    ctx.SaveChanges();

                    RebindGrids();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlApprovedStatus_DataBinding(object sender, EventArgs e)
        {
            RadDropDownList ddlApprovedStatus = (RadDropDownList)sender;
            ddlApprovedStatus.SelectedText = "Approved";
        }





        /*
         * Grid Ordered
         * 
         * 
         * 
         * 
         */


        protected void gridOrdered_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    UserLogic uLogic = new UserLogic();
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);
                    int userId = currentUser.SystemUserID;

                    GridBatchEditingEventArgument argument = e.CommandArgument as GridBatchEditingEventArgument;
                    Hashtable oldValues = argument.OldValues;
                    Hashtable newValues = argument.NewValues;
                    int recordId = Convert.ToInt32(oldValues["RecordId"].ToString());
                    int vendorId = Convert.ToInt32(newValues["Vendor.VendorName"].ToString());
                    string po = newValues["PurchaseOrderNumber"].ToString();
                    string materialGroup = newValues["MaterialGroup"].ToString();
                    decimal cost = Convert.ToDecimal(newValues["Cost"].ToString());
                    string containerNumber = newValues["ContainerNumber"].ToString();
                    int priorityId = Convert.ToInt32(newValues["Priority.PriorityText"].ToString());
                    DateTime actualShipDate = Convert.ToDateTime(newValues["ActualShipDate"].ToString());
                    DateTime estimatedTimeOfArrival = Convert.ToDateTime(newValues["EstimatedTimeOfArrival"].ToString());
                    DateTime orderDate = Convert.ToDateTime(newValues["OrderDate"].ToString());

                    var updateForm = ctx.InventoryApprovalItems.FirstOrDefault(x => x.RecordId == recordId);
                    updateForm.Vendor = ctx.Vendors.FirstOrDefault(x => x.RecordId == vendorId);
                    updateForm.PurchaseOrderNumber = po;
                    updateForm.MaterialGroup = materialGroup;
                    updateForm.Cost = cost;
                    updateForm.ContainerNumber = containerNumber;
                    updateForm.Priority = ctx.Priorities.FirstOrDefault(x => x.RecordId == priorityId);
                    updateForm.LastModifiedUser = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId);
                    updateForm.LastModifiedTimestamp = DateTime.Now;
                    updateForm.ActualShipDate = actualShipDate;
                    updateForm.EstimatedTimeOfArrival = estimatedTimeOfArrival;
                    updateForm.OrderDate = orderDate;

                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gridOrdered_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GetOrdered();
        }

        protected void gridOrdered_ItemDataBound(object sender, GridItemEventArgs e)
        {
            SetPriorityColors(sender, e);
        }

        protected void gridOrdered_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem && isExport)
            {
                e.Item.Visible = false;
            }
        }

        protected void gridOrdered_PreRender(object sender, EventArgs e){}

        public void UpdateOrdered(){}

        protected void ddlOrderedStatus_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = (sender as RadDropDownList).NamingContainer as GridEditableItem;
                RadDropDownList ddlApprovedStatus = sender as RadDropDownList;
                string statusDesc = ddlApprovedStatus.SelectedText;
                int formId = Convert.ToInt32(editedItem.GetDataKeyValue("RecordId"));
                System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                UserLogic uLogic = new UserLogic();
                SystemUsers currentUser = uLogic.GetCurrentUser(user);
                int userId = currentUser.SystemUserID;

                using (FormContext ctx = new FormContext())
                {
                    if (ctx.InventoryApprovalItems.Any(x => x.RecordId == formId))
                    {
                        var editForm = ctx.InventoryApprovalItems.FirstOrDefault(x => x.RecordId == formId);

                        editForm.Status = ctx.InventoryApprovalStatuses.FirstOrDefault(x => x.StatusDescription == statusDesc);
                        editForm.LastModifiedTimestamp = DateTime.Now;
                        editForm.LastModifiedUser = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId);

                        if (statusDesc == "Pending Approval")
                        {
                            editForm.ApprovedBy = null;
                            editForm.ApprovedDate = null;
                            editForm.OrderDate = null;
                            editForm.OrderedBy = null;
                            editForm.ActualShipDate = null;
                        }
                        else if (statusDesc == "Approved")
                        {
                            editForm.OrderedBy = null;
                            editForm.OrderDate = null;
                            editForm.ActualShipDate = null;
                        }
                        else if (statusDesc == "Arrived")
                        {
                            editForm.ReceivedBy = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId);
                            editForm.ArrivalDate = DateTime.Now;
                            editForm.TimeToArrival = Math.Round((DateTime.Now - editForm.EstimatedShipDate.Value).TotalDays / 7, 1).ToString() + " weeks";
                        }
                        else if (statusDesc == "Invoiced")
                        {
                            editForm.InvoicedBy = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId);
                            editForm.InvoiceDate = DateTime.Now;
                            editForm.TimeToArrival = Math.Round((DateTime.Now - editForm.EstimatedShipDate.Value).TotalDays / 7, 1).ToString() + " weeks";
                        }
                        else if (statusDesc == "Cancelled")
                        {
                            editForm.CancelledBy = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId);
                            editForm.CancelledDate = DateTime.Now;
                        }

                    }

                    ctx.SaveChanges();

                    RebindGrids();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlOrderedStatus_DataBinding(object sender, EventArgs e)
        {
            RadDropDownList ddlOrderedStatus = (RadDropDownList)sender;
            ddlOrderedStatus.SelectedText = "Ordered";
        }



        /*
         * Grid Arrived
         * 
         * 
         * 
         * 
         */

        protected void gridArrived_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    UserLogic uLogic = new UserLogic();
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);
                    int userId = currentUser.SystemUserID;

                    GridBatchEditingEventArgument argument = e.CommandArgument as GridBatchEditingEventArgument;
                    Hashtable oldValues = argument.OldValues;
                    Hashtable newValues = argument.NewValues;
                    int recordId = Convert.ToInt32(oldValues["RecordId"].ToString());
                    int vendorId = Convert.ToInt32(newValues["Vendor.VendorName"].ToString());
                    string po = newValues["PurchaseOrderNumber"].ToString();
                    string materialGroup = newValues["MaterialGroup"].ToString();
                    decimal cost = Convert.ToDecimal(newValues["Cost"].ToString());
                    string containerNumber = newValues["ContainerNumber"].ToString();
                    int priorityId = Convert.ToInt32(newValues["Priority.PriorityText"].ToString());
                    DateTime actualShipDate = Convert.ToDateTime(newValues["ActualShipDate"].ToString());
                    DateTime arrivalDate = Convert.ToDateTime(newValues["ArrivalDate"].ToString());
                    DateTime orderDate = Convert.ToDateTime(newValues["OrderDate"].ToString());

                    var updateForm = ctx.InventoryApprovalItems.FirstOrDefault(x => x.RecordId == recordId);
                    updateForm.Vendor = ctx.Vendors.FirstOrDefault(x => x.RecordId == vendorId);
                    updateForm.PurchaseOrderNumber = po;
                    updateForm.MaterialGroup = materialGroup;
                    updateForm.Cost = cost;
                    updateForm.ContainerNumber = containerNumber;
                    updateForm.Priority = ctx.Priorities.FirstOrDefault(x => x.RecordId == priorityId);
                    updateForm.LastModifiedUser = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId);
                    updateForm.LastModifiedTimestamp = DateTime.Now;
                    updateForm.ActualShipDate = actualShipDate;
                    updateForm.ArrivalDate = arrivalDate;
                    updateForm.TimeToArrival = Math.Round((arrivalDate - actualShipDate).TotalDays / 7, 1).ToString() + " weeks";
                    updateForm.OrderDate = orderDate;

                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gridArrived_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GetArrived();
        }

        protected void gridArrived_ItemDataBound(object sender, GridItemEventArgs e)
        {
            SetPriorityColors(sender, e);
        }

        protected void gridArrived_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem && isExport)
            {
                e.Item.Visible = false;
            }
        }

        protected void gridArrived_PreRender(object sender, EventArgs e)
        {
        }

        public void UpdateArrived()
        { }

        protected void ddlArrivedStatus_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = (sender as RadDropDownList).NamingContainer as GridEditableItem;
                RadDropDownList ddlApprovedStatus = sender as RadDropDownList;
                string statusDesc = ddlApprovedStatus.SelectedText;
                int formId = Convert.ToInt32(editedItem.GetDataKeyValue("RecordId"));
                System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                UserLogic uLogic = new UserLogic();
                SystemUsers currentUser = uLogic.GetCurrentUser(user);
                int userId = currentUser.SystemUserID;

                using (FormContext ctx = new FormContext())
                {
                    if (ctx.InventoryApprovalItems.Any(x => x.RecordId == formId))
                    {
                        var editForm = ctx.InventoryApprovalItems.FirstOrDefault(x => x.RecordId == formId);

                        editForm.Status = ctx.InventoryApprovalStatuses.FirstOrDefault(x => x.StatusDescription == statusDesc);
                        editForm.LastModifiedTimestamp = DateTime.Now;
                        editForm.LastModifiedUser = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId);

                        if (statusDesc == "Pending Approval")
                        {
                            editForm.ApprovedBy = null;
                            editForm.ApprovedDate = null;
                            editForm.OrderDate = null;
                            editForm.OrderedBy = null;
                            editForm.ActualShipDate = null;
                            editForm.ArrivalDate = null;
                            editForm.ReceivedBy = null;
                            editForm.TimeToArrival = null;
                        }
                        else if (statusDesc == "Approved")
                        {
                            editForm.OrderedBy = null;
                            editForm.OrderDate = null;
                            editForm.ActualShipDate = null;
                            editForm.ArrivalDate = null;
                            editForm.ReceivedBy = null;
                            editForm.TimeToArrival = null;
                        }
                        else if (statusDesc == "Ordered")
                        {
                            editForm.ActualShipDate = null;
                            editForm.ArrivalDate = null;
                            editForm.ReceivedBy = null;
                            editForm.TimeToArrival = null;

                        }
                        else if (statusDesc == "Invoiced")
                        {
                            editForm.InvoicedBy = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId);
                            editForm.InvoiceDate = DateTime.Now;
                        }
                        else if (statusDesc == "Cancelled")
                        {
                            editForm.CancelledBy = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId);
                            editForm.CancelledDate = DateTime.Now;
                        }

                    }

                    ctx.SaveChanges();

                    RebindGrids();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlArrivedStatus_DataBinding(object sender, EventArgs e)
        {
            RadDropDownList ddlArrivedStatus = (RadDropDownList)sender;
            ddlArrivedStatus.SelectedText = "Arrived";
        }


        /*
         * Grid Invoiced
         * 
         * 
         * 
         * 
         */

        protected void gridInvoiced_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    UserLogic uLogic = new UserLogic();
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);
                    int userId = currentUser.SystemUserID;

                    GridBatchEditingEventArgument argument = e.CommandArgument as GridBatchEditingEventArgument;
                    Hashtable oldValues = argument.OldValues;
                    Hashtable newValues = argument.NewValues;
                    int recordId = Convert.ToInt32(oldValues["RecordId"].ToString());
                    int vendorId = Convert.ToInt32(newValues["Vendor.VendorName"].ToString());
                    string po = newValues["PurchaseOrderNumber"].ToString();
                    string materialGroup = newValues["MaterialGroup"].ToString();
                    decimal cost = Convert.ToDecimal(newValues["Cost"].ToString());
                    string containerNumber = newValues["ContainerNumber"].ToString();
                    int priorityId = Convert.ToInt32(newValues["Priority.PriorityText"].ToString());
                    DateTime actualShipDate = Convert.ToDateTime(newValues["ActualShipDate"].ToString());
                    DateTime arrivalDate = Convert.ToDateTime(newValues["ArrivalDate"].ToString());
                    DateTime invoiceDate = Convert.ToDateTime(newValues["InvoiceDate"].ToString());
                    DateTime orderDate = Convert.ToDateTime(newValues["OrderDate"].ToString());

                    var updateForm = ctx.InventoryApprovalItems.FirstOrDefault(x => x.RecordId == recordId);
                    updateForm.Vendor = ctx.Vendors.FirstOrDefault(x => x.RecordId == vendorId);
                    updateForm.PurchaseOrderNumber = po;
                    updateForm.MaterialGroup = materialGroup;
                    updateForm.Cost = cost;
                    updateForm.ContainerNumber = containerNumber;
                    updateForm.Priority = ctx.Priorities.FirstOrDefault(x => x.RecordId == priorityId);
                    updateForm.LastModifiedUser = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId);
                    updateForm.LastModifiedTimestamp = DateTime.Now;
                    updateForm.ActualShipDate = actualShipDate;
                    updateForm.ArrivalDate = arrivalDate;
                    updateForm.TimeToArrival = Math.Round((arrivalDate - actualShipDate).TotalDays / 7, 1).ToString() + " weeks";
                    updateForm.InvoiceDate = invoiceDate;
                    updateForm.InvoicedBy = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId);
                    updateForm.OrderDate = orderDate;

                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gridInvoiced_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GetInvoiced();
        }

        protected void gridInvoiced_ItemDataBound(object sender, GridItemEventArgs e)
        {
            SetPriorityColors(sender, e);
        }

        protected void gridInvoiced_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem && isExport)
            {
                e.Item.Visible = false;
            }
        }

        protected void gridInvoiced_PreRender(object sender, EventArgs e)
        {
        }

        public void UpdateInvoiced()
        {

        }





        protected void ddlInvoicedStatus_DataBinding(object sender, EventArgs e)
        {
            RadDropDownList ddlInvoicedStatus = (RadDropDownList)sender;
            ddlInvoicedStatus.SelectedText = "Invoiced";
        }

        protected void ddlInvoicedStatus_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = (sender as RadDropDownList).NamingContainer as GridEditableItem;
                RadDropDownList ddlApprovedStatus = sender as RadDropDownList;
                string statusDesc = ddlApprovedStatus.SelectedText;
                int formId = Convert.ToInt32(editedItem.GetDataKeyValue("RecordId"));
                System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                UserLogic uLogic = new UserLogic();
                SystemUsers currentUser = uLogic.GetCurrentUser(user);
                int userId = currentUser.SystemUserID;

                using (FormContext ctx = new FormContext())
                {
                    if (ctx.InventoryApprovalItems.Any(x => x.RecordId == formId))
                    {
                        var editForm = ctx.InventoryApprovalItems.FirstOrDefault(x => x.RecordId == formId);

                        editForm.Status = ctx.InventoryApprovalStatuses.FirstOrDefault(x => x.StatusDescription == statusDesc);
                        editForm.LastModifiedTimestamp = DateTime.Now;
                        editForm.LastModifiedUser = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId);

                        if (statusDesc == "Pending Approval")
                        {
                            editForm.ApprovedBy = null;
                            editForm.ApprovedDate = null;
                            editForm.OrderDate = null;
                            editForm.OrderedBy = null;
                            editForm.ActualShipDate = null;
                            editForm.ArrivalDate = null;
                            editForm.ReceivedBy = null;
                            editForm.TimeToArrival = null;
                        }
                        else if (statusDesc == "Approved")
                        {
                            editForm.OrderedBy = null;
                            editForm.OrderDate = null;
                            editForm.ActualShipDate = null;
                            editForm.ArrivalDate = null;
                            editForm.ReceivedBy = null;
                            editForm.TimeToArrival = null;
                        }
                        else if (statusDesc == "Ordered")
                        {
                            editForm.ActualShipDate = null;
                            editForm.ArrivalDate = null;
                            editForm.ReceivedBy = null;
                            editForm.TimeToArrival = null;

                        }
                        else if (statusDesc == "Arrived")
                        {
                            editForm.InvoicedBy = null;
                            editForm.InvoiceDate = null;
                        }
                        else if (statusDesc == "Cancelled")
                        {
                            editForm.CancelledBy = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == userId);
                            editForm.CancelledDate = DateTime.Now;
                        }

                    }

                    ctx.SaveChanges();

                    RebindGrids();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void cbNotifyStandard_CheckedChanged(object sender, EventArgs e)
        {
            FillEmailAddressLabels();
        }

        protected void cbNotifyOther_CheckedChanged(object sender, EventArgs e)
        {
            FillEmailAddressLabels();
        }

        protected void ddlNotifyOther_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            FillEmailAddressLabels();
        }

        protected void ddlNotifyOther_ItemChecked(object sender, RadComboBoxItemEventArgs e)
        {
            FillEmailAddressLabels();
        }

        protected void ddlNotifyOther_CheckAllCheck(object sender, RadComboBoxCheckAllCheckEventArgs e)
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

        protected void btnSendNotification_Click(object sender, EventArgs e)
        {
            try
            {
                string emailListString = lblEmailsSentTo.Text.Replace(" ", "");
                List<string> emailList = emailListString.Split(',').ToList<string>();

                if (Page.IsValid && emailListString != "")
                {
                    string notifyAbout = ddlNotifyAbout.SelectedText;
                    string overLast = ddlNotifyOverLast.SelectedText;
                    int days = overLast == "All" ? 9999 : Convert.ToInt32(overLast);
                    
                    DateTime maxDate = DateTime.Now.AddDays(-1 * days);

                    string commentCountText = ddlCommentCount.SelectedText;
                    int commentCount = ddlCommentCount.SelectedText != "All" ? Convert.ToInt32(commentCountText) : 0; 
                    Boolean sendComments = cbSendComments.Checked;
                    Boolean anyComments = false;
                    IEnumerable<InventoryComments> finalComments = null;

                    if (_ctx.InventoryApprovalComments.Any(x => x.SystemComment == false))
                    {
                        anyComments = true;

                        IEnumerable<InventoryComments> commentsList = _ctx.InventoryApprovalComments
                            .Where(x => x.SystemComment == false)
                            .Include(x => x.User)
                            .OrderByDescending(x => x.RecordId)
                            .ToList();
                            
                        if (commentCount > 0)
                        {
                            commentsList = commentsList.Take(commentCount);
                        }

                        finalComments = commentsList;
                    }

                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    UserLogic uLogic = new UserLogic();
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);

                    SendEmail msg = new SendEmail();
                    StringBuilder bodyHtml = new StringBuilder();

                    bodyHtml.AppendLine("<div style=\"width:50%; text-align:center;\"><img src=\"http://www.wctingle.com/img/Logo.jpg\" /><br /><br />")
                        .Append("An Inventory Approval Notification has been sent by ").Append(currentUser.DisplayName).Append("<br /><br />")
                        .AppendLine("<table style=\"border: 4px solid #d0604c;background-color:#FFF;width:100%;margin-lefT:auto; margin-right:auto;\">")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td colspan=\"10\" style=\"text-align: center;vertical-align: middle;font-weight: bold;font-size: 20px;border: 4px solid #d0604c; color:#FFF; background-color:#bc4445;\">Inventory Approval Notification</td>")
                        .AppendLine("    </tr>")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td colspan=\"10\" style=\"text-align: center;vertical-align: middle;font-weight: bold;font-size: 14px;color:#FFF; background-color:#bc4445;\">")
                        .Append("Notification About ").Append(notifyAbout).Append(" Orders over the last ").Append(overLast).Append(" Days")
                        .AppendLine("</td>")
                        .AppendLine("    </tr>")
                        .AppendLine("    <tr>")
                        .AppendLine("        <td colspan=\"2\" style=\"text-align:right;font-size:12px;width:25%;color:#bc4445;\">Company:</td>")
                        .Append("        <td colspan=\"2\" style=\"text-align:left;font-size:12px;width:25%;color:#000;\">").Append(ddlCompanyGlobal.SelectedText).AppendLine("</td>")
                        .Append("        <td colspan=\"2\"></td>")
                        .AppendLine("        <td colspan=\"2\" style=\"text-align:right;font-size:12px;width:25%; color:#bc4445\">Vendor:</td>")
                        .AppendLine("        <td colspan=\"2\" style=\"text-align:left;font-size:12px;width:25%;color:#000\">").Append(ddlVendorsGlobal.SelectedText).Append("</td>")
                        .AppendLine("    </tr>")
                        .AppendLine("    <tr><td colspan=\"10\" style=\"padding-bottom:5px;\"</td></tr>");


                    if (notifyAbout == "Pending Approval" || notifyAbout == "All")
                    {
                        bodyHtml.AppendLine("    <tr>")
                            .AppendLine("        <td colspan=\"10\" style=\"text-align: center;vertical-align: middle;font-weight: bold;font-size: 16px;border-top: 4px solid #d0604c; border-bottom: 4px solid #d0604c; color:#000; background-color:#FFF;\">Pending Approval</td>")
                            .AppendLine("    </tr>");

                        using (FormContext ctx = new FormContext())
                        {
                            if (ctx.InventoryApprovalItems.Any(x => x.Status.StatusDescription == "Pending Approval" && x.Timestamp >= DbFunctions.AddDays(DateTime.Now, -1 * days)))
                            {
                                bodyHtml.AppendLine("    <tr>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:15%;color:#bc4445;\">Vendor</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">PO #</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:15%;color:#bc4445;\">Material Group</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Cost</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Container #</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Priority</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Order Entry Date</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Est. Ship Date</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">ETA</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Created By</td>")
                                    .AppendLine("    </tr>");

                                foreach (InventoryApprovalForm item in ctx.InventoryApprovalItems.Where(x => x.Status.StatusDescription == "Pending Approval" && x.Timestamp >= DbFunctions.AddDays(DateTime.Now, -1 * days)))
                                {
                                    bodyHtml.AppendLine("    <tr>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:15%;color:#000;\">").Append(item.Vendor.VendorName).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.PurchaseOrderNumber).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:15%;color:#000;\">").Append(item.MaterialGroup).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.Cost.ToString("c0")).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.ContainerNumber).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.Priority.PriorityText).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.Timestamp.ToString("MM/dd/yy")).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.EstimatedShipDate.Value.ToString("MM/dd/yy")).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.EstimatedTimeOfArrival.Value.ToString("MM/dd/yy")).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.SubmittedUser.DisplayName).Append("</td>")
                                        .AppendLine("    </tr>");
                                }
                            }

                            bodyHtml.AppendLine("    <tr><td colspan=\"10\" style=\"padding-bottom:5px;\"</td></tr>");
                        }
                    }

                    if (notifyAbout == "Approved" || notifyAbout == "All")
                    {
                        bodyHtml.AppendLine("    <tr>")
                            .AppendLine("        <td colspan=\"10\" style=\"text-align: center;vertical-align: middle;font-weight: bold;font-size: 16px;border-top: 4px solid #d0604c; border-bottom: 4px solid #d0604c; color:#000; background-color:#FFF;\">Approved</td>")
                            .AppendLine("    </tr>");

                        using (FormContext ctx = new FormContext())
                        {
                            if (ctx.InventoryApprovalItems.Any(x => x.Status.StatusDescription == "Approved" && x.ApprovedDate.Value >= DbFunctions.AddDays(DateTime.Now, -1 * days)))
                            {
                                bodyHtml.AppendLine("    <tr>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:15%;color:#bc4445;\">Vendor</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">PO #</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:15%;color:#bc4445;\">Material Group</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Cost</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Container #</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Priority</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Order Entry Date</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Est. Ship Date</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">ETA</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Approved</td>")
                                    .AppendLine("    </tr>");

                                foreach (InventoryApprovalForm item in ctx.InventoryApprovalItems.Where(x => x.Status.StatusDescription == "Approved" && x.Timestamp >= DbFunctions.AddDays(DateTime.Now, -1 * days)))
                                {
                                    bodyHtml.AppendLine("    <tr>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:15%;color:#000;\">").Append(item.Vendor.VendorName).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.PurchaseOrderNumber).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:15%;color:#000;\">").Append(item.MaterialGroup).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.Cost.ToString("c0")).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.ContainerNumber).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.Priority.PriorityText).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.Timestamp.ToString("MM/dd/yy")).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.EstimatedShipDate.Value.ToString("MM/dd/yy")).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.EstimatedTimeOfArrival.Value.ToString("MM/dd/yy")).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.ApprovedDate.Value.ToString("MM/dd/yy")).Append(" <br />by ").Append(item.ApprovedBy.DisplayName)
                                        .Append("</td>")
                                        .AppendLine("    </tr>");
                                }
                            }

                            bodyHtml.AppendLine("    <tr><td colspan=\"10\" style=\"padding-bottom:5px;\"</td></tr>");
                        }
                    }

                    if (notifyAbout == "Ordered" || notifyAbout == "All")
                    {
                        bodyHtml.AppendLine("    <tr>")
                            .AppendLine("        <td colspan=\"10\" style=\"text-align: center;vertical-align: middle;font-weight: bold;font-size: 16px;border-top: 4px solid #d0604c; border-bottom: 4px solid #d0604c; color:#000; background-color:#FFF;\">Ordered</td>")
                            .AppendLine("    </tr>");

                        using (FormContext ctx = new FormContext())
                        {
                            if (ctx.InventoryApprovalItems.Any(x => x.Status.StatusDescription == "Ordered" && x.OrderDate.Value >= DbFunctions.AddDays(DateTime.Now, -1 * days)))
                            {
                                bodyHtml.AppendLine("    <tr>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:15%;color:#bc4445;\">Vendor</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">PO #</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:15%;color:#bc4445;\">Material Group</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Cost</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Container #</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Priority</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Order Entry Date</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Ship Date</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">ETA</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Ordered</td>")
                                    .AppendLine("    </tr>");

                                foreach (InventoryApprovalForm item in ctx.InventoryApprovalItems.Where(x => x.Status.StatusDescription == "Ordered" && x.OrderDate >= DbFunctions.AddDays(DateTime.Now, -1 * days)))
                                {
                                    bodyHtml.AppendLine("    <tr>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:15%;color:#000;\">").Append(item.Vendor.VendorName).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.PurchaseOrderNumber).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:15%;color:#000;\">").Append(item.MaterialGroup).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.Cost.ToString("c0")).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.ContainerNumber).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.Priority.PriorityText).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.Timestamp.ToString("MM/dd/yy")).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.ActualShipDate.Value.ToString("MM/dd/yy")).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.EstimatedTimeOfArrival.Value.ToString("MM/dd/yy")).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.OrderDate.Value.ToString("MM/dd/yy")).Append(" <br />by ").Append(item.OrderedBy.DisplayName)
                                        .Append("</td>")
                                        .AppendLine("    </tr>");
                                }
                            }

                            bodyHtml.AppendLine("    <tr><td colspan=\"10\" style=\"padding-bottom:5px;\"</td></tr>");
                        }
                    }

                    if (notifyAbout == "Arrived" || notifyAbout == "All")
                    {
                        bodyHtml.AppendLine("    <tr>")
                            .AppendLine("        <td colspan=\"10\" style=\"text-align: center;vertical-align: middle;font-weight: bold;font-size: 16px;border-top: 4px solid #d0604c; border-bottom: 4px solid #d0604c; color:#000; background-color:#FFF;\">Arrived</td>")
                            .AppendLine("    </tr>");

                        using (FormContext ctx = new FormContext())
                        {
                            if (ctx.InventoryApprovalItems.Any(x => x.Status.StatusDescription == "Arrived" && x.ArrivalDate.Value >= DbFunctions.AddDays(DateTime.Now, -1 * days)))
                            {
                                bodyHtml.AppendLine("    <tr>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:15%;color:#bc4445;\">Vendor</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">PO #</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:15%;color:#bc4445;\">Material Group</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Cost</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Container #</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Priority</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Ordered Date</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Ship Date</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Arrived</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Time To Arrival</td>")
                                    .AppendLine("    </tr>");

                                foreach (InventoryApprovalForm item in ctx.InventoryApprovalItems.Where(x => x.Status.StatusDescription == "Arrived" && x.ArrivalDate >= DbFunctions.AddDays(DateTime.Now, -1 * days)))
                                {
                                    bodyHtml.AppendLine("    <tr>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:15%;color:#000;\">").Append(item.Vendor.VendorName).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.PurchaseOrderNumber).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:15%;color:#000;\">").Append(item.MaterialGroup).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.Cost.ToString("c0")).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.ContainerNumber).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.Priority.PriorityText).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.OrderDate.Value.ToString("MM/dd/yy")).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.ActualShipDate.Value.ToString("MM/dd/yy")).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.ArrivalDate.Value.ToString("MM/dd/yy")).Append(" <br />by ").Append(item.ReceivedBy.DisplayName)
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.TimeToArrival).Append("</td>")
                                        .Append("</td>")
                                        .AppendLine("    </tr>");
                                }
                            }

                            bodyHtml.AppendLine("    <tr><td colspan=\"10\" style=\"padding-bottom:5px;\"</td></tr>");
                        }
                    }

                    if (notifyAbout == "Invoiced" || notifyAbout == "All")
                    {
                        bodyHtml.AppendLine("    <tr>")
                            .AppendLine("        <td colspan=\"10\" style=\"text-align: center;vertical-align: middle;font-weight: bold;font-size: 16px;border-top: 4px solid #d0604c; border-bottom: 4px solid #d0604c; color:#000; background-color:#FFF;\">Invoiced</td>")
                            .AppendLine("    </tr>");

                        using (FormContext ctx = new FormContext())
                        {
                            if (ctx.InventoryApprovalItems.Any(x => x.Status.StatusDescription == "Invoiced" && x.InvoiceDate.Value >= DbFunctions.AddDays(DateTime.Now, -1 * days)))
                            {
                                bodyHtml.AppendLine("    <tr>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:15%;color:#bc4445;\">Vendor</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">PO #</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:15%;color:#bc4445;\">Material Group</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Cost</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Container #</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Priority</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Ordered Date</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Arrival Date</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Time To Arrival</td>")
                                    .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#bc4445;\">Invoiced</td>")
                                    .AppendLine("    </tr>");

                                foreach (InventoryApprovalForm item in ctx.InventoryApprovalItems.Where(x => x.Status.StatusDescription == "Invoiced" && x.InvoiceDate >= DbFunctions.AddDays(DateTime.Now, -1 * days)))
                                {
                                    bodyHtml.AppendLine("    <tr>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:15%;color:#000;\">").Append(item.Vendor.VendorName).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.PurchaseOrderNumber).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:15%;color:#000;\">").Append(item.MaterialGroup).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.Cost.ToString("c0")).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.ContainerNumber).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.Priority.PriorityText).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.OrderDate.Value.ToString("MM/dd/yy")).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.ArrivalDate.Value.ToString("MM/dd/yy")).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.TimeToArrival).Append("</td>")
                                        .AppendLine("        <td style=\"text-align:center;font-size:12px;width:10%;color:#000;\">").Append(item.InvoiceDate.Value.ToString("MM/dd/yy")).Append(" <br />by ").Append(item.InvoicedBy.DisplayName)
                                        
                                        .Append("</td>")
                                        .AppendLine("    </tr>");
                                }
                            }

                            bodyHtml.AppendLine("    <tr><td colspan=\"10\" style=\"padding-bottom:5px;\"</td></tr>");
                        }
                    }

                    bodyHtml.AppendLine("</table><br /><br />");

                    if (sendComments && anyComments)
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
                            //else
                            //{
                            //    bodyHtml.AppendLine("<div style=\"width:80%; background-color:#FFF; margin: 0 auto; text-align:right; font-size:12px; color:#bc4445; word-wrap: break-word;\">")
                            //        .AppendLine("<div style=\"width:95%; padding: 5px;\">").AppendLine(comment.Note).AppendLine("<br /><br />")
                            //        .AppendLine(comment.Timestamp.ToString())
                            //        .AppendLine("</div></div>");
                            //}
                        }
                    }

                    bool result = msg.SendInventoryNotification(emailList, bodyHtml.ToString(), currentUser);

                    lblNotificationMessage.Text = "Inventory Approval Notification Sent Successfully.";
                }
                else
                {
                    lblNotificationMessage.Text = "No Email Addresses were selected.  Notification not sent.";
                }

            }
            catch (Exception ex)
            {
                lblNotificationMessage.Text = "An error occured during the notification.  <br /><br />Please contact your System Administrator for more information.";
            }
        }

        protected void btnPostComment_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(txtComments.Text))
                {
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    UserLogic uLogic = new UserLogic();
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);

                    using (FormContext ctx = new FormContext())
                    {
                        InventoryComments newComment = new InventoryComments
                        {
                            Note = txtComments.Text,
                            SystemComment = false,
                            Timestamp = DateTime.Now,
                            User = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == currentUser.SystemUserID)
                        };

                        ctx.InventoryApprovalComments.Add(newComment);
                        ctx.SaveChanges();

                        txtComments.Text = "";
                        rptrComments.DataBind();
                        lblCommentMessage.Text = "";
                    }
                }
            }
            catch(Exception ex)
            {
                lblCommentMessage.Text = "Unable to Save Comment";
            }
        }

        public IEnumerable<InventoryComments> rptrComments_GetData()
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    return ctx.InventoryApprovalComments
                        .Include(x => x.User)
                        .OrderByDescending(x => x.RecordId)
                        .ToList();

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string LoadCommentLiteral(Boolean SystemComment, String Note, String DisplayName, String Timestamp)
        {
            try
            {
                if (SystemComment == false)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("<div style=\"width: 90%; max-width: 800px; background-color: #bc4445; margin: 0 auto; text-align: left; font-size: 14px; color: white; overflow: hidden; word-wrap: break-word;\">")
                        .AppendLine("<div style=\"width: 95%; padding: 5px;\">")
                        .Append(Note).AppendLine("<br /><br /><span style=\"padding-right: 20px\">by")
                        .Append(DisplayName).Append("</span>").Append(Timestamp).AppendLine("</div></div><br />");

                    return sb.ToString();
                }
                //else
                //{
                //    StringBuilder sb = new StringBuilder();

                //    CheckBox cbShowSystemComments = (CheckBox)fvReport.FindControl("cbShowSystemComments");

                //    if (cbShowSystemComments.Checked)
                //    {
                //        sb.AppendLine("<div style=\"width: 80%; max-width: 800px; background-color: #FFF; margin: 0 auto; text-align: right; font-size: 12px; color: #bc4445; overflow: hidden; word-wrap: break-word;\">")
                //            .AppendLine("<div style=\"width: 95%; padding: 5px; \">")
                //            .Append(Note).AppendLine("<br /><br />")
                //            .Append(Timestamp).AppendLine("</div></div><br />");
                //    }
                //    else
                //    {
                //        sb.AppendLine("");
                //    }

                //    return sb.ToString();
                //}
                
                return "";
            }
            catch
            {
                
                return "";
            }

        }

        protected void ddlOverLastGlobal_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            try
            {
                RebindGrids();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        protected void ddlCompanyGlobal_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            try
            {
                RebindGrids();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void ddlVendorsGlobal_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            try
            {
                RebindGrids();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void SetInTheLastText(object sender, EventArgs e)
        {
            Label lbl = sender as Label;

            if (ddlOverLastGlobal.SelectedText == "All")
            {
                lbl.Text = " - All Records";
            }
            else
            {
                lbl.Text = " in the last " + ddlOverLastGlobal.SelectedText;
            }
        }
    }
}