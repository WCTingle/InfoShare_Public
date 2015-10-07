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
    public partial class UserDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                UserLogic uLogic = new UserLogic();
                SystemUsers currentUser = uLogic.GetCurrentUser(user);

                LoadFavoriteForms();
                BindSummary();
                LoadFormPermissions();
            }


        }

        public void FormRedirect(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                if (btn.CommandArgument != null)
                {
                    Response.Redirect(btn.CommandArgument);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
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
                        divOrderCancellationLink.Visible = true;
                        divOrderCancellationLinkReq.Visible = true;
                    }
                    else
                    {
                        divOrderCancellationLink.Visible = false;
                        divOrderCancellationLinkReq.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Expedited Order" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divExpeditedOrderLink.Visible = true;
                        divExpeditedOrderLinkReq.Visible = true;
                    }
                    else
                    {
                        divExpeditedOrderLink.Visible = false;
                        divExpeditedOrderLinkReq.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Price Change Request" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divPriceChangeRequestLink.Visible = true;
                        divPriceChangeRequestLinkReq.Visible = true;
                    }
                    else
                    {
                        divPriceChangeRequestLink.Visible = false;
                        divPriceChangeRequestLinkReq.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Hot Rush" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divHotRushLink.Visible = true;
                        divHotRushLinkReq.Visible = true;
                    }
                    else
                    {
                        divHotRushLink.Visible = false;
                        divHotRushLinkReq.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Low Inventory" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divLowInventoryLink.Visible = true;
                        divLowInventoryLinkReq.Visible = true;
                    }
                    else
                    {
                        divLowInventoryLink.Visible = false;
                        divLowInventoryLinkReq.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Sample Request" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divSampleRequestLink.Visible = true;
                        divSampleRequestLinkReq.Visible = true;
                    }
                    else
                    {
                        divSampleRequestLink.Visible = false;
                        divSampleRequestLinkReq.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Direct Order" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divDirectOrderLink.Visible = true;
                        divDirectOrderLinkReq.Visible = true;
                    }
                    else
                    {
                        divDirectOrderLink.Visible = false;
                        divDirectOrderLinkReq.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Request For Check" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divRequestForCheckLink.Visible = true;
                        divRequestForCheckLinkReq.Visible = true;
                    }
                    else
                    {
                        divRequestForCheckLink.Visible = false;
                        divRequestForCheckLinkReq.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Must Include" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divMustIncludeLink.Visible = true;
                        divMustIncludeLinkReq.Visible = true;
                    }
                    else
                    {
                        divMustIncludeLink.Visible = false;
                        divMustIncludeLinkReq.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Cannot Wait For Container" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divCannotWaitForContainerLink.Visible = true;
                        divCannotWaitForContainerLinkReq.Visible = true;
                    }
                    else
                    {
                        divCannotWaitForContainerLink.Visible = false;
                        divCannotWaitForContainerLinkReq.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void LoadFavoriteForms()
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    UserLogic uLogic = new UserLogic();
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);
                    var formPermissions = ctx.FormPermissions;

                    if (currentUser != null)
                    {
                        if (ctx.FavoriteForms.Any(f => f.User.SystemUserID == currentUser.SystemUserID))
                        {
                            phNoFavorites.Visible = false;

                            var favForms = ctx.FavoriteForms.Where(f => f.User.SystemUserID == currentUser.SystemUserID).ToList();

                            if (favForms.Any(f => f.Form.FormName == "Expedited Order") &&
                                formPermissions.Any(x => x.FormName == "Expedited Order" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                            {
                                phFavoriteExpeditedOrder.Visible = true;
                            }
                            else
                            {
                                phFavoriteExpeditedOrder.Visible = false;
                            }

                            if (favForms.Any(f => f.Form.FormName == "Direct Order") &&
                                formPermissions.Any(x => x.FormName == "Direct Order" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                            {
                                phFavoriteDirectOrder.Visible = true;
                            }
                            else
                            {
                                phFavoriteDirectOrder.Visible = false;
                            }

                            if (favForms.Any(f => f.Form.FormName == "Hot Rush") &&
                                formPermissions.Any(x => x.FormName == "Hot Rush" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                            {
                                phFavoriteHotRush.Visible = true;
                            }
                            else
                            {
                                phFavoriteHotRush.Visible = false;
                            }

                            if (favForms.Any(f => f.Form.FormName == "Order Cancellation") &&
                                formPermissions.Any(x => x.FormName == "Order Cancellation" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                            {
                                phFavoriteOrderCancellation.Visible = true;
                            }
                            else
                            {
                                phFavoriteOrderCancellation.Visible = false;
                            }

                            if (favForms.Any(f => f.Form.FormName == "Must Include") &&
                                formPermissions.Any(x => x.FormName == "Must Include" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                            {
                                phFavoriteMustInclude.Visible = true;
                            }
                            else
                            {
                                phFavoriteMustInclude.Visible = false;
                            }

                            if (favForms.Any(f => f.Form.FormName == "Sample Request") &&
                                formPermissions.Any(x => x.FormName == "Sample Request" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                            {
                                phFavoriteSampleRequest.Visible = true;
                            }
                            else
                            {
                                phFavoriteSampleRequest.Visible = false;
                            }

                            if (favForms.Any(f => f.Form.FormName == "Low Inventory") &&
                                formPermissions.Any(x => x.FormName == "Low Inventory" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                            {
                                phFavoriteLowInventory.Visible = true;
                            }
                            else
                            {
                                phFavoriteLowInventory.Visible = false;
                            }

                            if (favForms.Any(f => f.Form.FormName == "Price Change Request") &&
                                formPermissions.Any(x => x.FormName == "Price Change Request" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                            {
                                phFavoritePriceChangeRequest.Visible = true;
                            }
                            else
                            {
                                phFavoritePriceChangeRequest.Visible = false;
                            }

                            if (favForms.Any(f => f.Form.FormName == "Request For Check") &&
                                formPermissions.Any(x => x.FormName == "Request For Check" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                            {
                                phFavoriteRequestForCheck.Visible = true;
                            }
                            else
                            {
                                phFavoriteRequestForCheck.Visible = false;
                            }

                            if (favForms.Any(f => f.Form.FormName == "Cannot Wait For Container") &&
                                formPermissions.Any(x => x.FormName == "Cannot Wait For Container" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                            {
                                phFavoriteCannotWaitForContainer.Visible = true;
                            }
                            else
                            {
                                phFavoriteCannotWaitForContainer.Visible = false;
                            }

                        }
                        else
                        {
                            phNoFavorites.Visible = true;

                            phFavoriteExpeditedOrder.Visible = false;
                            phFavoritePriceChangeRequest.Visible = false;
                            phFavoriteOrderCancellation.Visible = false;
                            phFavoriteHotRush.Visible = false;
                            phFavoriteLowInventory.Visible = false;
                            phFavoriteSampleRequest.Visible = false;
                            phFavoriteDirectOrder.Visible = false;
                            phFavoriteRequestForCheck.Visible = false;
                            phFavoriteMustInclude.Visible = false;
                            phFavoriteCannotWaitForContainer.Visible = false;

                        }
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
                    IQueryable<Tingle_WebForms.Models.ExpeditedOrderForm> expeditedOrderList = ctx.ExpeditedOrderForms.Where(x => x.Status.StatusText != "Completed");
                    IQueryable<Tingle_WebForms.Models.DirectOrderForm> directOrderList = ctx.DirectOrderForms.Where(x => x.Status.StatusText != "Completed");
                    IQueryable<Tingle_WebForms.Models.HotRushForm> hotRushList = ctx.HotRushForms.Where(x => x.Status.StatusText != "Completed");
                    IQueryable<Tingle_WebForms.Models.OrderCancellationForm> orderCancellationList = ctx.OrderCancellationForms.Where(x => x.Status.StatusText != "Completed");
                    IQueryable<Tingle_WebForms.Models.MustIncludeForm> mustIncludeList = ctx.MustIncludeForms.Where(x => x.Status.StatusText != "Completed");
                    IQueryable<Tingle_WebForms.Models.SampleRequestForm> sampleRequestList = ctx.SampleRequestForms.Where(x => x.Status.StatusText != "Completed");
                    IQueryable<Tingle_WebForms.Models.LowInventoryForm> lowInventoryList = ctx.LowInventoryForms.Where(x => x.Status.StatusText != "Completed");
                    IQueryable<Tingle_WebForms.Models.PriceChangeRequestForm> priceChangeRequestList = ctx.PriceChangeRequestForms.Where(x => x.Status.StatusText != "Completed");
                    IQueryable<Tingle_WebForms.Models.RequestForCheckForm> requestForCheckList = ctx.RequestForCheckForms.Where(x => x.Status.StatusText != "Completed");
                    IQueryable<Tingle_WebForms.Models.CannotWaitForContainerForm> cannotWaitForContainerList = ctx.CannotWaitForContainerForms.Where(x => x.Status.StatusText != "Completed");


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



                    //Load Requested LinkButtons
                    IQueryable<Tingle_WebForms.Models.ExpeditedOrderForm> expeditedOrderListReq = ctx.ExpeditedOrderForms.Where(x => x.Status.StatusText != "Completed");
                    IQueryable<Tingle_WebForms.Models.DirectOrderForm> directOrderListReq = ctx.DirectOrderForms.Where(x => x.Status.StatusText != "Completed");
                    IQueryable<Tingle_WebForms.Models.HotRushForm> hotRushListReq = ctx.HotRushForms.Where(x => x.Status.StatusText != "Completed");
                    IQueryable<Tingle_WebForms.Models.OrderCancellationForm> orderCancellationListReq = ctx.OrderCancellationForms.Where(x => x.Status.StatusText != "Completed");
                    IQueryable<Tingle_WebForms.Models.MustIncludeForm> mustIncludeListReq = ctx.MustIncludeForms.Where(x => x.Status.StatusText != "Completed");
                    IQueryable<Tingle_WebForms.Models.SampleRequestForm> sampleRequestListReq = ctx.SampleRequestForms.Where(x => x.Status.StatusText != "Completed");
                    IQueryable<Tingle_WebForms.Models.LowInventoryForm> lowInventoryListReq = ctx.LowInventoryForms.Where(x => x.Status.StatusText != "Completed");
                    IQueryable<Tingle_WebForms.Models.PriceChangeRequestForm> priceChangeRequestListReq = ctx.PriceChangeRequestForms.Where(x => x.Status.StatusText != "Completed");
                    IQueryable<Tingle_WebForms.Models.RequestForCheckForm> requestForCheckListReq = ctx.RequestForCheckForms.Where(x => x.Status.StatusText != "Completed");
                    IQueryable<Tingle_WebForms.Models.CannotWaitForContainerForm> cannotWaitForContainerListReq = ctx.CannotWaitForContainerForms.Where(x => x.Status.StatusText != "Completed");

                    expeditedOrderListReq = expeditedOrderListReq.Where(f => f.RequestedUser.SystemUserID == currentUser.SystemUserID);

                    directOrderListReq = directOrderListReq.Where(f => f.RequestedUser.SystemUserID == currentUser.SystemUserID);

                    hotRushListReq = hotRushListReq.Where(f => f.RequestedUser.SystemUserID == currentUser.SystemUserID);

                    orderCancellationListReq = orderCancellationListReq.Where(f => f.RequestedUser.SystemUserID == currentUser.SystemUserID);

                    mustIncludeListReq = mustIncludeListReq.Where(f => f.RequestedUser.SystemUserID == currentUser.SystemUserID);

                    sampleRequestListReq = sampleRequestListReq.Where(f => f.RequestedUser.SystemUserID == currentUser.SystemUserID);

                    lowInventoryListReq = lowInventoryListReq.Where(f => f.RequestedUser.SystemUserID == currentUser.SystemUserID);

                    priceChangeRequestListReq = priceChangeRequestListReq.Where(f => f.RequestedUser.SystemUserID == currentUser.SystemUserID);

                    requestForCheckListReq = requestForCheckListReq.Where(f => f.RequestedUser.SystemUserID == currentUser.SystemUserID);

                    cannotWaitForContainerListReq = cannotWaitForContainerListReq.Where(f => f.RequestedUser.SystemUserID == currentUser.SystemUserID);


                    if (expeditedOrderListReq.Count() != 1) { lbExpeditedOrderCountReq.Text = expeditedOrderListReq.Count().ToString() + " Requests"; }
                    else { lbExpeditedOrderCountReq.Text = expeditedOrderListReq.Count().ToString() + " Request"; }

                    if (directOrderListReq.Count() != 1) { lbDirectOrderCountReq.Text = directOrderListReq.Count().ToString() + " Requests"; }
                    else { lbDirectOrderCountReq.Text = directOrderListReq.Count().ToString() + " Request"; }

                    if (hotRushListReq.Count() != 1) { lbHotRushCountReq.Text = hotRushListReq.Count().ToString() + " Requests"; }
                    else { lbHotRushCountReq.Text = hotRushListReq.Count().ToString() + " Request"; }

                    if (orderCancellationListReq.Count() != 1) { lbOrderCancellationCountReq.Text = orderCancellationListReq.Count().ToString() + " Requests"; }
                    else { lbOrderCancellationCountReq.Text = orderCancellationListReq.Count().ToString() + " Request"; }

                    if (mustIncludeListReq.Count() != 1) { lbMustIncludeCountReq.Text = mustIncludeListReq.Count().ToString() + " Requests"; }
                    else { lbMustIncludeCountReq.Text = mustIncludeListReq.Count().ToString() + " Request"; }

                    if (sampleRequestListReq.Count() != 1) { lbSampleRequestCountReq.Text = sampleRequestListReq.Count().ToString() + " Requests"; }
                    else { lbSampleRequestCountReq.Text = sampleRequestListReq.Count().ToString() + " Request"; }

                    if (lowInventoryListReq.Count() != 1) { lbLowInventoryCountReq.Text = lowInventoryListReq.Count().ToString() + " Requests"; }
                    else { lbLowInventoryCountReq.Text = lowInventoryListReq.Count().ToString() + " Request"; }

                    if (priceChangeRequestListReq.Count() != 1) { lbPriceChangeRequestCountReq.Text = priceChangeRequestListReq.Count().ToString() + " Requests"; }
                    else { lbPriceChangeRequestCountReq.Text = priceChangeRequestListReq.Count().ToString() + " Request"; }

                    if (requestForCheckListReq.Count() != 1) { lbRequestForCheckCountReq.Text = requestForCheckListReq.Count().ToString() + " Requests"; }
                    else { lbRequestForCheckCountReq.Text = requestForCheckListReq.Count().ToString() + " Request"; }

                    if (cannotWaitForContainerListReq.Count() != 1) { lbCannotWaitForContainerCountReq.Text = cannotWaitForContainerListReq.Count().ToString() + " Requests"; }
                    else { lbCannotWaitForContainerCountReq.Text = cannotWaitForContainerListReq.Count().ToString() + " Request"; }

                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        protected void lbExpeditedOrderCount_Click(object sender, EventArgs e)
        {
            Session["MyForms"] = "Assigned";
            Response.Redirect("/ReportExpeditedOrder.aspx");
        }

        protected void lbDirectOrderCount_Click(object sender, EventArgs e)
        {
            Session["MyForms"] = "Assigned";
            Response.Redirect("/ReportDirectOrder.aspx");
        }

        protected void lbHotRushCount_Click(object sender, EventArgs e)
        {
            Session["MyForms"] = "Assigned";
            Response.Redirect("/ReportHotRush.aspx");
        }

        protected void lbOrderCancellationCount_Click(object sender, EventArgs e)
        {
            Session["MyForms"] = "Assigned";
            Response.Redirect("/ReportOrderCancellation.aspx");
        }

        protected void lbMustIncludeCount_Click(object sender, EventArgs e)
        {
            Session["MyForms"] = "Assigned";
            Response.Redirect("/ReportMustInclude.aspx");
        }

        protected void lbSampleRequestCount_Click(object sender, EventArgs e)
        {
            Session["MyForms"] = "Assigned";
            Response.Redirect("/ReportSampleRequest.aspx");
        }

        protected void lbLowInventoryCount_Click(object sender, EventArgs e)
        {
            Session["MyForms"] = "Assigned";
            Response.Redirect("/ReportLowInventory.aspx");
        }

        protected void lbPriceChangeRequestCount_Click(object sender, EventArgs e)
        {
            Session["MyForms"] = "Assigned";
            Response.Redirect("/ReportPriceChangeRequest.aspx");
        }

        protected void lbRequestForCheckCount_Click(object sender, EventArgs e)
        {
            Session["MyForms"] = "Assigned";
            Response.Redirect("/ReportRequestForCheck.aspx");
        }

        protected void lbExpeditedOrderCountReq_Click(object sender, EventArgs e)
        {
            Session["MyForms"] = "Requested";
            Response.Redirect("/ReportExpeditedOrder.aspx");
        }

        protected void lbDirectOrderCountReq_Click(object sender, EventArgs e)
        {
            Session["MyForms"] = "Requested";
            Response.Redirect("/ReportDirectOrder.aspx");
        }

        protected void lbHotRushCountReq_Click(object sender, EventArgs e)
        {
            Session["MyForms"] = "Requested";
            Response.Redirect("/ReportHotRush.aspx");
        }

        protected void lbOrderCancellationCountReq_Click(object sender, EventArgs e)
        {
            Session["MyForms"] = "Requested";
            Response.Redirect("/ReportOrderCancellation.aspx");
        }

        protected void lbMustIncludeCountReq_Click(object sender, EventArgs e)
        {
            Session["MyForms"] = "Requested";
            Response.Redirect("/ReportMustInclude.aspx");
        }

        protected void lbSampleRequestCountReq_Click(object sender, EventArgs e)
        {
            Session["MyForms"] = "Requested";
            Response.Redirect("/ReportSampleRequest.aspx");
        }

        protected void lbLowInventoryCountReq_Click(object sender, EventArgs e)
        {
            Session["MyForms"] = "Requested";
            Response.Redirect("/ReportLowInventory.aspx");
        }

        protected void lbPriceChangeRequestCountReq_Click(object sender, EventArgs e)
        {
            Session["MyForms"] = "Requested";
            Response.Redirect("/ReportPriceChangeRequest.aspx");
        }

        protected void lbRequestForCheckCountReq_Click(object sender, EventArgs e)
        {
            Session["MyForms"] = "Requested";
            Response.Redirect("/ReportRequestForCheck.aspx");
        }

        protected void lbCannotWaitForContainerCount_Click(object sender, EventArgs e)
        {
            Session["MyForms"] = "Assigned";
            Response.Redirect("/ReportCannotWaitForContainer.aspx");
        }

        protected void lbCannotWaitForContainerCountReq_Click(object sender, EventArgs e)
        {
            Session["MyForms"] = "Requested";
            Response.Redirect("/ReportCannotWaitForContainer.aspx");
        }

    }
}