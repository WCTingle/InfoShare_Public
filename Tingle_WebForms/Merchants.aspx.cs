using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Web.ModelBinding;
using Telerik.Web.UI;
using System.Collections;
using System.Web.Security;
using System.Text;

namespace Tingle_WebForms
{
public partial class Merchants : System.Web.UI.Page
    {
        ApplicationDbContext _globalCtx = new ApplicationDbContext();
        Logic.Logic _newLogic = new Logic.Logic();

        bool isExport = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                var currentUser = _globalCtx.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

                if (currentUser != null)
                {
                    if (!Page.IsPostBack)
                    {
                        ltlAdminWelcome.Text = "<p class=\"adminHeaderText\">Central Cash Merchant Management</p>";
                    }
                }
                else
                {
                    Page.Response.Redirect("/Default", false);
                }

            }
            else
            {
                Page.Response.Redirect("/Default", false);
            }
        }



        protected void gridMerchantList_UpdateCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void gridMerchantList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GetMerchants();
        }

        protected void gridMerchantList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem dataBoundItem = e.Item as GridDataItem;
                    TableCell statusCell = dataBoundItem["StatusDescription"];
                    Label lblStatus = (Label)dataBoundItem.FindControl("lblStatus");
                    string statusDesc = "";
                    if (lblStatus != null)
                    {
                        statusDesc = lblStatus.Text.ToLower();
                    }

                    switch (statusDesc)
                    {
                        case "pre-enrolled":
                            statusCell.BackColor = System.Drawing.Color.BurlyWood;
                            break;
                        case "enrolled":
                            statusCell.BackColor = System.Drawing.Color.Gray;
                            break;
                        case "active":
                            statusCell.BackColor = System.Drawing.Color.Green;
                            break;
                        case "suspended":
                            statusCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#F00000");
                            break;
                        case "cancelled":
                            statusCell.BackColor = System.Drawing.Color.Black;
                            break;
                        case "denied":
                            statusCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#680000");
                            break;
                    }

                    TableCell uwStatusCell = dataBoundItem["UnderwritingStatusDescription"];
                    Label lblUWStatus = (Label)dataBoundItem.FindControl("lblUWStatus");
                    string uwStatusDesc = "";
                    if (lblUWStatus != null)
                    {
                        uwStatusDesc = lblUWStatus.Text.ToLower();
                    }

                    switch (uwStatusDesc)
                    {
                        case "pending":
                            uwStatusCell.ForeColor = System.Drawing.Color.DarkGray;
                            break;
                        case "approved":
                            uwStatusCell.ForeColor = System.Drawing.Color.Green;
                            break;
                        case "denied":
                            uwStatusCell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#680000");
                            break;
                        case "outdated":
                            uwStatusCell.ForeColor = System.Drawing.Color.Red;
                            break;
                        case "cancelled":
                            uwStatusCell.ForeColor = System.Drawing.Color.Black;
                            break;
                    }



                }

                if (e.Item is GridFilteringItem)
                {
                    GridFilteringItem filterItem = (GridFilteringItem)e.Item;
                    filterItem["RecordId"].HorizontalAlign = HorizontalAlign.Center;
                    filterItem["CorpName"].HorizontalAlign = HorizontalAlign.Center;
                    filterItem["MerchantPrincipal.Contact.FirstName"].HorizontalAlign = HorizontalAlign.Center;
                    filterItem["MerchantPrincipal.Contact.LastName"].HorizontalAlign = HorizontalAlign.Center;
                    filterItem["Business.HomePhone"].HorizontalAlign = HorizontalAlign.Center;
                    filterItem["UnderwritingStatusDescription"].HorizontalAlign = HorizontalAlign.Center;
                    filterItem["StatusDescription"].HorizontalAlign = HorizontalAlign.Center;

                }
            }
            catch(System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "gridMerchantList_ItemDataBound");
            }
        } 

        protected void gridMerchantList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName)
            {
                gridMerchantList.ExportSettings.ExportOnlyData = false;
                gridMerchantList.MasterTableView.ExportToCSV();
            }

            if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName)
            {
                isExport = true;

                gridMerchantList.MasterTableView.ExportToExcel();
            }

            if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToPdfCommandName)
            {
                isExport = true;

                gridMerchantList.MasterTableView.ExportToPdf();
            }

            if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName)
            {
                isExport = true;

                gridMerchantList.MasterTableView.ExportToWord();
            }
        }

        protected void gridMerchantList_ExportCellFormatting(object sender, ExportCellFormattingEventArgs e)
        {
            if (e.FormattedColumn.UniqueName == "StatusDescription")
            {
                e.Cell.Style["Color"] = "#FFF";
            }
        }

        protected void gridMerchantList_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem && isExport)
            {
                e.Item.Visible = false;
            }
        }

        protected void gridMerchantList_CancelCommand(object sender, GridCommandEventArgs e)
        {
            gridMerchantList.MasterTableView.ClearEditItems();
            gridMerchantList.MasterTableView.ClearChildEditItems();
        }

        protected void gridExistingUsers_ItemCommand(object sender, GridCommandEventArgs e)
        {
            string userId;

            if (e.CommandName == Telerik.Web.UI.RadGrid.EditCommandName)
            {
                userId = (e.Item as GridDataItem).GetDataKeyValue("Id").ToString();
                SelectExistingUser(userId);
            }
        }

        

        protected void btnAddNewMerchant_Click(object sender, EventArgs e)
        {
            pnlMerchantList.Visible = false;
            pnlExistingUser.Visible = true;
            pnlRegisterUser.Visible = false;
            pnlNewMerchant.Visible = true;

            tabNewMerchantMenu.SelectedIndex = 0;
            multiPage1.SelectedIndex = 0;

            ltlAdminWelcome.Text = "<p class=\"adminHeaderText\">Add New Merchant</p>";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlRegisterUser.Visible = false;
            pnlMerchantList.Visible = true;

            ltlAdminWelcome.Text = "<p class=\"adminHeaderText\">Central Cash Merchant Management</p>";
        }

        protected void btnNewUser_Click(object sender, EventArgs e)
        {
            pnlMerchantList.Visible = false;
            pnlRegisterUser.Visible = false;
            pnlExistingUser.Visible = true;
        }

        protected void btnExistingUser_Click(object sender, EventArgs e)
        {
            pnlMerchantList.Visible = false;
            pnlRegisterUser.Visible = false;
            pnlExistingUser.Visible = true;
        }

        protected void btnAddNewUser_Click(object sender, EventArgs e)
        {
            pnlExistingUser.Visible = false;
            pnlRegisterUser.Visible = true;

        }

        protected void btnSelectExistingUser_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    //var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));
                    Logic.CustomUserValidator customerUserValidator = new Logic.CustomUserValidator();
                    manager.UserValidator = customerUserValidator;
                    var user = new ApplicationUser()
                    {
                        UserName = txtUsername.Text,
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        Email = txtEmail.Text,
                        PhoneNumber = txtPhone.Text,
                        Status = ctx.UserStatuses.First(us => us.StatusDescription == "Active"),
                        HasBeenDisassociated = false,
                        DisassociatedMerchant = null
                    };

                    String randomPassword = CreatePassword();

                    IdentityResult result = manager.Create(user, randomPassword);

                    if (result.Succeeded)
                    {
                        lblNewUserMessage.Text = "";

                        ApplicationUser newUser = manager.FindByName(user.UserName);

                        if (newUser != null)
                        {
                            if (!manager.IsInRole(newUser.Id, "Merchant"))
                            {
                                manager.AddToRole(newUser.Id, "Merchant");
                                manager.Update(newUser);
                                ctx.SaveChanges();
                            }



                        }
                        else
                        {
                            lblNewUserMessage.Text = "An error occurred while adding the new user.  Please contact your system administrator before continuing.";
                        }

                        NewUserAdded();

                        btnContinueAddUser.Visible = true;
                        btnCancelAddUser.Visible = false;
                        btnAddUser.Visible = false;
                    }
                    else
                    {
                        lblNewUserMessage.Text = result.Errors.FirstOrDefault();
                    }
                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "btnAddUser_Click");
            }
        }

        protected void btnCancelAddUser_Click(object sender, EventArgs e)
        {
            pnlRegisterUser.Visible = false;
            txtEmail.Text = "";
            txtEmailConfirm.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtUsername.Text = "";

            pnlNewMerchant.Visible = false;
            pnlMerchantList.Visible = true;
        }

        protected void btnContinueAddUser_Click(object sender, EventArgs e)
        {
            NewUserAdded();
        }

        protected void btnCompleteRegistration_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlExistingUser.SelectedValue == "")
                {
                    multiPage1.SelectedIndex = 0;
                    tabNewMerchantMenu.SelectedIndex = 0;
                    ddlExistingUser.Focus();
                    ddlExistingUser.BorderColor = System.Drawing.Color.Red;
                    ddlExistingUser.BorderStyle = BorderStyle.Dashed;
                    ddlExistingUser.BorderWidth = Unit.Pixel(2);
                    lblExistingUserRequired.Visible = true;
                }
                else
                {
                    if (txtCorpName.Text.Trim() == "")
                    {
                        multiPage1.SelectedIndex = 1;
                        tabNewMerchantMenu.SelectedIndex = 1;
                        txtCorpName.Focus();
                        txtCorpName.BorderColor = System.Drawing.Color.Red;
                        txtCorpName.BorderStyle = BorderStyle.Dashed;
                        txtCorpName.BorderWidth = Unit.Pixel(2);
                        lblCorpNameRequired.Visible = true;
                    }
                    else
                    {
                        lblCorpNameRequired.Visible = false;
                        lblExistingUserRequired.Visible = false;

                        Boolean formResult = SubmitForm();

                        if (formResult)
                        {
                            pnlNewMerchant.Visible = false;
                            pnlMerchantList.Visible = true;
                            gridMerchantList.Rebind();
                        }
                        else
                        {
                            if (lblSubmissionMessage.Text == "")
                            {
                                lblSubmissionMessage.Text = "New Merchant Registration Failed.  Please try again or contact your system administrator for more information";
                            }
                        }
                    }
                }

                
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "btnCompleteRegistration_Click");
            }
        }

        protected void btnCancelRegistration_Click(object sender, EventArgs e)
        {
            pnlNewMerchant.Visible = false;
            pnlMerchantList.Visible = true;
            ltlAdminWelcome.Text = "<p class=\"adminHeaderText\">Central Cash Merchant Management</p>";
        }



        protected Boolean SubmitForm()
        {
            StringBuilder formattedHtml = new StringBuilder();
            StringBuilder formattedInternalHtml = new StringBuilder();
            string seasonalMonths = "";

            try
            {
                foreach (ListItem item in cblSeasonal.Items)
                {
                    if (item.Selected)
                    {
                        seasonalMonths += item.Value + " - ";
                    }
                }

                if (seasonalMonths.Length > 3)
                {
                    seasonalMonths = seasonalMonths.Substring(0, seasonalMonths.Length - 3);
                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "AdminSubmitForm - Get Seasonal Months");
            }

            try
            {
                //Instanciate new model objects for each piece of data to be created
                MerchantModel newMerchant = new MerchantModel();
                MerchantPrincipalModel newMerchantPrincipal = new MerchantPrincipalModel();
                ContactModel newMerchantPrincipalContact = new ContactModel();
                AddressModel newMerchantPrincipalContactAddress = new AddressModel();
                ContactModel newContact = new ContactModel();
                ContactModel newBusiness = new ContactModel();
                AddressModel newBusinessAddress = new AddressModel();
                ProcessorModel newProcessor = new ProcessorModel();
                DebitCardModel newDebitCard = new DebitCardModel();
                BankModel newBankModel = new BankModel();
                BankAccountModel newBankAccountModel = new BankAccountModel();

                //Set base merchant information in newMerchant object
                if (txtMerchantId.Text != "") { newMerchant.MerchantId = txtMerchantId.Text; }
                if (txtCorpName.Text != "") { newMerchant.CorpName = txtCorpName.Text; }
                if (txtDBAName.Text != "") { newMerchant.DbaName = txtDBAName.Text; }
                if (txtBusLicNumber.Text != "") { newMerchant.BusLicNumber = txtBusLicNumber.Text; }
                if (txtBusLicType.Text != "") { newMerchant.BusLicType = txtBusLicType.Text; }
                if (txtBusLicIssuer.Text != "") { newMerchant.BusLicIssuer = txtBusLicIssuer.Text; }
                if (radBusLicDate.SelectedDate.HasValue) { newMerchant.BusLicDate = Convert.ToDateTime(radBusLicDate.SelectedDate); }
                if (txtFedTaxId.Text != "") { newMerchant.FedTaxId = txtFedTaxId.Text; }
                if (txtMerchandiseSold.Text != "") { newMerchant.MerchandiseSold = txtMerchandiseSold.Text; }
                if (txtYearsInBus.Text != "") { newMerchant.YearsInBusiness = Convert.ToInt32(txtYearsInBus.Text); }
                if (txtMonthsInBus.Text != "") { newMerchant.MonthsInBusiness = Convert.ToInt32(txtMonthsInBus.Text); }
                if (rblSeasonal.SelectedValue != "") { newMerchant.SeasonalSales = Convert.ToBoolean(rblSeasonal.SelectedValue); }
                if (seasonalMonths != "") { newMerchant.SeasonalMonths = seasonalMonths; }
                if (txtSwipedPct.Text != "") { newMerchant.SwipedPct = Convert.ToInt32(txtSwipedPct.Text); }
                if (txtAvgMonthlySales.Text != "") { newMerchant.AvgMonthlySales = Convert.ToDecimal(txtAvgMonthlySales.Text); }
                if (txtHighestMonthlySales.Text != "") { newMerchant.HighestMonthlySales = Convert.ToDecimal(txtHighestMonthlySales.Text); }
                if (txtAvgWeeklySales.Text != "") { newMerchant.AvgWeeklySales = Convert.ToDecimal(txtAvgWeeklySales.Text); }
                if (rblHighRisk.SelectedValue != "") { newMerchant.HighRisk = Convert.ToBoolean(rblHighRisk.SelectedValue); }
                if (txtHighRiskWho.Text != "") { newMerchant.HighRiskWho = txtHighRiskWho.Text; }
                if (radHighRiskDate.SelectedDate.HasValue) { newMerchant.HighRiskDate = Convert.ToDateTime(radHighRiskDate.SelectedDate); }
                if (rblBankruptcy.SelectedValue != "") { newMerchant.Bankruptcy = Convert.ToBoolean(rblBankruptcy.SelectedValue); }
                if (radBankruptcyDate.SelectedDate.HasValue) { newMerchant.BankruptcyDate = Convert.ToDateTime(radBankruptcyDate.SelectedDate); }

                //Add Legal Org State to merchant
                if (ddlLegalOrgState.SelectedValue != "")
                {
                    Int32 legalOrgStateId = Convert.ToInt32(ddlLegalOrgState.SelectedValue);
                    newMerchant.LegalOrgState = _globalCtx.GeoStates.Where(gs => gs.RecordId == legalOrgStateId).FirstOrDefault();
                }

                //Add Legal Org Type to merchant
                if (ddlLegalOrgType.SelectedValue != "")
                {
                    Int32 legalOrgTypeId = Convert.ToInt32(ddlLegalOrgType.SelectedValue);
                    newMerchant.LegalOrgType = _globalCtx.LegalOrgTypes.Where(lot => lot.RecordId == legalOrgTypeId).FirstOrDefault();
                }

                //Add Merchant Type to Merchant
                if (rblMerchantType.SelectedValue != "") { newMerchant.MerchantType = _globalCtx.MerchantTypes.Where(mt => mt.MerchantTypeName == rblMerchantType.SelectedValue).FirstOrDefault(); }

                //Add MCC to merchant
                if (ddlMCC.SelectedValue != "")
                {
                    Int32 mccId = Convert.ToInt32(ddlMCC.SelectedValue);
                    newMerchant.Mcc = _globalCtx.MerchantCategoryCodes.Where(mcc => mcc.RecordId == mccId).FirstOrDefault();
                }

                //Add Business Contact info - Email, Phone, Fax
                if (txtBusEmail.Text != "") { newBusiness.Email = txtBusEmail.Text; }
                if (txtBusFax.Text != "") { newBusiness.Fax = txtBusFax.Text; }
                if (txtBusPhone.Text != "") { newBusiness.HomePhone = txtBusPhone.Text; }

                _globalCtx.Contacts.Add(newBusiness);

                //Add Business Contact Addess
                if (txtCorpAddress.Text != "") { newBusinessAddress.Address = txtCorpAddress.Text; }
                if (txtCorpCity.Text != "") { newBusinessAddress.City = txtCorpCity.Text; }
                if (ddlCorpState.SelectedValue != "") 
                { 
                    Int32 businessAddressStateId = Convert.ToInt32(ddlCorpState.SelectedValue);
                    newBusinessAddress.State = _globalCtx.GeoStates.Where(gs => gs.RecordId == businessAddressStateId).FirstOrDefault();
                }
                if (txtCorpZip.Text != "") { newBusinessAddress.Zip = txtCorpZip.Text; }

                _globalCtx.Addresses.Add(newBusinessAddress);

                //Add new Business Contact Address to new Business
                newBusiness.Address = newBusinessAddress;

                //Add new Contact to new Merchant
                newMerchant.Business = newBusiness;

                //Add new Contact
                if (txtContactFirstName.Text != "") { newContact.FirstName = txtContactFirstName.Text; }
                if (txtContactLastName.Text != "") { newContact.LastName = txtContactLastName.Text; }
                if (txtContactEmail.Text != "") { newContact.Email = txtContactEmail.Text; }
                if (txtContactPhone.Text != "") { newContact.HomePhone = txtContactPhone.Text; }
                if (txtContactFax.Text != "") { newContact.Fax = txtContactFax.Text; }

                _globalCtx.Contacts.Add(newContact);

                //Add new contact to new Merchant
                newMerchant.Contact = newContact;

                //Add new Merchant Principal
                if (txtPrincipalDLNumber.Text != "") { newMerchantPrincipal.PrincipalDLNumber = PWDTK.StringToUtf8Bytes(txtPrincipalDLNumber.Text); }
                if (ddlPrincipalDLState.SelectedValue != "") 
                { 
                    Int32 dlStateId = Convert.ToInt32(ddlPrincipalDLState.SelectedValue);
                    newMerchantPrincipal.PrincipalDLState = _globalCtx.GeoStates.Where(gs => gs.RecordId == dlStateId).FirstOrDefault();
                }
                if (radPrincipalDoB.SelectedDate.HasValue) { newMerchantPrincipal.PrincipalDoB = Convert.ToDateTime(radPrincipalDoB.SelectedDate); }
                if (txtPrincipalPctOwn.Text != "") { newMerchantPrincipal.PrincipalPctOwn = Convert.ToInt32(txtPrincipalPctOwn.Text); }

                _globalCtx.MerchantPrincipal.Add(newMerchantPrincipal);

                //Create new contact for Merchant Principal
                if (txtPrincipalFirstName.Text != "") { newMerchantPrincipalContact.FirstName = txtPrincipalFirstName.Text; }
                if (txtPrincipalLastName.Text != "") { newMerchantPrincipalContact.LastName = txtPrincipalLastName.Text; }
                if (txtPrincipalMI.Text != "") { newMerchantPrincipalContact.MiddleInitial = txtPrincipalMI.Text; }
                if (txtPrincipalTitle.Text != "") { newMerchantPrincipalContact.Title = txtPrincipalTitle.Text; }
                if (txtPrincipalCellPhone.Text != "") { newMerchantPrincipalContact.CellPhone = txtPrincipalCellPhone.Text; }
                if (txtPrincipalHomePhone.Text != "") { newMerchantPrincipalContact.HomePhone = txtPrincipalHomePhone.Text; }

                _globalCtx.Contacts.Add(newMerchantPrincipalContact);

                //Create new address for Merchant principal Contact
                if (txtPrincipalAddress.Text != "") { newMerchantPrincipalContactAddress.Address = txtPrincipalAddress.Text; }
                if (txtPrincipalCity.Text != "") { newMerchantPrincipalContactAddress.City = txtPrincipalCity.Text; }
                if (ddlPrincipalState.SelectedValue != "") 
                {
                    Int32 mpcStateId = Convert.ToInt32(ddlPrincipalState.SelectedValue);
                    newMerchantPrincipalContactAddress.State = _globalCtx.GeoStates.Where(gs => gs.RecordId == mpcStateId).FirstOrDefault();
                }
                if (txtPrincipalZip.Text != "") { newMerchantPrincipalContactAddress.Zip = txtPrincipalZip.Text; }

                _globalCtx.Addresses.Add(newMerchantPrincipalContactAddress);

                //Add new address to Merchant Principal Contact
                newMerchantPrincipalContact.Address = newMerchantPrincipalContactAddress;

                //Add new Contact to Merchant Principal
                newMerchantPrincipal.Contact = newMerchantPrincipalContact;

                //Add new Principal to the new merchant
                newMerchant.MerchantPrincipal = newMerchantPrincipal;

                //Check if merchant processor already exists, if so link to merchant.  If not, create it and add to merchant.
                if (txtCardProcessor.Text != "")
                {
                    if (_globalCtx.Processor.Where(p => p.ProcessorName == txtCardProcessor.Text.Trim()).ToList().Count > 0)
                    {
                        newMerchant.Processor = _globalCtx.Processor.First(p => p.ProcessorName == txtCardProcessor.Text.Trim());
                    }
                    else
                    {
                        newProcessor.ProcessorName = txtCardProcessor.Text.Trim();
                        _globalCtx.Processor.Add(newProcessor);
                        newMerchant.Processor = newProcessor;
                    }
                }

                _globalCtx.Banks.Add(newBankModel);

                newBankAccountModel.Bank = newBankModel;
                newDebitCard.Bank = newBankModel;

                _globalCtx.BankAccounts.Add(newBankAccountModel);
                _globalCtx.DebitCards.Add(newDebitCard);

                newMerchant.BankAccount = newBankAccountModel;
                newMerchant.DebitCard = newDebitCard;

                //Set Merchant Status to "Admin Registered"
                newMerchant.MerchantStatus = _globalCtx.MerchantStatuses.FirstOrDefault(ms => ms.StatusDescription == "Pre-Enrolled");

                //Set Underwriting Status to "Pending"
                newMerchant.UnderwritingStatus = _globalCtx.UnderwritingStatuses.FirstOrDefault(ms => ms.StatusDescription == "Pending");

                newMerchant.AdvancePlan = _globalCtx.AdvancePlans.First(ap => ap.DefaultPlan == true);

                //Add new Merchant to context
                _globalCtx.Merchants.Add(newMerchant);

                //Add new merchant to selected User
                if (txtSelectedUserName.Text != "")
                {
                    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_globalCtx));
                    ApplicationUser selectedUser = manager.FindByName(txtSelectedUserName.Text);
                    if (selectedUser != null)
                    {
                        selectedUser.Merchant = newMerchant;

                        //Save Context and Update DB
                        _globalCtx.SaveChanges();
                    }
                }
                else
                {
                    lblSubmissionMessage.Text = "Please select a User to join with this merchant.";
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "AdminSubmitForm - Add Data to DB");
                return false;
            }

            return true;
        }

        public string CreatePassword()
        {
            const string nums = "0123456789";
            const string chars = "!@#$%^&*_";
            const string lowers = "abcdefghijklmnopqrstuvwxyz";
            const string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            res.Append(nums[rnd.Next(nums.Length)]);
            res.Append(uppers[rnd.Next(uppers.Length)]);
            res.Append(lowers[rnd.Next(lowers.Length)]);
            res.Append(chars[rnd.Next(chars.Length)]);
            res.Append(valid[rnd.Next(valid.Length)]);
            res.Append(valid[rnd.Next(valid.Length)]);
            res.Append(valid[rnd.Next(valid.Length)]);
            res.Append(valid[rnd.Next(valid.Length)]);
            res.Append(valid[rnd.Next(valid.Length)]);
            res.Append(valid[rnd.Next(valid.Length)]);

            return res.ToString();
        }

        public void NewUserAdded()
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = manager.FindByName(txtUsername.Text);

            pnlRegisterUser.Visible = false;
            pnlExistingUser.Visible = true;
            ddlExistingUser.Items.Clear();
            ddlExistingUser.DataBind();

            if (user != null)
            {
                ddlExistingUser.SelectedText = user.UserName;
                txtSelectedFirstName.Text = user.FirstName;
                txtSelectedLastName.Text = user.LastName;
                txtSelectedUserName.Text = user.UserName;
                txtSelectedEmail.Text = user.Email;
            }
        }

        public void UpdateMerchant(int RecordId)
        {

        }

        public void SelectExistingUser(string userId)
        {

        }



        protected void ddlExistingUser_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            try
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = manager.FindByName(ddlExistingUser.SelectedText);

                if (user != null)
                {
                    txtSelectedFirstName.Text = user.FirstName;
                    txtSelectedLastName.Text = user.LastName;
                    txtSelectedUserName.Text = user.UserName;
                    txtSelectedEmail.Text = user.Email;
                }

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "ddlExistingUser_SelectedIndexChanged");
            }
        }

        protected void rblSeasonal_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblSeasonal.SelectedValue == "True")
                {
                    pnlSeasonalMonths.Visible = true;
                }
                else
                {
                    pnlSeasonalMonths.Visible = false;
                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "rblSeasonal_SelectedIndexChanged");
            }
        }

        protected void rblHighRisk_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblHighRisk.SelectedValue == "Yes")
                {
                    pnlHighRisk.Visible = true;
                }
                else
                {
                    txtHighRiskWho.Text = "";
                    radHighRiskDate.Clear();
                    pnlHighRisk.Visible = false;
                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "rblHighRisk_SelectedIndexChanged");
            }
        }

        protected void rblBankruptcy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblBankruptcy.SelectedValue == "Yes")
                {
                    pnlBankruptcy.Visible = true;
                }
                else
                {

                    radBankruptcyDate.Clear();
                    pnlBankruptcy.Visible = false;
                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "rblBankruptcy_SelectedIndexChanged");
            }
        }



        public IQueryable<MerchantModel> GetMerchants()
        {
            try
            {
                if (_globalCtx.Merchants.Any(x => x.LastPhaseCompleted == 3))
                {
                    IQueryable<MerchantModel> merchantList = _globalCtx.Merchants.Where(x => x.LastPhaseCompleted == 3).OrderByDescending(m => m.Timestamp);

                    return merchantList;
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "GetMerchants");
                return null;
            }
        }

        public IQueryable<LegalOrgTypeModel> BindLegalOrgTypes()
        {
            try
            {
                IQueryable<LegalOrgTypeModel> legalOrgTypes = _globalCtx.LegalOrgTypes;

                return legalOrgTypes;
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "BindLegalOrgTypes");

                return null;
            }
        }

        public IQueryable<GeoStateModel> BindLegalOrgStates()
        {
            try
            {
                IQueryable<GeoStateModel> legalOrgStates = _globalCtx.GeoStates;

                return legalOrgStates;
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "BindLegalOrgStates");

                return null;
            }
        }

        public IQueryable<MccModel> BindMCCs()
        {
            try
            {
                IQueryable<MccModel> merchantCategoryCodes = _globalCtx.MerchantCategoryCodes;

                return merchantCategoryCodes;
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "BindMCCs");

                return null;
            }
        }

        public IQueryable<ApplicationUser> BindExistingUsers()
        {
            try
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_globalCtx));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_globalCtx));

                string roleId = roleManager.Roles.First(r => r.Name == "Merchant").Id;

                IQueryable<ApplicationUser> userList = _globalCtx.Users.Where(u => u.Roles.Any(r => r.RoleId == roleId) && u.Merchant == null && u.Status.StatusDescription == "Active")
                                                                .OrderBy(u => u.UserName);

                return userList;
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "BindExistingUsers");
                return null;
            }
        }

        protected void gridMerchantList_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EditMerchantRecordId"] != null)
                {
                    String editId = Session["EditMerchantRecordId"].ToString();

                    foreach (GridItem item in gridMerchantList.MasterTableView.Items)
                    {
                        if (item is GridEditableItem)
                        {
                            GridEditableItem editableItem = item as GridDataItem;
                            Label lblRecordId = (Label)editableItem.FindControl("lblRecordId");

                            if (lblRecordId != null && lblRecordId.Text == editId)
                            {
                                editableItem.Edit = true;
                            }
                        }
                    }

                    gridMerchantList.Rebind();
                    Session.Clear();
                }
            }
        }
    }
}