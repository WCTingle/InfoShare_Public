using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tingle_WebForms.Models;
using Tingle_WebForms.Logic;
using System.Text;

namespace Tingle_WebForms.Reports
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                rblMe.SelectedIndex = 0;
                BindSummary();
                LoadFormPermissions();
            }

            
            Session["Company"] = ddlCompany.SelectedValue;
            Session["GlobalStatus"] = ddlGlobalStatus.SelectedValue;
            Session["MyForms"] = rblMe.SelectedValue;
        }

        protected void LoadFormPermissions()
        {
            try
            {
                System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                UserLogic uLogic = new UserLogic();
                SystemUsers currentUser = uLogic.GetCurrentUser(user);

                using (FormContext ctx = new FormContext())
                {
                    var formPermissions = ctx.FormPermissions;

                    if (formPermissions.Any(x => x.FormName == "Order Cancellation" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divOrderCancellation.Visible = true;
                        divOrderCancellationLink.Visible = true;
                    }
                    else
                    {
                        divOrderCancellation.Visible = false;
                        divOrderCancellationLink.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Expedited Order" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divExpeditedOrder.Visible = true;
                        divExpeditedOrderLink.Visible = true;
                    }
                    else
                    {
                        divExpeditedOrder.Visible = false;
                        divExpeditedOrderLink.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Price Change Request" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divPriceChangeRequest.Visible = true;
                        divPriceChangeRequestLink.Visible = true;
                    }
                    else
                    {
                        divPriceChangeRequest.Visible = false;
                        divPriceChangeRequestLink.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Hot Rush" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divHotRush.Visible = true;
                        divHotRushLink.Visible = true;
                    }
                    else
                    {
                        divHotRush.Visible = false;
                        divHotRushLink.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Low Inventory" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divLowInventory.Visible = true;
                        divLowInventoryLink.Visible = true;
                    }
                    else
                    {
                        divLowInventory.Visible = false;
                        divLowInventoryLink.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Sample Request" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divSampleRequest.Visible = true;
                        divSampleRequestLink.Visible = true;
                    }
                    else
                    {
                        divSampleRequest.Visible = false;
                        divSampleRequestLink.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Direct Order" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divDirectOrder.Visible = true;
                        divDirectOrderLink.Visible = true;
                    }
                    else
                    {
                        divDirectOrder.Visible = false;
                        divDirectOrderLink.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Request For Check" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divRequestForCheck.Visible = true;
                        divRequestForCheckLink.Visible = true;
                    }
                    else
                    {
                        divRequestForCheck.Visible = false;
                        divRequestForCheckLink.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Must Include" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divMustInclude.Visible = true;
                        divMustIncludeLink.Visible = true;
                    }
                    else
                    {
                        divMustInclude.Visible = false;
                        divMustIncludeLink.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Cannot Wait For Container" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divCannotWaitForContainer.Visible = true;
                        divCannotWaitForContainerLink.Visible = true;
                    }
                    else
                    {
                        divCannotWaitForContainer.Visible = false;
                        divCannotWaitForContainerLink.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }


        }

        private void BindSummary()
        {
            try
            {
                System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                UserLogic uLogic = new UserLogic();
                SystemUsers currentUser = uLogic.GetCurrentUser(user);

                using (FormContext ctx = new FormContext())
                {
                    IQueryable<Tingle_WebForms.Models.ExpeditedOrderForm> expeditedOrderList = ctx.ExpeditedOrderForms;
                    IQueryable<Tingle_WebForms.Models.DirectOrderForm> directOrderList = ctx.DirectOrderForms;
                    IQueryable<Tingle_WebForms.Models.HotRushForm> hotRushList = ctx.HotRushForms;
                    IQueryable<Tingle_WebForms.Models.OrderCancellationForm> orderCancellationList = ctx.OrderCancellationForms;
                    IQueryable<Tingle_WebForms.Models.MustIncludeForm> mustIncludeList = ctx.MustIncludeForms;
                    IQueryable<Tingle_WebForms.Models.SampleRequestForm> sampleRequestList = ctx.SampleRequestForms;
                    IQueryable<Tingle_WebForms.Models.LowInventoryForm> lowInventoryList = ctx.LowInventoryForms;
                    IQueryable<Tingle_WebForms.Models.PriceChangeRequestForm> priceChangeRequestList = ctx.PriceChangeRequestForms;
                    IQueryable<Tingle_WebForms.Models.RequestForCheckForm> requestForCheckList = ctx.RequestForCheckForms;
                    IQueryable<Tingle_WebForms.Models.CannotWaitForContainerForm> cannotWaitForContainerList = ctx.CannotWaitForContainerForms;

                    if (Session["Company"] != null)
                    {
                        ddlCompany.SelectedValue = Session["Company"].ToString();

                        if (Session["Company"].ToString() != "Any")
                        {
                            String sessionCompany = Session["Company"].ToString();

                            expeditedOrderList = expeditedOrderList.Where(f => f.Company == sessionCompany);
                            directOrderList = directOrderList.Where(f => f.Company == sessionCompany);
                            hotRushList = hotRushList.Where(f => f.Company == sessionCompany);
                            orderCancellationList = orderCancellationList.Where(f => f.Company == sessionCompany);
                            mustIncludeList = mustIncludeList.Where(f => f.Company == sessionCompany);
                            sampleRequestList = sampleRequestList.Where(f => f.Company == sessionCompany);
                            lowInventoryList = lowInventoryList.Where(f => f.Company == sessionCompany);
                            priceChangeRequestList = priceChangeRequestList.Where(f => f.Company == sessionCompany);
                            requestForCheckList = requestForCheckList.Where(f => f.Company == sessionCompany);
                            cannotWaitForContainerList = cannotWaitForContainerList.Where(f => f.Company == sessionCompany);
                        }
                    }
                    else
                    {
                        Session["Company"] = "Any";
                        ddlCompany.SelectedValue = "Any";
                    }

                    if (Session["GlobalStatus"] != null)
                    {
                        ddlGlobalStatus.SelectedValue = Session["GlobalStatus"].ToString();

                        if (Session["GlobalStatus"].ToString() == "Active")
                        {
                            expeditedOrderList = expeditedOrderList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                            directOrderList = directOrderList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                            hotRushList = hotRushList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                            orderCancellationList = orderCancellationList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                            mustIncludeList = mustIncludeList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                            sampleRequestList = sampleRequestList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                            lowInventoryList = lowInventoryList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                            priceChangeRequestList = priceChangeRequestList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                            requestForCheckList = requestForCheckList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                            cannotWaitForContainerList = cannotWaitForContainerList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                        }
                        else if (Session["GlobalStatus"].ToString() == "Archive")
                        {
                            expeditedOrderList = expeditedOrderList.Where(f => f.Status.StatusText == "Completed");
                            directOrderList = directOrderList.Where(f => f.Status.StatusText == "Completed");
                            hotRushList = hotRushList.Where(f => f.Status.StatusText == "Completed");
                            orderCancellationList = orderCancellationList.Where(f => f.Status.StatusText == "Completed");
                            mustIncludeList = mustIncludeList.Where(f => f.Status.StatusText == "Completed");
                            sampleRequestList = sampleRequestList.Where(f => f.Status.StatusText == "Completed");
                            lowInventoryList = lowInventoryList.Where(f => f.Status.StatusText == "Completed");
                            priceChangeRequestList = priceChangeRequestList.Where(f => f.Status.StatusText == "Completed");
                            requestForCheckList = requestForCheckList.Where(f => f.Status.StatusText == "Completed");
                            cannotWaitForContainerList = cannotWaitForContainerList.Where(f => f.Status.StatusText == "Completed");
                        }
                    }
                    else
                    {
                        Session["GlobalStatus"] = "Active";
                        ddlGlobalStatus.SelectedValue = "Active";

                        expeditedOrderList = expeditedOrderList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                        directOrderList = directOrderList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                        hotRushList = hotRushList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                        orderCancellationList = orderCancellationList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                        mustIncludeList = mustIncludeList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                        sampleRequestList = sampleRequestList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                        lowInventoryList = lowInventoryList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                        priceChangeRequestList = priceChangeRequestList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                        requestForCheckList = requestForCheckList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                        cannotWaitForContainerList = cannotWaitForContainerList.Where(f => f.Status.StatusText == "In Progress" || f.Status.StatusText == "On Hold");
                    }

                    if (currentUser.UserRole.RoleName == "ReportsUser")
                    {
                        expeditedOrderList = expeditedOrderList.Where(f => f.SubmittedUser.SystemUserID == currentUser.SystemUserID
                            || f.RequestedUser.SystemUserID == currentUser.SystemUserID
                            || f.AssignedUser.SystemUserID == currentUser.SystemUserID);

                        directOrderList = directOrderList.Where(f => f.SubmittedUser.SystemUserID == currentUser.SystemUserID
                            || f.RequestedUser.SystemUserID == currentUser.SystemUserID
                            || f.AssignedUser.SystemUserID == currentUser.SystemUserID);

                        hotRushList = hotRushList.Where(f => f.SubmittedUser.SystemUserID == currentUser.SystemUserID
                            || f.RequestedUser.SystemUserID == currentUser.SystemUserID
                            || f.AssignedUser.SystemUserID == currentUser.SystemUserID);

                        orderCancellationList = orderCancellationList.Where(f => f.SubmittedUser.SystemUserID == currentUser.SystemUserID
                            || f.RequestedUser.SystemUserID == currentUser.SystemUserID
                            || f.AssignedUser.SystemUserID == currentUser.SystemUserID);

                        mustIncludeList = mustIncludeList.Where(f => f.SubmittedUser.SystemUserID == currentUser.SystemUserID
                            || f.RequestedUser.SystemUserID == currentUser.SystemUserID
                            || f.AssignedUser.SystemUserID == currentUser.SystemUserID);

                        sampleRequestList = sampleRequestList.Where(f => f.SubmittedUser.SystemUserID == currentUser.SystemUserID
                            || f.RequestedUser.SystemUserID == currentUser.SystemUserID
                            || f.AssignedUser.SystemUserID == currentUser.SystemUserID);

                        lowInventoryList = lowInventoryList.Where(f => f.SubmittedUser.SystemUserID == currentUser.SystemUserID
                            || f.RequestedUser.SystemUserID == currentUser.SystemUserID
                            || f.AssignedUser.SystemUserID == currentUser.SystemUserID);

                        priceChangeRequestList = priceChangeRequestList.Where(f => f.SubmittedUser.SystemUserID == currentUser.SystemUserID
                            || f.RequestedUser.SystemUserID == currentUser.SystemUserID
                            || f.AssignedUser.SystemUserID == currentUser.SystemUserID);

                        requestForCheckList = requestForCheckList.Where(f => f.SubmittedUser.SystemUserID == currentUser.SystemUserID
                            || f.RequestedUser.SystemUserID == currentUser.SystemUserID
                            || f.AssignedUser.SystemUserID == currentUser.SystemUserID);

                        cannotWaitForContainerList = cannotWaitForContainerList.Where(f => f.SubmittedUser.SystemUserID == currentUser.SystemUserID
                            || f.RequestedUser.SystemUserID == currentUser.SystemUserID
                            || f.AssignedUser.SystemUserID == currentUser.SystemUserID);
                    }

                    if (rblMe.SelectedValue == "Assigned")
                    {
                        expeditedOrderList = expeditedOrderList.Where(f => f.AssignedUser.SystemUserID == currentUser.SystemUserID);
                        directOrderList = directOrderList.Where(f => f.AssignedUser.SystemUserID == currentUser.SystemUserID);
                        hotRushList = hotRushList.Where(f => f.AssignedUser.SystemUserID == currentUser.SystemUserID);
                        orderCancellationList = orderCancellationList.Where(f => f.AssignedUser.SystemUserID == currentUser.SystemUserID);
                        mustIncludeList = mustIncludeList.Where(f => f.AssignedUser.SystemUserID == currentUser.SystemUserID);
                        sampleRequestList = sampleRequestList.Where(f => f.AssignedUser.SystemUserID == currentUser.SystemUserID);
                        lowInventoryList = lowInventoryList.Where(f => f.AssignedUser.SystemUserID == currentUser.SystemUserID);
                        priceChangeRequestList = priceChangeRequestList.Where(f => f.AssignedUser.SystemUserID == currentUser.SystemUserID);
                        requestForCheckList = requestForCheckList.Where(f => f.AssignedUser.SystemUserID == currentUser.SystemUserID);
                        cannotWaitForContainerList = cannotWaitForContainerList.Where(f => f.AssignedUser.SystemUserID == currentUser.SystemUserID);
                    }

                    if (rblMe.SelectedValue == "Requested")
                    {
                        expeditedOrderList = expeditedOrderList.Where(f => f.RequestedUser.SystemUserID == currentUser.SystemUserID);
                        directOrderList = directOrderList.Where(f => f.RequestedUser.SystemUserID == currentUser.SystemUserID);
                        hotRushList = hotRushList.Where(f => f.RequestedUser.SystemUserID == currentUser.SystemUserID);
                        orderCancellationList = orderCancellationList.Where(f => f.RequestedUser.SystemUserID == currentUser.SystemUserID);
                        mustIncludeList = mustIncludeList.Where(f => f.RequestedUser.SystemUserID == currentUser.SystemUserID);
                        sampleRequestList = sampleRequestList.Where(f => f.RequestedUser.SystemUserID == currentUser.SystemUserID);
                        lowInventoryList = lowInventoryList.Where(f => f.RequestedUser.SystemUserID == currentUser.SystemUserID);
                        priceChangeRequestList = priceChangeRequestList.Where(f => f.RequestedUser.SystemUserID == currentUser.SystemUserID);
                        requestForCheckList = requestForCheckList.Where(f => f.RequestedUser.SystemUserID == currentUser.SystemUserID);
                        cannotWaitForContainerList = cannotWaitForContainerList.Where(f => f.RequestedUser.SystemUserID == currentUser.SystemUserID);
                    }
                    if (rblMe.SelectedValue == "Created")
                    {
                        expeditedOrderList = expeditedOrderList.Where(f => f.SubmittedUser.SystemUserID == currentUser.SystemUserID);
                        directOrderList = directOrderList.Where(f => f.SubmittedUser.SystemUserID == currentUser.SystemUserID);
                        hotRushList = hotRushList.Where(f => f.SubmittedUser.SystemUserID == currentUser.SystemUserID);
                        orderCancellationList = orderCancellationList.Where(f => f.SubmittedUser.SystemUserID == currentUser.SystemUserID);
                        mustIncludeList = mustIncludeList.Where(f => f.SubmittedUser.SystemUserID == currentUser.SystemUserID);
                        sampleRequestList = sampleRequestList.Where(f => f.SubmittedUser.SystemUserID == currentUser.SystemUserID);
                        lowInventoryList = lowInventoryList.Where(f => f.SubmittedUser.SystemUserID == currentUser.SystemUserID);
                        priceChangeRequestList = priceChangeRequestList.Where(f => f.SubmittedUser.SystemUserID == currentUser.SystemUserID);
                        requestForCheckList = requestForCheckList.Where(f => f.SubmittedUser.SystemUserID == currentUser.SystemUserID);
                        cannotWaitForContainerList = cannotWaitForContainerList.Where(f => f.SubmittedUser.SystemUserID == currentUser.SystemUserID);
                    }


                    if (expeditedOrderList.Count() != 1) { lbExpeditedOrderCount.Text = expeditedOrderList.Count().ToString() + " Requests"; }
                    else { lbExpeditedOrderCount.Text = expeditedOrderList.Count().ToString() + " Request"; }

                    if (directOrderList.Count() != 1) { lbDirectOrderCount.Text = directOrderList.Count().ToString() + " Requests"; }
                    else { lbDirectOrderCount.Text = directOrderList.Count().ToString() + " Request"; }

                    if (hotRushList.Count() != 1) { lbHotRushCount.Text = hotRushList.Count().ToString() + " Requests"; }
                    else { lbHotRushCount.Text = hotRushList.Count().ToString() + " Request"; }

                    if (orderCancellationList.Count() != 1) { lbOrderCancellationCount.Text = orderCancellationList.Count().ToString() + " Requests"; }
                    else { lbOrderCancellationCount.Text = orderCancellationList.Count().ToString() + " Request"; }

                    if (mustIncludeList.Count() != 1) { lbMustIncludeCount.Text = mustIncludeList.Count().ToString() + " Requests"; }
                    else { lbMustIncludeCount.Text = mustIncludeList.Count().ToString() + " Request"; }

                    if (sampleRequestList.Count() != 1) { lbSampleRequestCount.Text = sampleRequestList.Count().ToString() + " Requests"; }
                    else { lbSampleRequestCount.Text = sampleRequestList.Count().ToString() + " Request"; }

                    if (lowInventoryList.Count() != 1) { lbLowInventoryCount.Text = lowInventoryList.Count().ToString() + " Requests"; }
                    else { lbLowInventoryCount.Text = lowInventoryList.Count().ToString() + " Request"; }

                    if (priceChangeRequestList.Count() != 1) { lbPriceChangeRequestCount.Text = priceChangeRequestList.Count().ToString() + " Requests"; }
                    else { lbPriceChangeRequestCount.Text = priceChangeRequestList.Count().ToString() + " Request"; }

                    if (requestForCheckList.Count() != 1) { lbRequestForCheckCount.Text = requestForCheckList.Count().ToString() + " Requests"; }
                    else { lbRequestForCheckCount.Text = requestForCheckList.Count().ToString() + " Request"; }

                    if (cannotWaitForContainerList.Count() != 1) { lbCannotWaitForContainerCount.Text = cannotWaitForContainerList.Count().ToString() + " Requests"; }
                    else { lbCannotWaitForContainerCount.Text = cannotWaitForContainerList.Count().ToString() + " Request"; }

                    UpdatePanel1.Update();


                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Company"] = ddlCompany.SelectedValue;
            BindSummary();
            UpdatePanel1.Update();
            LoadFormPermissions();
        }

        protected void ddlGlobalStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["GlobalStatus"] = ddlGlobalStatus.SelectedValue;
            BindSummary();
            UpdatePanel1.Update();
            LoadFormPermissions();
        }

        protected void rblMe_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["MyForms"] = rblMe.SelectedValue;
            BindSummary();
            UpdatePanel1.Update();
            LoadFormPermissions();
        }

        protected void rblMe_PreRender(object sender, EventArgs e)
        {
            if (Session["MyForms"] != null)
            {
                rblMe.SelectedValue = Session["MyForms"].ToString();
                BindSummary();
                UpdatePanel1.Update();
            }
            else
            {
                rblMe.SelectedIndex = 0;
                BindSummary();
                UpdatePanel1.Update();
            }
        }

    }
}