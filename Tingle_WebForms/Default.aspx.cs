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
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadFavoriteForms();
                LoadFormPermissions();
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
                        divOrderCancellation.Visible = true;
                    }
                    else
                    {
                        divOrderCancellation.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Expedited Order" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divExpeditedOrder.Visible = true;
                    }
                    else
                    {
                        divExpeditedOrder.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Price Change Request" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divPriceChangeRequest.Visible = true;
                    }
                    else
                    {
                        divPriceChangeRequest.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Hot Rush" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divHotRush.Visible = true;
                    }
                    else
                    {
                        divHotRush.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Low Inventory" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divLowInventory.Visible = true;
                    }
                    else
                    {
                        divLowInventory.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Sample Request" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divSampleRequest.Visible = true;
                    }
                    else
                    {
                        divSampleRequest.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Direct Order" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divDirectOrder.Visible = true;
                    }
                    else
                    {
                        divDirectOrder.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Request For Check" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divRequestForCheck.Visible = true;
                    }
                    else
                    {
                        divRequestForCheck.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Must Include" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divMustInclude.Visible = true;
                    }
                    else
                    {
                        divMustInclude.Visible = false;
                    }

                    if (formPermissions.Any(x => x.FormName == "Cannot Wait For Container" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                    {
                        divCannotWaitForContainer.Visible = true;
                    }
                    else
                    {
                        divCannotWaitForContainer.Visible = false;
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
                                cbFavoriteExpeditedOrder.Checked = true;
                            }
                            else
                            {
                                phFavoriteExpeditedOrder.Visible = false;
                                cbFavoriteExpeditedOrder.Checked = false;
                            }

                            if (favForms.Any(f => f.Form.FormName == "Direct Order") &&
                                formPermissions.Any(x => x.FormName == "Direct Order" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                            {
                                phFavoriteDirectOrder.Visible = true;
                                cbFavoriteDirectOrder.Checked = true;
                            }
                            else
                            {
                                phFavoriteDirectOrder.Visible = false;
                                cbFavoriteDirectOrder.Checked = false;
                            }

                            if (favForms.Any(f => f.Form.FormName == "Hot Rush") &&
                                formPermissions.Any(x => x.FormName == "Hot Rush" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                            {
                                phFavoriteHotRush.Visible = true;
                                cbFavoriteHotRush.Checked = true;
                            }
                            else
                            {
                                phFavoriteHotRush.Visible = false;
                                cbFavoriteHotRush.Checked = false;
                            }

                            if (favForms.Any(f => f.Form.FormName == "Order Cancellation") &&
                                formPermissions.Any(x => x.FormName == "Order Cancellation" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                            {
                                phFavoriteOrderCancellation.Visible = true;
                                cbFavoriteOrderCancellation.Checked = true;
                            }
                            else
                            {
                                phFavoriteOrderCancellation.Visible = false;
                                cbFavoriteOrderCancellation.Checked = false;
                            }

                            if (favForms.Any(f => f.Form.FormName == "Must Include") &&
                                formPermissions.Any(x => x.FormName == "Must Include" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                            {
                                phFavoriteMustInclude.Visible = true;
                                cbFavoriteMustInclude.Checked = true;
                            }
                            else
                            {
                                phFavoriteMustInclude.Visible = false;
                                cbFavoriteMustInclude.Checked = false;
                            }

                            if (favForms.Any(f => f.Form.FormName == "Sample Request") &&
                                formPermissions.Any(x => x.FormName == "Sample Request" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                            {
                                phFavoriteSampleRequest.Visible = true;
                                cbFavoriteSampleRequest.Checked = true;
                            }
                            else
                            {
                                phFavoriteSampleRequest.Visible = false;
                                cbFavoriteSampleRequest.Checked = false;
                            }

                            if (favForms.Any(f => f.Form.FormName == "Low Inventory") &&
                                formPermissions.Any(x => x.FormName == "Low Inventory" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                            {
                                phFavoriteLowInventory.Visible = true;
                                cbFavoriteLowInventory.Checked = true;
                            }
                            else
                            {
                                phFavoriteLowInventory.Visible = false;
                                cbFavoriteLowInventory.Checked = false;
                            }

                            if (favForms.Any(f => f.Form.FormName == "Cannot Wait For Container") &&
                                formPermissions.Any(x => x.FormName == "Cannot Wait For Container" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                            {
                                phFavoriteCannotWaitForContainer.Visible = true;
                                cbFavoriteCannotWaitForContainer.Checked = true;
                            }
                            else
                            {
                                phFavoriteCannotWaitForContainer.Visible = false;
                                cbFavoriteCannotWaitForContainer.Checked = false;
                            }

                            if (favForms.Any(f => f.Form.FormName == "Price Change Request") &&
                                formPermissions.Any(x => x.FormName == "Price Change Request" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                            {
                                phFavoritePriceChangeRequest.Visible = true;
                                cbFavoritePriceChangeRequest.Checked = true;
                            }
                            else
                            {
                                phFavoritePriceChangeRequest.Visible = false;
                                cbFavoritePriceChangeRequest.Checked = false;
                            }

                            if (favForms.Any(f => f.Form.FormName == "Request For Check") &&
                                formPermissions.Any(x => x.FormName == "Request For Check" && x.UserRole.UserRoleId == currentUser.UserRole.UserRoleId && x.Enabled == true))
                            {
                                phFavoriteRequestForCheck.Visible = true;
                                cbFavoriteRequestForCheck.Checked = true;
                            }
                            else
                            {
                                phFavoriteRequestForCheck.Visible = false;
                                cbFavoriteRequestForCheck.Checked = false;
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

                            cbFavoriteExpeditedOrder.Checked = false;
                            cbFavoriteHotRush.Checked = false;
                            cbFavoriteOrderCancellation.Checked = false;
                            cbFavoritePriceChangeRequest.Checked = false;
                            cbFavoriteLowInventory.Checked = false;
                            cbFavoriteSampleRequest.Checked = false;
                            cbFavoriteDirectOrder.Checked = false;
                            cbFavoriteRequestForCheck.Checked = false;
                            cbFavoriteMustInclude.Checked = false;
                            cbFavoriteCannotWaitForContainer.Checked = false;

                        }
                    }

                    upFavorites.Update();
                }
            }
            catch (Exception ex)
            {
                throw;
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

        protected void cbFavoriteExpeditedOrder_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    UserLogic uLogic = new UserLogic();
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);
                    if (currentUser != null)
                    {
                        if (cbFavoriteExpeditedOrder.Checked == true)
                        {
                            FavoriteForm favForm = new FavoriteForm();
                            if (ctx.SystemUsers.Any(s => s.SystemUserID == currentUser.SystemUserID) && ctx.TForms.Any(t => t.FormName == "Expedited Order"))
                            {
                                if (!ctx.FavoriteForms.Any(f => f.Form.FormName == "Expedited Order" && f.User.SystemUserID == currentUser.SystemUserID))
                                {
                                    favForm.User = ctx.SystemUsers.First(s => s.SystemUserID == currentUser.SystemUserID);
                                    favForm.Form = ctx.TForms.First(t => t.FormName == "Expedited Order");

                                    ctx.FavoriteForms.Add(favForm);
                                }
                            }
                            else
                            {
                                lblUserMessage.Visible = true;
                                lblUserMessage.Text = "Unable to add Favorite Form.  Please contact your System Administrator for more information.";
                            }

                        }
                        else
                        {
                            if (ctx.FavoriteForms.Any(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Expedited Order"))
                            {
                                var favForm = ctx.FavoriteForms.First(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Expedited Order");
                                ctx.FavoriteForms.Remove(favForm);
                            }
                        }

                        ctx.SaveChanges();
                        LoadFavoriteForms();

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void cbFavoritePriceChangeRequest_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    UserLogic uLogic = new UserLogic();
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);
                    if (currentUser != null)
                    {
                        if (cbFavoritePriceChangeRequest.Checked == true)
                        {
                            FavoriteForm favForm = new FavoriteForm();
                            if (ctx.SystemUsers.Any(s => s.SystemUserID == currentUser.SystemUserID) && ctx.TForms.Any(t => t.FormName == "Price Change Request"))
                            {
                                if (!ctx.FavoriteForms.Any(f => f.Form.FormName == "Price Change Request" && f.User.SystemUserID == currentUser.SystemUserID))
                                {
                                    favForm.User = ctx.SystemUsers.First(s => s.SystemUserID == currentUser.SystemUserID);
                                    favForm.Form = ctx.TForms.First(t => t.FormName == "Price Change Request");

                                    ctx.FavoriteForms.Add(favForm);
                                }
                            }
                            else
                            {
                                lblUserMessage.Visible = true;
                                lblUserMessage.Text = "Unable to add Favorite Form.  Please contact your System Administrator for more information.";
                            }

                        }
                        else
                        {
                            if (ctx.FavoriteForms.Any(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Price Change Request"))
                            {
                                var favForm = ctx.FavoriteForms.First(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Price Change Request");
                                ctx.FavoriteForms.Remove(favForm);
                            }
                        }

                        ctx.SaveChanges();
                        LoadFavoriteForms();

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void cbFavoriteOrderCancellation_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    UserLogic uLogic = new UserLogic();
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);
                    if (currentUser != null)
                    {
                        if (cbFavoriteOrderCancellation.Checked == true)
                        {
                            FavoriteForm favForm = new FavoriteForm();
                            if (ctx.SystemUsers.Any(s => s.SystemUserID == currentUser.SystemUserID) && ctx.TForms.Any(t => t.FormName == "Order Cancellation"))
                            {
                                if (!ctx.FavoriteForms.Any(f => f.Form.FormName == "Order Cancellation" && f.User.SystemUserID == currentUser.SystemUserID))
                                {
                                    favForm.User = ctx.SystemUsers.First(s => s.SystemUserID == currentUser.SystemUserID);
                                    favForm.Form = ctx.TForms.First(t => t.FormName == "Order Cancellation");

                                    ctx.FavoriteForms.Add(favForm);
                                }
                            }
                            else
                            {
                                lblUserMessage.Visible = true;
                                lblUserMessage.Text = "Unable to add Favorite Form.  Please contact your System Administrator for more information.";
                            }
                        }
                        else
                        {
                            if (ctx.FavoriteForms.Any(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Order Cancellation"))
                            {
                                var favForm = ctx.FavoriteForms.First(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Order Cancellation");
                                ctx.FavoriteForms.Remove(favForm);
                            }
                        }

                        ctx.SaveChanges();
                        LoadFavoriteForms();

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void cbFavoriteHotRush_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    UserLogic uLogic = new UserLogic();
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);
                    if (currentUser != null)
                    {
                        if (cbFavoriteHotRush.Checked == true)
                        {
                            FavoriteForm favForm = new FavoriteForm();
                            if (ctx.SystemUsers.Any(s => s.SystemUserID == currentUser.SystemUserID) && ctx.TForms.Any(t => t.FormName == "Hot Rush"))
                            {
                                if (!ctx.FavoriteForms.Any(f => f.Form.FormName == "Hot Rush" && f.User.SystemUserID == currentUser.SystemUserID))
                                {
                                    favForm.User = ctx.SystemUsers.First(s => s.SystemUserID == currentUser.SystemUserID);
                                    favForm.Form = ctx.TForms.First(t => t.FormName == "Hot Rush");

                                    ctx.FavoriteForms.Add(favForm);
                                }
                            }
                            else
                            {
                                lblUserMessage.Visible = true;
                                lblUserMessage.Text = "Unable to add Favorite Form.  Please contact your System Administrator for more information.";
                            }
                        }
                        else
                        {
                            if (ctx.FavoriteForms.Any(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Hot Rush"))
                            {
                                var favForm = ctx.FavoriteForms.First(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Hot Rush");
                                ctx.FavoriteForms.Remove(favForm);
                            }
                        }

                        ctx.SaveChanges();
                        LoadFavoriteForms();

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void cbFavoriteLowInventory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    UserLogic uLogic = new UserLogic();
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);
                    if (currentUser != null)
                    {
                        if (cbFavoriteLowInventory.Checked == true)
                        {
                            FavoriteForm favForm = new FavoriteForm();
                            if (ctx.SystemUsers.Any(s => s.SystemUserID == currentUser.SystemUserID) && ctx.TForms.Any(t => t.FormName == "Low Inventory"))
                            {
                                if (!ctx.FavoriteForms.Any(f => f.Form.FormName == "Low Inventory" && f.User.SystemUserID == currentUser.SystemUserID))
                                {
                                    favForm.User = ctx.SystemUsers.First(s => s.SystemUserID == currentUser.SystemUserID);
                                    favForm.Form = ctx.TForms.First(t => t.FormName == "Low Inventory");

                                    ctx.FavoriteForms.Add(favForm);
                                }
                            }
                            else
                            {
                                lblUserMessage.Visible = true;
                                lblUserMessage.Text = "Unable to add Favorite Form.  Please contact your System Administrator for more information.";
                            }
                        }
                        else
                        {
                            if (ctx.FavoriteForms.Any(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Low Inventory"))
                            {
                                var favForm = ctx.FavoriteForms.First(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Low Inventory");
                                ctx.FavoriteForms.Remove(favForm);
                            }
                        }

                        ctx.SaveChanges();
                        LoadFavoriteForms();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void cbFavoriteSampleRequest_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    UserLogic uLogic = new UserLogic();
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);
                    if (currentUser != null)
                    {
                        if (cbFavoriteSampleRequest.Checked == true)
                        {
                            FavoriteForm favForm = new FavoriteForm();
                            if (ctx.SystemUsers.Any(s => s.SystemUserID == currentUser.SystemUserID) && ctx.TForms.Any(t => t.FormName == "Sample Request"))
                            {
                                if (!ctx.FavoriteForms.Any(f => f.Form.FormName == "Sample Request" && f.User.SystemUserID == currentUser.SystemUserID))
                                {
                                    favForm.User = ctx.SystemUsers.First(s => s.SystemUserID == currentUser.SystemUserID);
                                    favForm.Form = ctx.TForms.First(t => t.FormName == "Sample Request");

                                    ctx.FavoriteForms.Add(favForm);
                                }
                            }
                            else
                            {
                                lblUserMessage.Visible = true;
                                lblUserMessage.Text = "Unable to add Favorite Form.  Please contact your System Administrator for more information.";
                            }
                        }
                        else
                        {
                            if (ctx.FavoriteForms.Any(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Sample Request"))
                            {
                                var favForm = ctx.FavoriteForms.First(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Sample Request");
                                ctx.FavoriteForms.Remove(favForm);
                            }
                        }

                        ctx.SaveChanges();
                        LoadFavoriteForms();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void cbFavoriteDirectOrder_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    UserLogic uLogic = new UserLogic();
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);
                    if (currentUser != null)
                    {
                        if (cbFavoriteDirectOrder.Checked == true)
                        {
                            FavoriteForm favForm = new FavoriteForm();
                            if (ctx.SystemUsers.Any(s => s.SystemUserID == currentUser.SystemUserID) && ctx.TForms.Any(t => t.FormName == "Direct Order"))
                            {
                                if (!ctx.FavoriteForms.Any(f => f.Form.FormName == "Direct Order" && f.User.SystemUserID == currentUser.SystemUserID))
                                {
                                    favForm.User = ctx.SystemUsers.First(s => s.SystemUserID == currentUser.SystemUserID);
                                    favForm.Form = ctx.TForms.First(t => t.FormName == "Direct Order");

                                    ctx.FavoriteForms.Add(favForm);
                                }
                            }
                            else
                            {
                                lblUserMessage.Visible = true;
                                lblUserMessage.Text = "Unable to add Favorite Form.  Please contact your System Administrator for more information.";
                            }
                        }
                        else
                        {
                            if (ctx.FavoriteForms.Any(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Direct Order"))
                            {
                                var favForm = ctx.FavoriteForms.First(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Direct Order");
                                ctx.FavoriteForms.Remove(favForm);
                            }
                        }

                        ctx.SaveChanges();
                        LoadFavoriteForms();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void cbFavoriteRequestForCheck_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    UserLogic uLogic = new UserLogic();
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);
                    if (currentUser != null)
                    {
                        if (cbFavoriteRequestForCheck.Checked == true)
                        {
                            FavoriteForm favForm = new FavoriteForm();
                            if (ctx.SystemUsers.Any(s => s.SystemUserID == currentUser.SystemUserID) && ctx.TForms.Any(t => t.FormName == "Request For Check"))
                            {
                                if (!ctx.FavoriteForms.Any(f => f.Form.FormName == "Request For Check" && f.User.SystemUserID == currentUser.SystemUserID))
                                {
                                    favForm.User = ctx.SystemUsers.First(s => s.SystemUserID == currentUser.SystemUserID);
                                    favForm.Form = ctx.TForms.First(t => t.FormName == "Request For Check");

                                    ctx.FavoriteForms.Add(favForm);
                                }
                            }
                            else
                            {
                                lblUserMessage.Visible = true;
                                lblUserMessage.Text = "Unable to add Favorite Form.  Please contact your System Administrator for more information.";
                            }
                        }
                        else
                        {
                            if (ctx.FavoriteForms.Any(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Request For Check"))
                            {
                                var favForm = ctx.FavoriteForms.First(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Request For Check");
                                ctx.FavoriteForms.Remove(favForm);
                            }
                        }

                        ctx.SaveChanges();
                        LoadFavoriteForms();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void cbFavoriteMustInclude_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    UserLogic uLogic = new UserLogic();
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);
                    if (currentUser != null)
                    {
                        if (cbFavoriteMustInclude.Checked == true)
                        {
                            FavoriteForm favForm = new FavoriteForm();
                            if (ctx.SystemUsers.Any(s => s.SystemUserID == currentUser.SystemUserID) && ctx.TForms.Any(t => t.FormName == "Must Include"))
                            {
                                if (!ctx.FavoriteForms.Any(f => f.Form.FormName == "Must Include" && f.User.SystemUserID == currentUser.SystemUserID))
                                {
                                    favForm.User = ctx.SystemUsers.First(s => s.SystemUserID == currentUser.SystemUserID);
                                    favForm.Form = ctx.TForms.First(t => t.FormName == "Must Include");

                                    ctx.FavoriteForms.Add(favForm);
                                }
                            }
                            else
                            {
                                lblUserMessage.Visible = true;
                                lblUserMessage.Text = "Unable to add Favorite Form.  Please contact your System Administrator for more information.";
                            }
                        }
                        else
                        {
                            if (ctx.FavoriteForms.Any(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Must Include"))
                            {
                                var favForm = ctx.FavoriteForms.First(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Must Include");
                                ctx.FavoriteForms.Remove(favForm);
                            }
                        }

                        ctx.SaveChanges();
                        LoadFavoriteForms();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void cbFavoriteCannotWaitForContainer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                using (FormContext ctx = new FormContext())
                {
                    System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                    UserLogic uLogic = new UserLogic();
                    SystemUsers currentUser = uLogic.GetCurrentUser(user);
                    if (currentUser != null)
                    {
                        if (cbFavoriteCannotWaitForContainer.Checked == true)
                        {
                            FavoriteForm favForm = new FavoriteForm();
                            if (ctx.SystemUsers.Any(s => s.SystemUserID == currentUser.SystemUserID) && ctx.TForms.Any(t => t.FormName == "Cannot Wait For Container"))
                            {
                                if (!ctx.FavoriteForms.Any(f => f.Form.FormName == "Cannot Wait For Container" && f.User.SystemUserID == currentUser.SystemUserID))
                                {
                                    favForm.User = ctx.SystemUsers.First(s => s.SystemUserID == currentUser.SystemUserID);
                                    favForm.Form = ctx.TForms.First(t => t.FormName == "Cannot Wait For Container");

                                    ctx.FavoriteForms.Add(favForm);
                                }
                            }
                            else
                            {
                                lblUserMessage.Visible = true;
                                lblUserMessage.Text = "Unable to add Favorite Form.  Please contact your System Administrator for more information.";
                            }
                        }
                        else
                        {
                            if (ctx.FavoriteForms.Any(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Cannot Wait For Container"))
                            {
                                var favForm = ctx.FavoriteForms.First(f => f.User.SystemUserID == currentUser.SystemUserID && f.Form.FormName == "Cannot Wait For Container");
                                ctx.FavoriteForms.Remove(favForm);
                            }
                        }

                        ctx.SaveChanges();
                        LoadFavoriteForms();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}