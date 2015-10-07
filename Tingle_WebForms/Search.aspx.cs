using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tingle_WebForms.Models;
using Tingle_WebForms.Logic;
using System.Text;
using System.Reflection;


namespace Tingle_WebForms
{
    public partial class Search : System.Web.UI.Page
    {
        private int RowCount
        {
            get
            {
                if (ViewState["RowCount"] != null)
                {
                    return (int)ViewState["RowCount"];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["RowCount"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["SearchValue"] != null)
                {
                    txtSearch.Text = Session["SearchValue"].ToString();
                    SearchForText(10, 0);
                }
            }
            else
            {
                plcPaging.Controls.Clear();
                CreatePagingControl();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Session["SearchValue"] = txtSearch.Text;
            SearchForText(10, 0);
        }

        private void SearchForText(int takeCount, int skipCount)
        {
            if (txtSearch.Text != "")
            {
                using (FormContext ctx = new FormContext())
                {
                    LiteralControl lineBreak = new LiteralControl("<br />");
                    LiteralControl divBegin = new LiteralControl("<div style=\"width:100%; float:left\">");
                    LiteralControl divEnd = new LiteralControl("</div>");

                    var searchResults = new List<SearchModel>();

                    int count = 0;
                    int resultCount = 0;

                    IQueryable<dynamic> expeditedOrderForms = ctx.ExpeditedOrderForms
                        .GroupJoin(ctx.Comments, f => f.RecordId, c => c.RelatedFormId, (f, c) => new { f, c })
                        .GroupJoin(ctx.SkuQuantityItems, fc => fc.f.RecordId, s => s.ExpeditedOrderForm.RecordId, (fc, s) => new { fc, s })
                        .GroupJoin(ctx.SystemUsers, fcs => fcs.fc.f.SubmittedUser.SystemUserID, su => su.SystemUserID, (fcs, su) => new { fcs, su })
                        .GroupJoin(ctx.SystemUsers, fcssu => fcssu.fcs.fc.f.LastModifiedUser.SystemUserID, mu => mu.SystemUserID, (fcssu, mu) => new { fcssu, mu })
                        .GroupJoin(ctx.SystemUsers, fcssumu => fcssumu.fcssu.fcs.fc.f.RequestedUser.SystemUserID, ru => ru.SystemUserID, (fcssumu, ru) => new { fcssumu, ru })
                        .Where(x => x.fcssumu.fcssu.fcs.fc.f.AccountNumber != null && x.fcssumu.fcssu.fcs.fc.f.AccountNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.Company != null && x.fcssumu.fcssu.fcs.fc.f.Company.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.ContactName != null && x.fcssumu.fcssu.fcs.fc.f.ContactName.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.Customer != null && x.fcssumu.fcssu.fcs.fc.f.Customer.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.ExpediteCode.Code != null && x.fcssumu.fcssu.fcs.fc.f.ExpediteCode.Code.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.ExpediteCode.Description != null && x.fcssumu.fcssu.fcs.fc.f.ExpediteCode.Description.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.OowOrderNumber != null && x.fcssumu.fcssu.fcs.fc.f.OowOrderNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.PhoneNumber != null && x.fcssumu.fcssu.fcs.fc.f.PhoneNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.PurchaseOrderNumber != null && x.fcssumu.fcssu.fcs.fc.f.PurchaseOrderNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.ShipToAddress != null && x.fcssumu.fcssu.fcs.fc.f.ShipToAddress.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.ShipToCity != null && x.fcssumu.fcssu.fcs.fc.f.ShipToCity.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.ShipToName != null && x.fcssumu.fcssu.fcs.fc.f.ShipToName.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.ShipToState != null && x.fcssumu.fcssu.fcs.fc.f.ShipToState.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.ShipToZip != null && x.fcssumu.fcssu.fcs.fc.f.ShipToZip.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.ShipToPhone != null && x.fcssumu.fcssu.fcs.fc.f.ShipToPhone.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.SM != null && x.fcssumu.fcssu.fcs.fc.f.SM.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.s.Any(sku => sku.MaterialSku.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcssumu.fcssu.fcs.s.Any(quan => quan.Quantity.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcssumu.fcssu.fcs.fc.f.Status.StatusText != null && x.fcssumu.fcssu.fcs.fc.f.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.su.Any(su => su.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcssumu.fcssu.su.Any(su => su.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcssumu.fcssu.su.Any(su => su.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcssumu.mu.Any(mu => mu.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcssumu.mu.Any(mu => mu.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcssumu.mu.Any(mu => mu.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcssumu.fcssu.fcs.fc.c.Any(comm => comm.Note.ToLower().Contains(txtSearch.Text.ToLower()) && comm.Form.FormName == "Expedited Order")
                        )
                        .SelectMany(z => z.fcssumu.fcssu.fcs.fc.c.DefaultIfEmpty(), (z, zz) => new
                        {
                            BaseFormData = z.fcssumu.fcssu.fcs.fc.f,
                            CommentsData = z.fcssumu.fcssu.fcs.fc.c,
                            SkuQuantityData = z.fcssumu.fcssu.fcs.s.DefaultIfEmpty(),
                            SubmittedUserData = z.fcssumu.fcssu.su.DefaultIfEmpty(),
                            ModifiedUserData = z.fcssumu.mu.DefaultIfEmpty(),
                            RequestedUserData = z.ru.DefaultIfEmpty()
                        });


                    IQueryable<dynamic> directOrderForms = ctx.DirectOrderForms
                        .GroupJoin(ctx.Comments, f => f.RecordId, c => c.RelatedFormId, (f, c) => new { f, c })
                        .GroupJoin(ctx.SkuQuantityItems, fc => fc.f.RecordId, s => s.DirectOrderForm.RecordId, (fc, s) => new { fc, s })
                        .GroupJoin(ctx.SystemUsers, fcs => fcs.fc.f.SubmittedUser.SystemUserID, su => su.SystemUserID, (fcs, su) => new { fcs, su })
                        .GroupJoin(ctx.SystemUsers, fcssu => fcssu.fcs.fc.f.LastModifiedUser.SystemUserID, mu => mu.SystemUserID, (fcssu, mu) => new { fcssu, mu })
                        .GroupJoin(ctx.SystemUsers, fcssumu => fcssumu.fcssu.fcs.fc.f.RequestedUser.SystemUserID, ru => ru.SystemUserID, (fcssumu, ru) => new { fcssumu, ru })
                        .Where(x => x.fcssumu.fcssu.fcs.fc.f.AccountNumber != null && x.fcssumu.fcssu.fcs.fc.f.AccountNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.Status.StatusText != null && x.fcssumu.fcssu.fcs.fc.f.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.ContactName != null && x.fcssumu.fcssu.fcs.fc.f.ContactName.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.Customer != null && x.fcssumu.fcssu.fcs.fc.f.Customer.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.ExpediteCode.Code != null && x.fcssumu.fcssu.fcs.fc.f.ExpediteCode.Code.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.ExpediteCode.Description != null && x.fcssumu.fcssu.fcs.fc.f.ExpediteCode.Description.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.OowOrderNumber != null && x.fcssumu.fcssu.fcs.fc.f.OowOrderNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.PhoneNumber != null && x.fcssumu.fcssu.fcs.fc.f.PhoneNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.PurchaseOrderNumber != null && x.fcssumu.fcssu.fcs.fc.f.PurchaseOrderNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.ShipToAddress != null && x.fcssumu.fcssu.fcs.fc.f.ShipToAddress.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.ShipToCity != null && x.fcssumu.fcssu.fcs.fc.f.ShipToCity.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.ShipToName != null && x.fcssumu.fcssu.fcs.fc.f.ShipToName.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.ShipToState != null && x.fcssumu.fcssu.fcs.fc.f.ShipToState.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.ShipToZip != null && x.fcssumu.fcssu.fcs.fc.f.ShipToZip.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.ShipToPhone != null && x.fcssumu.fcssu.fcs.fc.f.ShipToPhone.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.fc.f.SM != null && x.fcssumu.fcssu.fcs.fc.f.SM.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcssumu.fcssu.fcs.s.Any(sku => sku.MaterialSku.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcssumu.fcssu.fcs.s.Any(quan => quan.Quantity.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcssumu.fcssu.su.Any(su => su.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcssumu.fcssu.su.Any(su => su.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcssumu.fcssu.su.Any(su => su.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcssumu.mu.Any(mu => mu.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcssumu.mu.Any(mu => mu.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcssumu.mu.Any(mu => mu.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcssumu.fcssu.fcs.fc.c.Any(comm => comm.Note.ToLower().Contains(txtSearch.Text.ToLower()) && comm.Form.FormName == "Direct Order")
                    )
                    .SelectMany(z => z.fcssumu.fcssu.fcs.fc.c.DefaultIfEmpty(), (z, zz) => new
                    {
                        BaseFormData = z.fcssumu.fcssu.fcs.fc.f,
                        CommentsData = z.fcssumu.fcssu.fcs.fc.c.DefaultIfEmpty(),
                        SkuQuantityData = z.fcssumu.fcssu.fcs.s.DefaultIfEmpty(),
                        SubmittedUserData = z.fcssumu.fcssu.su.DefaultIfEmpty(),
                        ModifiedUserData = z.fcssumu.mu.DefaultIfEmpty(),
                        RequestedUserData = z.ru.DefaultIfEmpty()
                    });


                    IQueryable<dynamic> hotRushForms = ctx.HotRushForms
                        .GroupJoin(ctx.Comments, f => f.RecordId, c => c.RelatedFormId, (f, c) => new { f, c })
                        .GroupJoin(ctx.SystemUsers, fc => fc.f.SubmittedUser.SystemUserID, su => su.SystemUserID, (fc, su) => new { fc, su })
                        .GroupJoin(ctx.SystemUsers, fcsu => fcsu.fc.f.LastModifiedUser.SystemUserID, mu => mu.SystemUserID, (fcsu, mu) => new { fcsu, mu })
                        .GroupJoin(ctx.SystemUsers, fcsumu => fcsumu.fcsu.fc.f.RequestedUser.SystemUserID, ru => ru.SystemUserID, (fcsumu, ru) => new { fcsumu, ru })
                        .Where(x => x.fcsumu.fcsu.fc.f.Company != null && x.fcsumu.fcsu.fc.f.Company.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Status.StatusText != null && x.fcsumu.fcsu.fc.f.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Customer != null && x.fcsumu.fcsu.fc.f.Customer.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.EntireOrderOrLineNumber != null && x.fcsumu.fcsu.fc.f.EntireOrderOrLineNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.OrderNumber != null && x.fcsumu.fcsu.fc.f.OrderNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.su.Any(su => su.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.su.Any(su => su.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.su.Any(su => su.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.fc.c.Any(comm => comm.Note.ToLower().Contains(txtSearch.Text.ToLower()) && comm.Form.FormName == "Hot Rush")
                    )
                    .SelectMany(z => z.fcsumu.fcsu.fc.c.DefaultIfEmpty(), (z, zz) => new
                    {
                        BaseFormData = z.fcsumu.fcsu.fc.f,
                        CommentsData = z.fcsumu.fcsu.fc.c.DefaultIfEmpty(),
                        SubmittedUserData = z.fcsumu.fcsu.su.DefaultIfEmpty(),
                        ModifiedUserData = z.fcsumu.mu.DefaultIfEmpty(),
                        RequestedUserData = z.ru.DefaultIfEmpty()
                    });

                    IQueryable<dynamic> orderCancellationForms = ctx.OrderCancellationForms
                        .GroupJoin(ctx.Comments, f => f.RecordId, c => c.RelatedFormId, (f, c) => new { f, c })
                        .GroupJoin(ctx.SystemUsers, fc => fc.f.SubmittedUser.SystemUserID, su => su.SystemUserID, (fc, su) => new { fc, su })
                        .GroupJoin(ctx.SystemUsers, fcsu => fcsu.fc.f.LastModifiedUser.SystemUserID, mu => mu.SystemUserID, (fcsu, mu) => new { fcsu, mu })
                        .GroupJoin(ctx.SystemUsers, fcsumu => fcsumu.fcsu.fc.f.RequestedUser.SystemUserID, ru => ru.SystemUserID, (fcsumu, ru) => new { fcsumu, ru })
                        .Where(x => x.fcsumu.fcsu.fc.f.Company != null && x.fcsumu.fcsu.fc.f.Company.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Status.StatusText != null && x.fcsumu.fcsu.fc.f.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Customer != null && x.fcsumu.fcsu.fc.f.Customer.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.OrderNumber != null && x.fcsumu.fcsu.fc.f.OrderNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.ArmstrongReference != null && x.fcsumu.fcsu.fc.f.ArmstrongReference.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.SKU != null && x.fcsumu.fcsu.fc.f.SKU.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.PO != null && x.fcsumu.fcsu.fc.f.PO.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.POStatusList != null && x.fcsumu.fcsu.fc.f.POStatusList.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Line != null && x.fcsumu.fcsu.fc.f.Line.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.LineOfPO != null && x.fcsumu.fcsu.fc.f.LineOfPO.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Size != null && x.fcsumu.fcsu.fc.f.Size.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.ShipVia != null && x.fcsumu.fcsu.fc.f.ShipVia.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Serial != null && x.fcsumu.fcsu.fc.f.Serial.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.TruckRoute != null && x.fcsumu.fcsu.fc.f.TruckRoute.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.su.Any(su => su.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.su.Any(su => su.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.su.Any(su => su.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.fc.c.Any(comm => comm.Note.ToLower().Contains(txtSearch.Text.ToLower()) && comm.Form.FormName == "Order Cancellation")
                    )
                    .SelectMany(z => z.fcsumu.fcsu.fc.c.DefaultIfEmpty(), (z, zz) => new
                    {
                        BaseFormData = z.fcsumu.fcsu.fc.f,
                        CommentsData = z.fcsumu.fcsu.fc.c.DefaultIfEmpty(),
                        SubmittedUserData = z.fcsumu.fcsu.su.DefaultIfEmpty(),
                        ModifiedUserData = z.fcsumu.mu.DefaultIfEmpty(),
                        RequestedUserData = z.ru.DefaultIfEmpty()
                    });



                    IQueryable<dynamic> mustIncludeForms = ctx.MustIncludeForms
                        .GroupJoin(ctx.Comments, f => f.RecordId, c => c.RelatedFormId, (f, c) => new { f, c })
                        .GroupJoin(ctx.SystemUsers, fc => fc.f.SubmittedUser.SystemUserID, su => su.SystemUserID, (fc, su) => new { fc, su })
                        .GroupJoin(ctx.SystemUsers, fcsu => fcsu.fc.f.LastModifiedUser.SystemUserID, mu => mu.SystemUserID, (fcsu, mu) => new { fcsu, mu })
                        .GroupJoin(ctx.SystemUsers, fcsumu => fcsumu.fcsu.fc.f.RequestedUser.SystemUserID, ru => ru.SystemUserID, (fcsumu, ru) => new { fcsumu, ru })
                        .Where(x => x.fcsumu.fcsu.fc.f.Company != null && x.fcsumu.fcsu.fc.f.Company.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Status.StatusText != null && x.fcsumu.fcsu.fc.f.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.PO != null && x.fcsumu.fcsu.fc.f.PO.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.ArmstrongReference != null && x.fcsumu.fcsu.fc.f.ArmstrongReference.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Customer != null && x.fcsumu.fcsu.fc.f.Customer.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.OrderNumber != null && x.fcsumu.fcsu.fc.f.OrderNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Line != null && x.fcsumu.fcsu.fc.f.Line.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Pattern != null && x.fcsumu.fcsu.fc.f.Pattern.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Warehouse != null && x.fcsumu.fcsu.fc.f.Warehouse.WarehouseText.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.su.Any(su => su.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.su.Any(su => su.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.su.Any(su => su.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.fc.c.Any(comm => comm.Note.ToLower().Contains(txtSearch.Text.ToLower()) && comm.Form.FormName == "Must Include")
                    )
                    .SelectMany(z => z.fcsumu.fcsu.fc.c.DefaultIfEmpty(), (z, zz) => new
                    {
                        BaseFormData = z.fcsumu.fcsu.fc.f,
                        CommentsData = z.fcsumu.fcsu.fc.c.DefaultIfEmpty(),
                        SubmittedUserData = z.fcsumu.fcsu.su.DefaultIfEmpty(),
                        ModifiedUserData = z.fcsumu.mu.DefaultIfEmpty(),
                        RequestedUserData = z.ru.DefaultIfEmpty()
                    });

                    IQueryable<dynamic> sampleRequestForms = ctx.SampleRequestForms
                        .GroupJoin(ctx.Comments, f => f.RecordId, c => c.RelatedFormId, (f, c) => new { f, c })
                        .GroupJoin(ctx.SystemUsers, fc => fc.f.SubmittedUser.SystemUserID, su => su.SystemUserID, (fc, su) => new { fc, su })
                        .GroupJoin(ctx.SystemUsers, fcsu => fcsu.fc.f.LastModifiedUser.SystemUserID, mu => mu.SystemUserID, (fcsu, mu) => new { fcsu, mu })
                        .GroupJoin(ctx.SystemUsers, fcsumu => fcsumu.fcsu.fc.f.RequestedUser.SystemUserID, ru => ru.SystemUserID, (fcsumu, ru) => new { fcsumu, ru })
                        .Where(x => x.fcsumu.fcsu.fc.f.Company != null && x.fcsumu.fcsu.fc.f.Company.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Status.StatusText != null && x.fcsumu.fcsu.fc.f.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.ProjectName != null && x.fcsumu.fcsu.fc.f.ProjectName.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.ItemNumber != null && x.fcsumu.fcsu.fc.f.ItemNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Customer != null && x.fcsumu.fcsu.fc.f.Customer.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.StyleNameColor != null && x.fcsumu.fcsu.fc.f.StyleNameColor.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.AccountNumber != null && x.fcsumu.fcsu.fc.f.AccountNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Size != null && x.fcsumu.fcsu.fc.f.Size.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Contact != null && x.fcsumu.fcsu.fc.f.Contact.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Quantity != null && x.fcsumu.fcsu.fc.f.Quantity.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.PhoneNumber != null && x.fcsumu.fcsu.fc.f.PhoneNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.su.Any(su => su.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.su.Any(su => su.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.su.Any(su => su.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.fc.c.Any(comm => comm.Note.ToLower().Contains(txtSearch.Text.ToLower()) && comm.Form.FormName == "Sample Request")
                    )
                    .SelectMany(z => z.fcsumu.fcsu.fc.c.DefaultIfEmpty(), (z, zz) => new
                    {
                        BaseFormData = z.fcsumu.fcsu.fc.f,
                        CommentsData = z.fcsumu.fcsu.fc.c.DefaultIfEmpty(),
                        SubmittedUserData = z.fcsumu.fcsu.su.DefaultIfEmpty(),
                        ModifiedUserData = z.fcsumu.mu.DefaultIfEmpty(),
                        RequestedUserData = z.ru.DefaultIfEmpty()
                    });

                    IQueryable<dynamic> lowInventoryForms = ctx.LowInventoryForms
                        .GroupJoin(ctx.Comments, f => f.RecordId, c => c.RelatedFormId, (f, c) => new { f, c })
                        .GroupJoin(ctx.SystemUsers, fc => fc.f.SubmittedUser.SystemUserID, su => su.SystemUserID, (fc, su) => new { fc, su })
                        .GroupJoin(ctx.SystemUsers, fcsu => fcsu.fc.f.LastModifiedUser.SystemUserID, mu => mu.SystemUserID, (fcsu, mu) => new { fcsu, mu })
                        .GroupJoin(ctx.SystemUsers, fcsumu => fcsumu.fcsu.fc.f.RequestedUser.SystemUserID, ru => ru.SystemUserID, (fcsumu, ru) => new { fcsumu, ru })
                        .Where(x => x.fcsumu.fcsu.fc.f.Company != null && x.fcsumu.fcsu.fc.f.Company.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Status.StatusText != null && x.fcsumu.fcsu.fc.f.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.OrderNumber != null && x.fcsumu.fcsu.fc.f.OrderNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Plant != null && x.fcsumu.fcsu.fc.f.Plant.PlantText.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Line != null && x.fcsumu.fcsu.fc.f.Line.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Quantity != null && x.fcsumu.fcsu.fc.f.Quantity.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.SKU != null && x.fcsumu.fcsu.fc.f.SKU.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.su.Any(su => su.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.su.Any(su => su.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.su.Any(su => su.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.fc.c.Any(comm => comm.Note.ToLower().Contains(txtSearch.Text.ToLower()) && comm.Form.FormName == "Low Inventory")
                    )
                    .SelectMany(z => z.fcsumu.fcsu.fc.c.DefaultIfEmpty(), (z, zz) => new
                    {
                        BaseFormData = z.fcsumu.fcsu.fc.f,
                        CommentsData = z.fcsumu.fcsu.fc.c.DefaultIfEmpty(),
                        SubmittedUserData = z.fcsumu.fcsu.su.DefaultIfEmpty(),
                        ModifiedUserData = z.fcsumu.mu.DefaultIfEmpty(),
                        RequestedUserData = z.ru.DefaultIfEmpty()
                    });

                    IQueryable<dynamic> cannotWaitForContainerForms = ctx.CannotWaitForContainerForms
                        .GroupJoin(ctx.Comments, f => f.RecordId, c => c.RelatedFormId, (f, c) => new { f, c })
                        .GroupJoin(ctx.SystemUsers, fc => fc.f.SubmittedUser.SystemUserID, su => su.SystemUserID, (fc, su) => new { fc, su })
                        .GroupJoin(ctx.SystemUsers, fcsu => fcsu.fc.f.LastModifiedUser.SystemUserID, mu => mu.SystemUserID, (fcsu, mu) => new { fcsu, mu })
                        .GroupJoin(ctx.SystemUsers, fcsumu => fcsumu.fcsu.fc.f.RequestedUser.SystemUserID, ru => ru.SystemUserID, (fcsumu, ru) => new { fcsumu, ru })
                        .Where(x => x.fcsumu.fcsu.fc.f.Company != null && x.fcsumu.fcsu.fc.f.Company.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Status.StatusText != null && x.fcsumu.fcsu.fc.f.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.OrderNumber != null && x.fcsumu.fcsu.fc.f.OrderNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Plant != null && x.fcsumu.fcsu.fc.f.Plant.PlantText.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Line != null && x.fcsumu.fcsu.fc.f.Line.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Quantity != null && x.fcsumu.fcsu.fc.f.Quantity.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.SKU != null && x.fcsumu.fcsu.fc.f.SKU.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.su.Any(su => su.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.su.Any(su => su.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.su.Any(su => su.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.fc.c.Any(comm => comm.Note.ToLower().Contains(txtSearch.Text.ToLower()) && comm.Form.FormName == "Cannot Wait For Container")
                    )
                    .SelectMany(z => z.fcsumu.fcsu.fc.c.DefaultIfEmpty(), (z, zz) => new
                    {
                        BaseFormData = z.fcsumu.fcsu.fc.f,
                        CommentsData = z.fcsumu.fcsu.fc.c.DefaultIfEmpty(),
                        SubmittedUserData = z.fcsumu.fcsu.su.DefaultIfEmpty(),
                        ModifiedUserData = z.fcsumu.mu.DefaultIfEmpty(),
                        RequestedUserData = z.ru.DefaultIfEmpty()
                    });

                    IQueryable<dynamic> priceChangeRequestForms = ctx.PriceChangeRequestForms
                        .GroupJoin(ctx.Comments, f => f.RecordId, c => c.RelatedFormId, (f, c) => new { f, c })
                        .GroupJoin(ctx.SystemUsers, fc => fc.f.SubmittedUser.SystemUserID, su => su.SystemUserID, (fc, su) => new { fc, su })
                        .GroupJoin(ctx.SystemUsers, fcsu => fcsu.fc.f.LastModifiedUser.SystemUserID, mu => mu.SystemUserID, (fcsu, mu) => new { fcsu, mu })
                        .GroupJoin(ctx.SystemUsers, fcsumu => fcsumu.fcsu.fc.f.RequestedUser.SystemUserID, ru => ru.SystemUserID, (fcsumu, ru) => new { fcsumu, ru })
                        .Where(x => x.fcsumu.fcsu.fc.f.Company != null && x.fcsumu.fcsu.fc.f.Company.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Status.StatusText != null && x.fcsumu.fcsu.fc.f.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Product != null && x.fcsumu.fcsu.fc.f.Product.ProductText.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Customer != null && x.fcsumu.fcsu.fc.f.Customer.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.LineNumber != null && x.fcsumu.fcsu.fc.f.LineNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.AccountNumber != null && x.fcsumu.fcsu.fc.f.AccountNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Quantity != null && x.fcsumu.fcsu.fc.f.Quantity.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.SKU != null && x.fcsumu.fcsu.fc.f.SKU.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.SalesRep != null && x.fcsumu.fcsu.fc.f.SalesRep.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.OrderNumber != null && x.fcsumu.fcsu.fc.f.OrderNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Price != null && x.fcsumu.fcsu.fc.f.Price.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.CrossReferenceOldOrderNumber != null && x.fcsumu.fcsu.fc.f.CrossReferenceOldOrderNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.su.Any(su => su.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.su.Any(su => su.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.su.Any(su => su.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.fc.c.Any(comm => comm.Note.ToLower().Contains(txtSearch.Text.ToLower()) && comm.Form.FormName == "Price Change Request")
                    )
                    .SelectMany(z => z.fcsumu.fcsu.fc.c.DefaultIfEmpty(), (z, zz) => new
                    {
                        BaseFormData = z.fcsumu.fcsu.fc.f,
                        CommentsData = z.fcsumu.fcsu.fc.c.DefaultIfEmpty(),
                        SubmittedUserData = z.fcsumu.fcsu.su.DefaultIfEmpty(),
                        ModifiedUserData = z.fcsumu.mu.DefaultIfEmpty(),
                        RequestedUserData = z.ru.DefaultIfEmpty()
                    });

                    IQueryable<dynamic> requestForCheckForms = ctx.RequestForCheckForms
                        .GroupJoin(ctx.Comments, f => f.RecordId, c => c.RelatedFormId, (f, c) => new { f, c })
                        .GroupJoin(ctx.SystemUsers, fc => fc.f.SubmittedUser.SystemUserID, su => su.SystemUserID, (fc, su) => new { fc, su })
                        .GroupJoin(ctx.SystemUsers, fcsu => fcsu.fc.f.LastModifiedUser.SystemUserID, mu => mu.SystemUserID, (fcsu, mu) => new { fcsu, mu })
                        .GroupJoin(ctx.SystemUsers, fcsumu => fcsumu.fcsu.fc.f.RequestedUser.SystemUserID, ru => ru.SystemUserID, (fcsumu, ru) => new { fcsumu, ru })
                        .Where(x => x.fcsumu.fcsu.fc.f.Company != null && x.fcsumu.fcsu.fc.f.Company.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Status.StatusText != null && x.fcsumu.fcsu.fc.f.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.PayableTo != null && x.fcsumu.fcsu.fc.f.PayableTo.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.ChargeToOther != null && x.fcsumu.fcsu.fc.f.ChargeToOther.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.ChargeToAccountNumber != null && x.fcsumu.fcsu.fc.f.ChargeToAccountNumber.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.Amount != null && x.fcsumu.fcsu.fc.f.Amount.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.For != null && x.fcsumu.fcsu.fc.f.For.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.RequestedBy != null && x.fcsumu.fcsu.fc.f.RequestedBy.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.fc.f.ApprovedBy != null && x.fcsumu.fcsu.fc.f.ApprovedBy.ToLower().Contains(txtSearch.Text.ToLower())
                            || x.fcsumu.fcsu.su.Any(su => su.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.su.Any(su => su.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.su.Any(su => su.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.mu.Any(mu => mu.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.UserName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.DisplayName.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.ru.Any(ru => ru.EmailAddress.ToLower().Contains(txtSearch.Text.ToLower()))
                            || x.fcsumu.fcsu.fc.c.Any(comm => comm.Note.ToLower().Contains(txtSearch.Text.ToLower()) && comm.Form.FormName == "Request For Check")
                    )
                    .SelectMany(z => z.fcsumu.fcsu.fc.c.DefaultIfEmpty(), (z, zz) => new
                    {
                        BaseFormData = z.fcsumu.fcsu.fc.f,
                        CommentsData = z.fcsumu.fcsu.fc.c.DefaultIfEmpty(),
                        SubmittedUserData = z.fcsumu.fcsu.su.DefaultIfEmpty(),
                        ModifiedUserData = z.fcsumu.mu.DefaultIfEmpty(),
                        RequestedUserData = z.ru.DefaultIfEmpty()
                    });

                    //count += expeditedOrderForms.Count();
                    //count += directOrderForms.Count();
                    //count += hotRushForms.Count();
                    //count += orderCancellationForms.Count();
                    //count += mustIncludeForms.Count();
                    //count += sampleRequestForms.Count();
                    //count += lowInventoryForms.Count();
                    //count += priceChangeRequestForms.Count();
                    //count += requestForCheckForms.Count();

                    //lblResults.Text = count + " Results Found";

                    FindExpeditedOrderForms(searchResults, ref resultCount, expeditedOrderForms);
                    FindDirectOrderForms(searchResults, ref resultCount, directOrderForms);
                    FindHotRushForms(searchResults, ref resultCount, hotRushForms);
                    FindOrderCancellationForms(searchResults, ref resultCount, orderCancellationForms);
                    FindMustIncludeForms(searchResults, ref resultCount, mustIncludeForms);
                    FindSampleRequestForms(searchResults, ref resultCount, sampleRequestForms);
                    FindLowInventoryForms(searchResults, ref resultCount, lowInventoryForms);
                    FindPriceChangeRequestForms(searchResults, ref resultCount, priceChangeRequestForms);
                    FindRequestForCheckForms(searchResults, ref resultCount, requestForCheckForms);
                    FindCannotWaitForContainerForms(searchResults, ref resultCount, cannotWaitForContainerForms);

                    lblResults.Text = resultCount + " Results Found";

                    var pagedSearchResults = searchResults.Take(takeCount).Skip(skipCount).ToList();

                    PagedDataSource page = new PagedDataSource();
                    page.AllowCustomPaging = true;
                    page.AllowPaging = true;
                    page.DataSource = pagedSearchResults.ToList();
                    page.PageSize = 10;
                    rptrSearchResults.DataSource = page;
                    rptrSearchResults.DataBind();

                    RowCount = searchResults.ToList().Count;
                    plcPaging.Controls.Clear();
                    CreatePagingControl();
                }
            }
        }

        private void FindExpeditedOrderForms(List<SearchModel> searchResults, ref int resultCount, IQueryable<dynamic> expeditedOrderForms)
        {
            foreach (dynamic form in expeditedOrderForms)
            {
                string textMatches = "";

                if (form.BaseFormData.Company != null) { if (form.BaseFormData.Company.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Company:</b> " + form.BaseFormData.Company + ", "; } }
                if (form.BaseFormData.Customer != null) { if (form.BaseFormData.Customer.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Customer:</b> " + form.BaseFormData.Customer + ", "; } }
                if (form.BaseFormData.ExpediteCode != null)
                {
                    if (form.BaseFormData.ExpediteCode.Code.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Expedite Code:</b> " + form.BaseFormData.ExpediteCode.Code + ", "; }
                    if (form.BaseFormData.ExpediteCode.Description.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Expedite Code Description:</b> " + form.BaseFormData.ExpediteCode.Description + ", "; }
                }
                if (form.BaseFormData.ContactName != null) { if (form.BaseFormData.ContactName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Contact Name:</b> " + form.BaseFormData.ContactName + ", "; } }
                if (form.BaseFormData.PhoneNumber != null) { if (form.BaseFormData.PhoneNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Phone Number:</b> " + form.BaseFormData.PhoneNumber + ", "; } }
                if (form.BaseFormData.AccountNumber != null) { if (form.BaseFormData.AccountNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Account Number:</b> " + form.BaseFormData.AccountNumber + ", "; } }
                if (form.BaseFormData.PurchaseOrderNumber != null) { if (form.BaseFormData.PurchaseOrderNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Purchase Order #:</b> " + form.BaseFormData.PurchaseOrderNumber + ", "; } }
                if (form.BaseFormData.OowOrderNumber != null) { if (form.BaseFormData.OowOrderNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>OOW Order Number:</b> " + form.BaseFormData.OowOrderNumber + ", "; } }
                if (form.BaseFormData.SM != null) { if (form.BaseFormData.SM.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>S/M:</b> " + form.BaseFormData.SM + ", "; } }
                if (form.BaseFormData.ShipToName != null) { if (form.BaseFormData.ShipToName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship To Name:</b> " + form.BaseFormData.ShipToName + ", "; } }
                if (form.BaseFormData.ShipToAddress != null) { if (form.BaseFormData.ShipToAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship To Address:</b> " + form.BaseFormData.ShipToAddress + ", "; } }
                if (form.BaseFormData.ShipToCity != null) { if (form.BaseFormData.ShipToCity.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship To City:</b> " + form.BaseFormData.ShipToCity + ", "; } }
                if (form.BaseFormData.ShipToState != null) { if (form.BaseFormData.ShipToState.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship To State:</b> " + form.BaseFormData.ShipToState + ", "; } }
                if (form.BaseFormData.ShipToZip != null) { if (form.BaseFormData.ShipToZip.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship To Zip:</b> " + form.BaseFormData.ShipToZip + ", "; } }
                if (form.BaseFormData.ShipToPhone != null) { if (form.BaseFormData.ShipToPhone.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship To Phone:</b> " + form.BaseFormData.ShipToPhone + ", "; } }
                if (form.BaseFormData.Status != null) { if (form.BaseFormData.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Status:</b> " + form.BaseFormData.Status.StatusText + ", "; } }

                if (form.SubmittedUserData[0] != null)
                {
                    if (form.SubmittedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Display Name:</b> " + form.SubmittedUserData[0].DisplayName + ", "; }
                    if (form.SubmittedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Username:</b> " + form.SubmittedUserData[0].UserName + ", "; }
                    if (form.SubmittedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Email:</b> " + form.SubmittedUserData[0].EmailAddress + ", "; }
                }

                if (form.ModifiedUserData[0] != null)
                {
                    if (form.ModifiedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Display Name:</b> " + form.ModifiedUserData[0].DisplayName + ", "; }
                    if (form.ModifiedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Username:</b> " + form.ModifiedUserData[0].UserName + ", "; }
                    if (form.ModifiedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Email:</b> " + form.ModifiedUserData[0].EmailAddress + ", "; }
                }

                if (form.RequestedUserData[0] != null)
                {
                    if (form.RequestedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Display Name:</b> " + form.RequestedUserData[0].DisplayName + ", "; }
                    if (form.RequestedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Username:</b> " + form.RequestedUserData[0].UserName + ", "; }
                    if (form.RequestedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Email:</b> " + form.RequestedUserData[0].EmailAddress + ", "; }
                }

                foreach (dynamic comment in form.CommentsData)
                {
                    if (comment != null && comment.User != null)
                    {
                        if (comment.Note.ToLower().Contains(txtSearch.Text.ToLower()) && comment.Form.FormName == "Expedited Order")
                        {
                            textMatches += "<b>Comment by " + comment.User.DisplayName + ":</b> " + comment.Note + ", ";
                        }
                    }
                }

                foreach (dynamic skuQuantity in form.SkuQuantityData)
                {
                    if (skuQuantity != null)
                    {
                        if (skuQuantity.MaterialSku.ToLower().Contains(txtSearch.Text.ToLower()))
                        {
                            textMatches += "<b>Material Sku: </b> " + skuQuantity.MaterialSku + ", ";
                        }

                        if (skuQuantity.Quantity.ToLower().Contains(txtSearch.Text.ToLower()))
                        {
                            textMatches += "<b>Quantity: </b> " + skuQuantity.Quantity + ", ";
                        }
                    }
                }

                if (textMatches.Length >= 2)
                {
                    if (textMatches.Substring(textMatches.Length - 2) == ", ")
                    {
                        textMatches = textMatches.Substring(0, textMatches.Length - 2);
                    }
                }

                string postBackUrl = "/ReportExpeditedOrder.aspx?formId=" + form.BaseFormData.RecordId;

                string matchHtml = "<div style=\"padding-left:18px; max-width:960px\"><b>Submitted On:</b> " + form.BaseFormData.Timestamp.ToShortDateString()
                    + "     <b>By:</b> " + form.SubmittedUserData[0].DisplayName
                    + "     <b>Status:</b> " + form.BaseFormData.Status.StatusText + "<br /><br />"
                    + "<b>Matched Fields</b><br />" + textMatches + "<br /><br /></div>";

                var thisResult = new SearchModel
                {
                    FormId = form.BaseFormData.RecordId.ToString(),
                    FormName = "Expedited Order",
                    MatchedFieldsHtml = matchHtml,
                    PostBackUrl = postBackUrl,
                    Status = form.BaseFormData.Status.StatusText,
                    SubmittedBy = form.SubmittedUserData[0].DisplayName,
                    SubmittedDate = form.BaseFormData.Timestamp.ToShortDateString(),
                    ResultIndex = resultCount + 1
                };

                if (!searchResults.Any(x => x.FormId == thisResult.FormId && x.FormName == thisResult.FormName)) { resultCount++; searchResults.Add(thisResult); }


            }
        }

        private void FindDirectOrderForms(List<SearchModel> searchResults, ref int resultCount, IQueryable<dynamic> directOrderForms)
        {
            foreach (dynamic form in directOrderForms)
            {
                string textMatches = "";

                if (form.BaseFormData.Company != null) { if (form.BaseFormData.Company.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Company:</b> " + form.BaseFormData.Company + ", "; } }
                if (form.BaseFormData.Status != null) { if (form.BaseFormData.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Status:</b> " + form.Status.BaseFormData.StatusText + ", "; } }
                if (form.BaseFormData.Customer != null) { if (form.BaseFormData.Customer.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Customer:</b> " + form.BaseFormData.Customer + ", "; } }
                if (form.BaseFormData.ExpediteCode != null)
                {
                    if (form.BaseFormData.ExpediteCode.Code.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Expedite Code:</b> " + form.BaseFormData.ExpediteCode.Code + ", "; }
                    if (form.BaseFormData.ExpediteCode.Description.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Expedite Code Description:</b> " + form.BaseFormData.ExpediteCode.Description + ", "; }
                }
                if (form.BaseFormData.ContactName != null) { if (form.BaseFormData.ContactName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Contact Name:</b> " + form.BaseFormData.ContactName + ", "; } }
                if (form.BaseFormData.PhoneNumber != null) { if (form.BaseFormData.PhoneNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Phone Number:</b> " + form.BaseFormData.PhoneNumber + ", "; } }
                if (form.BaseFormData.AccountNumber != null) { if (form.BaseFormData.AccountNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Account Number:</b> " + form.BaseFormData.AccountNumber + ", "; } }
                if (form.BaseFormData.PurchaseOrderNumber != null) { if (form.BaseFormData.PurchaseOrderNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Purchase Order #:</b> " + form.BaseFormData.PurchaseOrderNumber + ", "; } }
                if (form.BaseFormData.OowOrderNumber != null) { if (form.BaseFormData.OowOrderNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>OOW Order Number:</b> " + form.BaseFormData.OowOrderNumber + ", "; } }
                if (form.BaseFormData.SM != null) { if (form.BaseFormData.SM.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>S/M:</b> " + form.BaseFormData.SM + ", "; } }
                if (form.BaseFormData.ShipVia != null) { if (form.BaseFormData.ShipVia.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship Via:</b> " + form.BaseFormData.ShipVia + ", "; } }
                if (form.BaseFormData.Reserve != null) { if (form.BaseFormData.Reserve.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Reserve:</b> " + form.BaseFormData.Reserve + ", "; } }
                if (form.BaseFormData.ShipToName != null) { if (form.BaseFormData.ShipToName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship To Name:</b> " + form.BaseFormData.ShipToName + ", "; } }
                if (form.BaseFormData.ShipToAddress != null) { if (form.BaseFormData.ShipToAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship To Address:</b> " + form.BaseFormData.ShipToAddress + ", "; } }
                if (form.BaseFormData.ShipToCity != null) { if (form.BaseFormData.ShipToCity.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship To City:</b> " + form.BaseFormData.ShipToCity + ", "; } }
                if (form.BaseFormData.ShipToState != null) { if (form.BaseFormData.ShipToState.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship To State:</b> " + form.BaseFormData.ShipToState + ", "; } }
                if (form.BaseFormData.ShipToZip != null) { if (form.BaseFormData.ShipToZip.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship To Zip:</b> " + form.BaseFormData.ShipToZip + ", "; } }
                if (form.BaseFormData.ShipToPhone != null) { if (form.BaseFormData.ShipToPhone.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship To Phone:</b> " + form.BaseFormData.ShipToPhone + ", "; } }

                if (form.SubmittedUserData[0] != null)
                {
                    if (form.SubmittedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Display Name:</b> " + form.SubmittedUserData[0].DisplayName + ", "; }
                    if (form.SubmittedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Username:</b> " + form.SubmittedUserData[0].UserName + ", "; }
                    if (form.SubmittedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Email:</b> " + form.SubmittedUserData[0].EmailAddress + ", "; }
                }

                if (form.ModifiedUserData[0] != null)
                {
                    if (form.ModifiedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Display Name:</b> " + form.ModifiedUserData[0].DisplayName + ", "; }
                    if (form.ModifiedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Username:</b> " + form.ModifiedUserData[0].UserName + ", "; }
                    if (form.ModifiedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Email:</b> " + form.ModifiedUserData[0].EmailAddress + ", "; }
                }

                if (form.RequestedUserData[0] != null)
                {
                    if (form.RequestedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Display Name:</b> " + form.RequestedUserData[0].DisplayName + ", "; }
                    if (form.RequestedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Username:</b> " + form.RequestedUserData[0].UserName + ", "; }
                    if (form.RequestedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Email:</b> " + form.RequestedUserData[0].EmailAddress + ", "; }
                }

                foreach (dynamic comment in form.CommentsData)
                {
                    if (comment != null && comment.User != null)
                    {
                        if (comment.Note.ToLower().Contains(txtSearch.Text.ToLower()) && comment.Form.FormName == "Direct Order")
                        {
                            textMatches += "<b>Comment by " + comment.User.DisplayName + ":</b> " + comment.Note + ", ";
                        }
                    }
                }

                foreach (dynamic skuQuantity in form.SkuQuantityData)
                {
                    if (skuQuantity != null)
                    {
                        if (skuQuantity.MaterialSku.ToLower().Contains(txtSearch.Text.ToLower()))
                        {
                            textMatches += "<b>Material Sku: </b> " + skuQuantity.MaterialSku + ", ";
                        }

                        if (skuQuantity.Quantity.ToLower().Contains(txtSearch.Text.ToLower()))
                        {
                            textMatches += "<b>Quantity: </b> " + skuQuantity.Quantity + ", ";
                        }
                    }
                }

                if (textMatches.Substring(textMatches.Length - 2) == ", ")
                {
                    textMatches = textMatches.Substring(0, textMatches.Length - 2);
                }

                string postBackUrl = "/ReportDirectOrder.aspx?formId=" + form.BaseFormData.RecordId;

                string matchHtml = "<div style=\"padding-left:18px; max-width:960px\"><b>Submitted On:</b> " + form.BaseFormData.Timestamp.ToShortDateString()
                    + "     <b>By:</b> " + form.SubmittedUserData[0].DisplayName
                    + "     <b>Status:</b> " + form.BaseFormData.Status.StatusText + "<br /><br />"
                    + "<b>Matched Fields</b><br />" + textMatches + "<br /><br /></div>";

                var thisResult = new SearchModel
                {
                    FormId = form.BaseFormData.RecordId.ToString(),
                    FormName = "Direct Order",
                    MatchedFieldsHtml = matchHtml,
                    PostBackUrl = postBackUrl,
                    Status = form.BaseFormData.Status.StatusText,
                    SubmittedBy = form.SubmittedUserData[0].DisplayName,
                    SubmittedDate = form.BaseFormData.Timestamp.ToShortDateString(),
                    ResultIndex = resultCount + 1
                };
                if (!searchResults.Any(x => x.FormId == thisResult.FormId && x.FormName == thisResult.FormName)) { resultCount++; searchResults.Add(thisResult); }
                if (!searchResults.Any(x => x.FormId == thisResult.FormId && x.FormName == thisResult.FormName)) { resultCount++; searchResults.Add(thisResult); }

            }
        }

        private void FindHotRushForms(List<SearchModel> searchResults, ref int resultCount, IQueryable<dynamic> hotRushForms)
        {
            foreach (dynamic form in hotRushForms)
            {
                string textMatches = "";

                if (form.BaseFormData.Company != null) { if (form.BaseFormData.Company.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Company:</b> " + form.BaseFormData.Company + ", "; } }
                if (form.BaseFormData.Status != null) { if (form.BaseFormData.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Status:</b> " + form.BaseFormData.Status.BaseFormData.StatusText + ", "; } }
                if (form.BaseFormData.Customer != null) { if (form.BaseFormData.Customer.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Customer:</b> " + form.BaseFormData.Customer + ", "; } }
                if (form.BaseFormData.EntireOrderOrLineNumber != null) { if (form.BaseFormData.EntireOrderOrLineNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Entire Order OR Line #:</b> " + form.BaseFormData.EntireOrderOrLineNumber + ", "; } }
                if (form.BaseFormData.OrderNumber != null) { if (form.BaseFormData.OrderNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Order Number:</b> " + form.BaseFormData.OrderNumber + ", "; } }

                if (form.SubmittedUserData[0] != null)
                {
                    if (form.SubmittedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Display Name:</b> " + form.SubmittedUserData[0].DisplayName + ", "; }
                    if (form.SubmittedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Username:</b> " + form.SubmittedUserData[0].UserName + ", "; }
                    if (form.SubmittedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Email:</b> " + form.SubmittedUserData[0].EmailAddress + ", "; }
                }

                if (form.ModifiedUserData[0] != null)
                {
                    if (form.ModifiedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Display Name:</b> " + form.ModifiedUserData[0].DisplayName + ", "; }
                    if (form.ModifiedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Username:</b> " + form.ModifiedUserData[0].UserName + ", "; }
                    if (form.ModifiedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Email:</b> " + form.ModifiedUserData[0].EmailAddress + ", "; }
                }

                if (form.RequestedUserData[0] != null)
                {
                    if (form.RequestedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Display Name:</b> " + form.RequestedUserData[0].DisplayName + ", "; }
                    if (form.RequestedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Username:</b> " + form.RequestedUserData[0].UserName + ", "; }
                    if (form.RequestedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Email:</b> " + form.RequestedUserData[0].EmailAddress + ", "; }
                }

                foreach (dynamic comment in form.CommentsData)
                {
                    if (comment != null && comment.User != null)
                    {
                        if (comment.Note.ToLower().Contains(txtSearch.Text.ToLower()) && comment.Form.FormName == "Hot Rush")
                        {
                            textMatches += "<b>Comment by " + comment.User.DisplayName + ":</b> " + comment.Note + ", ";
                        }
                    }
                }

                if (textMatches.Substring(textMatches.Length - 2) == ", ")
                {
                    textMatches = textMatches.Substring(0, textMatches.Length - 2);
                }

                string postBackUrl = "/ReportHotRush.aspx?formId=" + form.BaseFormData.RecordId;

                string matchHtml = "<div style=\"padding-left:18px; max-width:960px\"><b>Submitted On:</b> " + form.BaseFormData.Timestamp.ToShortDateString()
                    + "     <b>By:</b> " + form.SubmittedUserData[0].DisplayName
                    + "     <b>Status:</b> " + form.BaseFormData.Status.StatusText + "<br /><br />"
                    + "<b>Matched Fields</b><br />" + textMatches + "<br /><br /></div>";

                var thisResult = new SearchModel
                {
                    FormId = form.BaseFormData.RecordId.ToString(),
                    FormName = "Hot Rush",
                    MatchedFieldsHtml = matchHtml,
                    PostBackUrl = postBackUrl,
                    Status = form.BaseFormData.Status.StatusText,
                    SubmittedBy = form.SubmittedUserData[0].DisplayName,
                    SubmittedDate = form.BaseFormData.Timestamp.ToShortDateString(),
                    ResultIndex = resultCount + 1
                };

                if (!searchResults.Any(x => x.FormId == thisResult.FormId && x.FormName == thisResult.FormName)) { resultCount++; searchResults.Add(thisResult); }
            }
        }

        private void FindOrderCancellationForms(List<SearchModel> searchResults, ref int resultCount, IQueryable<dynamic> orderCancellationForms)
        {
            foreach (dynamic form in orderCancellationForms)
            {
                string textMatches = "";

                if (form.BaseFormData.Company != null) { if (form.BaseFormData.Company.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Company:</b> " + form.BaseFormData.Company + ", "; } }
                if (form.BaseFormData.Status != null) { if (form.BaseFormData.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Status:</b> " + form.BaseFormData.Status.StatusText + ", "; } }
                if (form.BaseFormData.OrderNumber != null) { if (form.BaseFormData.OrderNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Order Number:</b> " + form.BaseFormData.OrderNumber + ", "; } }
                if (form.BaseFormData.ArmstrongReference != null) { if (form.BaseFormData.ArmstrongReference.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Armstrong Reference:</b> " + form.BaseFormData.ArmstrongReference + ", "; } }
                if (form.BaseFormData.Customer != null) { if (form.BaseFormData.Customer.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Customer:</b> " + form.BaseFormData.Customer + ", "; } }
                if (form.BaseFormData.PO != null) { if (form.BaseFormData.PO.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>PO:</b> " + form.BaseFormData.PO + ", "; } }
                if (form.BaseFormData.SKU != null) { if (form.BaseFormData.SKU.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>SKU:</b> " + form.BaseFormData.SKU + ", "; } }
                if (form.BaseFormData.POStatusList != null) { if (form.BaseFormData.POStatusList.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Status of PO:</b> " + form.BaseFormData.POStatusList + ", "; } }
                if (form.BaseFormData.Line != null) { if (form.BaseFormData.Line.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Line:</b> " + form.BaseFormData.Line + ", "; } }
                if (form.BaseFormData.LineOfPO != null) { if (form.BaseFormData.LineOfPO.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Line of PO:</b> " + form.BaseFormData.LineOfPO + ", "; } }
                if (form.BaseFormData.Size != null) { if (form.BaseFormData.Size.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Size:</b> " + form.BaseFormData.Size + ", "; } }
                if (form.BaseFormData.ShipVia != null) { if (form.BaseFormData.ShipVia.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Ship Via:</b> " + form.BaseFormData.ShipVia + ", "; } }
                if (form.BaseFormData.Serial != null) { if (form.BaseFormData.Serial.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Serial:</b> " + form.BaseFormData.Serial + ", "; } }
                if (form.BaseFormData.TruckRoute != null) { if (form.BaseFormData.TruckRoute.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Truck Route:</b> " + form.BaseFormData.TruckRoute + ", "; } }

                if (form.SubmittedUserData[0] != null)
                {
                    if (form.SubmittedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Display Name:</b> " + form.SubmittedUserData[0].DisplayName + ", "; }
                    if (form.SubmittedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Username:</b> " + form.SubmittedUserData[0].UserName + ", "; }
                    if (form.SubmittedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Email:</b> " + form.SubmittedUserData[0].EmailAddress + ", "; }
                }

                if (form.ModifiedUserData[0] != null)
                {
                    if (form.ModifiedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Display Name:</b> " + form.ModifiedUserData[0].DisplayName + ", "; }
                    if (form.ModifiedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Username:</b> " + form.ModifiedUserData[0].UserName + ", "; }
                    if (form.ModifiedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Email:</b> " + form.ModifiedUserData[0].EmailAddress + ", "; }
                }

                if (form.RequestedUserData[0] != null)
                {
                    if (form.RequestedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Display Name:</b> " + form.RequestedUserData[0].DisplayName + ", "; }
                    if (form.RequestedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Username:</b> " + form.RequestedUserData[0].UserName + ", "; }
                    if (form.RequestedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Email:</b> " + form.RequestedUserData[0].EmailAddress + ", "; }
                }

                foreach (dynamic comment in form.CommentsData)
                {
                    if (comment != null && comment.User != null)
                    {
                        if (comment.Note.ToLower().Contains(txtSearch.Text.ToLower()) && comment.Form.FormName == "Order Cancellation")
                        {
                            textMatches += "<b>Comment by " + comment.User.DisplayName + ":</b> " + comment.Note + ", ";
                        }
                    }
                }

                if (textMatches.Substring(textMatches.Length - 2) == ", ")
                {
                    textMatches = textMatches.Substring(0, textMatches.Length - 2);
                }

                string postBackUrl = "/ReportOrderCancellation.aspx?formId=" + form.BaseFormData.RecordId;

                string matchHtml = "<div style=\"padding-left:18px; max-width:960px\"><b>Submitted On:</b> " + form.BaseFormData.Timestamp.ToShortDateString()
                    + "     <b>By:</b> " + form.SubmittedUserData[0].DisplayName
                    + "     <b>Status:</b> " + form.BaseFormData.Status.StatusText + "<br /><br />"
                    + "<b>Matched Fields</b><br />" + textMatches + "<br /><br /></div>";

                var thisResult = new SearchModel
                {
                    FormId = form.BaseFormData.RecordId.ToString(),
                    FormName = "Order Cancellation",
                    MatchedFieldsHtml = matchHtml,
                    PostBackUrl = postBackUrl,
                    Status = form.BaseFormData.Status.StatusText,
                    SubmittedBy = form.SubmittedUserData[0].DisplayName,
                    SubmittedDate = form.BaseFormData.Timestamp.ToShortDateString(),
                    ResultIndex = resultCount + 1
                };

                if (!searchResults.Any(x => x.FormId == thisResult.FormId && x.FormName == thisResult.FormName)) { resultCount++; searchResults.Add(thisResult); }
            }
        }

        private void FindMustIncludeForms(List<SearchModel> searchResults, ref int resultCount, IQueryable<dynamic> mustIncludeForms)
        {
            foreach (dynamic form in mustIncludeForms)
            {
                string textMatches = "";

                if (form.BaseFormData.Company != null) { if (form.BaseFormData.Company.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Company:</b> " + form.BaseFormData.Company + ", "; } }
                if (form.BaseFormData.Status != null) { if (form.BaseFormData.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Status:</b> " + form.BaseFormData.Status.StatusText + ", "; } }
                if (form.BaseFormData.OrderNumber != null) { if (form.BaseFormData.OrderNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Order Number:</b> " + form.BaseFormData.OrderNumber + ", "; } }
                if (form.BaseFormData.ArmstrongReference != null) { if (form.BaseFormData.ArmstrongReference.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Armstrong Reference:</b> " + form.BaseFormData.ArmstrongReference + ", "; } }
                if (form.BaseFormData.Customer != null) { if (form.BaseFormData.Customer.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Customer:</b> " + form.BaseFormData.Customer + ", "; } }
                if (form.BaseFormData.PO != null) { if (form.BaseFormData.PO.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>PO:</b> " + form.BaseFormData.PO + ", "; } }
                if (form.BaseFormData.Pattern != null) { if (form.BaseFormData.Pattern.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Pattern:</b> " + form.BaseFormData.Pattern + ", "; } }
                if (form.BaseFormData.OrderNumber != null) { if (form.BaseFormData.OrderNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Order Number:</b> " + form.BaseFormData.OrderNumber + ", "; } }
                if (form.BaseFormData.Line != null) { if (form.BaseFormData.Line.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Line:</b> " + form.BaseFormData.Line + ", "; } }
                if (form.BaseFormData.Warehouse != null) { if (form.BaseFormData.Warehouse.WarehouseText.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Warehouse:</b> " + form.BaseFormData.Warehouse.WarehouseText + ", "; } }

                if (form.SubmittedUserData[0] != null)
                {
                    if (form.SubmittedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Display Name:</b> " + form.SubmittedUserData[0].DisplayName + ", "; }
                    if (form.SubmittedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Username:</b> " + form.SubmittedUserData[0].UserName + ", "; }
                    if (form.SubmittedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Email:</b> " + form.SubmittedUserData[0].EmailAddress + ", "; }
                }

                if (form.ModifiedUserData[0] != null)
                {
                    if (form.ModifiedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Display Name:</b> " + form.ModifiedUserData[0].DisplayName + ", "; }
                    if (form.ModifiedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Username:</b> " + form.ModifiedUserData[0].UserName + ", "; }
                    if (form.ModifiedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Email:</b> " + form.ModifiedUserData[0].EmailAddress + ", "; }
                }

                if (form.RequestedUserData[0] != null)
                {
                    if (form.RequestedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Display Name:</b> " + form.RequestedUserData[0].DisplayName + ", "; }
                    if (form.RequestedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Username:</b> " + form.RequestedUserData[0].UserName + ", "; }
                    if (form.RequestedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Email:</b> " + form.RequestedUserData[0].EmailAddress + ", "; }
                }

                foreach (dynamic comment in form.CommentsData)
                {
                    if (comment != null && comment.User != null)
                    {
                        if (comment.Note.ToLower().Contains(txtSearch.Text.ToLower()) && comment.Form.FormName == "Must Include")
                        {
                            textMatches += "<b>Comment by " + comment.User.DisplayName + ":</b> " + comment.Note + ", ";
                        }
                    }
                }

                if (textMatches.Substring(textMatches.Length - 2) == ", ")
                {
                    textMatches = textMatches.Substring(0, textMatches.Length - 2);
                }

                string postBackUrl = "/ReportMustInclude.aspx?formId=" + form.BaseFormData.RecordId;

                string matchHtml = "<div style=\"padding-left:18px; max-width:960px\"><b>Submitted On:</b> " + form.BaseFormData.Timestamp.ToShortDateString()
                        + "     <b>By:</b> " + form.SubmittedUserData[0].DisplayName
                        + "     <b>Status:</b> " + form.BaseFormData.Status.StatusText + "<br /><br />"
                        + "<b>Matched Fields</b><br />" + textMatches + "<br /><br /></div>";

                var thisResult = new SearchModel
                {
                    FormId = form.BaseFormData.RecordId.ToString(),
                    FormName = "Must Include",
                    MatchedFieldsHtml = matchHtml,
                    PostBackUrl = postBackUrl,
                    Status = form.BaseFormData.Status.StatusText,
                    SubmittedBy = form.SubmittedUserData[0].DisplayName,
                    SubmittedDate = form.BaseFormData.Timestamp.ToShortDateString(),
                    ResultIndex = resultCount + 1
                };

                if (!searchResults.Any(x => x.FormId == thisResult.FormId && x.FormName == thisResult.FormName)) { resultCount++; searchResults.Add(thisResult); }
            }
        }

        private void FindSampleRequestForms(List<SearchModel> searchResults, ref int resultCount, IQueryable<dynamic> sampleRequestForms)
        {
            foreach (dynamic form in sampleRequestForms)
            {
                string textMatches = "";

                if (form.BaseFormData.Company != null) { if (form.BaseFormData.Company.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Company:</b> " + form.BaseFormData.Company + ", "; } }
                if (form.BaseFormData.Status != null) { if (form.BaseFormData.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Status:</b> " + form.BaseFormData.Status.StatusText + ", "; } }
                if (form.BaseFormData.ProjectName != null) { if (form.BaseFormData.ProjectName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Project Name:</b> " + form.BaseFormData.ProjectName + ", "; } }
                if (form.BaseFormData.ItemNumber != null) { if (form.BaseFormData.ItemNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Item Number:</b> " + form.BaseFormData.ItemNumber + ", "; } }
                if (form.BaseFormData.Customer != null) { if (form.BaseFormData.Customer.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Customer:</b> " + form.BaseFormData.Customer + ", "; } }
                if (form.BaseFormData.StyleNameColor != null) { if (form.BaseFormData.StyleNameColor.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Style Name & Color:</b> " + form.BaseFormData.StyleNameColor + ", "; } }
                if (form.BaseFormData.AccountNumber != null) { if (form.BaseFormData.AccountNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Account Number:</b> " + form.BaseFormData.AccountNumber + ", "; } }
                if (form.BaseFormData.Size != null) { if (form.BaseFormData.Size.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Size:</b> " + form.BaseFormData.Size + ", "; } }
                if (form.BaseFormData.Contact != null) { if (form.BaseFormData.Contact.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Contact:</b> " + form.BaseFormData.Contact + ", "; } }
                if (form.BaseFormData.Quantity != null) { if (form.BaseFormData.Quantity.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Quantity:</b> " + form.BaseFormData.Quantity + ", "; } }
                if (form.BaseFormData.PhoneNumber != null) { if (form.BaseFormData.PhoneNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Phone Number:</b> " + form.BaseFormData.PhoneNumber + ", "; } }

                if (form.SubmittedUserData[0] != null)
                {
                    if (form.SubmittedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Display Name:</b> " + form.SubmittedUserData[0].DisplayName + ", "; }
                    if (form.SubmittedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Username:</b> " + form.SubmittedUserData[0].UserName + ", "; }
                    if (form.SubmittedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Email:</b> " + form.SubmittedUserData[0].EmailAddress + ", "; }
                }

                if (form.ModifiedUserData[0] != null)
                {
                    if (form.ModifiedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Display Name:</b> " + form.ModifiedUserData[0].DisplayName + ", "; }
                    if (form.ModifiedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Username:</b> " + form.ModifiedUserData[0].UserName + ", "; }
                    if (form.ModifiedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Email:</b> " + form.ModifiedUserData[0].EmailAddress + ", "; }
                }

                if (form.RequestedUserData[0] != null)
                {
                    if (form.RequestedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Display Name:</b> " + form.RequestedUserData[0].DisplayName + ", "; }
                    if (form.RequestedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Username:</b> " + form.RequestedUserData[0].UserName + ", "; }
                    if (form.RequestedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Email:</b> " + form.RequestedUserData[0].EmailAddress + ", "; }
                }

                foreach (dynamic comment in form.CommentsData)
                {
                    if (comment != null && comment.User != null)
                    {
                        if (comment.Note.ToLower().Contains(txtSearch.Text.ToLower()) && comment.Form.FormName == "Sample Request")
                        {
                            textMatches += "<b>Comment by " + comment.User.DisplayName + ":</b> " + comment.Note + ", ";
                        }
                    }
                }

                if (textMatches.Substring(textMatches.Length - 2) == ", ")
                {
                    textMatches = textMatches.Substring(0, textMatches.Length - 2);
                }

                string postBackUrl = "/ReportSampleRequest.aspx?formId=" + form.BaseFormData.RecordId;

                string matchHtml = "<div style=\"padding-left:18px; max-width:960px\"><b>Submitted On:</b> " + form.BaseFormData.Timestamp.ToShortDateString()
                        + "     <b>By:</b> " + form.SubmittedUserData[0].DisplayName
                        + "     <b>Status:</b> " + form.BaseFormData.Status.StatusText + "<br /><br />"
                        + "<b>Matched Fields</b><br />" + textMatches + "<br /><br /></div>";

                var thisResult = new SearchModel
                {
                    FormId = form.BaseFormData.RecordId.ToString(),
                    FormName = "Sample Request",
                    MatchedFieldsHtml = matchHtml,
                    PostBackUrl = postBackUrl,
                    Status = form.BaseFormData.Status.StatusText,
                    SubmittedBy = form.SubmittedUserData[0].DisplayName,
                    SubmittedDate = form.BaseFormData.Timestamp.ToShortDateString(),
                    ResultIndex = resultCount + 1
                };

                if (!searchResults.Any(x => x.FormId == thisResult.FormId && x.FormName == thisResult.FormName)) { resultCount++; searchResults.Add(thisResult); }
            }
        }

        private void FindLowInventoryForms(List<SearchModel> searchResults, ref int resultCount, IQueryable<dynamic> lowInventoryForms)
        {
            foreach (dynamic form in lowInventoryForms)
            {
                string textMatches = "";

                if (form.BaseFormData.Company != null) { if (form.BaseFormData.Company.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Company:</b> " + form.BaseFormData.Company + ", "; } }
                if (form.BaseFormData.Status != null) { if (form.BaseFormData.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Status:</b> " + form.BaseFormData.Status.StatusText + ", "; } }
                if (form.BaseFormData.OrderNumber != null) { if (form.BaseFormData.OrderNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Order Number:</b> " + form.BaseFormData.OrderNumber + ", "; } }
                if (form.BaseFormData.Plant != null) { if (form.BaseFormData.Plant.PlantText.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Plant:</b> " + form.BaseFormData.Plant.PlantText + ", "; } }
                if (form.BaseFormData.Line != null) { if (form.BaseFormData.Line.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Line:</b> " + form.BaseFormData.Line + ", "; } }
                if (form.BaseFormData.Quantity != null) { if (form.BaseFormData.Quantity.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Quantity:</b> " + form.BaseFormData.Quantity + ", "; } }
                if (form.BaseFormData.SKU != null) { if (form.BaseFormData.SKU.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>SKU:</b> " + form.BaseFormData.SKU + ", "; } }

                if (form.SubmittedUserData[0] != null)
                {
                    if (form.SubmittedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Display Name:</b> " + form.SubmittedUserData[0].DisplayName + ", "; }
                    if (form.SubmittedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Username:</b> " + form.SubmittedUserData[0].UserName + ", "; }
                    if (form.SubmittedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Email:</b> " + form.SubmittedUserData[0].EmailAddress + ", "; }
                }

                if (form.ModifiedUserData[0] != null)
                {
                    if (form.ModifiedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Display Name:</b> " + form.ModifiedUserData[0].DisplayName + ", "; }
                    if (form.ModifiedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Username:</b> " + form.ModifiedUserData[0].UserName + ", "; }
                    if (form.ModifiedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Email:</b> " + form.ModifiedUserData[0].EmailAddress + ", "; }
                }

                if (form.RequestedUserData[0] != null)
                {
                    if (form.RequestedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Display Name:</b> " + form.RequestedUserData[0].DisplayName + ", "; }
                    if (form.RequestedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Username:</b> " + form.RequestedUserData[0].UserName + ", "; }
                    if (form.RequestedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Email:</b> " + form.RequestedUserData[0].EmailAddress + ", "; }
                }

                foreach (dynamic comment in form.CommentsData)
                {
                    if (comment != null && comment.User != null)
                    {
                        if (comment.Note.ToLower().Contains(txtSearch.Text.ToLower()) && comment.Form.FormName == "Low Inventory")
                        {
                            textMatches += "<b>Comment by " + comment.User.DisplayName + ":</b> " + comment.Note + ", ";
                        }
                    }
                }

                if (textMatches.Substring(textMatches.Length - 2) == ", ")
                {
                    textMatches = textMatches.Substring(0, textMatches.Length - 2);
                }

                string postBackUrl = "/ReportLowInventory.aspx?formId=" + form.BaseFormData.RecordId;

                string matchHtml = "<div style=\"padding-left:18px; max-width:960px\"><b>Submitted On:</b> " + form.BaseFormData.Timestamp.ToShortDateString()
                        + "     <b>By:</b> " + form.SubmittedUserData[0].DisplayName
                        + "     <b>Status:</b> " + form.BaseFormData.Status.StatusText + "<br /><br />"
                        + "<b>Matched Fields</b><br />" + textMatches + "<br /><br /></div>";

                var thisResult = new SearchModel
                {
                    FormId = form.BaseFormData.RecordId.ToString(),
                    FormName = "Low Inventory",
                    MatchedFieldsHtml = matchHtml,
                    PostBackUrl = postBackUrl,
                    Status = form.BaseFormData.Status.StatusText,
                    SubmittedBy = form.SubmittedUserData[0].DisplayName,
                    SubmittedDate = form.BaseFormData.Timestamp.ToShortDateString(),
                    ResultIndex = resultCount + 1
                };

                if (!searchResults.Any(x => x.FormId == thisResult.FormId && x.FormName == thisResult.FormName)) { resultCount++; searchResults.Add(thisResult); }
            }
        }

        private void FindCannotWaitForContainerForms(List<SearchModel> searchResults, ref int resultCount, IQueryable<dynamic> cannotWaitForContainerForms)
        {
            foreach (dynamic form in cannotWaitForContainerForms)
            {
                string textMatches = "";

                if (form.BaseFormData.Company != null) { if (form.BaseFormData.Company.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Company:</b> " + form.BaseFormData.Company + ", "; } }
                if (form.BaseFormData.Status != null) { if (form.BaseFormData.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Status:</b> " + form.BaseFormData.Status.StatusText + ", "; } }
                if (form.BaseFormData.OrderNumber != null) { if (form.BaseFormData.OrderNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Order Number:</b> " + form.BaseFormData.OrderNumber + ", "; } }
                if (form.BaseFormData.Plant != null) { if (form.BaseFormData.Plant.PlantText.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Plant:</b> " + form.BaseFormData.Plant.PlantText + ", "; } }
                if (form.BaseFormData.Line != null) { if (form.BaseFormData.Line.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Line:</b> " + form.BaseFormData.Line + ", "; } }
                if (form.BaseFormData.Quantity != null) { if (form.BaseFormData.Quantity.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Quantity:</b> " + form.BaseFormData.Quantity + ", "; } }
                if (form.BaseFormData.SKU != null) { if (form.BaseFormData.SKU.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>SKU:</b> " + form.BaseFormData.SKU + ", "; } }

                if (form.SubmittedUserData[0] != null)
                {
                    if (form.SubmittedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Display Name:</b> " + form.SubmittedUserData[0].DisplayName + ", "; }
                    if (form.SubmittedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Username:</b> " + form.SubmittedUserData[0].UserName + ", "; }
                    if (form.SubmittedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Email:</b> " + form.SubmittedUserData[0].EmailAddress + ", "; }
                }

                if (form.ModifiedUserData[0] != null)
                {
                    if (form.ModifiedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Display Name:</b> " + form.ModifiedUserData[0].DisplayName + ", "; }
                    if (form.ModifiedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Username:</b> " + form.ModifiedUserData[0].UserName + ", "; }
                    if (form.ModifiedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Email:</b> " + form.ModifiedUserData[0].EmailAddress + ", "; }
                }

                if (form.RequestedUserData[0] != null)
                {
                    if (form.RequestedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Display Name:</b> " + form.RequestedUserData[0].DisplayName + ", "; }
                    if (form.RequestedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Username:</b> " + form.RequestedUserData[0].UserName + ", "; }
                    if (form.RequestedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Email:</b> " + form.RequestedUserData[0].EmailAddress + ", "; }
                }

                foreach (dynamic comment in form.CommentsData)
                {
                    if (comment != null && comment.User != null)
                    {
                        if (comment.Note.ToLower().Contains(txtSearch.Text.ToLower()) && comment.Form.FormName == "Cannot Wait For Container")
                        {
                            textMatches += "<b>Comment by " + comment.User.DisplayName + ":</b> " + comment.Note + ", ";
                        }
                    }
                }

                if (textMatches.Substring(textMatches.Length - 2) == ", ")
                {
                    textMatches = textMatches.Substring(0, textMatches.Length - 2);
                }

                string postBackUrl = "/ReportCannotWaitForContainer.aspx?formId=" + form.BaseFormData.RecordId;

                string matchHtml = "<div style=\"padding-left:18px; max-width:960px\"><b>Submitted On:</b> " + form.BaseFormData.Timestamp.ToShortDateString()
                        + "     <b>By:</b> " + form.SubmittedUserData[0].DisplayName
                        + "     <b>Status:</b> " + form.BaseFormData.Status.StatusText + "<br /><br />"
                        + "<b>Matched Fields</b><br />" + textMatches + "<br /><br /></div>";

                var thisResult = new SearchModel
                {
                    FormId = form.BaseFormData.RecordId.ToString(),
                    FormName = "Low Inventory",
                    MatchedFieldsHtml = matchHtml,
                    PostBackUrl = postBackUrl,
                    Status = form.BaseFormData.Status.StatusText,
                    SubmittedBy = form.SubmittedUserData[0].DisplayName,
                    SubmittedDate = form.BaseFormData.Timestamp.ToShortDateString(),
                    ResultIndex = resultCount + 1
                };

                if (!searchResults.Any(x => x.FormId == thisResult.FormId && x.FormName == thisResult.FormName)) { resultCount++; searchResults.Add(thisResult); }
            }
        }

        private void FindPriceChangeRequestForms(List<SearchModel> searchResults, ref int resultCount, IQueryable<dynamic> priceChangeRequestForms)
        {
            foreach (dynamic form in priceChangeRequestForms)
            {
                string textMatches = "";

                if (form.BaseFormData.Company != null) { if (form.BaseFormData.Company.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Company:</b> " + form.BaseFormData.Company + ", "; } }
                if (form.BaseFormData.Status != null) { if (form.BaseFormData.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Status:</b> " + form.BaseFormData.Status.StatusText + ", "; } }
                if (form.BaseFormData.Customer != null) { if (form.BaseFormData.Customer.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Customer:</b> " + form.BaseFormData.Customer + ", "; } }
                if (form.BaseFormData.LineNumber != null) { if (form.BaseFormData.LineNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Line Number:</b> " + form.BaseFormData.LineNumber + ", "; } }
                if (form.BaseFormData.AccountNumber != null) { if (form.BaseFormData.AccountNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Account Number:</b> " + form.BaseFormData.AccountNumber + ", "; } }
                if (form.BaseFormData.Quantity != null) { if (form.BaseFormData.Quantity.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Quantity:</b> " + form.BaseFormData.Quantity + ", "; } }
                if (form.BaseFormData.SKU != null) { if (form.BaseFormData.SKU.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Material SKU:</b> " + form.BaseFormData.SKU + ", "; } }
                if (form.BaseFormData.SalesRep != null) { if (form.BaseFormData.SalesRep.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Sales Rep:</b> " + form.BaseFormData.SalesRep + ", "; } }
                if (form.BaseFormData.Product != null) { if (form.BaseFormData.Product.ProductText.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Product:</b> " + form.BaseFormData.Product.ProductText + ", "; } }
                if (form.BaseFormData.OrderNumber != null) { if (form.BaseFormData.OrderNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Reference / Order Number:</b> " + form.BaseFormData.OrderNumber + ", "; } }
                if (form.BaseFormData.Price != null) { if (form.BaseFormData.Price.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Price:</b> " + form.BaseFormData.Price + ", "; } }
                if (form.BaseFormData.CrossReferenceOldOrderNumber != null) { if (form.BaseFormData.CrossReferenceOldOrderNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Cross Reference Old Order #:</b> " + form.BaseFormData.CrossReferenceOldOrderNumber + ", "; } }

                if (form.SubmittedUserData[0] != null)
                {
                    if (form.SubmittedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Display Name:</b> " + form.SubmittedUserData[0].DisplayName + ", "; }
                    if (form.SubmittedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Username:</b> " + form.SubmittedUserData[0].UserName + ", "; }
                    if (form.SubmittedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Email:</b> " + form.SubmittedUserData[0].EmailAddress + ", "; }
                }

                if (form.ModifiedUserData[0] != null)
                {
                    if (form.ModifiedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Display Name:</b> " + form.ModifiedUserData[0].DisplayName + ", "; }
                    if (form.ModifiedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Username:</b> " + form.ModifiedUserData[0].UserName + ", "; }
                    if (form.ModifiedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Email:</b> " + form.ModifiedUserData[0].EmailAddress + ", "; }
                }

                if (form.RequestedUserData[0] != null)
                {
                    if (form.RequestedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Display Name:</b> " + form.RequestedUserData[0].DisplayName + ", "; }
                    if (form.RequestedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Username:</b> " + form.RequestedUserData[0].UserName + ", "; }
                    if (form.RequestedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Email:</b> " + form.RequestedUserData[0].EmailAddress + ", "; }
                }

                foreach (dynamic comment in form.CommentsData)
                {
                    if (comment != null && comment.User != null)
                    {
                        if (comment.Note.ToLower().Contains(txtSearch.Text.ToLower()) && comment.Form.FormName == "Price Change Request")
                        {
                            textMatches += "<b>Comment by " + comment.User.DisplayName + ":</b> " + comment.Note + ", ";
                        }
                    }
                }

                if (textMatches.Substring(textMatches.Length - 2) == ", ")
                {
                    textMatches = textMatches.Substring(0, textMatches.Length - 2);
                }

                string postBackUrl = "/ReportPriceChangeRequest.aspx?formId=" + form.BaseFormData.RecordId;

                string matchHtml = "<div style=\"padding-left:18px; max-width:960px\"><b>Submitted On:</b> " + form.BaseFormData.Timestamp.ToShortDateString()
                        + "     <b>By:</b> " + form.SubmittedUserData[0].DisplayName
                        + "     <b>Status:</b> " + form.BaseFormData.Status.StatusText + "<br /><br />"
                        + "<b>Matched Fields</b><br />" + textMatches + "<br /><br /></div>";

                var thisResult = new SearchModel
                {
                    FormId = form.BaseFormData.RecordId.ToString(),
                    FormName = "Price Change Request",
                    MatchedFieldsHtml = matchHtml,
                    PostBackUrl = postBackUrl,
                    Status = form.BaseFormData.Status.StatusText,
                    SubmittedBy = form.SubmittedUserData[0].DisplayName,
                    SubmittedDate = form.BaseFormData.Timestamp.ToShortDateString(),
                    ResultIndex = resultCount + 1
                };

                if (!searchResults.Any(x => x.FormId == thisResult.FormId && x.FormName == thisResult.FormName)) { resultCount++; searchResults.Add(thisResult); }
            }
        }

        private void FindRequestForCheckForms(List<SearchModel> searchResults, ref int resultCount, IQueryable<dynamic> requestForCheckForms)
        {
            foreach (dynamic form in requestForCheckForms)
            {
                string textMatches = "";

                if (form.BaseFormData.Company != null) { if (form.BaseFormData.Company.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Company:</b> " + form.BaseFormData.Company + ", "; } }
                if (form.BaseFormData.Status != null) { if (form.BaseFormData.Status.StatusText.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Status:</b> " + form.BaseFormData.Status.StatusText + ", "; } }
                if (form.BaseFormData.PayableTo != null) { if (form.BaseFormData.PayableTo.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Payable To:</b> " + form.BaseFormData.PayableTo + ", "; } }
                if (form.BaseFormData.ChargeToAccountNumber != null) { if (form.BaseFormData.ChargeToAccountNumber.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Charge To Account Number:</b> " + form.BaseFormData.ChargeToAccountNumber + ", "; } }
                if (form.BaseFormData.ChargeToOther != null) { if (form.BaseFormData.ChargeToOther.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Charge To Other:</b> " + form.BaseFormData.ChargeToOther + ", "; } }
                if (form.BaseFormData.Amount != null) { if (form.BaseFormData.Amount.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Amount:</b> " + form.BaseFormData.Amount + ", "; } }
                if (form.BaseFormData.For != null) { if (form.BaseFormData.For.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>For:</b> " + form.BaseFormData.For + ", "; } }
                if (form.BaseFormData.RequestedBy != null) { if (form.BaseFormData.RequestedBy.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested By:</b> " + form.BaseFormData.RequestedBy + ", "; } }
                if (form.BaseFormData.ApprovedBy != null) { if (form.BaseFormData.ApprovedBy.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Approved By:</b> " + form.BaseFormData.ApprovedBy + ", "; } }

                if (form.SubmittedUserData[0] != null)
                {
                    if (form.SubmittedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Display Name:</b> " + form.SubmittedUserData[0].DisplayName + ", "; }
                    if (form.SubmittedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Username:</b> " + form.SubmittedUserData[0].UserName + ", "; }
                    if (form.SubmittedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Submitted User Email:</b> " + form.SubmittedUserData[0].EmailAddress + ", "; }
                }

                if (form.ModifiedUserData[0] != null)
                {
                    if (form.ModifiedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Display Name:</b> " + form.ModifiedUserData[0].DisplayName + ", "; }
                    if (form.ModifiedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Username:</b> " + form.ModifiedUserData[0].UserName + ", "; }
                    if (form.ModifiedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Modified User Email:</b> " + form.ModifiedUserData[0].EmailAddress + ", "; }
                }

                if (form.RequestedUserData[0] != null)
                {
                    if (form.RequestedUserData[0].DisplayName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Display Name:</b> " + form.RequestedUserData[0].DisplayName + ", "; }
                    if (form.RequestedUserData[0].UserName.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Username:</b> " + form.RequestedUserData[0].UserName + ", "; }
                    if (form.RequestedUserData[0].EmailAddress.ToLower().Contains(txtSearch.Text.ToLower())) { textMatches += "<b>Requested User Email:</b> " + form.RequestedUserData[0].EmailAddress + ", "; }
                }

                foreach (dynamic comment in form.CommentsData)
                {
                    if (comment != null && comment.User != null)
                    {
                        if (comment.Note.ToLower().Contains(txtSearch.Text.ToLower()) && comment.Form.FormName == "Request For Check")
                        {
                            textMatches += "<b>Comment by " + comment.User.DisplayName + ":</b> " + comment.Note + ", ";
                        }
                    }
                }

                if (textMatches.Substring(textMatches.Length - 2) == ", ")
                {
                    textMatches = textMatches.Substring(0, textMatches.Length - 2);
                }

                string postBackUrl = "/ReportRequestForCheck.aspx?formId=" + form.BaseFormData.RecordId;

                string matchHtml = "<div style=\"padding-left:18px; max-width:960px\"><b>Submitted On:</b> " + form.BaseFormData.Timestamp.ToShortDateString()
                        + "     <b>By:</b> " + form.SubmittedUserData[0].DisplayName
                        + "     <b>Status:</b> " + form.BaseFormData.Status.StatusText + "<br /><br />"
                        + "<b>Matched Fields</b><br />" + textMatches + "<br /><br /></div>";

                var thisResult = new SearchModel
                {
                    FormId = form.BaseFormData.RecordId.ToString(),
                    FormName = "Request For Check",
                    MatchedFieldsHtml = matchHtml,
                    PostBackUrl = postBackUrl,
                    Status = form.BaseFormData.Status.StatusText,
                    SubmittedBy = form.SubmittedUserData[0].DisplayName,
                    SubmittedDate = form.BaseFormData.Timestamp.ToShortDateString(),
                    ResultIndex = resultCount + 1
                };

                if (!searchResults.Any(x => x.FormId == thisResult.FormId && x.FormName == thisResult.FormName)) { resultCount++; searchResults.Add(thisResult); }
            }
        }

        private void CreatePagingControl()
        {
            int buttonCount = RowCount % 10 == 0 ? RowCount / 10 : (RowCount / 10) + 1;

            plcPaging.Controls.Add(new LiteralControl("<div style=\"width:100%; text-align:center;\">"));

            for (int i = 0; i < buttonCount; i++)
            {
                plcPaging.Controls.Add(new LiteralControl("<div style=\"float:left; padding-right:2px;\">"));
                LinkButton lnk = new LinkButton();
                lnk.Click += new EventHandler(lbl_Click);
                lnk.ID = "lnkPage" + (i + 1).ToString();
                lnk.Text = (i + 1).ToString();
                plcPaging.Controls.Add(lnk);
                plcPaging.Controls.Add(new LiteralControl("</div>"));
            }
            plcPaging.Controls.Add(new LiteralControl("<div style=\"width:5%; text-align:center;\">"));
        }

        void lbl_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            int currentPage = int.Parse(lnk.Text);
            int take = currentPage * 10;
            int skip = currentPage == 1 ? 0 : take - 10;
            SearchForText(take, skip);
        }

    }
}