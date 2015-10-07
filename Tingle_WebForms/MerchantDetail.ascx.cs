using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Telerik.Web.UI;


namespace Tingle_WebForms
{
    public partial class MerchantDetail : System.Web.UI.UserControl
    {
        Logic.Logic _newLogic = new Logic.Logic();
        ApplicationDbContext _globalCtx = new ApplicationDbContext();
        NameValueCollection _miqroSettings = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection("miqroSettings");

        bool isExport = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (lblStatusDescription.Text.ToLower() == "denied")
            {
                btnConfirmChanges.Enabled = false;
            }
        }

        protected void FillDateDDLs()
        {
            //Clear all Date DropDowns
            ddlLicenseDateYear.Items.Clear();
            ddlLicenseDateMonth.Items.Clear();
            ddlLicenseDateDay.Items.Clear();

            ddlHighRiskYear.Items.Clear();
            ddlHighRiskMonth.Items.Clear();
            ddlHighRiskDay.Items.Clear();

            ddlBankruptcyYear.Items.Clear();
            ddlBankruptcyMonth.Items.Clear();
            ddlBankruptcyDay.Items.Clear();

            ddlBirthYear.Items.Clear();
            ddlBirthMonth.Items.Clear();
            ddlBirthDay.Items.Clear();


            //Load Years
            for (int i = DateTime.Now.Year; i >= 1900; i--)
            {
                ddlLicenseDateYear.Items.Add(new Telerik.Web.UI.DropDownListItem(i.ToString(), i.ToString()));
                ddlHighRiskYear.Items.Add(new Telerik.Web.UI.DropDownListItem(i.ToString(), i.ToString()));
                ddlBankruptcyYear.Items.Add(new Telerik.Web.UI.DropDownListItem(i.ToString(), i.ToString()));
                ddlBirthYear.Items.Add(new Telerik.Web.UI.DropDownListItem(i.ToString(), i.ToString()));
            }

            ddlLicenseDateYear.DataBind();
            ddlHighRiskYear.DataBind();
            ddlBankruptcyYear.DataBind();
            ddlBirthYear.DataBind();

            //Load Months
            for (int i = 1; i <= 12; i++)
            {
                string monthText = "";

                switch (i.ToString())
                {
                    case "1":
                        monthText = "Jan";
                        break;
                    case "2":
                        monthText = "Feb";
                        break;
                    case "3":
                        monthText = "Mar";
                        break;
                    case "4":
                        monthText = "Apr";
                        break;
                    case "5":
                        monthText = "May";
                        break;
                    case "6":
                        monthText = "Jun";
                        break;
                    case "7":
                        monthText = "Jul";
                        break;
                    case "8":
                        monthText = "Aug";
                        break;
                    case "9":
                        monthText = "Sep";
                        break;
                    case "10":
                        monthText = "Oct";
                        break;
                    case "11":
                        monthText = "Nov";
                        break;
                    case "12":
                        monthText = "Dec";
                        break;
                }

                ddlLicenseDateMonth.Items.Add(new Telerik.Web.UI.DropDownListItem(monthText, i.ToString()));
                ddlHighRiskMonth.Items.Add(new Telerik.Web.UI.DropDownListItem(monthText, i.ToString()));
                ddlBankruptcyMonth.Items.Add(new Telerik.Web.UI.DropDownListItem(monthText, i.ToString()));
                ddlBirthMonth.Items.Add(new Telerik.Web.UI.DropDownListItem(monthText, i.ToString()));
            }

            ddlLicenseDateMonth.DataBind();
            ddlHighRiskMonth.DataBind();
            ddlBankruptcyMonth.DataBind();
            ddlBirthMonth.DataBind();

            //Load Days
            for (int i = 1; i <= 31; i++)
            {
                ddlLicenseDateDay.Items.Add(new Telerik.Web.UI.DropDownListItem(i.ToString(), i.ToString()));
                ddlHighRiskDay.Items.Add(new Telerik.Web.UI.DropDownListItem(i.ToString(), i.ToString()));
                ddlBankruptcyDay.Items.Add(new Telerik.Web.UI.DropDownListItem(i.ToString(), i.ToString()));
                ddlBirthDay.Items.Add(new Telerik.Web.UI.DropDownListItem(i.ToString(), i.ToString()));
            }

            ddlLicenseDateDay.DataBind();
            ddlHighRiskDay.DataBind();
            ddlBankruptcyDay.DataBind();
            ddlBirthDay.DataBind();
        }

        public class MerchantChanges
        {
            private string fieldName;
            private string oldValue;
            private string newValue;

            public MerchantChanges(string fieldName, string oldValue, string newValue)
            {
                this.fieldName = fieldName;
                this.oldValue = oldValue;
                this.newValue = newValue;
            }

            public string FieldName
            {
                get
                {
                    return fieldName;
                }
            }

            public string OldValue
            {
                get
                {
                    return oldValue;
                }
            }

            public string NewValue
            {
                get
                {
                    return newValue;
                }
            }

        }

        public class DateYear
        {
            public string YearText { get; set; }
            public string YearValue { get; set; }
        }

        public class DateMonth
        {
            public string MonthText { get; set; }
            public string MonthValue { get; set; }
        }

        public class DateDay
        {
            public string DayText { get; set; }
            public string DayValue { get; set; }
        }

        private object _dataItem;

        public object DataItem
        {
            get
            {
                return this._dataItem;
            }
            set
            {
                this._dataItem = value;
            }
        }



        protected string MaskSsn(byte[] ssnByte, Int32 principalId)
        {
            try
            {
                if (ssnByte != null)
                {
                    Logic.Crypt crypto2 = new Logic.Crypt();
                    crypto2.CryptType = "SSN";
                    crypto2.CryptData = PWDTK.Utf8BytesToString(ssnByte);
                    crypto2.Purpose = "86d4b5b2-b34a-4968-9fde-f8ed677bca8b";
                    crypto2.AdditionalData = principalId.ToString();

                    byte[] ssnBytes = crypto2.Unprotect();

                    string ssnString = PWDTK.Utf8BytesToString(ssnBytes);

                    return "***-**-" + ssnString.Substring(ssnString.Length - 4, 4);
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "MaskSsn");

                return null;
            }

        }

        protected string ShowSsn(byte[] ssnByte, Int32 principalId)
        {
            try
            {
                if (ssnByte != null)
                {
                    Logic.Crypt crypto2 = new Logic.Crypt();
                    crypto2.CryptType = "SSN";
                    crypto2.CryptData = PWDTK.Utf8BytesToString(ssnByte);
                    crypto2.Purpose = "86d4b5b2-b34a-4968-9fde-f8ed677bca8b";
                    crypto2.AdditionalData = principalId.ToString();

                    byte[] ssnBytes = crypto2.Unprotect();

                    return PWDTK.Utf8BytesToString(ssnBytes);
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "ShowSsn");

                return null;
            }

        }

        protected string MaskDLNumber(byte[] dlByte, Int32 principalId)
        {
            string dlNumberOutput;

            try
            {
                if (dlByte != null)
                {
                    Logic.Crypt crypto1 = new Logic.Crypt();
                    crypto1.CryptType = "DL";
                    crypto1.CryptData = PWDTK.Utf8BytesToString(dlByte);
                    crypto1.Purpose = "29dc7202-ae1f-4d38-9694-9d700a94897b";
                    crypto1.AdditionalData = principalId.ToString();

                    byte[] dlBytes = crypto1.Unprotect();

                    string dlNumber = PWDTK.Utf8BytesToString(dlBytes);

                    if (!String.IsNullOrEmpty(dlNumber))
                    {
                        if (dlNumber.Length == 1)
                        {
                            dlNumberOutput = "*";
                        }
                        else if (dlNumber.Length == 2)
                        {
                            dlNumberOutput = "**";
                        }
                        else if (dlNumber.Length <= 6)
                        {
                            dlNumberOutput = dlNumber.Substring(0, 1);

                            for (int i = 0; i <= dlNumber.Length - 2; i++)
                            {
                                dlNumberOutput += "*";
                            }

                            dlNumberOutput += dlNumber.Substring(dlNumber.Length - 1, 1);
                        }
                        else
                        {
                            dlNumberOutput = dlNumber.Substring(0, 1);

                            for (int i = 0; i <= dlNumber.Length - 5; i++)
                            {
                                dlNumberOutput += "*";
                            }

                            dlNumberOutput += dlNumber.Substring(dlNumber.Length - 4, 4);
                        }

                        return dlNumberOutput;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "MaskDLNumber");

                return null;
            }

        }

        protected string ShowDLNumber(byte[] dlByte, Int32 principalId)
        {
            string dlNumberOutput;

            try
            {
                if (dlByte != null)
                {
                    Logic.Crypt crypto1 = new Logic.Crypt();
                    crypto1.CryptType = "DL";
                    crypto1.CryptData = PWDTK.Utf8BytesToString(dlByte);
                    crypto1.Purpose = "29dc7202-ae1f-4d38-9694-9d700a94897b";
                    crypto1.AdditionalData = principalId.ToString();

                    byte[] dlBytes = crypto1.Unprotect();

                    return PWDTK.Utf8BytesToString(dlBytes);
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "ShowDLNumber");

                return null;
            }

        }

        protected string MaskDebitCard(byte[] cardByte, Int32 cardId)
        {
            string cardNumberOutput = "";

            try
            {
                if (cardByte != null)
                {
                    Logic.Crypt crypto1 = new Logic.Crypt();
                    crypto1.CryptType = "Card";
                    crypto1.Purpose = "9c443c6a-87d5-47cc-9c4d-abf6a2bd2e06";
                    crypto1.CryptData = PWDTK.Utf8BytesToString(cardByte);
                    crypto1.AdditionalData = cardId.ToString();

                    byte[] cardNumberBytes = crypto1.Unprotect();

                    string cardNumber = PWDTK.Utf8BytesToString(cardNumberBytes);

                    cardNumberOutput += cardNumber.Substring(0, 6);

                    for (int i = 0; i <= cardNumber.Length - 10; i++)
                    {
                        cardNumberOutput += "*";
                    }

                    cardNumberOutput += cardNumber.Substring(cardNumber.Length - 10, 4);

                    return cardNumberOutput;
                }
                else
                {
                    return null;
                }

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "MaskDebitCard");

                return null;
            }
        }

        protected string ShowDebitCard(byte[] cardByte, Int32 cardId)
        {
            try
            {
                if (cardByte != null)
                {
                    Logic.Crypt crypto1 = new Logic.Crypt();
                    crypto1.CryptType = "Card";
                    crypto1.Purpose = "9c443c6a-87d5-47cc-9c4d-abf6a2bd2e06";
                    crypto1.CryptData = PWDTK.Utf8BytesToString(cardByte);
                    crypto1.AdditionalData = cardId.ToString();

                    byte[] cardNumberBytes = crypto1.Unprotect();

                    return PWDTK.Utf8BytesToString(cardNumberBytes);

                }
                else
                {
                    return null;
                }

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "ShowDebitCard");

                return null;
            }
        }

        protected string MaskAccountNumber(byte[] accountByte, Int32 accountId)
        {
            string accountNumberOutput = "";

            try
            {
                if (accountByte != null)
                {
                    Logic.Crypt crypto1 = new Logic.Crypt();
                    crypto1.CryptType = "Account";
                    crypto1.Purpose = "4742b06b-9e12-48cf-9034-aadb15dc7a58";
                    crypto1.CryptData = PWDTK.Utf8BytesToString(accountByte);
                    crypto1.AdditionalData = accountId.ToString();

                    byte[] accountNumberBytes = crypto1.Unprotect();

                    string accountNumber = PWDTK.Utf8BytesToString(accountNumberBytes);

                    for (int i = 0; i <= accountNumber.Length - 4; i++)
                    {
                        accountNumberOutput += "*";
                    }

                    accountNumberOutput += accountNumber.Substring(accountNumber.Length - 4, 4);

                    return accountNumberOutput;
                }
                else
                {
                    return null;
                }

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "MaskAccountNumber");

                return null;
            }

            return accountNumberOutput;
        }

        protected string ShowAccountNumber(byte[] accountByte, Int32 accountId)
        {
            try
            {
                if (accountByte != null)
                {
                    Logic.Crypt crypto1 = new Logic.Crypt();
                    crypto1.CryptType = "Account";
                    crypto1.Purpose = "4742b06b-9e12-48cf-9034-aadb15dc7a58";
                    crypto1.CryptData = PWDTK.Utf8BytesToString(accountByte);
                    crypto1.AdditionalData = accountId.ToString();

                    byte[] accountNumberBytes = crypto1.Unprotect();

                    return PWDTK.Utf8BytesToString(accountNumberBytes);
                }
                else
                {
                    return null;
                }

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "MaskAccountNumber");

                return null;
            }
        }

        protected void lblStatusDescription_PreRender(object sender, EventArgs e)
        {
            try
            {
                UpdateStatusLabelBg();
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "lblStatusDescription_PreRender");
            }
        }

        public void UpdateStatusLabelBg()
        {
            string statusDesc = lblStatusDescription.Text.ToLower();

            switch (statusDesc)
            {
                case "pre-enrolled":
                    tblEditAppHeader.Rows[1].Cells[2].BgColor = "BurlyWood";
                    break;
                case "enrolled":
                    tblEditAppHeader.Rows[1].Cells[2].BgColor = "Gray";
                    break;
                case "active":
                    tblEditAppHeader.Rows[1].Cells[2].BgColor = "Green";
                    break;
                case "suspended":
                    tblEditAppHeader.Rows[1].Cells[2].BgColor = "#F00000";
                    break;
                case "cancelled":
                    tblEditAppHeader.Rows[1].Cells[2].BgColor = "Black";
                    break;
                case "denied":
                    tblEditAppHeader.Rows[1].Cells[2].BgColor = "#680000";
                    break;
            }
        }

        public void UpdateStatusLabel()
        {
            try
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    int accountNumber = Convert.ToInt32(hMerchantRecordId.Value);

                    MerchantModel editMerchant = ctx.Merchants.First(m => m.RecordId == accountNumber);

                    lblStatusDescription.Text = editMerchant.MerchantStatus.StatusDescription;
                }

                UpdateStatusLabelBg();
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "UpdateStatusLabel");
            }

        }



        protected void btnExitCancel_Click(object sender, EventArgs e)
        {
            pnlEditMerchant.Visible = true;
            pnlConfirmSave.Visible = false;
        }

        protected void btnCancelConfirm_Click(object sender, EventArgs e)
        {
            pnlAdvancedButtons.Visible = false;
            pnlBasicButtons.Visible = true;
            pnlEditMerchant.Visible = true;
            pnlConfirmSave.Visible = false;
        }

        protected void btnConfirmChanges_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ConfirmChanges();
            }
        }

        protected void btnApplyChanges_Click(object sender, EventArgs e)
        {
            SaveChanges();
            pnlAdvancedButtons.Visible = false;
            pnlBasicButtons.Visible = true;
            pnlEditMerchant.Visible = true;
            pnlConfirmSave.Visible = false;
            UpdateStatusLabel();

        }


        protected void cbSelectAllBusinessChanges_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Control FooterTemplate = rptrBusinessChanges.Controls[rptrBusinessChanges.Controls.Count - 1].Controls[0];
                CheckBox cbSelectAll = (CheckBox)FooterTemplate.FindControl("cbSelectAllBusinessChanges");

                if (cbSelectAll != null)
                {
                    foreach (RepeaterItem rItem in rptrBusinessChanges.Items)
                    {
                        CheckBox cbConfirmed = (CheckBox)rItem.FindControl("cbConfirmed0");

                        if (cbSelectAll.Checked)
                        {
                            cbConfirmed.Checked = true;
                        }
                        else
                        {
                            cbConfirmed.Checked = false;
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "cbSelectAllBusinessChanges_CheckedChanged");
            }
        }

        protected void cbSelectAllPrincipalChanges_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Control FooterTemplate = rptrPrincipalChanges.Controls[rptrPrincipalChanges.Controls.Count - 1].Controls[0];
                CheckBox cbSelectAll = (CheckBox)FooterTemplate.FindControl("cbSelectAllPrincipalChanges");

                if (cbSelectAll != null)
                {
                    foreach (RepeaterItem rItem in rptrPrincipalChanges.Items)
                    {
                        CheckBox cbConfirmed = (CheckBox)rItem.FindControl("cbConfirmed1");

                        if (cbSelectAll.Checked)
                        {
                            cbConfirmed.Checked = true;
                        }
                        else
                        {
                            cbConfirmed.Checked = false;
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "cbSelectAllPrincipalChanges_CheckedChanged");
            }
        }

        protected void cbSelectAllContactChanges_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Control FooterTemplate = rptrContactChanges.Controls[rptrContactChanges.Controls.Count - 1].Controls[0];
                CheckBox cbSelectAll = (CheckBox)FooterTemplate.FindControl("cbSelectAllContactChanges");

                if (cbSelectAll != null)
                {
                    foreach (RepeaterItem rItem in rptrContactChanges.Items)
                    {
                        CheckBox cbConfirmed = (CheckBox)rItem.FindControl("cbConfirmed2");

                        if (cbSelectAll.Checked)
                        {
                            cbConfirmed.Checked = true;
                        }
                        else
                        {
                            cbConfirmed.Checked = false;
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "cbSelectAllContactChanges_CheckedChanged");
            }
        }

        protected void cbSelectAllBankingChanges_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Control FooterTemplate = rptrBankingChanges.Controls[rptrBankingChanges.Controls.Count - 1].Controls[0];
                CheckBox cbSelectAll = (CheckBox)FooterTemplate.FindControl("cbSelectAllBankingChanges");

                if (cbSelectAll != null)
                {
                    foreach (RepeaterItem rItem in rptrBankingChanges.Items)
                    {
                        CheckBox cbConfirmed = (CheckBox)rItem.FindControl("cbConfirmed3");

                        if (cbSelectAll.Checked)
                        {
                            cbConfirmed.Checked = true;
                        }
                        else
                        {
                            cbConfirmed.Checked = false;
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "cbSelectAllBankingChanges_CheckedChanged");
            }
        }

        protected void cbSelectAllMerchantAccountChanges_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Control FooterTemplate = rptrMerchantAccountChanges.Controls[rptrMerchantAccountChanges.Controls.Count - 1].Controls[0];
                CheckBox cbSelectAll = (CheckBox)FooterTemplate.FindControl("cbSelectAllMerchantAccountChanges");

                if (cbSelectAll != null)
                {
                    foreach (RepeaterItem rItem in rptrMerchantAccountChanges.Items)
                    {
                        CheckBox cbConfirmed = (CheckBox)rItem.FindControl("cbConfirmed4");

                        if (cbSelectAll.Checked)
                        {
                            cbConfirmed.Checked = true;
                        }
                        else
                        {
                            cbConfirmed.Checked = false;
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "cbSelectAllMerchantAccountChanges_CheckedChanged");
            }
        }

        protected void cbSelectAllAdvancePlanChanges_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Control FooterTemplate = rptrAdvancePlanChanges.Controls[rptrAdvancePlanChanges.Controls.Count - 1].Controls[0];
                CheckBox cbSelectAll = (CheckBox)FooterTemplate.FindControl("cbSelectAllAdvancePlanChanges");

                if (cbSelectAll != null)
                {
                    foreach (RepeaterItem rItem in rptrAdvancePlanChanges.Items)
                    {
                        CheckBox cbConfirmed = (CheckBox)rItem.FindControl("cbConfirmed4");

                        if (cbSelectAll.Checked)
                        {
                            cbConfirmed.Checked = true;
                        }
                        else
                        {
                            cbConfirmed.Checked = false;
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "cbSelectAllAdvancePlanChanges_CheckedChanged");
            }
        }



        protected void lbEditSSN_Click(object sender, EventArgs e)
        {
            txtPrincipalSSN.Text = "";
            txtPrincipalSSN.Enabled = true;
            txtPrincipalSSN.Mask = "###-##-####";
            txtPrincipalSSN.RequireCompleteText = true;
            txtPrincipalSSN.Focus();
        }

        protected void lbEditDL_Click(object sender, EventArgs e)
        {
            txtPrincipalDLNumber.Text = "";
            txtPrincipalDLNumber.Enabled = true;
            txtPrincipalDLNumber.Focus();
        }

        protected void lbEditDebitCard_Click(object sender, EventArgs e)
        {
            txtDebitCardNumber.Text = "";
            txtDebitCardNumber.Enabled = true;
            txtDebitCardNumber.Focus();
        }

        protected void lbEditBankAccount_Click(object sender, EventArgs e)
        {
            txtAccountNumber.Text = "";
            txtAccountNumber.Enabled = true;
            txtAccountNumber.Focus();
        }

        protected void lbEditExpDate_Click(object sender, EventArgs e)
        {
            ddlExpMonth.Enabled = true;
            ddlExpYear.Enabled = true;
            ddlExpMonth.Focus();
        }

        protected void lbEditRoutingNumber_Click(object sender, EventArgs e)
        {
            txtRoutingNumber.Enabled = true;
            txtRoutingNumber.Focus();
        }

        protected void rblSeasonal_DataBinding(object sender, EventArgs e)
        {
            if (DataBinder.Eval(DataItem, "SeasonalSales") != null)
            {
                rblSeasonal.SelectedValue = DataBinder.Eval(DataItem, "SeasonalSales").ToString();
            }
        }

        protected void cblSeasonal_DataBound(object sender, EventArgs e)
        {
            try
            {
                string seasonalMonths = DataBinder.Eval(DataItem, "SeasonalMonths") != null ? DataBinder.Eval(DataItem, "SeasonalMonths").ToString() : "";
                string seasonalSales = DataBinder.Eval(DataItem, "SeasonalSales") != null ? DataBinder.Eval(DataItem, "SeasonalSales").ToString() : null;

                if (!String.IsNullOrEmpty(seasonalSales))
                {
                    if (seasonalSales != null)
                    {
                        if (seasonalSales == "True")
                        {
                            if (!String.IsNullOrEmpty(seasonalMonths))
                            {
                                foreach (string month in seasonalMonths.Split('-'))
                                {
                                    ListItem li = cblSeasonal.Items.FindByValue(month.Replace(" ", ""));

                                    if (li != null)
                                    {
                                        li.Selected = true;
                                    }

                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "cblSeasonal_DataBound");
            }


        }

        protected void rblMerchantType_DataBinding(object sender, EventArgs e)
        {
            if (DataBinder.Eval(DataItem, "MerchantType.MerchantTypeName") != null)
            {
                rblMerchantType.SelectedValue = DataBinder.Eval(DataItem, "MerchantType.MerchantTypeName").ToString();
            }
        }






        protected void rblUWCorpInfoVerifiedResult_DataBound(object sender, EventArgs e)
        {
            try
            {
                string result = GetUnderwritingResults("CorpInfoResult");
                if (result != null)
                {
                    //rblUWCorpInfoVerifiedResult.SelectedValue = result;
                }

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "rblUWCorpInfoVerifiedResult_DataBound");
            }
        }

        protected void txtUWCorpInfoVerifiedNotes_PreRender(object sender, EventArgs e)
        {
            try
            {
                string result = GetUnderwritingResults("CorpInfoNotes");
                if (result != null)
                {
                    //txtUWCorpInfoVerifiedNotes.Text = result;
                }

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "txtUWCorpInfoVerifiedNotes_PreRender");
            }
        }

        protected void rblUWBusLicStatusResult_DataBound(object sender, EventArgs e)
        {
            try
            {
                string result = GetUnderwritingResults("BusLicStatusResult");
                if (result != null)
                {
                    //rblUWBusLicStatusResult.SelectedValue = result;
                }

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "rblUWBusLicStatusResult_DataBound");
            }
        }

        protected void txtUWBUsLicStatusVerifiedNotes_PreRender(object sender, EventArgs e)
        {
            try
            {
                string result = GetUnderwritingResults("BusLicStatusNotes");
                if (result != null)
                {
                    //txtUWBUsLicStatusVerifiedNotes.Text = result;
                }

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "rblUWBusLicStatusResult_DataBound");
            }
        }

        protected void tabStripMerchantDetail_TabClick(object sender, RadTabStripEventArgs e)
        {
            MerchantModel editMerchant = new MerchantModel();
            Int32 merchantRecordId;
            UnderwritingResultModel underwritingResult = new UnderwritingResultModel();

            try
            {
                merchantRecordId = Convert.ToInt32(hMerchantRecordId.Value);

                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    editMerchant = ctx.Merchants.First(m => m.RecordId == merchantRecordId);

                    if (ctx.UnderwritingResults.Any(ur => ur.Merchant.RecordId == merchantRecordId && ur.Active == true))
                    {
                        underwritingResult = ctx.UnderwritingResults.First(ur => ur.Merchant.RecordId == merchantRecordId && ur.Active == true);

                        if (underwritingResult.UnderwritingDecision == "Pending" || String.IsNullOrEmpty(underwritingResult.UnderwritingDecision))
                        {
                            if (underwritingResult.UnderwriterUser.Id == Page.User.Identity.GetUserId())
                            {
                                pnlUnderwritingHistory.Visible = false;
                                pnlNewUnderwriting.Visible = false;
                                pnlNewUnderwriting2.Visible = true;
                                pnlUnderwritingButtons.Visible = false;
                                pnlUnderwritingAdvancedButtons.Visible = true;
                                btnUnderwritingCancel.Visible = true;
                                btnUnderwritingComplete.Visible = true;
                                btnUnderwritingSave.Visible = true;
                                btnAddNewUnderwriting.Visible = false;

                                //Enable and Fill Radio Button Fields
                                rblUWCorpInfoVerifiedResult.Enabled = true;
                                rblUWBusLicStatusResult.Enabled = true;
                                rblUWEINVerifiedResult.Enabled = true;
                                rblUWPrincipalVerifiedResult.Enabled = true;
                                rblUWCardSalesIndicatorsVerifiedResult.Enabled = true;
                                rblUWBankingInfoVerifiedResult.Enabled = true;
                                rblUWMCCVerifiedResult.Enabled = true;
                                rblUWBVIVerifiedResult.Enabled = true;
                                rblUWTaxLiensVerifiedResult.Enabled = true;
                                rblUWRiskIndicatorVerifiedResult.Enabled = true;

                                rblUWCorpInfoVerifiedResult.SelectedValue = underwritingResult.CorpInfoResult.ToString();
                                rblUWBusLicStatusResult.SelectedValue = underwritingResult.BusLicStatusResult.ToString();
                                rblUWEINVerifiedResult.SelectedValue = underwritingResult.EINResult.ToString();
                                rblUWPrincipalVerifiedResult.SelectedValue = underwritingResult.PrincipalResult.ToString();
                                rblUWCardSalesIndicatorsVerifiedResult.SelectedValue = underwritingResult.CardSalesIndicatorResult.ToString();
                                rblUWBankingInfoVerifiedResult.SelectedValue = underwritingResult.BankingInfoResult.ToString();
                                rblUWMCCVerifiedResult.SelectedValue = underwritingResult.MCCResult.ToString();
                                rblUWBVIVerifiedResult.SelectedValue = underwritingResult.BVIResult.ToString();
                                rblUWTaxLiensVerifiedResult.SelectedValue = underwritingResult.TaxLiensResult.ToString();
                                rblUWRiskIndicatorVerifiedResult.SelectedValue = underwritingResult.RiskIndicatorResult.ToString();

                                //Enable and Fill Text Area Fields
                                txtUWCorpInfoVerifiedNotes.Enabled = true;
                                txtUWBUsLicStatusVerifiedNotes.Enabled = true;
                                txtUWEINVerifiedNotes.Enabled = true;
                                txtUWPrincipalVerifiedNotes.Enabled = true;
                                txtUWCardSalesIndicatorsVerifiedNotes.Enabled = true;
                                txtUWBankingInfoVerifiedNotes.Enabled = true;
                                txtUWMCCVerifiedNotes.Enabled = true;
                                txtUWBVIVerifiedNotes.Enabled = true;
                                txtUWTaxLiensVerifiedNotes.Enabled = true;
                                txtUWRiskIndicatorVerifiedNotes.Enabled = true;

                                txtUWCorpInfoVerifiedNotes.Text = underwritingResult.CorpInfoNotes;
                                txtUWBUsLicStatusVerifiedNotes.Text = underwritingResult.BusLicStatusNotes;
                                txtUWEINVerifiedNotes.Text = underwritingResult.EINNotes;
                                txtUWPrincipalVerifiedNotes.Text = underwritingResult.PrincipalNotes;
                                txtUWCardSalesIndicatorsVerifiedNotes.Text = underwritingResult.CardSalesIndicatorNotes;
                                txtUWBankingInfoVerifiedNotes.Text = underwritingResult.BankingInfoNotes;
                                txtUWMCCVerifiedNotes.Text = underwritingResult.MCCNotes;
                                txtUWBVIVerifiedNotes.Text = underwritingResult.BVINotes;
                                txtUWTaxLiensVerifiedNotes.Text = underwritingResult.TaxLiensNotes;
                                txtUWRiskIndicatorVerifiedNotes.Text = underwritingResult.RiskIndicatorNotes;

                                //Disable Begin Button
                                btnBeginUnderwritingUpdate.Enabled = false;

                                //Fill Underwriter Initials
                                txtUnderwriterInitials.Text = underwritingResult.UnderwriterInitials;
                                txtUnderwriterInitials.Enabled = false;

                                lblUnderwritingMessage2.Text = "The current Underwriting Process is still in progress.  Please complete or cancel this process to view Underwriting History.";
                            }
                            else
                            {
                                btnAddNewUnderwriting.Enabled = false;
                            }
                        }
                    }


                    if (e.Tab.Text == "Underwriting")
                    {
                        pnlBasicButtons.Visible = false;
                        pnlMessagingButtons.Visible = false;
                        pnlUnderwritingButtons.Visible = true;
                        pnlUserButtons.Visible = false;
                    }
                    else if (e.Tab.Text == "Email Messages")
                    {
                        pnlBasicButtons.Visible = false;
                        pnlMessagingButtons.Visible = false;
                        pnlUnderwritingButtons.Visible = false;
                        pnlUserButtons.Visible = false;
                    }
                    else if (e.Tab.Text == "Profile")
                    {
                        pnlMessagingButtons.Visible = false;
                        pnlUnderwritingButtons.Visible = false;

                        RadTabStrip tabStripMerchantProfile = (RadTabStrip)ProfileView.FindControl("tabStripMerchantProfile");

                        if (tabStripMerchantProfile.SelectedTab.Text == "Status")
                        {
                            pnlBasicButtons.Visible = false;
                            pnlUserButtons.Visible = false;

                            if (editMerchant.UnderwritingStatus.StatusDescription == "Pending")
                            {
                                phApproveMerchant.Visible = false;
                                phApproveDisallowed.Visible = true;
                                lblApprovalDisallowed.Text = "The Underwriting Status for this merchant is still Pending.  In order to Approve this merchant, the Underwriting Status must be Approved.";
                            }
                            else if (editMerchant.UnderwritingStatus.StatusDescription == "Approved")
                            {
                                phApproveMerchant.Visible = true;
                                phApproveDisallowed.Visible = false;
                                lblApprovalDisallowed.Text = "";
                            }
                            else if (editMerchant.UnderwritingStatus.StatusDescription == "Denied")
                            {
                                phApproveMerchant.Visible = false;
                                phApproveDisallowed.Visible = true;
                                lblApprovalDisallowed.Text = "The Underwriting Status for this merchant is Denied.  In order to Approve this merchant, the Underwriting Status must be Approved.";
                            }
                            else
                            {
                                phApproveMerchant.Visible = false;
                                phApproveDisallowed.Visible = true;
                                lblApprovalDisallowed.Text = "The Underwriting Status for this merchant is Outdated.  In order to Approve this merchant, please renew the Underwriting results.";
                            }
                        }
                        else if (tabStripMerchantProfile.SelectedTab.Text == "Users")
                        {
                            pnlBasicButtons.Visible = false;
                            pnlUserButtons.Visible = true;
                        }
                        else
                        {
                            pnlBasicButtons.Visible = true;
                        }
                    }
                    else
                    {
                        pnlBasicButtons.Visible = true;
                        pnlMessagingButtons.Visible = false;
                        pnlUnderwritingButtons.Visible = false;
                        pnlUserButtons.Visible = false;

                    }
                }

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "tabStripMerchantDetail_TabClick");
            }

        }






        protected void lblMerchantStatus_DataBound(object sender, EventArgs e)
        {


        }

        protected void ddlActivePlan_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            try
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    int planId = Convert.ToInt32(ddlActivePlan.SelectedValue);
                    var plan = ctx.AdvancePlans.First(ap => ap.RecordId == planId);

                    txtAdvancePlanDescription.Text = plan.PlanDescription;
                    txtPlanMarkupPct.Text = plan.PlanDiscountPct.ToString();
                    txtPaymentCount.Text = plan.PaymentCount.ToString();
                    txtStandardPlanDuration.Text = plan.StandardPlanDuration.ToString();
                    txtExtendedPlanDuration.Text = plan.ExtendedPlanDuration.ToString();
                    txtMinAdvanceAmount.Text = plan.MinimumAdvanceAmount.ToString();
                    txtMaxAdvanceAmount.Text = plan.MaximumAdvanceAmount.ToString();
                    txtPlanIncrementValue.Text = plan.IncrementValue.AdvancePlanIncrementName + " - " + plan.IncrementValue.IncrementAmount.ToString();
                    txtAdvancePlanStatus.Text = plan.Status.StatusDescription;
                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "ddlActivePlan_SelectedIndexChanged");
            }

        }



        protected void gridAdvanceHistory_EditCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void gridActiveAdvances_EditCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = (GridEditableItem)e.Item;

            ViewState["advanceId"] = item.GetDataKeyValue("RecordId").ToString();

            Session.Clear();
        }



        protected void gridUnderwritingHistory_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GetUnderwritingHistory();
        }

        protected void gridUnderwritingHistory_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                TableCell cell = (TableCell)dataItem["Active"];
                Label lblActive = (Label)dataItem.FindControl("lblActive");
                Image imgActive = (Image)dataItem.FindControl("imgActive");

                if (lblActive.Text == "True")
                {
                    imgActive.ImageUrl = "Images/star.png";
                    imgActive.ToolTip = "Current Active Underwriting Results";
                    imgActive.AlternateText = "Current Active Underwriting Results";
                }
                else
                {
                    imgActive.Visible = false;
                }
            }

            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;

                Label lblEditFormCorpInfoResult = (Label)editItem.FindControl("lblEditFormCorpInfoResult");
                Image imgCorpInfoResult = (Image)editItem.FindControl("imgCorpInfoResult");
                switch (lblEditFormCorpInfoResult.Text)
                {
                    case "0":
                        imgCorpInfoResult.ImageUrl = "Images/uw-pending.gif";
                        imgCorpInfoResult.ToolTip = "Pending";
                        break;
                    case "1":
                        imgCorpInfoResult.ImageUrl = "Images/uw-pass.gif";
                        imgCorpInfoResult.ToolTip = "Pass";
                        break;
                    case "2":
                        imgCorpInfoResult.ImageUrl = "Images/uw-fail.gif";
                        imgCorpInfoResult.ToolTip = "Fail";
                        break;
                    default:
                        imgCorpInfoResult.Visible = false;
                        break;
                }

                Label lblEditFormOFACMatchResult = (Label)editItem.FindControl("lblEditFormOFACMatchResult");
                Image imgOFACMatchResult = (Image)editItem.FindControl("imgOFACMatchResult");
                switch (lblEditFormOFACMatchResult.Text)
                {
                    case "0":
                        imgOFACMatchResult.ImageUrl = "Images/uw-pending.gif";
                        imgOFACMatchResult.ToolTip = "Pending";
                        break;
                    case "1":
                        imgOFACMatchResult.ImageUrl = "Images/uw-pass.gif";
                        imgOFACMatchResult.ToolTip = "Pass";
                        break;
                    case "2":
                        imgOFACMatchResult.ImageUrl = "Images/uw-fail.gif";
                        imgOFACMatchResult.ToolTip = "Fail";
                        break;
                    default:
                        imgCorpInfoResult.Visible = false;
                        break;
                }

                Label lblEditFormBusLicStatusResult = (Label)editItem.FindControl("lblEditFormBusLicStatusResult");
                Image imgBusLicStatusResult = (Image)editItem.FindControl("imgBusLicStatusResult");
                switch (lblEditFormBusLicStatusResult.Text)
                {
                    case "0":
                        imgBusLicStatusResult.ImageUrl = "Images/uw-pending.gif";
                        imgBusLicStatusResult.ToolTip = "Pending";
                        break;
                    case "1":
                        imgBusLicStatusResult.ImageUrl = "Images/uw-pass.gif";
                        imgBusLicStatusResult.ToolTip = "Pass";
                        break;
                    case "2":
                        imgBusLicStatusResult.ImageUrl = "Images/uw-fail.gif";
                        imgBusLicStatusResult.ToolTip = "Fail";
                        break;
                    default:
                        imgBusLicStatusResult.Visible = false;
                        break;
                }

                Label lblEditFormEINResult = (Label)editItem.FindControl("lblEditFormEINResult");
                Image imgEINResult = (Image)editItem.FindControl("imgEINResult");
                switch (lblEditFormEINResult.Text)
                {
                    case "0":
                        imgEINResult.ImageUrl = "Images/uw-pending.gif";
                        imgEINResult.ToolTip = "Pending";
                        break;
                    case "1":
                        imgEINResult.ImageUrl = "Images/uw-pass.gif";
                        imgEINResult.ToolTip = "Pass";
                        break;
                    case "2":
                        imgEINResult.ImageUrl = "Images/uw-fail.gif";
                        imgEINResult.ToolTip = "Fail";
                        break;
                    default:
                        imgEINResult.Visible = false;
                        break;
                }

                Label lblEditFormPrincipalResult = (Label)editItem.FindControl("lblEditFormPrincipalResult");
                Image imgPrincipalResult = (Image)editItem.FindControl("imgPrincipalResult");
                switch (lblEditFormPrincipalResult.Text)
                {
                    case "0":
                        imgPrincipalResult.ImageUrl = "Images/uw-pending.gif";
                        imgPrincipalResult.ToolTip = "Pending";
                        break;
                    case "1":
                        imgPrincipalResult.ImageUrl = "Images/uw-pass.gif";
                        imgPrincipalResult.ToolTip = "Pass";
                        break;
                    case "2":
                        imgPrincipalResult.ImageUrl = "Images/uw-fail.gif";
                        imgPrincipalResult.ToolTip = "Fail";
                        break;
                    default:
                        imgPrincipalResult.Visible = false;
                        break;
                }


                Label lblEditFormCardSalesIndicatorResult = (Label)editItem.FindControl("lblEditFormCardSalesIndicatorResult");
                Image imgCardSalesIndicatorResult = (Image)editItem.FindControl("imgCardSalesIndicatorResult");
                switch (lblEditFormCardSalesIndicatorResult.Text)
                {
                    case "0":
                        imgCardSalesIndicatorResult.ImageUrl = "Images/uw-pending.gif";
                        imgCardSalesIndicatorResult.ToolTip = "Pending";
                        break;
                    case "1":
                        imgCardSalesIndicatorResult.ImageUrl = "Images/uw-pass.gif";
                        imgCardSalesIndicatorResult.ToolTip = "Pass";
                        break;
                    case "2":
                        imgCardSalesIndicatorResult.ImageUrl = "Images/uw-fail.gif";
                        imgCardSalesIndicatorResult.ToolTip = "Fail";
                        break;
                    default:
                        imgCardSalesIndicatorResult.Visible = false;
                        break;
                }

                Label lblEditFormBankingInfoResult = (Label)editItem.FindControl("lblEditFormBankingInfoResult");
                Image imgBankingInfoResult = (Image)editItem.FindControl("imgBankingInfoResult");
                switch (lblEditFormBankingInfoResult.Text)
                {
                    case "0":
                        imgBankingInfoResult.ImageUrl = "Images/uw-pending.gif";
                        imgBankingInfoResult.ToolTip = "Pending";
                        break;
                    case "1":
                        imgBankingInfoResult.ImageUrl = "Images/uw-pass.gif";
                        imgBankingInfoResult.ToolTip = "Pass";
                        break;
                    case "2":
                        imgBankingInfoResult.ImageUrl = "Images/uw-fail.gif";
                        imgBankingInfoResult.ToolTip = "Fail";
                        break;
                    default:
                        imgBankingInfoResult.Visible = false;
                        break;
                }

                Label lblEditFormMCCResult = (Label)editItem.FindControl("lblEditFormMCCResult");
                Image imgMCCResult = (Image)editItem.FindControl("imgMCCResult");
                switch (lblEditFormMCCResult.Text)
                {
                    case "0":
                        imgMCCResult.ImageUrl = "Images/uw-pending.gif";
                        imgMCCResult.ToolTip = "Pending";
                        break;
                    case "1":
                        imgMCCResult.ImageUrl = "Images/uw-pass.gif";
                        imgMCCResult.ToolTip = "Pass";
                        break;
                    case "2":
                        imgMCCResult.ImageUrl = "Images/uw-fail.gif";
                        imgMCCResult.ToolTip = "Fail";
                        break;
                    default:
                        imgMCCResult.Visible = false;
                        break;
                }

                Label lblEditFormBVIResult = (Label)editItem.FindControl("lblEditFormBVIResult");
                Image imgBVIResult = (Image)editItem.FindControl("imgBVIResult");
                switch (lblEditFormBVIResult.Text)
                {
                    case "0":
                        imgBVIResult.ImageUrl = "Images/uw-pending.gif";
                        imgBVIResult.ToolTip = "Pending";
                        break;
                    case "1":
                        imgBVIResult.ImageUrl = "Images/uw-pass.gif";
                        imgBVIResult.ToolTip = "Pass";
                        break;
                    case "2":
                        imgBVIResult.ImageUrl = "Images/uw-fail.gif";
                        imgBVIResult.ToolTip = "Fail";
                        break;
                    default:
                        imgBVIResult.Visible = false;
                        break;
                }

                Label lblEditFormTaxLiensResult = (Label)editItem.FindControl("lblEditFormTaxLiensResult");
                Image imgTaxLiensResult = (Image)editItem.FindControl("imgTaxLiensResult");
                switch (lblEditFormTaxLiensResult.Text)
                {
                    case "0":
                        imgTaxLiensResult.ImageUrl = "Images/uw-pending.gif";
                        imgTaxLiensResult.ToolTip = "Pending";
                        break;
                    case "1":
                        imgTaxLiensResult.ImageUrl = "Images/uw-pass.gif";
                        imgTaxLiensResult.ToolTip = "Pass";
                        break;
                    case "2":
                        imgTaxLiensResult.ImageUrl = "Images/uw-fail.gif";
                        imgTaxLiensResult.ToolTip = "Fail";
                        break;
                    default:
                        imgTaxLiensResult.Visible = false;
                        break;
                }

                Label lblEditFormRiskIndicatorResult = (Label)editItem.FindControl("lblEditFormRiskIndicatorResult");
                Image imgRiskIndicatorResult = (Image)editItem.FindControl("imgRiskIndicatorResult");
                switch (lblEditFormRiskIndicatorResult.Text)
                {
                    case "0":
                        imgRiskIndicatorResult.ImageUrl = "Images/uw-pending.gif";
                        imgRiskIndicatorResult.ToolTip = "Pending";
                        break;
                    case "1":
                        imgRiskIndicatorResult.ImageUrl = "Images/uw-pass.gif";
                        imgRiskIndicatorResult.ToolTip = "Pass";
                        break;
                    case "2":
                        imgRiskIndicatorResult.ImageUrl = "Images/uw-fail.gif";
                        imgRiskIndicatorResult.ToolTip = "Fail";
                        break;
                    default:
                        imgRiskIndicatorResult.Visible = false;
                        break;
                }


                HyperLink linkTempXmlFile = (HyperLink)editItem.FindControl("linkTempXmlFile");

                MerchantModel editMerchant = new MerchantModel();
                Int32 merchantRecordId;

                merchantRecordId = Convert.ToInt32(hMerchantRecordId.Value);

                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    MerchantModel merchant = ctx.Merchants.First(m => m.RecordId == merchantRecordId);

                    if (merchant != null)
                    {
                        if (ctx.UnderwritingResults.Any(x => x.Active == true && x.Merchant.RecordId == merchant.RecordId))
                        {
                            UnderwritingResultModel uwActiveResults = ctx.UnderwritingResults.FirstOrDefault(x => x.Active == true && x.Merchant.RecordId == merchant.RecordId);

                            if (uwActiveResults != null)
                            {
                                string fileName = "rawxml_" + Guid.NewGuid() + ".xml";

                                if (!File.Exists(Server.MapPath("~/TempUWFiles/" + fileName)))
                                {
                                    using (File.Create(Server.MapPath("~/TempUWFiles/" + fileName))) { }

                                    File.WriteAllText(Server.MapPath("~/TempUWFiles/" + fileName), uwActiveResults.LexisNexisResults.LexisNexisRawResult.XmlContent);
                                }

                                linkTempXmlFile.NavigateUrl = "~/TempUWFiles/" + fileName;

                            }
                        }
                    }
                }

            }
        }

        protected void gridUnderwritingHistory_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void gridUnderwritingHistory_ExportCellFormatting(object sender, ExportCellFormattingEventArgs e)
        {

        }

        protected void gridUnderwritingHistory_ItemCreated(object sender, GridItemEventArgs e)
        {

        }

        public String GetRequestAge(object timestamp, object completedTimestamp, object status)
        {
            try
            {
                if (timestamp != null && status != null)
                {
                    if (status.ToString() == "Completed")
                    {
                        if (completedTimestamp != null)
                        {
                            return (Convert.ToDateTime(completedTimestamp).Date - Convert.ToDateTime(timestamp).Date).TotalDays.ToString() + " Day(s)";
                        }
                        else
                        {
                            return "N/A";
                        }
                    }
                    else
                    {
                        return (DateTime.UtcNow.Date - Convert.ToDateTime(timestamp).Date).TotalDays.ToString() + " Day(s)";
                    }

                }
                else
                {
                    return "N/A";
                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "GetRequestAge");

                return "";
            }
        }

        public String GetFundingAge(object timestamp, object completedTimestamp, object status)
        {
            try
            {
                if (timestamp != null && status != null)
                {
                    if (status.ToString() == "Completed")
                    {
                        if (completedTimestamp != null)
                        {
                            return (Convert.ToDateTime(completedTimestamp).Date - Convert.ToDateTime(timestamp).Date).TotalDays.ToString() + " Day(s)";
                        }
                        else
                        {
                            return "N/A";
                        }
                    }
                    else
                    {
                        return (DateTime.UtcNow.Date - Convert.ToDateTime(timestamp).Date).TotalDays.ToString() + " Day(s)";
                    }

                }
                else
                {
                    return "N/A";
                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "GetFundingAge");

                return "";
            }
        }

        private string StringifySeasonalMonths(string seasonalMonths)
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

            return seasonalMonths;
        }



        /// <summary>
        /// Bind all Legal Org Types from DB to IQueryable for DropDownList
        /// </summary>
        /// <returns>IQueryable<LegalOrgTypes></returns>
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

        public IQueryable<MerchantStatusModel> BindAccountStatuses()
        {
            try
            {
                IQueryable<MerchantStatusModel> merchantStatuses = _globalCtx.MerchantStatuses.OrderBy(ms => ms.RecordId);

                return merchantStatuses;
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "BindAccountStatuses");

                return null;
            }
        }

        public IQueryable<MerchantStatusModel> BindAccountStatusesWithoutPreEnrolled()
        {
            try
            {
                IQueryable<MerchantStatusModel> merchantStatuses = _globalCtx.MerchantStatuses.Where(ms => ms.StatusDescription != "Pre-Enrolled").OrderBy(ms => ms.RecordId);

                return merchantStatuses;
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "BindAccountStatuses");

                return null;
            }
        }

        public IQueryable<UnderwritingStatusModel> BindUnderwritingStatuses()
        {
            try
            {
                IQueryable<UnderwritingStatusModel> underwritingStatuses = _globalCtx.UnderwritingStatuses.OrderBy(us => us.RecordId);

                return underwritingStatuses;
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "BindUnderwritingStatuses");

                return null;
            }
        }

        public IQueryable<MerchantSettlementDetailModel> GetSettlements()
        {
            try
            {
                Int32 merchantRecordId = Convert.ToInt32(hMerchantRecordId.Value);
                MerchantModel editMerchant = _globalCtx.Merchants.First(m => m.RecordId == merchantRecordId);

                if (editMerchant != null)
                {
                    if (_globalCtx.MerchantSettlementDetails.Any(x => x.Merchant.RecordId == editMerchant.RecordId))
                    {
                        IQueryable<MerchantSettlementDetailModel> dailySettlements = _globalCtx.MerchantSettlementDetails
                            .Where(x => x.Merchant.RecordId == editMerchant.RecordId)
                            .OrderByDescending(x => x.ActivityDate);

                        return dailySettlements;
                    }
                }

                return null;
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "GetSettlements");

                return null;
            }
        }

        public IQueryable<UnderwritingResultModel> GetUnderwritingHistory()
        {
            try
            {
                Int32 merchantRecordId = Convert.ToInt32(hMerchantRecordId.Value);
                MerchantModel editMerchant = _globalCtx.Merchants.First(m => m.RecordId == merchantRecordId);

                if (editMerchant != null)
                {
                    IQueryable<UnderwritingResultModel> underwritingHistoryList = _globalCtx.UnderwritingResults
                        .Where(ur => ur.Merchant.RecordId == editMerchant.RecordId)
                        .OrderByDescending(ur => ur.Timestamp);

                    return underwritingHistoryList;
                }
                else
                {
                    return null;
                }

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "GetUnderwritingHistory");
                return null;
            }
        }

        public IQueryable<AdvancePlanIncrementModel> BindPlanIncrementValues()
        {
            try
            {
                IQueryable<AdvancePlanIncrementModel> incrementValues = _globalCtx.AdvancePlanIncrements.OrderBy(api => api.IncrementAmount);

                return incrementValues;
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "BindPlanIncrementValues");

                return null;
            }
        }

        public IQueryable<AdvancePlanStatusModel> BindAdvancePlanStatuses()
        {
            try
            {
                IQueryable<AdvancePlanStatusModel> advancePlanStatuses = _globalCtx.AdvancePlanStatuses;

                return advancePlanStatuses;
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "BindAdvancePlanStatuses");

                return null;
            }
        }

        public IQueryable<AdvancePlanModel> BindActiveAdvancePlans()
        {
            try
            {
                int brandId = _globalCtx.Brands.First(b => b.BrandName == "Central Cash").RecordId;

                IQueryable<AdvancePlanModel> advancePlanList = _globalCtx.AdvancePlans.Where(ap => ap.Brand.RecordId == brandId && ap.Status.StatusDescription == "Active").OrderByDescending(ap => ap.DefaultPlan)
                    .ThenBy(ap => ap.Status.StatusDescription)
                    .ThenByDescending(ap => ap.Timestamp);

                return advancePlanList;
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "GetAdvancePlans");
                return null;
            }
        }

        public IQueryable<AdvanceModel> GetActiveAdvances()
        {
            try
            {
                Int32 merchantRecordId = Convert.ToInt32(hMerchantRecordId.Value);
                IQueryable<AdvanceModel> activeAdvances = _globalCtx.Advances.Where(a => a.Merchant.RecordId == merchantRecordId && a.Status.StatusDescription == "Approved").OrderByDescending(a => a.Timestamp);

                return activeAdvances;
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "GetActiveAdvances");

                return null;
            }
        }

        public IQueryable<AdvanceModel> GetAdvanceHistory()
        {
            try
            {
                Int32 merchantRecordId = Convert.ToInt32(hMerchantRecordId.Value);
                IQueryable<AdvanceModel> advanceHistory = _globalCtx.Advances.Where(a => a.Merchant.RecordId == merchantRecordId).OrderByDescending(a => a.Timestamp);

                return advanceHistory;
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "GetAdvanceHistory");

                return null;
            }
        }

        public IQueryable<AdvancePaymentModel> GetAdvanceRepayments()
        {
            try
            {
                foreach (GridDataItem item in gridActiveAdvances.Items)
                {
                    if (item.EditFormItem.IsInEditMode)
                    {
                        if (item.GetDataKeyValue("RecordId") != null)
                        {
                            Int32 advanceId = Convert.ToInt32(item.GetDataKeyValue("RecordId"));

                            IQueryable<AdvancePaymentModel> advancePayments = _globalCtx.AdvancePayments.Where(ap => ap.Advance.RecordId == advanceId).OrderByDescending(ap => ap.Timestamp);

                            return advancePayments;
                        }
                    }

                }

                return null;
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "GetAdvanceRepayments");

                return null;
            }
        }

        public IQueryable<AdvancePaymentModel> GetAdvanceHistoryRepayments()
        {
            try
            {
                foreach (GridDataItem item in gridAdvanceHistory.Items)
                {
                    if (item.EditFormItem.IsInEditMode)
                    {
                        if (item.GetDataKeyValue("RecordId") != null)
                        {
                            Int32 advanceId = Convert.ToInt32(item.GetDataKeyValue("RecordId"));

                            IQueryable<AdvancePaymentModel> advancePayments = _globalCtx.AdvancePayments.Where(ap => ap.Advance.RecordId == advanceId).OrderByDescending(ap => ap.Timestamp);

                            return advancePayments;
                        }
                    }

                }

                return null;
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "GetAdvanceRepayments");

                return null;
            }
        }

        public IQueryable<AdvanceStatusModel> BindAdvanceStatuses()
        {
            try
            {
                IQueryable<AdvanceStatusModel> advanceStatuses = _globalCtx.AdvanceStatuses.OrderBy(advs => advs.RecordId);

                return advanceStatuses;

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "BindAdvanceStatuses");

                return null;
            }
        }

        public IQueryable<FundingStatusModel> BindFundingStatuses()
        {
            try
            {
                IQueryable<FundingStatusModel> fundingStatuses = _globalCtx.FundingStatuses.OrderBy(fs => fs.RecordId);

                return fundingStatuses;

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "BindFundingStatuses");

                return null;
            }
        }

        public IQueryable<MessagingLogModel> GetEmailMessages()
        {
            try
            {
                Int32 merchantId = 0;
                Int32.TryParse(hMerchantRecordId.Value, out merchantId);
                if (merchantId != 0)
                {
                    var userList = _globalCtx.Users.Where(u => u.Merchant.RecordId == merchantId).Select(u => u.Id);

                    IQueryable<MessagingLogModel> emailMessages = _globalCtx.MessagingLogs
                        .Where(ml => ml.Merchant.RecordId == merchantId || userList.Contains(ml.User.Id))
                        .OrderByDescending(ml => ml.Timestamp);

                    return emailMessages;
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "GetEmailMessages");

                return null;
            }
        }

        public IQueryable<MerchantSuspensionReasonModel> BingSuspendReasons()
        {
            try
            {
                IQueryable<MerchantSuspensionReasonModel> suspendReasons = _globalCtx.MerchantSuspensionReasons.OrderBy(msr => msr.DisplayOrder);

                return suspendReasons;
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "BingSuspendReasons");

                return null;
            }
        }

        public IQueryable<DateYear> FillDateYears()
        {
            try
            {
                IQueryable<Int32> _years = Enumerable.Range(1800, DateTime.Now.Year - 1799).Reverse().AsQueryable();
                IQueryable<DateYear> years = _years.Select(x => new DateYear { YearText = x.ToString(), YearValue = x.ToString() });

                return years;
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "FillDateYears");

                return null;
            }
        }

        public IQueryable<DateMonth> FillDateMonths()
        {
            try
            {
                DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);

                IQueryable<Int32> _months = Enumerable.Range(1, 12).AsQueryable();
                IQueryable<DateMonth> months = _months.Select(x => new DateMonth { MonthText = info.GetMonthName(x).Substring(0, 3), MonthValue = x.ToString() });

                return months;
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "FillDateMonths");

                return null;
            }
        }

        public IQueryable<DateDay> FillDateDays()
        {
            try
            {
                IQueryable<Int32> _days = Enumerable.Range(1, 31).AsQueryable();
                IQueryable<DateDay> days = _days.Select(x => new DateDay { DayText = x.ToString(), DayValue = x.ToString() });

                return days;
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "FillDateDays");

                return null;
            }
        }




        protected void btnDenyAdvance_Click(object sender, EventArgs e)
        {
            try
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    int advanceId;
                    Decimal availableAdvAmount = 0;

                    foreach (GridDataItem item in gridActiveAdvances.Items)
                    {
                        if (item.EditFormItem.IsInEditMode)
                        {
                            Label lblActionMessage = (Label)item.EditFormItem.FindControl("lblActionMessage");
                            RadTextBox txtDenyNotes = (RadTextBox)item.EditFormItem.FindControl("txtDenyNotes");

                            advanceId = Convert.ToInt32(item.GetDataKeyValue("RecordId"));
                            AdvanceModel advance = ctx.Advances.First(a => a.RecordId == advanceId);
                            MerchantModel merchant = ctx.Merchants.First(m => m.RecordId == advance.Merchant.RecordId);

                            string userId = Context.User.Identity.GetUserId();

                            advance.Status = ctx.AdvanceStatuses.First(advs => advs.StatusDescription == "Denied");
                            advance.FundingStatus = ctx.FundingStatuses.First(fs => fs.StatusDescription == "Rejected");
                            advance.PrincipalBalance = 0;
                            advance.FeeBalance = 0;
                            advance.ApprovedTimestamp = DateTime.UtcNow;
                            advance.LastUpdateTimestamp = DateTime.UtcNow;
                            advance.ApprovedBy = ctx.Users.First(u => u.Id == userId);
                            advance.AdminNotes = txtDenyNotes.Text;

                            availableAdvAmount = merchant.AvailableAdvanceAmount.Value + advance.PrincipalAmount.Value;
                            merchant.AvailableAdvanceAmount = availableAdvAmount > merchant.ApprovedAdvanceAmount.Value ? merchant.ApprovedAdvanceAmount.Value : availableAdvAmount;

                            ctx.SaveChanges();

                            gridActiveAdvances.MasterTableView.ClearEditItems();

                            gridAdvanceHistory.Rebind();
                            gridActiveAdvances.Rebind();

                            Session["ActionMessage"] = "The Advance and Funding Statuses have been successfully updated.";

                            //Send Email
                            Logic.Messaging messaging = new Logic.Messaging();
                            Boolean emailSent = false;
                            var template = messaging.GetTemplate("DeniedAdvance");

                            if (template != null)
                            {
                                String html = messaging.GetTemplateHtml(template);

                                foreach (EmailTemplateVariableModel variable in messaging.GetRequiredVariables(template))
                                {
                                    if (variable.VariableName == "MERCHANT_FIRST_NAME")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.MerchantPrincipal.Contact.FirstName);
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.MerchantPrincipal.Contact.FirstName);
                                    }
                                    if (variable.VariableName == "MERCHANT_LAST_NAME")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.MerchantPrincipal.Contact.LastName);
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.MerchantPrincipal.Contact.LastName);
                                    }
                                    if (variable.VariableName == "MERCHANT_NAME")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.CorpName);
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.CorpName);
                                    }
                                    if (variable.VariableName == "MERCHANT_ADDRESS")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.Address);
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.Address);
                                    }
                                    if (variable.VariableName == "MERCHANT_CITY")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.City);
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.City);
                                    }
                                    if (variable.VariableName == "MERCHANT_STATE")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.State.Name);
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.State.Name);
                                    }
                                    if (variable.VariableName == "MERCHANT_ZIP")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.Zip);
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.Zip);
                                    }
                                    if (variable.VariableName == "ADVANCE_AMOUNT")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", advance.PrincipalAmount.ToString());
                                        html = html.Replace("<$" + variable.VariableName + ">", advance.PrincipalAmount.ToString());
                                    }
                                    if (variable.VariableName == "DENY_NOTES")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", advance.AdminNotes);
                                        html = html.Replace("<$" + variable.VariableName + ">", advance.AdminNotes);
                                    }

                                }

                                if (html != null && advance.RequestedBy != null)
                                {
                                    emailSent = messaging.SendEmail(advance.RequestedBy.Email, template.EmailSubject, html, template, merchant, advance.RequestedBy);
                                }

                            }
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                Session["ActionMessage"] = "An error has occurred while attempting to deny this Advance.  Please contact your system administrator for additional information.";

                _newLogic.WriteExceptionToDB(ex, "btnDenyAdvance_Click");
            }


        }

        protected void btnRetryFunding_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            try
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    int advanceId;

                    foreach (GridDataItem item in gridActiveAdvances.Items)
                    {
                        if (item.EditFormItem.IsInEditMode)
                        {
                            Label lblActionMessage = (Label)item.EditFormItem.FindControl("lblActionMessage");
                            RadDropDownList ddlAdvanceStatus = (RadDropDownList)item.EditFormItem.FindControl("ddlAdvanceStatus");
                            RadDropDownList ddlFundingStatus = (RadDropDownList)item.EditFormItem.FindControl("ddlFundingStatus");
                            int advStatusId = Convert.ToInt32(ddlAdvanceStatus.SelectedValue);
                            int fundStatusId = Convert.ToInt32(ddlFundingStatus.SelectedValue);

                            advanceId = Convert.ToInt32(item.GetDataKeyValue("RecordId"));
                            AdvanceModel advance = ctx.Advances.First(a => a.RecordId == advanceId);

                            advance.Status = ctx.AdvanceStatuses.First(advs => advs.RecordId == advStatusId);
                            advance.FundingStatus = ctx.FundingStatuses.First(fs => fs.RecordId == fundStatusId);

                            ctx.SaveChanges();

                            Session["ActionMessage"] = "The Advance and Funding Statuses have been successfully updated.";
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                Session["ActionMessage"] = "An error has occurred while updating the status.  Please contact your system administrator for additional information.";

                _newLogic.WriteExceptionToDB(ex, "btnUpdateStatus_Click");
            }
        }

        protected void lblActionMessage_DataBinding(object sender, EventArgs e)
        {

        }

        protected void lblActionMessage_PreRender(object sender, EventArgs e)
        {
            if (Session["ActionMessage"] != null)
            {
                Label lblActionMessage = (Label)sender;
                lblActionMessage.Text = Session["ActionMessage"].ToString();
            }
        }

        protected void btnCloseAdvanceDetails_Click(object sender, EventArgs e)
        {
            Session.Clear();
        }

        protected void btnFundAdvance_Click(object sender, EventArgs e)
        {
            try
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    int advanceId;

                    foreach (GridDataItem item in gridActiveAdvances.Items)
                    {
                        if (item.EditFormItem.IsInEditMode)
                        {
                            Label lblActionMessage = (Label)item.EditFormItem.FindControl("lblActionMessage");
                            RadTextBox txtFundNotes = (RadTextBox)item.EditFormItem.FindControl("txtFundNotes");

                            advanceId = Convert.ToInt32(item.GetDataKeyValue("RecordId"));
                            AdvanceModel advance = ctx.Advances.First(a => a.RecordId == advanceId);

                            string userId = Context.User.Identity.GetUserId();

                            advance.Status = ctx.AdvanceStatuses.First(advs => advs.StatusDescription == "Approved");
                            advance.FundingStatus = ctx.FundingStatuses.First(fs => fs.StatusDescription == "Approved");
                            advance.PrincipalBalance = advance.PrincipalAmount;
                            advance.FeeBalance = advance.FeeAmount;
                            advance.FundedTimestamp = DateTime.UtcNow;
                            advance.LastUpdateTimestamp = DateTime.UtcNow;
                            advance.ApprovedBy = ctx.Users.First(u => u.Id == userId);
                            advance.AdminNotes = txtFundNotes.Text;
                            advance.NextPaymentAmount = (advance.PrincipalAmount + advance.FeeAmount) / 10;
                            advance.NextPrincipalAmount = (advance.PrincipalAmount) / 10;
                            advance.NextFeeAmount = (advance.NextFeeAmount) / 10;


                            ctx.SaveChanges();

                            gridActiveAdvances.MasterTableView.ClearEditItems();

                            gridAdvanceHistory.Rebind();
                            gridActiveAdvances.Rebind();

                            Session["ActionMessage"] = "The Advance has been successfully updated.";
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                Session["ActionMessage"] = "An error has occurred while attempting to fund this Advance.  Please contact your system administrator for additional information.";

                _newLogic.WriteExceptionToDB(ex, "btnFundAdvance_Click");
            }
        }



        protected void gridMessages_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem)
            {
                GridFilteringItem filterItem = (GridFilteringItem)e.Item;
                filterItem["Emailtemplate.EmailSubject"].HorizontalAlign = HorizontalAlign.Center;
                filterItem["User.Email"].HorizontalAlign = HorizontalAlign.Center;
                filterItem["Timestamp"].HorizontalAlign = HorizontalAlign.Center;


            }
        }

        protected void btnViewEmail_Click(object sender, EventArgs e)
        {
            try
            {
                RadButton btnViewEmail = (RadButton)sender;
                Int32 templateId = 0;
                Int32.TryParse(btnViewEmail.CommandArgument, out templateId);

                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    if (ctx.MessagingLogs.Any(ml => ml.RecordId == templateId))
                    {
                        string emailContent = ctx.MessagingLogs.First(ml => ml.RecordId == templateId).MessageContent;
                        ltlEmailContent.Text = System.Web.HttpUtility.HtmlDecode(emailContent);
                    }
                }

                phGridMessages.Visible = false;
                phEmailContent.Visible = true;
                pnlBasicButtons.Visible = false;
                pnlMessagingButtons.Visible = true;

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "btnViewEmail_Click");
            }
        }

        protected void btnEmailBack_Click(object sender, EventArgs e)
        {
            phGridMessages.Visible = true;
            phEmailContent.Visible = false;
            pnlMessagingButtons.Visible = false;
        }

        protected void lblMerchantActionMessage_PreRender(object sender, EventArgs e)
        {

        }

        protected void btnApproveMerchant_Click(object sender, EventArgs e)
        {
            try
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    int merchantId;
                    Int32.TryParse(hMerchantRecordId.Value, out merchantId);
                    string adminId = Context.User.Identity.GetUserId();

                    RadTextBox txtMerchantApprovalNotes = (RadTextBox)StatusView.FindControl("txtMerchantApprovalNotes");

                    if (ctx.Merchants.Any(m => m.RecordId == merchantId))
                    {
                        MerchantModel merchant = ctx.Merchants.First(m => m.RecordId == merchantId);
                        Decimal approvedAdvanceAmount = 0;
                        Decimal.TryParse(txtApprovedAdvanceAmount.Text, out approvedAdvanceAmount);

                        merchant.MerchantStatus = ctx.MerchantStatuses.First(ms => ms.StatusDescription == "Active");
                        merchant.ApprovedBy = ctx.Users.First(u => u.Id == adminId);
                        merchant.ApprovalNotes = txtMerchantApprovalNotes.Text;
                        merchant.ApprovedDate = DateTime.UtcNow.Date;
                        merchant.ApprovedAdvanceAmount = approvedAdvanceAmount;
                        merchant.AvailableAdvanceAmount = approvedAdvanceAmount;

                        ctx.SaveChanges();

                        StatusChangeModel statusChange = new StatusChangeModel
                        {
                            AdminUser = ctx.Users.First(u => u.Id == adminId),
                            Merchant = merchant,
                            MerchantStatus = merchant.MerchantStatus,
                            UnderwritingStatus = merchant.UnderwritingStatus,
                            Notes = merchant.ApprovalNotes,
                            Timestamp = DateTime.UtcNow
                        };

                        ctx.StatusChanges.Add(statusChange);

                        ctx.SaveChanges();


                        //Send Email
                        Logic.Messaging messaging = new Logic.Messaging();
                        Boolean emailSent = false;
                        var template = messaging.GetTemplate("ApprovedEnrollment");

                        if (template != null)
                        {
                            String html = messaging.GetTemplateHtml(template);

                            foreach (EmailTemplateVariableModel variable in messaging.GetRequiredVariables(template))
                            {
                                if (merchant.MerchantPrincipal != null && merchant.MerchantPrincipal.Contact != null)
                                {
                                    if (variable.VariableName == "MERCHANT_FIRST_NAME")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.MerchantPrincipal.Contact.FirstName);
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.MerchantPrincipal.Contact.FirstName);
                                    }
                                    if (variable.VariableName == "MERCHANT_LAST_NAME")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.MerchantPrincipal.Contact.LastName);
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.MerchantPrincipal.Contact.LastName);
                                    }
                                }
                                else
                                {
                                    if (variable.VariableName == "MERCHANT_FIRST_NAME")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                    if (variable.VariableName == "MERCHANT_LAST_NAME")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                }
                                if (variable.VariableName == "MERCHANT_NAME")
                                {
                                    html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.CorpName);
                                    html = html.Replace("<$" + variable.VariableName + ">", merchant.CorpName);
                                }
                                if (merchant.Business.Address != null)
                                {
                                    if (variable.VariableName == "MERCHANT_ADDRESS")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.Address ?? "");
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.Address ?? "");
                                    }
                                    if (variable.VariableName == "MERCHANT_CITY")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.City ?? "");
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.City ?? "");
                                    }
                                    if (variable.VariableName == "MERCHANT_ZIP")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.Zip ?? "");
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.Zip ?? "");
                                    }
                                    if (merchant.Business.Address.State != null)
                                    {
                                        if (variable.VariableName == "MERCHANT_STATE")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.State.Name ?? "");
                                            html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.State.Name ?? "");
                                        }
                                    }
                                    else
                                    {
                                        if (variable.VariableName == "MERCHANT_STATE")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                            html = html.Replace("<$" + variable.VariableName + ">", "");
                                        }
                                    }
                                }
                                else
                                {
                                    if (variable.VariableName == "MERCHANT_ADDRESS")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                    if (variable.VariableName == "MERCHANT_CITY")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                    if (variable.VariableName == "MERCHANT_STATE")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                    if (variable.VariableName == "MERCHANT_ZIP")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                }
                                if (variable.VariableName == "APPROVED_ADVANCE_AMOUNT")
                                {
                                    html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.ApprovedAdvanceAmount.ToString());
                                    html = html.Replace("<$" + variable.VariableName + ">", merchant.ApprovedAdvanceAmount.ToString());
                                }
                            }

                            if (ctx.Users.Any(u => u.Merchant.RecordId == merchant.RecordId))
                            {
                                IQueryable<ApplicationUser> userList = ctx.Users.Where(u => u.Merchant.RecordId == merchant.RecordId);

                                if (userList != null)
                                {
                                    foreach (ApplicationUser thisUser in userList)
                                    {
                                        if (html != null)
                                        {
                                            String newHtml = html.Replace("&lt;$USERNAME&gt;", thisUser.UserName);
                                            newHtml = newHtml.Replace("<$USERNAME>", thisUser.UserName);

                                            emailSent = messaging.SendEmail(thisUser.Email, template.EmailSubject, newHtml, template, merchant, thisUser);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    lblMerchantActionMessage.Text = "Merchant Approved Successfully.";

                    UpdateStatusLabel();
                }
            }
            catch (System.Exception ex)
            {
                lblMerchantActionMessage.Text = "Merchant was unable to be approved.  Please contact your system administrator for more information.";
                _newLogic.WriteExceptionToDB(ex, "btnApproveMerchant_Click");
            }
        }

        protected void btnDenyMerchant_Click(object sender, EventArgs e)
        {
            try
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    int merchantId;
                    Int32.TryParse(hMerchantRecordId.Value, out merchantId);
                    string adminId = Context.User.Identity.GetUserId();

                    RadTextBox txtMerchantDenialNotes = (RadTextBox)StatusView.FindControl("txtMerchantDenialNotes");

                    if (ctx.Merchants.Any(m => m.RecordId == merchantId))
                    {
                        MerchantModel merchant = ctx.Merchants.First(m => m.RecordId == merchantId);

                        merchant.MerchantStatus = ctx.MerchantStatuses.First(ms => ms.StatusDescription == "Denied");
                        merchant.DeniedBy = ctx.Users.First(u => u.Id == adminId);
                        merchant.DenialNotes = txtMerchantDenialNotes.Text;
                        merchant.DeniedDate = DateTime.UtcNow.Date;
                        merchant.ApprovedAdvanceAmount = 0;
                        merchant.AvailableAdvanceAmount = 0;

                        ctx.SaveChanges();

                        StatusChangeModel statusChange = new StatusChangeModel
                        {
                            AdminUser = ctx.Users.First(u => u.Id == adminId),
                            Merchant = merchant,
                            MerchantStatus = merchant.MerchantStatus,
                            UnderwritingStatus = merchant.UnderwritingStatus,
                            Notes = merchant.DenialNotes,
                            Timestamp = DateTime.UtcNow
                        };

                        ctx.StatusChanges.Add(statusChange);

                        ctx.SaveChanges();


                        //Send Email
                        Logic.Messaging messaging = new Logic.Messaging();
                        Boolean emailSent = false;
                        var template = messaging.GetTemplate("DeniedEnrollment");

                        if (template != null)
                        {
                            String html = messaging.GetTemplateHtml(template);

                            foreach (EmailTemplateVariableModel variable in messaging.GetRequiredVariables(template))
                            {
                                if (merchant.MerchantPrincipal != null && merchant.MerchantPrincipal.Contact != null)
                                {
                                    if (variable.VariableName == "MERCHANT_FIRST_NAME")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.MerchantPrincipal.Contact.FirstName);
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.MerchantPrincipal.Contact.FirstName);
                                    }
                                    if (variable.VariableName == "MERCHANT_LAST_NAME")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.MerchantPrincipal.Contact.LastName);
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.MerchantPrincipal.Contact.LastName);
                                    }
                                }
                                else
                                {
                                    if (variable.VariableName == "MERCHANT_FIRST_NAME")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                    if (variable.VariableName == "MERCHANT_LAST_NAME")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                }
                                if (variable.VariableName == "MERCHANT_NAME")
                                {
                                    html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.CorpName);
                                    html = html.Replace("<$" + variable.VariableName + ">", merchant.CorpName);
                                }
                                if (merchant.Business.Address != null)
                                {
                                    if (variable.VariableName == "MERCHANT_ADDRESS")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.Address ?? "");
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.Address ?? "");
                                    }
                                    if (variable.VariableName == "MERCHANT_CITY")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.City ?? "");
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.City ?? "");
                                    }
                                    if (variable.VariableName == "MERCHANT_ZIP")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.Zip ?? "");
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.Zip ?? "");
                                    }
                                    if (merchant.Business.Address.State != null)
                                    {
                                        if (variable.VariableName == "MERCHANT_STATE")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.State.Name ?? "");
                                            html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.State.Name ?? "");
                                        }
                                    }
                                    else
                                    {
                                        if (variable.VariableName == "MERCHANT_STATE")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                            html = html.Replace("<$" + variable.VariableName + ">", "");
                                        }
                                    }
                                }
                                else
                                {
                                    if (variable.VariableName == "MERCHANT_ADDRESS")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                    if (variable.VariableName == "MERCHANT_CITY")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                    if (variable.VariableName == "MERCHANT_STATE")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                    if (variable.VariableName == "MERCHANT_ZIP")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                }
                                if (variable.VariableName == "DENY_NOTES")
                                {
                                    html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.DenialNotes);
                                    html = html.Replace("<$" + variable.VariableName + ">", merchant.DenialNotes);
                                }
                            }

                            if (ctx.Users.Any(u => u.Merchant.RecordId == merchant.RecordId))
                            {
                                IQueryable<ApplicationUser> userList = ctx.Users.Where(u => u.Merchant.RecordId == merchant.RecordId);

                                if (userList != null)
                                {
                                    foreach (ApplicationUser thisUser in userList)
                                    {
                                        if (html != null)
                                        {
                                            emailSent = messaging.SendEmail(thisUser.Email, template.EmailSubject, html, template, merchant, thisUser);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    lblMerchantActionMessage.Text = "Merchant Denied Successfully.";

                    UpdateStatusLabel();
                }
            }
            catch (System.Exception ex)
            {
                lblMerchantActionMessage.Text = "Merchant was unable to be Denied.  Please contact your system administrator for more information.";
                _newLogic.WriteExceptionToDB(ex, "btnDenyMerchant_Click");
            }
        }

        protected void btnSuspendMerchant_Click(object sender, EventArgs e)
        {
            try
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    int merchantId;
                    Int32.TryParse(hMerchantRecordId.Value, out merchantId);
                    string adminId = Context.User.Identity.GetUserId();

                    RadTextBox txtMerchantSuspensionNotes = (RadTextBox)StatusView.FindControl("txtMerchantSuspensionNotes");

                    if (ctx.Merchants.Any(m => m.RecordId == merchantId))
                    {
                        MerchantModel merchant = ctx.Merchants.First(m => m.RecordId == merchantId);
                        MerchantSuspensionModel suspension = new MerchantSuspensionModel();
                        Int32 reasonId = 0;
                        Int32.TryParse(ddlSuspendReasons.SelectedValue, out reasonId);

                        merchant.MerchantStatus = ctx.MerchantStatuses.First(ms => ms.StatusDescription == "Suspended");
                        suspension.Merchant = merchant;
                        suspension.OtherReasonNotes = txtMerchantSuspensionNotes.Text;
                        suspension.SuspendedBy = ctx.Users.First(u => u.Id == adminId);
                        suspension.SuspensionReason = ctx.MerchantSuspensionReasons.First(msr => msr.RecordId == reasonId);
                        suspension.Timestamp = DateTime.UtcNow;

                        ctx.MerchantSuspensions.Add(suspension);

                        ctx.SaveChanges();

                        StatusChangeModel statusChange = new StatusChangeModel
                        {
                            AdminUser = ctx.Users.First(u => u.Id == adminId),
                            Merchant = merchant,
                            MerchantStatus = merchant.MerchantStatus,
                            UnderwritingStatus = merchant.UnderwritingStatus,
                            Notes = suspension.SuspensionReason.ReasonName,
                            Timestamp = DateTime.UtcNow
                        };

                        ctx.StatusChanges.Add(statusChange);

                        ctx.SaveChanges();


                        //Send Email
                        Logic.Messaging messaging = new Logic.Messaging();
                        Boolean emailSent = false;
                        var template = messaging.GetTemplate("MerchantSuspended");

                        String suspendNotes = ddlSuspendReasons.SelectedText + ": " + txtMerchantSuspensionNotes.Text;

                        if (template != null)
                        {
                            String html = messaging.GetTemplateHtml(template);

                            foreach (EmailTemplateVariableModel variable in messaging.GetRequiredVariables(template))
                            {
                                if (merchant.MerchantPrincipal != null && merchant.MerchantPrincipal.Contact != null)
                                {
                                    if (variable.VariableName == "MERCHANT_FIRST_NAME")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.MerchantPrincipal.Contact.FirstName);
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.MerchantPrincipal.Contact.FirstName);
                                    }
                                    if (variable.VariableName == "MERCHANT_LAST_NAME")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.MerchantPrincipal.Contact.LastName);
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.MerchantPrincipal.Contact.LastName);
                                    }
                                }
                                else
                                {
                                    if (variable.VariableName == "MERCHANT_FIRST_NAME")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                    if (variable.VariableName == "MERCHANT_LAST_NAME")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                }
                                if (variable.VariableName == "MERCHANT_NAME")
                                {
                                    html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.CorpName);
                                    html = html.Replace("<$" + variable.VariableName + ">", merchant.CorpName);
                                }
                                if (merchant.Business.Address != null)
                                {
                                    if (variable.VariableName == "MERCHANT_ADDRESS")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.Address ?? "");
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.Address ?? "");
                                    }
                                    if (variable.VariableName == "MERCHANT_CITY")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.City ?? "");
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.City ?? "");
                                    }
                                    if (variable.VariableName == "MERCHANT_ZIP")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.Zip ?? "");
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.Zip ?? "");
                                    }
                                    if (merchant.Business.Address.State != null)
                                    {
                                        if (variable.VariableName == "MERCHANT_STATE")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.State.Name ?? "");
                                            html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.State.Name ?? "");
                                        }
                                    }
                                    else
                                    {
                                        if (variable.VariableName == "MERCHANT_STATE")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                            html = html.Replace("<$" + variable.VariableName + ">", "");
                                        }
                                    }
                                }
                                else
                                {
                                    if (variable.VariableName == "MERCHANT_ADDRESS")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                    if (variable.VariableName == "MERCHANT_CITY")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                    if (variable.VariableName == "MERCHANT_STATE")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                    if (variable.VariableName == "MERCHANT_ZIP")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                }
                                if (variable.VariableName == "SUSPEND_NOTES")
                                {
                                    html = html.Replace("&lt;$" + variable.VariableName + "&gt;", suspendNotes);
                                    html = html.Replace("<$" + variable.VariableName + ">", suspendNotes);
                                }
                            }

                            if (ctx.Users.Any(u => u.Merchant.RecordId == merchant.RecordId))
                            {
                                IQueryable<ApplicationUser> userList = ctx.Users.Where(u => u.Merchant.RecordId == merchant.RecordId);

                                if (userList != null)
                                {
                                    foreach (ApplicationUser thisUser in userList)
                                    {
                                        if (html != null)
                                        {
                                            emailSent = messaging.SendEmail(thisUser.Email, template.EmailSubject, html, template, merchant, thisUser);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    lblMerchantActionMessage.Text = "Merchant Suspended Successfully.";

                    UpdateStatusLabel();

                    txtMerchantSuspensionNotes.Text = "";
                    ddlSuspendReasons.SelectedIndex = -1;


                }
            }
            catch (System.Exception ex)
            {
                lblMerchantActionMessage.Text = "Merchant was unable to be Suspeded.  Please contact your system administrator for more information.";
                _newLogic.WriteExceptionToDB(ex, "btnSuspendMerchant_Click");
            }
        }

        protected void btnReinstateMerchant_Click(object sender, EventArgs e)
        {
            try
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    int merchantId;
                    Int32.TryParse(hMerchantRecordId.Value, out merchantId);
                    string adminId = Context.User.Identity.GetUserId();

                    RadTextBox txtMerchantReinstateNotes = (RadTextBox)StatusView.FindControl("txtMerchantReinstateNotes");

                    if (ctx.Merchants.Any(m => m.RecordId == merchantId))
                    {
                        MerchantModel merchant = ctx.Merchants.First(m => m.RecordId == merchantId);

                        if (ctx.MerchantSuspensions.Any(ms => ms.Merchant.RecordId == merchant.RecordId && ms.ReinstatedDate == null))
                        {
                            MerchantSuspensionModel activeSuspension = ctx.MerchantSuspensions.Where(ms => ms.Merchant.RecordId == merchant.RecordId && ms.ReinstatedDate == null).OrderByDescending(ms => ms.Timestamp).First();

                            activeSuspension.ReinstatedDate = DateTime.UtcNow.Date;
                            activeSuspension.ReinstatedBy = ctx.Users.First(u => u.Id == adminId);
                            activeSuspension.ReinstateNotes = txtMerchantReinstateNotes.Text;

                            merchant.MerchantStatus = ctx.MerchantStatuses.First(ms => ms.StatusDescription == "Active");

                            ctx.SaveChanges();

                            StatusChangeModel statusChange = new StatusChangeModel
                            {
                                AdminUser = ctx.Users.First(u => u.Id == adminId),
                                Merchant = merchant,
                                MerchantStatus = merchant.MerchantStatus,
                                UnderwritingStatus = merchant.UnderwritingStatus,
                                Notes = activeSuspension.ReinstateNotes,
                                Timestamp = DateTime.UtcNow
                            };

                            ctx.StatusChanges.Add(statusChange);

                            ctx.SaveChanges();

                            //Send Email
                            Logic.Messaging messaging = new Logic.Messaging();
                            Boolean emailSent = false;
                            var template = messaging.GetTemplate("MerchantReinstated");

                            if (template != null)
                            {
                                String html = messaging.GetTemplateHtml(template);

                                foreach (EmailTemplateVariableModel variable in messaging.GetRequiredVariables(template))
                                {
                                    if (merchant.MerchantPrincipal != null && merchant.MerchantPrincipal.Contact != null)
                                    {
                                        if (variable.VariableName == "MERCHANT_FIRST_NAME")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.MerchantPrincipal.Contact.FirstName);
                                            html = html.Replace("<$" + variable.VariableName + ">", merchant.MerchantPrincipal.Contact.FirstName);
                                        }
                                        if (variable.VariableName == "MERCHANT_LAST_NAME")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.MerchantPrincipal.Contact.LastName);
                                            html = html.Replace("<$" + variable.VariableName + ">", merchant.MerchantPrincipal.Contact.LastName);
                                        }
                                    }
                                    else
                                    {
                                        if (variable.VariableName == "MERCHANT_FIRST_NAME")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                            html = html.Replace("<$" + variable.VariableName + ">", "");
                                        }
                                        if (variable.VariableName == "MERCHANT_LAST_NAME")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                            html = html.Replace("<$" + variable.VariableName + ">", "");
                                        }
                                    }
                                    if (variable.VariableName == "MERCHANT_NAME")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.CorpName);
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.CorpName);
                                    }
                                    if (merchant.Business.Address != null)
                                    {
                                        if (variable.VariableName == "MERCHANT_ADDRESS")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.Address ?? "");
                                            html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.Address ?? "");
                                        }
                                        if (variable.VariableName == "MERCHANT_CITY")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.City ?? "");
                                            html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.City ?? "");
                                        }
                                        if (variable.VariableName == "MERCHANT_ZIP")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.Zip ?? "");
                                            html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.Zip ?? "");
                                        }
                                        if (merchant.Business.Address.State != null)
                                        {
                                            if (variable.VariableName == "MERCHANT_STATE")
                                            {
                                                html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.State.Name ?? "");
                                                html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.State.Name ?? "");
                                            }
                                        }
                                        else
                                        {
                                            if (variable.VariableName == "MERCHANT_STATE")
                                            {
                                                html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                                html = html.Replace("<$" + variable.VariableName + ">", "");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (variable.VariableName == "MERCHANT_ADDRESS")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                            html = html.Replace("<$" + variable.VariableName + ">", "");
                                        }
                                        if (variable.VariableName == "MERCHANT_CITY")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                            html = html.Replace("<$" + variable.VariableName + ">", "");
                                        }
                                        if (variable.VariableName == "MERCHANT_STATE")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                            html = html.Replace("<$" + variable.VariableName + ">", "");
                                        }
                                        if (variable.VariableName == "MERCHANT_ZIP")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                            html = html.Replace("<$" + variable.VariableName + ">", "");
                                        }
                                    }
                                    if (variable.VariableName == "APPROVED_ADVANCE_AMOUNT")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.ApprovedAdvanceAmount.ToString());
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.ApprovedAdvanceAmount.ToString());
                                    }
                                }

                                if (ctx.Users.Any(u => u.Merchant.RecordId == merchant.RecordId))
                                {
                                    IQueryable<ApplicationUser> userList = ctx.Users.Where(u => u.Merchant.RecordId == merchant.RecordId);

                                    if (userList != null)
                                    {
                                        foreach (ApplicationUser thisUser in userList)
                                        {
                                            if (html != null)
                                            {
                                                emailSent = messaging.SendEmail(thisUser.Email, template.EmailSubject, html, template, merchant, thisUser);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            lblMerchantActionMessage.Text = "Merchant was unable to be Reinstated.  Please contact your system administrator for more information.";
                        }
                    }

                    lblMerchantActionMessage.Text = "Merchant Reinstated Successfully.";

                    UpdateStatusLabel();


                }
            }
            catch (System.Exception ex)
            {
                lblMerchantActionMessage.Text = "Merchant was unable to be Reinstated.  Please contact your system administrator for more information.";
                _newLogic.WriteExceptionToDB(ex, "btnReinstateMerchant_Click");
            }
        }

        protected void btnCancelMerchant_Click(object sender, EventArgs e)
        {
            try
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    int merchantId;
                    Int32.TryParse(hMerchantRecordId.Value, out merchantId);
                    string adminId = Context.User.Identity.GetUserId();

                    RadTextBox txtMerchantCancellationNotes = (RadTextBox)StatusView.FindControl("txtMerchantCancellationNotes");

                    if (ctx.Merchants.Any(m => m.RecordId == merchantId))
                    {
                        MerchantModel merchant = ctx.Merchants.First(m => m.RecordId == merchantId);

                        merchant.MerchantStatus = ctx.MerchantStatuses.First(ms => ms.StatusDescription == "Cancelled");
                        merchant.UnderwritingStatus = ctx.UnderwritingStatuses.First(us => us.StatusDescription == "Cancelled");
                        merchant.CancelledDate = DateTime.UtcNow;
                        merchant.CancelledBy = ctx.Users.First(u => u.Id == adminId);
                        merchant.CancellationNotes = txtMerchantCancellationNotes.Text;
                        merchant.ApprovedAdvanceAmount = 0;
                        merchant.AvailableAdvanceAmount = 0;

                        ctx.SaveChanges();

                        StatusChangeModel statusChange = new StatusChangeModel
                        {
                            AdminUser = ctx.Users.First(u => u.Id == adminId),
                            Merchant = merchant,
                            MerchantStatus = merchant.MerchantStatus,
                            UnderwritingStatus = merchant.UnderwritingStatus,
                            Notes = merchant.CancellationNotes,
                            Timestamp = DateTime.UtcNow
                        };

                        ctx.StatusChanges.Add(statusChange);

                        ctx.SaveChanges();

                        //Send Email
                        Logic.Messaging messaging = new Logic.Messaging();
                        Boolean emailSent = false;
                        var template = messaging.GetTemplate("CancelledMerchant");

                        if (template != null)
                        {
                            String html = messaging.GetTemplateHtml(template);

                            foreach (EmailTemplateVariableModel variable in messaging.GetRequiredVariables(template))
                            {
                                if (merchant.MerchantPrincipal != null && merchant.MerchantPrincipal.Contact != null)
                                {
                                    if (variable.VariableName == "MERCHANT_FIRST_NAME")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.MerchantPrincipal.Contact.FirstName);
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.MerchantPrincipal.Contact.FirstName);
                                    }
                                    if (variable.VariableName == "MERCHANT_LAST_NAME")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.MerchantPrincipal.Contact.LastName);
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.MerchantPrincipal.Contact.LastName);
                                    }
                                }
                                else
                                {
                                    if (variable.VariableName == "MERCHANT_FIRST_NAME")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                    if (variable.VariableName == "MERCHANT_LAST_NAME")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                }
                                if (variable.VariableName == "MERCHANT_NAME")
                                {
                                    html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.CorpName);
                                    html = html.Replace("<$" + variable.VariableName + ">", merchant.CorpName);
                                }
                                if (merchant.Business.Address != null)
                                {
                                    if (variable.VariableName == "MERCHANT_ADDRESS")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.Address ?? "");
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.Address ?? "");
                                    }
                                    if (variable.VariableName == "MERCHANT_CITY")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.City ?? "");
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.City ?? "");
                                    }
                                    if (variable.VariableName == "MERCHANT_ZIP")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.Zip ?? "");
                                        html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.Zip ?? "");
                                    }
                                    if (merchant.Business.Address.State != null)
                                    {
                                        if (variable.VariableName == "MERCHANT_STATE")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.Business.Address.State.Name ?? "");
                                            html = html.Replace("<$" + variable.VariableName + ">", merchant.Business.Address.State.Name ?? "");
                                        }
                                    }
                                    else
                                    {
                                        if (variable.VariableName == "MERCHANT_STATE")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                            html = html.Replace("<$" + variable.VariableName + ">", "");
                                        }
                                    }
                                }
                                else
                                {
                                    if (variable.VariableName == "MERCHANT_ADDRESS")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                    if (variable.VariableName == "MERCHANT_CITY")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                    if (variable.VariableName == "MERCHANT_STATE")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                    if (variable.VariableName == "MERCHANT_ZIP")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                        html = html.Replace("<$" + variable.VariableName + ">", "");
                                    }
                                }
                                if (variable.VariableName == "CANCELLATION_NOTES")
                                {
                                    html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.CancellationNotes);
                                    html = html.Replace("<$" + variable.VariableName + ">", merchant.CancellationNotes);
                                }
                            }

                            if (ctx.Users.Any(u => u.Merchant.RecordId == merchant.RecordId))
                            {
                                IQueryable<ApplicationUser> userList = ctx.Users.Where(u => u.Merchant.RecordId == merchant.RecordId);

                                if (userList != null)
                                {
                                    foreach (ApplicationUser thisUser in userList)
                                    {
                                        if (html != null)
                                        {
                                            emailSent = messaging.SendEmail(thisUser.Email, template.EmailSubject, html, template, merchant, thisUser);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    lblMerchantActionMessage.Text = "Merchant Cancelled Successfully.";

                    UpdateStatusLabel();
                }
            }
            catch (System.Exception ex)
            {
                lblMerchantActionMessage.Text = "Merchant was unable to be Cancelled.  Please contact your system administrator for more information.";
                _newLogic.WriteExceptionToDB(ex, "btnCancelMerchant_Click");
            }
        }

        protected void txtApprovedAdvanceAmount_PreRender(object sender, EventArgs e)
        {
            try
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    int merchantId;
                    Int32.TryParse(hMerchantRecordId.Value, out merchantId);

                    if (ctx.Merchants.Any(m => m.RecordId == merchantId))
                    {
                        MerchantModel merchant = ctx.Merchants.First(m => m.RecordId == merchantId);

                        if (merchant != null)
                        {
                            if (ctx.AdvancePlans.Any(ap => ap.RecordId == merchant.AdvancePlan.RecordId))
                            {
                                AdvancePlanModel advancePlan = ctx.AdvancePlans.First(ap => ap.RecordId == merchant.AdvancePlan.RecordId);

                                if (advancePlan != null)
                                {
                                    Double maxAmount = 0;
                                    Double.TryParse(advancePlan.MaximumAdvanceAmount.ToString(), out maxAmount);
                                    txtApprovedAdvanceAmount.MaxValue = maxAmount;
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "txtApprovedAdvanceAmount_PreRender");
            }
        }

        protected void StatusView_PreRender(object sender, EventArgs e)
        {
            MerchantModel editMerchant = new MerchantModel();
            Int32 merchantRecordId;
            UnderwritingResultModel underwritingResult = new UnderwritingResultModel();

            try
            {
                rptrStatusChanges.DataBind();

                merchantRecordId = Convert.ToInt32(hMerchantRecordId.Value);

                if (lblStatusDescription.Text.ToLower() == "pre-enrolled")
                {
                    phQuickActions.Visible = false;
                    lblMerchantActionMessage.Text = "No Actions can be performed on a Pre-Enrolled merchant.";
                }
                else if (lblStatusDescription.Text.ToLower() == "enrolled")
                {
                    phDenyMerchant.Visible = true;
                    phDenyDisallowed.Visible = false;
                    lblDenyDisallowed.Text = "";

                    phSuspendMerchant.Visible = false;
                    phSuspendDisallowed.Visible = true;
                    lblSuspendDisallowed.Text = "This merchant is still in Enrolled status.  You cannot Suspend a merchant until they are Active.";

                    phReinstateMerchant.Visible = false;
                    phReinstateDisallowed.Visible = true;
                    lblReinstateDisallowed.Text = "You can only Reinstate a merchant if they are in Suspended Status.";

                    phCancelMerchant.Visible = true;
                    phCancelDisallowed.Visible = false;
                    lblCancelDisallowed.Text = "";

                    using (ApplicationDbContext ctx = new ApplicationDbContext())
                    {
                        if (ctx.Merchants.Any(m => m.RecordId == merchantRecordId))
                        {
                            editMerchant = ctx.Merchants.First(m => m.RecordId == merchantRecordId);

                            if (ctx.UnderwritingResults.Any(ur => ur.Merchant.RecordId == merchantRecordId && ur.Active == true))
                            {
                                underwritingResult = ctx.UnderwritingResults.First(ur => ur.Merchant.RecordId == merchantRecordId && ur.Active == true);

                                if (editMerchant.UnderwritingStatus.StatusDescription == "Pending")
                                {
                                    phApproveMerchant.Visible = false;
                                    phApproveDisallowed.Visible = true;
                                    lblApprovalDisallowed.Text = "The Underwriting Status for this merchant is still Pending.  In order to Approve this merchant, the Underwriting Status must be Approved.";
                                }
                                else if (editMerchant.UnderwritingStatus.StatusDescription == "Approved")
                                {
                                    phApproveMerchant.Visible = true;
                                    phApproveDisallowed.Visible = false;
                                    lblApprovalDisallowed.Text = "";
                                }
                                else if (editMerchant.UnderwritingStatus.StatusDescription == "Denied")
                                {
                                    phApproveMerchant.Visible = false;
                                    phApproveDisallowed.Visible = true;
                                    lblApprovalDisallowed.Text = "The Underwriting Status for this merchant is Denied.  In order to Approve this merchant, the Underwriting Status must be Approved.";
                                }
                                else
                                {
                                    phApproveMerchant.Visible = false;
                                    phApproveDisallowed.Visible = true;
                                    lblApprovalDisallowed.Text = "The Underwriting Status for this merchant is Outdated.  In order to Approve this merchant, please renew the Underwriting results.";
                                }

                            }
                            else
                            {
                                phApproveMerchant.Visible = false;
                                phApproveDisallowed.Visible = true;
                                lblApprovalDisallowed.Text = "This merchant has not been underwritten.  In order to Approve this merchant, please Underwrite them first.";

                            }
                        }
                    }
                }
                else if (lblStatusDescription.Text.ToLower() == "active")
                {
                    phApproveMerchant.Visible = false;
                    phApproveDisallowed.Visible = true;
                    lblApprovalDisallowed.Text = "This merchant is already Active.";

                    phDenyMerchant.Visible = false;
                    phDenyDisallowed.Visible = true;
                    lblDenyDisallowed.Text = "This merchant is already Active.  You can Suspend or Cancel them if necessary.";

                    phSuspendMerchant.Visible = true;
                    phSuspendDisallowed.Visible = false;
                    lblSuspendDisallowed.Text = "";

                    phReinstateMerchant.Visible = false;
                    phReinstateDisallowed.Visible = true;
                    lblReinstateDisallowed.Text = "You can only Reinstate a merchant if they are in Suspended Status.";

                    phCancelMerchant.Visible = true;
                    phCancelDisallowed.Visible = false;
                    lblCancelDisallowed.Text = "";
                }
                else if (lblStatusDescription.Text.ToLower() == "suspended")
                {
                    phApproveMerchant.Visible = false;
                    phApproveDisallowed.Visible = true;
                    lblApprovalDisallowed.Text = "This merchant is Suspended.  To end the merchant's suspension, Reinstate them.";

                    phDenyMerchant.Visible = false;
                    phDenyDisallowed.Visible = true;
                    lblDenyDisallowed.Text = "This merchant cannot be Denied.  You can Cancel them if necessary.";

                    phSuspendMerchant.Visible = false;
                    phSuspendDisallowed.Visible = true;
                    lblSuspendDisallowed.Text = "This merchant is already suspended.";

                    phReinstateMerchant.Visible = true;
                    phReinstateDisallowed.Visible = false;
                    lblReinstateDisallowed.Text = "";

                    phCancelMerchant.Visible = true;
                    phCancelDisallowed.Visible = false;
                    lblCancelDisallowed.Text = "";
                }
                else if (lblStatusDescription.Text.ToLower() == "cancelled")
                {
                    phQuickActions.Visible = false;
                    lblMerchantActionMessage.Text = "No Actions can be performed on a Cancelled merchant.";
                }
                else if (lblStatusDescription.Text.ToLower() == "denied")
                {
                    phQuickActions.Visible = false;
                    lblMerchantActionMessage.Text = "No Actions can be performed on a Denied merchant.";
                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "StatusView_PreRender");
            }
        }

        protected void tabStripMerchantProfile_TabClick(object sender, RadTabStripEventArgs e)
        {
            try
            {
                if (e.Tab.Text == "Status")
                {
                    pnlBasicButtons.Visible = false;
                    pnlMessagingButtons.Visible = false;
                    pnlUnderwritingButtons.Visible = false;
                    pnlUserButtons.Visible = false;
                }
                else if (e.Tab.Text == "Users")
                {
                    pnlBasicButtons.Visible = false;
                    pnlMessagingButtons.Visible = false;
                    pnlUnderwritingButtons.Visible = false;
                    pnlUserButtons.Visible = true;
                }
                else
                {
                    pnlBasicButtons.Visible = true;
                    pnlMessagingButtons.Visible = false;
                    pnlUnderwritingButtons.Visible = false;
                    pnlUserButtons.Visible = false;
                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "tabStripMerchantProfile_TabClick");
            }
        }

        protected void DashboardView_PreRender(object sender, EventArgs e)
        {
            MerchantModel editMerchant = new MerchantModel();
            Int32 merchantRecordId;

            try
            {
                merchantRecordId = Convert.ToInt32(hMerchantRecordId.Value);

                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    MerchantModel merchant = ctx.Merchants.First(m => m.RecordId == merchantRecordId);

                    if (merchant != null)
                    {
                        if (merchant.MerchantStatus != null) { txtMDStatus.Text = merchant.MerchantStatus.StatusDescription; }
                        if (merchant.UnderwritingStatus != null) { txtMDUnderwritingStatus.Text = merchant.UnderwritingStatus.StatusDescription; }
                        txtMDApprovedAmount.Text = "$" + merchant.ApprovedAdvanceAmount.ToString();
                        txtMDAvailableAmount.Text = "$" + merchant.AvailableAdvanceAmount.ToString();

                        if (ctx.Advances
                            .Any(a => a.Merchant.RecordId == merchant.RecordId
                                && a.Status.StatusDescription == "Approved"
                                && a.FundingStatus.StatusDescription == "Approved"))
                        {
                            txtMDTotalAdvances.Text = "$" + ctx.Advances
                                .Where(a => a.Merchant.RecordId == merchant.RecordId
                                && a.Status.StatusDescription == "Approved"
                                && a.FundingStatus.StatusDescription == "Approved")
                                .Sum(a => a.PrincipalAmount).ToString();
                        }
                        else { txtMDTotalAdvances.Text = "$0.00"; }


                        if (ctx.Advances
                            .Any(a => a.Merchant.RecordId == merchant.RecordId
                                && a.Status.StatusDescription == "Approved"
                                && a.FundingStatus.StatusDescription == "Approved"))
                        {
                            txtMDTotalAdvanceFees.Text = "$" + ctx.Advances
                                .Where(a => a.Merchant.RecordId == merchant.RecordId
                                && a.Status.StatusDescription == "Approved"
                                && a.FundingStatus.StatusDescription == "Approved")
                                .Sum(a => a.FeeAmount).ToString();
                        }
                        else { txtMDTotalAdvanceFees.Text = "$0.00"; }

                        if (ctx.Advances
                            .Any(a => a.Merchant.RecordId == merchant.RecordId
                                && a.Status.StatusDescription == "Approved"
                                && a.FundingStatus.StatusDescription == "Approved"))
                        {
                            txtMDTotalRepaid.Text = "$" + ctx.Advances
                                .Where(a => a.Merchant.RecordId == merchant.RecordId
                                && a.Status.StatusDescription == "Approved"
                                && a.FundingStatus.StatusDescription == "Approved")
                                .Sum(a => (a.PrincipalAmount + a.FeeAmount) - (a.PrincipalBalance + a.FeeBalance)).ToString();
                        }
                        else { txtMDTotalRepaid.Text = "$0.00"; }

                        if (ctx.Advances
                            .Any(a => a.Merchant.RecordId == merchant.RecordId
                                && a.Status.StatusDescription == "Approved"
                                && a.FundingStatus.StatusDescription == "Approved"))
                        {
                            txtMDTotalDue.Text = "$" + ctx.Advances
                                .Where(a => a.Merchant.RecordId == merchant.RecordId
                                && a.Status.StatusDescription == "Approved"
                                && a.FundingStatus.StatusDescription == "Approved")
                                .Sum(a => (a.PrincipalBalance + a.FeeBalance)).ToString();
                        }
                        else { txtMDTotalDue.Text = "$0.00"; }

                        if (ctx.AdvancePayments
                            .Any(ap => ap.Advance.Merchant.RecordId == merchant.RecordId
                                && ap.Advance.Status.StatusDescription == "Approved"
                                && ap.Advance.FundingStatus.StatusDescription == "Approved"
                                && ap.Status.StatusDescription != "Pending"))
                        {
                            txtMDLastPaymentDate.Text = ctx.AdvancePayments
                                .Where(ap => ap.Advance.Merchant.RecordId == merchant.RecordId
                                && ap.Advance.Status.StatusDescription == "Approved"
                                && ap.Advance.FundingStatus.StatusDescription == "Approved"
                                && ap.Status.StatusDescription != "Pending")
                                .OrderByDescending(ap => ap.Timestamp)
                                .First().Timestamp.ToShortDateString();
                        }
                        else { txtMDLastPaymentDate.Text = "N/A"; }

                        if (ctx.AdvancePayments
                            .Any(ap => ap.Advance.Merchant.RecordId == merchant.RecordId
                            && ap.Advance.Status.StatusDescription == "Approved"
                            && ap.Advance.FundingStatus.StatusDescription == "Approved"
                            && ap.Status.StatusDescription != "Pending"))
                        {
                            txtMDLastPaymentAmount.Text = "$" + ctx.AdvancePayments
                                .Where(ap => ap.Advance.Merchant.RecordId == merchant.RecordId
                                && ap.Advance.Status.StatusDescription == "Approved"
                                && ap.Advance.FundingStatus.StatusDescription == "Approved"
                                && ap.Status.StatusDescription != "Pending")
                                .OrderByDescending(ap => ap.Timestamp)
                                .First().ActualTotalAmount.Value.ToString();
                        }
                        else { txtMDLastPaymentAmount.Text = "N/A"; }

                        if (ctx.Advances
                            .Any(a => a.Merchant.RecordId == merchant.RecordId
                            && (a.Status.StatusDescription == "Pending"
                                || a.FundingStatus.StatusDescription == "Pending"
                                || a.FundingStatus.StatusDescription == "Rejected"
                                || a.FundingStatus.StatusDescription == "Need Alternate Funding Method")
                                ))
                        {
                            txtMDPendingAdvances.Text = "$" + ctx.Advances
                                .Where(a => a.Merchant.RecordId == merchant.RecordId
                                && (a.Status.StatusDescription == "Pending" || a.FundingStatus.StatusDescription == "Pending" || a.FundingStatus.StatusDescription == "Rejected" || a.FundingStatus.StatusDescription == "Need Alternate Funding Method"))
                                .Sum(a => a.PrincipalBalance).ToString();
                        }
                        else { txtMDPendingAdvances.Text = "$0.00"; }

                        if (ctx.Advances
                            .Any(a => a.Merchant.RecordId == merchant.RecordId
                            && a.Status.StatusDescription == "Approved"
                            && a.FundingStatus.StatusDescription == "Approved"))
                        {
                            txtMDNextPaymentAmount.Text = "$" + ctx.Advances
                                .Where(a => a.Merchant.RecordId == merchant.RecordId
                                && a.Status.StatusDescription == "Approved"
                                && a.FundingStatus.StatusDescription == "Approved")
                                .Sum(a => a.NextPaymentAmount).ToString();
                        }
                        else { txtMDNextPaymentAmount.Text = "$0.00"; }

                        if (merchant.MerchantStatus.StatusDescription == "Suspended")
                        {
                            phSuspension.Visible = true;

                            if (ctx.MerchantSuspensions.Any(ms => ms.Merchant.RecordId == merchant.RecordId
                                && ms.ReinstatedBy == null
                                && ms.ReinstatedDate == null
                                && ms.ReinstateNotes == null
                                ))
                            {
                                var suspension = ctx.MerchantSuspensions
                                    .OrderByDescending(ms => ms.Timestamp)
                                    .First(ms => ms.Merchant.RecordId == merchant.RecordId
                                    && ms.ReinstatedBy == null
                                    && ms.ReinstatedDate == null
                                    && ms.ReinstateNotes == null);

                                txtSuspensionReason.Text = suspension.SuspensionReason.ReasonName;
                                txtSuspensionNotes.Text = suspension.OtherReasonNotes;
                                txtSuspensionAdmin.Text = suspension.SuspendedBy.UserName;
                                txtSuspensionTimestamp.Text = suspension.Timestamp.ToShortDateString();
                            }
                        }
                        else
                        {
                            phSuspension.Visible = false;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "DashboardView_PreRender");
            }
        }

        protected void gridDailySettlements_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    RadToolTip ttPaymentDetails = (RadToolTip)item.FindControl("ttPaymentDetails");
                    Label lblRecordId = (Label)item.FindControl("lblRecordId");
                    StringBuilder ttText = new StringBuilder();

                    using (ApplicationDbContext ctx = new ApplicationDbContext())
                    {
                        Int32 settlementId = 0;
                        Int32.TryParse(lblRecordId.Text, out settlementId);

                        if (ctx.DailySettlementAdvance.Any(dsa => dsa.DailySettlement.RecordId == settlementId))
                        {
                            ttText.Append("<div style=\"width:300px\"><table class=\"tableEditAppForm\"><tr><td colspan=\"2\" class=\"editFormNames\">Advances Paid with This Settlement</td>");

                            IQueryable<DailySettlementAdvanceModel> settlementAdvances = ctx.DailySettlementAdvance.Where(dsa => dsa.DailySettlement.RecordId == settlementId);

                            if (settlementAdvances != null)
                            {
                                foreach (DailySettlementAdvanceModel dsa in settlementAdvances)
                                {
                                    ttText.Append("<tr><td class=\"leftFormValues\">Advance ID: <span style=\"font-weight:bold\">").Append(dsa.Advance.RecordId.ToString()).Append("</span></td>");
                                    ttText.Append("<td class=\"leftFormValues\">Amount Paid: <span style=\"font-weight:bold\">$").Append(dsa.Amount.ToString()).Append("</span></td></tr>");
                                }
                            }

                            ttText.Append("</table>");
                        }
                        else
                        {
                            ttText.Append("<div style=\"width:300px; font-weight:bold\">No Advances were paid with this Settlement.");
                        }
                    }

                    ttText.Append("</div>");
                    ttPaymentDetails.Text = ttText.ToString();
                }

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "gridDailySettlements_ItemDataBound");
            }
        }

        protected void cvLicenseDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ddlLicenseDateYear.SelectedIndex == -1 && ddlLicenseDateMonth.SelectedIndex == -1 && ddlLicenseDateDay.SelectedIndex == -1)
            {
                args.IsValid = true;
            }
            else
            {
                if (ddlLicenseDateYear.SelectedIndex != -1 && ddlLicenseDateMonth.SelectedIndex != -1 && ddlLicenseDateDay.SelectedIndex != -1)
                {
                    Int32 year = Convert.ToInt32(ddlLicenseDateYear.SelectedValue);
                    Int32 month = Convert.ToInt32(ddlLicenseDateMonth.SelectedValue);
                    Int32 day = Convert.ToInt32(ddlLicenseDateDay.SelectedValue);
                    DateTime licenseDate;

                    args.IsValid = (DateTime.TryParse(year + "-" + month + "-" + day, out licenseDate));
                }
                else
                {
                    args.IsValid = false;
                }
            }
        }

        protected void cvDoB_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ddlBirthYear.SelectedIndex == -1 && ddlBirthMonth.SelectedIndex == -1 && ddlBirthDay.SelectedIndex == -1)
            {
                args.IsValid = true;
            }
            else
            {
                if (ddlBirthYear.SelectedIndex != -1 && ddlBirthMonth.SelectedIndex != -1 && ddlBirthDay.SelectedIndex != -1)
                {
                    Int32 year = Convert.ToInt32(ddlBirthYear.SelectedValue);
                    Int32 month = Convert.ToInt32(ddlBirthMonth.SelectedValue);
                    Int32 day = Convert.ToInt32(ddlBirthDay.SelectedValue);
                    DateTime birthDate;

                    args.IsValid = (DateTime.TryParse(year + "-" + month + "-" + day, out birthDate));
                }
                else
                {
                    args.IsValid = false;
                }
            }
        }

        protected void cvHighRiskDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (rblHighRisk.SelectedValue == "True")
            {
                if (ddlHighRiskYear.SelectedIndex != -1 && ddlHighRiskMonth.SelectedIndex != -1 && ddlHighRiskDay.SelectedIndex != -1)
                {
                    Int32 year = Convert.ToInt32(ddlHighRiskYear.SelectedValue);
                    Int32 month = Convert.ToInt32(ddlHighRiskMonth.SelectedValue);
                    Int32 day = Convert.ToInt32(ddlHighRiskDay.SelectedValue);
                    DateTime highRiskDate;

                    args.IsValid = (DateTime.TryParse(year + "-" + month + "-" + day, out highRiskDate));
                }
                else
                {
                    args.IsValid = false;
                }
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void cvBankruptcyDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (rblBankruptcy.SelectedValue == "True")
            {
                if (ddlBankruptcyYear.SelectedIndex != -1 && ddlBankruptcyMonth.SelectedIndex != -1 && ddlBankruptcyDay.SelectedIndex != -1)
                {
                    Int32 year = Convert.ToInt32(ddlBankruptcyYear.SelectedValue);
                    Int32 month = Convert.ToInt32(ddlBankruptcyMonth.SelectedValue);
                    Int32 day = Convert.ToInt32(ddlBankruptcyDay.SelectedValue);
                    DateTime bankruptcyDate;

                    args.IsValid = (DateTime.TryParse(year + "-" + month + "-" + day, out bankruptcyDate));
                }
                else
                {
                    args.IsValid = false;
                }
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void cvExpDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ddlExpYear.SelectedIndex != -1 && ddlExpMonth.SelectedIndex != -1)
            {
                Int32 daysInMonth = DateTime.DaysInMonth(Convert.ToInt32(ddlExpYear.SelectedText), Convert.ToInt32(ddlExpMonth.SelectedIndex + 1));
                DateTime expDate = new DateTime(Convert.ToInt32(ddlExpYear.SelectedText), Convert.ToInt32(ddlExpMonth.SelectedIndex + 1), daysInMonth);

                if (DateTime.Now >= expDate)
                {
                    args.IsValid = false;
                }
                else
                {
                    args.IsValid = true;
                }
            }
        }

        protected void cvHighRiskWho_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (rblHighRisk.SelectedValue == "True")
            {
                args.IsValid = (!String.IsNullOrEmpty(txtHighRiskWho.Text));
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void rblBankruptcy_DataBinding(object sender, EventArgs e)
        {
            if (DataBinder.Eval(DataItem, "Bankruptcy") != null)
            {
                rblBankruptcy.SelectedValue = DataBinder.Eval(DataItem, "Bankruptcy").ToString();
            }
        }

        protected void rblHighRisk_DataBinding(object sender, EventArgs e)
        {
            if (DataBinder.Eval(DataItem, "HighRisk") != null)
            {
                rblHighRisk.SelectedValue = DataBinder.Eval(DataItem, "HighRisk").ToString();
            }
        }


        //Determine if any changes have been made to the merchant details
        public void ConfirmChanges()
        {
            pnlEditMerchant.Visible = false;
            pnlConfirmSave.Visible = true;
            pnlBasicButtons.Visible = false;
            pnlAdvancedButtons.Visible = true;

            ArrayList arrBusinessChanges = new ArrayList();
            ArrayList arrPrincipalChanges = new ArrayList();
            ArrayList arrContactChanges = new ArrayList();
            ArrayList arrBankingChanges = new ArrayList();
            ArrayList arrMerchantAccountChanges = new ArrayList();
            ArrayList arrAdvancePlanChanges = new ArrayList();

            RadPanelItem businessItem = pbMerchantChanges.FindItemByValue("Business");
            RadPanelItem principalItem = pbMerchantChanges.FindItemByValue("Principal");
            RadPanelItem contactItem = pbMerchantChanges.FindItemByValue("Contact");
            RadPanelItem bankingItem = pbMerchantChanges.FindItemByValue("Banking");
            RadPanelItem merchantAccountItem = pbMerchantChanges.FindItemByValue("MerchantAccount");
            RadPanelItem advancePlanItem = pbMerchantChanges.FindItemByValue("AdvancePlan");

            try
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    int accountNumber = Convert.ToInt32(hMerchantRecordId.Value);

                    MerchantModel editMerchant = ctx.Merchants.First(m => m.RecordId == accountNumber);

                    //Business Changes
                    DetermineBusinessChanges(editMerchant, arrBusinessChanges);

                    //Principal Changes
                    DeterminePrincipalChanges(editMerchant, arrPrincipalChanges);

                    //Contact Changes
                    DetermineContactChanges(editMerchant, arrContactChanges);

                    //Banking Changes
                    DetermineBankingChanges(editMerchant, arrBankingChanges);

                    //Merchant Account Changes
                    DetermineMerchantAccountChanges(editMerchant, arrMerchantAccountChanges);

                    //Advance Plan Changes
                    DetermineAdvancePlanChanges(editMerchant, arrAdvancePlanChanges);

                }

                //Bind Advance Plan Changes Array to Advnace Plan Changes Repeater
                rptrAdvancePlanChanges.DataSource = arrAdvancePlanChanges;
                rptrAdvancePlanChanges.DataBind();

                //Bind Merchant Account Changes Array to Merchant Account Changes Repeater
                rptrMerchantAccountChanges.DataSource = arrMerchantAccountChanges;
                rptrMerchantAccountChanges.DataBind();

                //Bind Contact Changes Array to Contact Changes Repeater
                rptrContactChanges.DataSource = arrContactChanges;
                rptrContactChanges.DataBind();

                //Bind Principal Changes Array to Principal Changes Repeater
                rptrPrincipalChanges.DataSource = arrPrincipalChanges;
                rptrPrincipalChanges.DataBind();

                //Bind Business Changes Array to Business Changes Repeater
                rptrBusinessChanges.DataSource = arrBusinessChanges;
                rptrBusinessChanges.DataBind();

                //Bind Banking Changes Array to Banking Changes Repeater
                rptrBankingChanges.DataSource = arrBankingChanges;
                rptrBankingChanges.DataBind();

                //Determine if changes were made to any of the sections.
                if (arrBankingChanges.Count == 0 && arrBusinessChanges.Count == 0 && arrContactChanges.Count == 0 && arrPrincipalChanges.Count == 0
                    && arrMerchantAccountChanges.Count == 0 && arrAdvancePlanChanges.Count == 0)
                {
                    pnlChangesExist.Visible = false;
                    pnlNoChangesExist.Visible = true;
                    pbMerchantChanges.Visible = false;
                    btnApplyChanges.Visible = false;
                }
                else
                {
                    pnlChangesExist.Visible = true;
                    pnlNoChangesExist.Visible = false;
                    pbMerchantChanges.Visible = true;
                    btnApplyChanges.Visible = true;

                    businessItem.Enabled = arrBusinessChanges.Count == 0 ? false : true;
                    principalItem.Enabled = arrPrincipalChanges.Count == 0 ? false : true;
                    contactItem.Enabled = arrContactChanges.Count == 0 ? false : true;
                    bankingItem.Enabled = arrBankingChanges.Count == 0 ? false : true;
                    merchantAccountItem.Enabled = arrMerchantAccountChanges.Count == 0 ? false : true;
                    advancePlanItem.Enabled = arrAdvancePlanChanges.Count == 0 ? false : true;


                    if (pbMerchantChanges.Items.Count > 0)
                    {
                        foreach (RadPanelItem panelItem in pbMerchantChanges.Items)
                        {
                            if (panelItem.Enabled == true)
                            {
                                panelItem.Expanded = true;
                                break;
                            }
                        }
                    }
                }


            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "ConfirmChanges");
            }
        }

        private void DetermineBusinessChanges(MerchantModel editMerchant, ArrayList arrBusinessChanges)
        {
            if (editMerchant.LegalOrgType != null)
            {
                if (Convert.ToInt32(ddlLegalOrgType.SelectedValue) != editMerchant.LegalOrgType.RecordId)
                {
                    string oldValue = "";

                    oldValue = editMerchant.LegalOrgType.LegalOrgTypeName;

                    arrBusinessChanges.Add(new MerchantChanges("Legal Org Type", oldValue, ddlLegalOrgType.SelectedText));
                }
            }
            else if (editMerchant.LegalOrgType == null && ddlLegalOrgType.SelectedValue != "")
            {
                string oldValue = "";

                arrBusinessChanges.Add(new MerchantChanges("Legal Org Type", oldValue, ddlLegalOrgType.SelectedText));
            }



            if (editMerchant.LegalOrgState != null)
            {
                if (Convert.ToInt32(ddlLegalOrgState.SelectedValue) != editMerchant.LegalOrgState.RecordId)
                {
                    string oldValue = "";

                    oldValue = editMerchant.LegalOrgState.Name;

                    arrBusinessChanges.Add(new MerchantChanges("Legal Org State", oldValue, ddlLegalOrgState.SelectedText));
                }
            }
            else if (editMerchant.LegalOrgState == null && ddlLegalOrgState.SelectedValue != "")
            {
                string oldValue = "";

                arrBusinessChanges.Add(new MerchantChanges("Legal Org State", oldValue, ddlLegalOrgState.SelectedText));
            }



            if (editMerchant.MerchantType != null)
            {
                if (rblMerchantType.SelectedValue != editMerchant.MerchantType.MerchantTypeName)
                {
                    string oldValue = "";

                    oldValue = editMerchant.MerchantType.MerchantTypeName;

                    arrBusinessChanges.Add(new MerchantChanges("Merchant Type", oldValue, rblMerchantType.SelectedValue));
                }
            }
            else if (editMerchant.MerchantType == null && rblMerchantType.SelectedValue != "")
            {
                string oldValue = "";

                arrBusinessChanges.Add(new MerchantChanges("Merchant Type", oldValue, rblMerchantType.SelectedValue));
            }



            if (editMerchant.Mcc != null)
            {
                if (ddlMCC.SelectedValue != editMerchant.Mcc.RecordId.ToString())
                {
                    string oldValue = "";

                    oldValue = editMerchant.Mcc.MerchantCategoryCode;

                    arrBusinessChanges.Add(new MerchantChanges("MCC", oldValue, ddlMCC.SelectedText));
                }
            }
            else if (editMerchant.Mcc == null && ddlMCC.SelectedValue != "")
            {
                string oldValue = "";

                arrBusinessChanges.Add(new MerchantChanges("MCC", oldValue, ddlMCC.SelectedValue));
            }



            if (txtCorpName.Text != _newLogic.Coalesce(editMerchant.CorpName))
            {
                string oldValue = "";

                oldValue = editMerchant.CorpName;

                arrBusinessChanges.Add(new MerchantChanges("Legal Name", oldValue, txtCorpName.Text));
            }



            if (txtDBAName.Text != _newLogic.Coalesce(editMerchant.DbaName))
            {
                string oldValue = "";

                oldValue = editMerchant.DbaName;

                arrBusinessChanges.Add(new MerchantChanges("DBA Name", oldValue, txtDBAName.Text));
            }



            if (txtCorpAddress.Text != _newLogic.Coalesce(editMerchant.Business.Address.Address))
            {
                string oldValue = "";

                oldValue = editMerchant.Business.Address.Address;

                arrBusinessChanges.Add(new MerchantChanges("Business Address", oldValue, txtCorpAddress.Text));
            }



            if (txtCorpCity.Text != _newLogic.Coalesce(editMerchant.Business.Address.City))
            {
                string oldValue = "";

                oldValue = editMerchant.Business.Address.City;

                arrBusinessChanges.Add(new MerchantChanges("Business City", oldValue, txtCorpCity.Text));
            }



            if (editMerchant.Business.Address.State != null)
            {
                if (ddlCorpState.SelectedValue != editMerchant.Business.Address.State.RecordId.ToString())
                {
                    string oldValue = "";

                    oldValue = editMerchant.Business.Address.State.Name;

                    arrBusinessChanges.Add(new MerchantChanges("Business State", oldValue, ddlCorpState.SelectedText));
                }
            }
            else if (ddlCorpState.SelectedValue != "")
            {
                string oldValue = "";

                arrBusinessChanges.Add(new MerchantChanges("Business State", oldValue, ddlCorpState.SelectedText));
            }



            if (txtCorpZip.Text != _newLogic.Coalesce(editMerchant.Business.Address.Zip))
            {
                string oldValue = "";

                oldValue = editMerchant.Business.Address.Zip;

                arrBusinessChanges.Add(new MerchantChanges("Business Zip", oldValue, txtCorpZip.Text));
            }



            if (txtBusLicNumber.Text != _newLogic.Coalesce(editMerchant.BusLicNumber))
            {
                string oldValue = "";

                oldValue = editMerchant.BusLicNumber;

                arrBusinessChanges.Add(new MerchantChanges("Business License #", oldValue, txtBusLicNumber.Text));
            }



            if (txtBusLicType.Text != _newLogic.Coalesce(editMerchant.BusLicType))
            {
                string oldValue = "";

                oldValue = editMerchant.BusLicType;

                arrBusinessChanges.Add(new MerchantChanges("License Type", oldValue, txtBusLicType.Text));
            }



            if (txtBusLicIssuer.Text != _newLogic.Coalesce(editMerchant.BusLicIssuer))
            {
                string oldValue = "";

                oldValue = editMerchant.BusLicIssuer;

                arrBusinessChanges.Add(new MerchantChanges("License Issuer", oldValue, txtBusLicIssuer.Text));
            }



            if (ddlLicenseDateYear.SelectedIndex != -1 && ddlLicenseDateMonth.SelectedIndex != -1 && ddlLicenseDateDay.SelectedIndex != -1)
            {
                Int32 year = Convert.ToInt32(ddlLicenseDateYear.SelectedValue);
                Int32 month = Convert.ToInt32(ddlLicenseDateMonth.SelectedValue);
                Int32 day = Convert.ToInt32(ddlLicenseDateDay.SelectedValue);
                DateTime busLicDate;

                if (DateTime.TryParse(year + "-" + month + "-" + day, out busLicDate))
                {
                    if (editMerchant.BusLicDate.HasValue)
                    {
                        if (_newLogic.Coalesce(busLicDate) != _newLogic.Coalesce(editMerchant.BusLicDate.Value))
                        {
                            string oldValue = "";

                            oldValue = editMerchant.BusLicDate.HasValue ? editMerchant.BusLicDate.Value.ToString("MM/dd/yyyy") : "";

                            arrBusinessChanges.Add(new MerchantChanges("License Date", oldValue, busLicDate.ToString("MM/dd/yyyy")));
                        }
                    }
                    else
                    {
                        string oldValue = "";

                        arrBusinessChanges.Add(new MerchantChanges("License Date", oldValue, busLicDate.ToString("MM/dd/yyyy")));
                    }
                }
            }



            if (txtFedTaxId.Text != _newLogic.Coalesce(editMerchant.FedTaxId))
            {
                string oldValue = "";

                oldValue = editMerchant.FedTaxId;

                arrBusinessChanges.Add(new MerchantChanges("Federal Tax ID", oldValue, txtFedTaxId.Text));
            }



            if (txtBusEmail.Text != _newLogic.Coalesce(editMerchant.Business.Email))
            {
                string oldValue = "";

                oldValue = editMerchant.Business.Email;

                arrBusinessChanges.Add(new MerchantChanges("Email", oldValue, txtBusEmail.Text));
            }



            if (txtBusPhone.Text != _newLogic.Coalesce(editMerchant.Business.HomePhone))
            {
                string oldValue = "";

                oldValue = editMerchant.Business.HomePhone;

                arrBusinessChanges.Add(new MerchantChanges("Phone", oldValue, txtBusPhone.Text));
            }



            if (txtBusFax.Text != _newLogic.Coalesce(editMerchant.Business.Fax))
            {
                string oldValue = "";

                oldValue = editMerchant.Business.Fax;

                arrBusinessChanges.Add(new MerchantChanges("Fax", oldValue, txtBusFax.Text));
            }



            if (txtMerchandiseSold.Text != _newLogic.Coalesce(editMerchant.MerchandiseSold))
            {
                string oldValue = "";

                oldValue = editMerchant.MerchandiseSold;

                arrBusinessChanges.Add(new MerchantChanges("Merchandise Sold", oldValue, txtMerchandiseSold.Text));
            }



            if (txtYearsInBus.Text != _newLogic.Coalesce(editMerchant.YearsInBusiness))
            {
                string oldValue = "";

                oldValue = editMerchant.YearsInBusiness.ToString();

                arrBusinessChanges.Add(new MerchantChanges("Years In Business", oldValue, txtYearsInBus.Text));
            }



            if (txtMonthsInBus.Text != _newLogic.Coalesce(editMerchant.MonthsInBusiness))
            {
                string oldValue = "";

                oldValue = editMerchant.MonthsInBusiness.ToString();

                arrBusinessChanges.Add(new MerchantChanges("Months In Business", oldValue, txtMonthsInBus.Text));
            }



            if (rblSeasonal.SelectedValue != _newLogic.Coalesce(editMerchant.SeasonalSales))
            {
                string oldValue = "";

                oldValue = editMerchant.SeasonalSales.ToString();

                arrBusinessChanges.Add(new MerchantChanges("Seasonal Sales", oldValue, rblSeasonal.SelectedValue));
            }



            //Get String of months selected for seasonal sales
            string seasonalMonths = "";

            seasonalMonths = StringifySeasonalMonths(seasonalMonths);

            if (seasonalMonths != _newLogic.Coalesce(editMerchant.SeasonalMonths))
            {
                string oldValue = "";

                oldValue = editMerchant.SeasonalMonths;

                arrBusinessChanges.Add(new MerchantChanges("Seasonal Months", oldValue, seasonalMonths));
            }
        }

        private void DeterminePrincipalChanges(MerchantModel editMerchant, ArrayList arrPrincipalChanges)
        {
            //Principal Changes
            if (txtPrincipalFirstName.Text != _newLogic.Coalesce(editMerchant.MerchantPrincipal.Contact.FirstName))
            {
                string oldValue = "";

                oldValue = editMerchant.MerchantPrincipal.Contact.FirstName;

                arrPrincipalChanges.Add(new MerchantChanges("First Name", oldValue, txtPrincipalFirstName.Text));
            }



            if (txtPrincipalLastName.Text != _newLogic.Coalesce(editMerchant.MerchantPrincipal.Contact.LastName))
            {
                string oldValue = "";

                oldValue = editMerchant.MerchantPrincipal.Contact.LastName;

                arrPrincipalChanges.Add(new MerchantChanges("Last Name", oldValue, txtPrincipalLastName.Text));
            }



            if (txtPrincipalMI.Text != _newLogic.Coalesce(editMerchant.MerchantPrincipal.Contact.MiddleInitial))
            {
                string oldValue = "";

                oldValue = editMerchant.MerchantPrincipal.Contact.MiddleInitial;

                arrPrincipalChanges.Add(new MerchantChanges("M.I.", oldValue, txtPrincipalMI.Text));
            }


            if (ddlBirthYear.SelectedIndex != -1 && ddlBirthMonth.SelectedIndex != -1 && ddlBirthDay.SelectedIndex != -1)
            {
                Int32 year = Convert.ToInt32(ddlBirthYear.SelectedValue);
                Int32 month = Convert.ToInt32(ddlBirthMonth.SelectedValue);
                Int32 day = Convert.ToInt32(ddlBirthDay.SelectedValue);
                DateTime birthDate;

                if (DateTime.TryParse(year + "-" + month + "-" + day, out birthDate))
                {
                    if (_newLogic.Coalesce(birthDate) != _newLogic.Coalesce(editMerchant.MerchantPrincipal.PrincipalDoB))
                    {
                        string oldValue = "";

                        oldValue = editMerchant.MerchantPrincipal.PrincipalDoB.HasValue ? editMerchant.MerchantPrincipal.PrincipalDoB.Value.ToString("MM/dd/yyyy") : "";

                        arrPrincipalChanges.Add(new MerchantChanges("D.O.B.", oldValue, birthDate.ToString("MM/dd/yyyy")));
                    }
                }
            }


            if (txtPrincipalSSN.Enabled)
            {
                if (editMerchant.MerchantPrincipal.PrincipalSsn != null)
                {
                    if (txtPrincipalSSN.TextWithLiterals != ShowSsn(editMerchant.MerchantPrincipal.PrincipalSsn, editMerchant.MerchantPrincipal.RecordId))
                    {
                        string oldValue = "";

                        oldValue = ShowSsn(editMerchant.MerchantPrincipal.PrincipalSsn, editMerchant.MerchantPrincipal.RecordId);

                        arrPrincipalChanges.Add(new MerchantChanges("S.S.N.", "***-**-" + oldValue.Substring(oldValue.Length - 4, 4), txtPrincipalSSN.TextWithLiterals));
                    }
                }
                else if (txtPrincipalSSN.Text != "")
                {
                    string oldValue = "";

                    arrPrincipalChanges.Add(new MerchantChanges("S.S.N.", oldValue, txtPrincipalSSN.TextWithLiterals));
                }
            }


            if (editMerchant.MerchantPrincipal.PrincipalPctOwn != null)
            {
                if (Convert.ToDecimal(txtPrincipalPctOwn.Text) != editMerchant.MerchantPrincipal.PrincipalPctOwn)
                {
                    string oldValue = "";

                    oldValue = editMerchant.MerchantPrincipal.PrincipalPctOwn.ToString() + " %";

                    arrPrincipalChanges.Add(new MerchantChanges("% Ownership", oldValue, txtPrincipalPctOwn.Text + " %"));
                }
            }
            else if (txtPrincipalPctOwn.Text != "")
            {
                string oldValue = "";

                arrPrincipalChanges.Add(new MerchantChanges("% Ownership", oldValue, txtPrincipalPctOwn.Text + " %"));
            }


            if (txtPrincipalTitle.Text != _newLogic.Coalesce(editMerchant.MerchantPrincipal.Contact.Title))
            {
                string oldValue = "";

                oldValue = editMerchant.MerchantPrincipal.Contact.Title;

                arrPrincipalChanges.Add(new MerchantChanges("Title", oldValue, txtPrincipalTitle.Text));
            }



            if (txtPrincipalDLNumber.Enabled)
            {
                if (editMerchant.MerchantPrincipal.PrincipalDLNumber != null)
                {
                    if (txtPrincipalDLNumber.Text != ShowDLNumber(editMerchant.MerchantPrincipal.PrincipalDLNumber, editMerchant.MerchantPrincipal.RecordId))
                    {
                        string oldValue = "";

                        oldValue = MaskDLNumber(editMerchant.MerchantPrincipal.PrincipalDLNumber, editMerchant.MerchantPrincipal.RecordId);

                        arrPrincipalChanges.Add(new MerchantChanges("DL Number", oldValue, txtPrincipalDLNumber.Text));
                    }
                }
                else if (txtPrincipalDLNumber.Text != "")
                {
                    string oldValue = "";

                    arrPrincipalChanges.Add(new MerchantChanges("DL Number", oldValue, txtPrincipalDLNumber.Text));
                }
            }


            if (editMerchant.MerchantPrincipal.PrincipalDLState != null)
            {
                if (ddlPrincipalDLState.SelectedValue != editMerchant.MerchantPrincipal.PrincipalDLState.RecordId.ToString())
                {
                    string oldValue = "";

                    oldValue = editMerchant.MerchantPrincipal.PrincipalDLState.RecordId.ToString();

                    arrPrincipalChanges.Add(new MerchantChanges("DL State", oldValue, ddlPrincipalDLState.SelectedText));
                }
            }
            else if (editMerchant.MerchantPrincipal.PrincipalDLState == null && ddlPrincipalDLState.SelectedValue != "")
            {
                string oldValue = "";

                arrPrincipalChanges.Add(new MerchantChanges("DL State", oldValue, ddlPrincipalDLState.SelectedText));
            }



            if (editMerchant.MerchantPrincipal.Contact != null)
            {
                if (editMerchant.MerchantPrincipal.Contact.Address == null)
                {
                    if (txtPrincipalAddress.Text != "")
                    {
                        string oldValue = "";

                        arrPrincipalChanges.Add(new MerchantChanges("Address", oldValue, txtPrincipalAddress.Text));
                    }

                    if (txtPrincipalCity.Text != "")
                    {
                        string oldValue = "";

                        arrPrincipalChanges.Add(new MerchantChanges("City", oldValue, txtPrincipalCity.Text));
                    }

                    if (txtPrincipalZip.Text != "")
                    {
                        string oldValue = "";

                        arrPrincipalChanges.Add(new MerchantChanges("Zip", oldValue, txtPrincipalZip.Text));
                    }

                    if (ddlPrincipalState.SelectedIndex != -1)
                    {
                        string oldValue = "";

                        arrPrincipalChanges.Add(new MerchantChanges("State", oldValue, ddlPrincipalState.SelectedText));
                    }
                }
                else
                {
                    if (txtPrincipalAddress.Text != _newLogic.Coalesce(editMerchant.MerchantPrincipal.Contact.Address.Address))
                    {
                        string oldValue = "";

                        oldValue = editMerchant.MerchantPrincipal.Contact.Address.Address;

                        arrPrincipalChanges.Add(new MerchantChanges("Address", oldValue, txtPrincipalAddress.Text));
                    }


                    if (txtPrincipalCity.Text != _newLogic.Coalesce(editMerchant.MerchantPrincipal.Contact.Address.City))
                    {
                        string oldValue = "";

                        oldValue = editMerchant.MerchantPrincipal.Contact.Address.City;

                        arrPrincipalChanges.Add(new MerchantChanges("City", oldValue, txtPrincipalCity.Text));
                    }


                    if (txtPrincipalZip.Text != _newLogic.Coalesce(editMerchant.MerchantPrincipal.Contact.Address.Zip))
                    {
                        string oldValue = "";

                        oldValue = editMerchant.MerchantPrincipal.Contact.Address.Zip;

                        arrPrincipalChanges.Add(new MerchantChanges("Zip", oldValue, txtPrincipalZip.Text));
                    }


                    if (editMerchant.MerchantPrincipal.Contact.Address.State != null)
                    {
                        if (ddlPrincipalState.SelectedValue != editMerchant.MerchantPrincipal.Contact.Address.State.RecordId.ToString())
                        {
                            string oldValue = "";

                            oldValue = editMerchant.MerchantPrincipal.Contact.Address.State.Name.ToString();

                            arrPrincipalChanges.Add(new MerchantChanges("State", oldValue, ddlPrincipalState.SelectedText));
                        }
                    }
                    else if (ddlPrincipalState.SelectedValue != "")
                    {
                        string oldValue = "";

                        arrPrincipalChanges.Add(new MerchantChanges("State", oldValue, ddlPrincipalState.SelectedText));
                    }
                }

            }



            if (txtPrincipalHomePhone.Text != _newLogic.Coalesce(editMerchant.MerchantPrincipal.Contact.HomePhone))
            {
                string oldValue = "";

                oldValue = editMerchant.MerchantPrincipal.Contact.HomePhone;

                arrPrincipalChanges.Add(new MerchantChanges("Home Phone", oldValue, txtPrincipalHomePhone.Text));
            }



            if (txtPrincipalCellPhone.Text != _newLogic.Coalesce(editMerchant.MerchantPrincipal.Contact.CellPhone))
            {
                string oldValue = "";

                oldValue = editMerchant.MerchantPrincipal.Contact.CellPhone;

                arrPrincipalChanges.Add(new MerchantChanges("Cell Phone", oldValue, txtPrincipalCellPhone.Text));
            }
        }

        private void DetermineContactChanges(MerchantModel editMerchant, ArrayList arrContactChanges)
        {
            if (editMerchant.Contact == null)
            {
                if (txtContactFirstName.Text != "")
                {
                    string oldValue = "";

                    arrContactChanges.Add(new MerchantChanges("FirstName", oldValue, txtContactFirstName.Text));
                }

                if (txtContactLastName.Text != "")
                {
                    string oldValue = "";

                    arrContactChanges.Add(new MerchantChanges("LastName", oldValue, txtContactLastName.Text));
                }

                if (txtContactEmail.Text != "")
                {
                    string oldValue = "";

                    arrContactChanges.Add(new MerchantChanges("Email", oldValue, txtContactEmail.Text));
                }


                if (txtContactPhone.Text != "")
                {
                    string oldValue = "";

                    arrContactChanges.Add(new MerchantChanges("Phone", oldValue, txtContactPhone.Text));
                }


                if (txtContactFax.Text != "")
                {
                    string oldValue = "";

                    arrContactChanges.Add(new MerchantChanges("Fax", oldValue, txtContactFax.Text));
                }
            }
            else
            {
                if (txtContactFirstName.Text != _newLogic.Coalesce(editMerchant.Contact.FirstName))
                {
                    string oldValue = "";

                    oldValue = editMerchant.Contact.FirstName;

                    arrContactChanges.Add(new MerchantChanges("FirstName", oldValue, txtContactFirstName.Text));
                }


                if (txtContactLastName.Text != _newLogic.Coalesce(editMerchant.Contact.LastName))
                {
                    string oldValue = "";

                    oldValue = editMerchant.Contact.LastName;

                    arrContactChanges.Add(new MerchantChanges("LastName", oldValue, txtContactLastName.Text));
                }


                if (txtContactEmail.Text != _newLogic.Coalesce(editMerchant.Contact.Email))
                {
                    string oldValue = "";

                    oldValue = editMerchant.Contact.Email;

                    arrContactChanges.Add(new MerchantChanges("Email", oldValue, txtContactEmail.Text));
                }


                if (txtContactPhone.Text != _newLogic.Coalesce(editMerchant.Contact.HomePhone))
                {
                    string oldValue = "";

                    oldValue = editMerchant.Contact.HomePhone;

                    arrContactChanges.Add(new MerchantChanges("Phone", oldValue, txtContactPhone.Text));
                }


                if (txtContactFax.Text != _newLogic.Coalesce(editMerchant.Contact.Fax))
                {
                    string oldValue = "";

                    oldValue = editMerchant.Contact.Fax;

                    arrContactChanges.Add(new MerchantChanges("Fax", oldValue, txtContactFax.Text));
                }
            }

        }

        private void DetermineBankingChanges(MerchantModel editMerchant, ArrayList arrBankingChanges)
        {
            //Banking Changes
            if (editMerchant.BankAccount != null)
            {
                if (editMerchant.BankAccount.Bank != null)
                {
                    if (txtBankName.Text != _newLogic.Coalesce(editMerchant.BankAccount.Bank.BankName))
                    {
                        string oldValue = "";

                        oldValue = editMerchant.BankAccount.Bank.BankName;

                        arrBankingChanges.Add(new MerchantChanges("Bank Name", oldValue, txtBankName.Text));
                    }
                }
                else if (txtBankName.Text != "")
                {
                    string oldValue = "";

                    arrBankingChanges.Add(new MerchantChanges("Bank Name", oldValue, txtBankName.Text));
                }

            }
            else if (txtBankName.Text != "")
            {
                string oldValue = "";

                arrBankingChanges.Add(new MerchantChanges("Bank Name", oldValue, txtBankName.Text));
            }



            if (editMerchant.BankAccount != null)
            {
                if (editMerchant.BankAccount.Bank != null)
                {
                    if (txtBankCity.Text != _newLogic.Coalesce(editMerchant.BankAccount.Bank.BankCity))
                    {
                        string oldValue = "";

                        oldValue = editMerchant.BankAccount.Bank.BankCity;

                        arrBankingChanges.Add(new MerchantChanges("Bank City", oldValue, txtBankCity.Text));
                    }
                }
                else if (txtBankCity.Text != "")
                {
                    string oldValue = "";

                    arrBankingChanges.Add(new MerchantChanges("Bank City", oldValue, txtBankName.Text));
                }
            }
            else if (txtBankCity.Text != "")
            {
                string oldValue = "";

                arrBankingChanges.Add(new MerchantChanges("Bank City", oldValue, txtBankCity.Text));
            }



            if (editMerchant.BankAccount != null)
            {
                if (editMerchant.BankAccount.Bank != null)
                {
                    if (txtBankPhone.TextWithLiterals != _newLogic.Coalesce(editMerchant.BankAccount.Bank.BankPhone))
                    {
                        string oldValue = "";

                        oldValue = editMerchant.BankAccount.Bank.BankPhone;

                        arrBankingChanges.Add(new MerchantChanges("Bank Phone", oldValue, txtBankPhone.TextWithLiterals));
                    }
                }
                else if (txtBankPhone.Text != "")
                {
                    string oldValue = "";

                    arrBankingChanges.Add(new MerchantChanges("Bank Phone", oldValue, txtBankPhone.TextWithLiterals));
                }
            }
            else if (txtBankPhone.Text != "")
            {
                string oldValue = "";

                arrBankingChanges.Add(new MerchantChanges("Bank Phone", oldValue, txtBankPhone.Text));
            }



            if (editMerchant.BankAccount != null)
            {
                if (editMerchant.BankAccount.Bank != null)
                {
                    if (editMerchant.BankAccount.Bank.BankState != null)
                    {
                        if (ddlBankState.SelectedValue != _newLogic.Coalesce(editMerchant.BankAccount.Bank.BankState.RecordId))
                        {
                            string oldValue = "";

                            oldValue = editMerchant.BankAccount.Bank.BankState.Name.ToString();

                            arrBankingChanges.Add(new MerchantChanges("Bank State", oldValue, ddlBankState.SelectedText));
                        }
                    }
                    else if (editMerchant.BankAccount.Bank.BankState == null && ddlBankState.SelectedValue != "")
                    {
                        string oldValue = "";

                        arrBankingChanges.Add(new MerchantChanges("Bank State", oldValue, ddlBankState.SelectedText));
                    }
                }
                else if (editMerchant.BankAccount.Bank == null && ddlBankState.SelectedValue != "")
                {
                    string oldValue = "";

                    arrBankingChanges.Add(new MerchantChanges("Bank State", oldValue, ddlBankState.SelectedText));
                }
            }
            else if (ddlBankState.SelectedValue != "")
            {
                string oldValue = "";

                arrBankingChanges.Add(new MerchantChanges("Bank State", oldValue, ddlBankState.SelectedText));
            }



            if (txtDebitCardNumber.Enabled)
            {
                if (editMerchant.DebitCard != null)
                {
                    if (editMerchant.DebitCard.DebitCardNumber != null)
                    {
                        if (txtDebitCardNumber.Text != ShowDebitCard(editMerchant.DebitCard.DebitCardNumber, editMerchant.DebitCard.RecordId))
                        {
                            string oldValue = "";

                            oldValue = MaskDebitCard(editMerchant.DebitCard.DebitCardNumber, editMerchant.DebitCard.RecordId);

                            arrBankingChanges.Add(new MerchantChanges("Debit Card", oldValue, txtDebitCardNumber.Text));
                        }
                    }
                    else if (txtDebitCardNumber.Text != "")
                    {
                        string oldValue = "";

                        arrBankingChanges.Add(new MerchantChanges("Debit Card", oldValue, txtDebitCardNumber.Text));
                    }
                }
                else if (txtDebitCardNumber.Text != "")
                {
                    string oldValue = "";

                    arrBankingChanges.Add(new MerchantChanges("Debit Card", oldValue, txtDebitCardNumber.Text));
                }
            }



            if (txtAccountNumber.Enabled)
            {
                if (editMerchant.BankAccount != null)
                {
                    if (editMerchant.BankAccount.AccountNumber != null)
                    {
                        if (txtAccountNumber.Text != ShowAccountNumber(editMerchant.BankAccount.AccountNumber, editMerchant.BankAccount.RecordId))
                        {
                            string oldValue = "";

                            oldValue = MaskAccountNumber(editMerchant.BankAccount.AccountNumber, editMerchant.BankAccount.RecordId);

                            arrBankingChanges.Add(new MerchantChanges("Account Number", oldValue, txtAccountNumber.Text));
                        }
                    }
                    else if (txtAccountNumber.Text != "")
                    {
                        string oldValue = "";

                        arrBankingChanges.Add(new MerchantChanges("Account Number", oldValue, txtAccountNumber.Text));
                    }
                }
                else if (txtAccountNumber.Text != "")
                {
                    string oldValue = "";

                    arrBankingChanges.Add(new MerchantChanges("Account Number", oldValue, txtAccountNumber.Text));
                }
            }



            if (editMerchant.DebitCard != null)
            {
                if (ddlExpMonth.SelectedValue != _newLogic.Coalesce(editMerchant.DebitCard.DebitCardExpMonth))
                {
                    string oldValue = "";

                    oldValue = editMerchant.DebitCard.DebitCardExpMonth.ToString();

                    arrBankingChanges.Add(new MerchantChanges("Exp Month", oldValue, ddlExpMonth.SelectedValue));
                }
            }
            else if (ddlExpMonth.SelectedValue != "")
            {
                string oldValue = "";

                arrBankingChanges.Add(new MerchantChanges("Exp Month", oldValue, ddlExpMonth.SelectedValue));
            }



            if (editMerchant.DebitCard != null)
            {
                if (ddlExpYear.SelectedValue != _newLogic.Coalesce(editMerchant.DebitCard.DebitCardExpYear))
                {
                    string oldValue = "";

                    oldValue = editMerchant.DebitCard.DebitCardExpYear.ToString();

                    arrBankingChanges.Add(new MerchantChanges("Exp Year", oldValue, ddlExpYear.SelectedValue));
                }
            }
            else if (ddlExpYear.SelectedValue != "")
            {
                string oldValue = "";

                arrBankingChanges.Add(new MerchantChanges("Exp Year", oldValue, ddlExpYear.SelectedValue));
            }



            if (editMerchant.BankAccount != null)
            {
                if (txtRoutingNumber.Text != _newLogic.Coalesce(editMerchant.BankAccount.RoutingNumber))
                {
                    string oldValue = "";

                    oldValue = editMerchant.BankAccount.RoutingNumber;

                    arrBankingChanges.Add(new MerchantChanges("Routing Number", oldValue, txtRoutingNumber.Text));
                }
            }
            else if (txtRoutingNumber.Text != "")
            {
                string oldValue = "";

                arrBankingChanges.Add(new MerchantChanges("Routing Number", oldValue, txtRoutingNumber.Text));
            }

        }

        private void DetermineMerchantAccountChanges(MerchantModel editMerchant, ArrayList arrMerchantAccountChanges)
        {
            //Merchant Account Changes
            if (editMerchant.SwipedPct != null)
            {
                if (txtSwipedPct.Text != _newLogic.Coalesce(editMerchant.SwipedPct))
                {
                    string oldValue = "";

                    oldValue = editMerchant.SwipedPct.ToString() + " %";

                    arrMerchantAccountChanges.Add(new MerchantChanges("Swiped %", oldValue, txtSwipedPct.Text + " %"));
                }
            }
            else if (txtSwipedPct.Text != "")
            {
                string oldValue = "";

                arrMerchantAccountChanges.Add(new MerchantChanges("Swiped %", oldValue, txtSwipedPct.Text + " %"));
            }


            if (editMerchant.AvgMonthlySales.HasValue)
            {
                if (Convert.ToDecimal(txtAvgMonthlySales.Text) != editMerchant.AvgMonthlySales)
                {
                    string oldValue = "";

                    oldValue = "$" + editMerchant.AvgMonthlySales.ToString();

                    arrMerchantAccountChanges.Add(new MerchantChanges("Avg Monthly Sales", oldValue, "$" + txtAvgMonthlySales.Text));
                }
            }
            else if (txtAvgMonthlySales.Text != "")
            {
                string oldValue = "";

                arrMerchantAccountChanges.Add(new MerchantChanges("Avg Monthly Sales", oldValue, "$" + txtAvgMonthlySales.Text));
            }


            if (editMerchant.HighestMonthlySales != null)
            {
                if (Convert.ToDecimal(txtHighestMonthlySales.Text) != editMerchant.HighestMonthlySales)
                {
                    string oldValue = "";

                    oldValue = "$" + editMerchant.HighestMonthlySales.ToString();

                    arrMerchantAccountChanges.Add(new MerchantChanges("Highest Monthly Sales", oldValue, "$" + txtHighestMonthlySales.Text));
                }
            }
            else if (txtHighestMonthlySales.Text != "")
            {
                string oldValue = "";

                arrMerchantAccountChanges.Add(new MerchantChanges("Highest Monthly Sales", oldValue, "$" + txtHighestMonthlySales.Text));
            }


            if (editMerchant.AvgWeeklySales != null)
            {
                if (Convert.ToDecimal(txtAvgWeeklySales.Text) != editMerchant.AvgWeeklySales)
                {
                    string oldValue = "";

                    oldValue = "$" + editMerchant.AvgWeeklySales.ToString();

                    arrMerchantAccountChanges.Add(new MerchantChanges("Avg Weekly Sales", oldValue, "$" + txtAvgWeeklySales.Text));
                }
            }
            else if (txtAvgWeeklySales.Text != "")
            {
                string oldValue = "";

                arrMerchantAccountChanges.Add(new MerchantChanges("Avg Weekly Sales", oldValue, "$" + txtAvgWeeklySales.Text));
            }


            if (editMerchant.Processor != null)
            {
                if (txtCardProcessor.Text != editMerchant.Processor.ProcessorName)
                {
                    string oldValue = "";

                    oldValue = editMerchant.Processor.ProcessorName;

                    arrMerchantAccountChanges.Add(new MerchantChanges("Processor", oldValue, txtCardProcessor.Text));
                }
            }
            else if (editMerchant.Processor == null && txtCardProcessor.Text != "")
            {
                string oldValue = "";

                arrMerchantAccountChanges.Add(new MerchantChanges("Processor", oldValue, txtCardProcessor.Text));
            }



            if (txtMerchantId.Text != _newLogic.Coalesce(editMerchant.MerchantId))
            {
                string oldValue = "";

                oldValue = editMerchant.MerchantId ?? "";

                arrMerchantAccountChanges.Add(new MerchantChanges("Merchant Id", oldValue, txtMerchantId.Text));
            }



            if (rblHighRisk.SelectedValue.ToLower() != _newLogic.Coalesce(editMerchant.HighRisk).ToLower())
            {
                string oldValue = "";

                oldValue = editMerchant.HighRisk.ToString() ?? "";

                arrMerchantAccountChanges.Add(new MerchantChanges("High Risk", oldValue, rblHighRisk.SelectedValue));
            }


            if (rblHighRisk.SelectedValue == "True")
            {
                if (txtHighRiskWho.Text != _newLogic.Coalesce(editMerchant.HighRiskWho))
                {
                    string oldValue = "";

                    oldValue = editMerchant.HighRiskWho ?? "";

                    arrMerchantAccountChanges.Add(new MerchantChanges("High Risk Who", oldValue, txtHighRiskWho.Text));
                }


                if (ddlHighRiskYear.SelectedIndex != -1 && ddlHighRiskMonth.SelectedIndex != -1 && ddlHighRiskDay.SelectedIndex != -1)
                {
                    Int32 year = Convert.ToInt32(ddlHighRiskYear.SelectedValue);
                    Int32 month = Convert.ToInt32(ddlHighRiskMonth.SelectedValue);
                    Int32 day = Convert.ToInt32(ddlHighRiskDay.SelectedValue);

                    DateTime highRiskDate;

                    if (DateTime.TryParse(year + "-" + month + "-" + day, out highRiskDate))
                    {
                        if (_newLogic.Coalesce(highRiskDate) != _newLogic.Coalesce(editMerchant.HighRiskDate))
                        {
                            string oldValue = "";

                            oldValue = editMerchant.HighRiskDate.HasValue ? editMerchant.HighRiskDate.Value.ToString("MM/dd/yyyy") : "";

                            arrMerchantAccountChanges.Add(new MerchantChanges("High Risk Date", oldValue, highRiskDate.ToString("MM/dd/yyyy")));
                        }
                    }
                }
            }



            if (rblBankruptcy.SelectedValue.ToLower() != _newLogic.Coalesce(editMerchant.Bankruptcy).ToLower())
            {
                string oldValue = "";

                oldValue = editMerchant.Bankruptcy.ToString() ?? "";

                arrMerchantAccountChanges.Add(new MerchantChanges("Bankruptcy", oldValue, rblBankruptcy.SelectedValue));
            }


            if (rblBankruptcy.SelectedValue == "True")
            {
                if (ddlBankruptcyYear.SelectedIndex != -1 && ddlBankruptcyMonth.SelectedIndex != -1 && ddlBankruptcyDay.SelectedIndex != -1)
                {
                    Int32 year = Convert.ToInt32(ddlBankruptcyYear.SelectedValue);
                    Int32 month = Convert.ToInt32(ddlBankruptcyMonth.SelectedValue);
                    Int32 day = Convert.ToInt32(ddlBankruptcyDay.SelectedValue);
                    DateTime bankruptcyDate;

                    if (DateTime.TryParse(year + "-" + month + "-" + day, out bankruptcyDate))
                    {
                        if (_newLogic.Coalesce(bankruptcyDate) != _newLogic.Coalesce(editMerchant.BankruptcyDate))
                        {
                            string oldValue = "";

                            oldValue = editMerchant.BankruptcyDate.HasValue ? editMerchant.BankruptcyDate.Value.ToString("MM/dd/yyyy") : "";

                            arrMerchantAccountChanges.Add(new MerchantChanges("Bankruptcy Date", oldValue, bankruptcyDate.ToString("MM/dd/yyyy")));
                        }
                    }
                }
            }

        }

        private void DetermineAdvancePlanChanges(MerchantModel editMerchant, ArrayList arrAdvancePlanChanges)
        {
            //Advance Plan Changes
            if (ddlActivePlan.SelectedText != editMerchant.AdvancePlan.PlanName)
            {
                string oldValue = "";

                oldValue = editMerchant.AdvancePlan.PlanName;

                arrAdvancePlanChanges.Add(new MerchantChanges("Advance Plan", oldValue, ddlActivePlan.SelectedText));
            }
        }


        public void AddChangeToAudit(Int32 MerchantId, String Field, String OldValue, String NewValue)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                String userId = Context.User.Identity.GetUserId();


                AccountChangeAuditModel audit = new AccountChangeAuditModel
                {
                    Timestamp = DateTime.UtcNow,
                    Merchant = ctx.Merchants.FirstOrDefault(x => x.RecordId == MerchantId),
                    User = ctx.Users.FirstOrDefault(x => x.Id == userId),
                    FieldName = Field,
                    OldValue = OldValue,
                    NewValue = NewValue
                };

                ctx.AuditLog.Add(audit);

                ctx.SaveChanges();
            }
        }

        //Save any changes that have been made to the merchant details
        public void SaveChanges()
        {
            try
            {
                int accountNumber = Convert.ToInt32(hMerchantRecordId.Value);

                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    MerchantModel editMerchant = ctx.Merchants.First(m => m.RecordId == accountNumber);

                    SaveBusinessChanges(editMerchant, ctx);

                    SavePrincipalChanges(editMerchant, ctx);

                    SaveContactChanges(editMerchant);

                    SaveBankingChanges(editMerchant, ctx);

                    SaveMerchantAccountChanges(editMerchant, ctx);

                    SaveAdvancePlanChanges(editMerchant, ctx);

                    ctx.SaveChanges();

                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "SaveChanges");
            }
        }

        private void SaveBusinessChanges(MerchantModel editMerchant, ApplicationDbContext ctx)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrBusinessChanges.Items)
                {
                    Label lblFieldName = (Label)rItem.FindControl("lblFieldName0");
                    Label lblNewValue = (Label)rItem.FindControl("lblNewValue0");
                    CheckBox cbConfirmed = (CheckBox)rItem.FindControl("cbConfirmed0");

                    if (lblFieldName != null && lblNewValue != null && cbConfirmed != null)
                    {
                        if (lblFieldName.Text == "Legal Org Type")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.LegalOrgType.LegalOrgTypeName, lblNewValue.Text);

                                editMerchant.LegalOrgType = ctx.LegalOrgTypes.First(lot => lot.LegalOrgTypeName == lblNewValue.Text);
                            }
                            else
                            {
                                ddlLegalOrgType.SelectedValue = editMerchant.LegalOrgType.RecordId.ToString();
                            }
                        }

                        if (lblFieldName.Text == "Legal Org State")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.LegalOrgState.Name, lblNewValue.Text);

                                editMerchant.LegalOrgState = ctx.GeoStates.First(gs => gs.Name == lblNewValue.Text);
                            }
                            else
                            {
                                ddlLegalOrgState.SelectedValue = editMerchant.LegalOrgState.RecordId.ToString();
                            }
                        }

                        if (lblFieldName.Text == "Merchant Type")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.MerchantType.MerchantTypeName, lblNewValue.Text);

                                editMerchant.MerchantType = ctx.MerchantTypes.First(mt => mt.MerchantTypeName == lblNewValue.Text);
                            }
                            else
                            {
                                rblMerchantType.SelectedValue = editMerchant.MerchantType.MerchantTypeName;
                            }
                        }

                        if (lblFieldName.Text == "MCC")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.Mcc.MerchantCategoryCode, lblNewValue.Text);

                                editMerchant.Mcc = ctx.MerchantCategoryCodes.First(mcc => mcc.MerchantCategoryCode == lblNewValue.Text);
                            }
                            else
                            {
                                ddlMCC.SelectedValue = editMerchant.Mcc.RecordId.ToString();
                            }
                        }

                        if (lblFieldName.Text == "Legal Name")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.CorpName, lblNewValue.Text);

                                editMerchant.CorpName = lblNewValue.Text;
                            }
                            else
                            {
                                txtCorpName.Text = editMerchant.CorpName;
                            }
                        }

                        if (lblFieldName.Text == "DBA Name")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.DbaName, lblNewValue.Text);

                                editMerchant.DbaName = lblNewValue.Text;
                            }
                            else
                            {
                                txtDBAName.Text = editMerchant.DbaName;
                            }
                        }

                        if (lblFieldName.Text == "Business Address")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.Business.Address.Address, lblNewValue.Text);

                                editMerchant.Business.Address.Address = lblNewValue.Text;
                            }
                            else
                            {
                                txtCorpAddress.Text = editMerchant.Business.Address.Address;
                            }
                        }

                        if (lblFieldName.Text == "Business City")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.Business.Address.City, lblNewValue.Text);

                                editMerchant.Business.Address.City = lblNewValue.Text;
                            }
                            else
                            {
                                txtCorpCity.Text = editMerchant.Business.Address.City;
                            }
                        }

                        if (lblFieldName.Text == "Business State")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.Business.Address.State.Name, lblNewValue.Text);

                                editMerchant.Business.Address.State = ctx.GeoStates.First(gs => gs.Name == lblNewValue.Text);
                            }
                            else
                            {
                                ddlCorpState.SelectedValue = editMerchant.Business.Address.State.RecordId.ToString();
                            }
                        }

                        if (lblFieldName.Text == "Business Zip")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.Business.Address.Zip, lblNewValue.Text);

                                editMerchant.Business.Address.Zip = lblNewValue.Text;
                            }
                            else
                            {
                                txtCorpZip.Text = editMerchant.Business.Address.Zip;
                            }
                        }

                        if (lblFieldName.Text == "Business License #")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.BusLicNumber, lblNewValue.Text);

                                editMerchant.BusLicNumber = lblNewValue.Text;
                            }
                            else
                            {
                                txtBusLicNumber.Text = editMerchant.BusLicNumber;
                            }
                        }

                        if (lblFieldName.Text == "License Type")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.BusLicType, lblNewValue.Text);

                                editMerchant.BusLicType = lblNewValue.Text;
                            }
                            else
                            {
                                txtBusLicType.Text = editMerchant.BusLicType;
                            }
                        }

                        if (lblFieldName.Text == "License Issuer")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.BusLicIssuer, lblNewValue.Text);

                                editMerchant.BusLicIssuer = lblNewValue.Text;
                            }
                            else
                            {
                                txtBusLicIssuer.Text = editMerchant.BusLicIssuer;
                            }
                        }

                        if (lblFieldName.Text == "License Date")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.BusLicDate.ToString(), lblNewValue.Text);

                                editMerchant.BusLicDate = Convert.ToDateTime(lblNewValue.Text);
                            }
                            else
                            {
                                if (editMerchant.BusLicDate.HasValue)
                                {
                                    ddlLicenseDateYear.SelectedValue = editMerchant.BusLicDate.Value.Year.ToString();
                                    ddlLicenseDateMonth.SelectedValue = editMerchant.BusLicDate.Value.Month.ToString();
                                    ddlLicenseDateDay.SelectedValue = editMerchant.BusLicDate.Value.Day.ToString();
                                }
                                else
                                {
                                    ddlLicenseDateYear.SelectedIndex = -1;
                                    ddlLicenseDateMonth.SelectedIndex = -1;
                                    ddlLicenseDateDay.SelectedIndex = -1;
                                }
                            }
                        }

                        if (lblFieldName.Text == "Federal Tax ID")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.FedTaxId, lblNewValue.Text);

                                editMerchant.FedTaxId = lblNewValue.Text;
                            }
                            else
                            {
                                txtFedTaxId.Text = editMerchant.FedTaxId;
                            }
                        }

                        if (lblFieldName.Text == "Email")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.Business.Email, lblNewValue.Text);

                                editMerchant.Business.Email = lblNewValue.Text;
                            }
                            else
                            {
                                txtBusEmail.Text = editMerchant.Business.Email;
                            }
                        }

                        if (lblFieldName.Text == "Phone")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.Business.HomePhone, lblNewValue.Text);

                                editMerchant.Business.HomePhone = lblNewValue.Text;
                            }
                            else
                            {
                                txtBusPhone.Text = editMerchant.Business.HomePhone;
                            }
                        }

                        if (lblFieldName.Text == "Fax")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.Business.Fax, lblNewValue.Text);

                                editMerchant.Business.Fax = lblNewValue.Text;
                            }
                            else
                            {
                                txtBusFax.Text = editMerchant.Business.Fax;
                            }
                        }

                        if (lblFieldName.Text == "Merchandise Sold")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.MerchandiseSold, lblNewValue.Text);

                                editMerchant.MerchandiseSold = lblNewValue.Text;
                            }
                            else
                            {
                                txtMerchandiseSold.Text = editMerchant.MerchandiseSold;
                            }
                        }

                        if (lblFieldName.Text == "Years In Business")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.YearsInBusiness.ToString(), lblNewValue.Text);

                                editMerchant.YearsInBusiness = Convert.ToInt16(lblNewValue.Text);
                            }
                            else
                            {
                                txtYearsInBus.Text = editMerchant.YearsInBusiness.ToString();
                            }
                        }

                        if (lblFieldName.Text == "Months In Business")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.MonthsInBusiness.ToString(), lblNewValue.Text);

                                editMerchant.MonthsInBusiness = Convert.ToInt16(lblNewValue.Text);
                            }
                            else
                            {
                                txtMonthsInBus.Text = editMerchant.MonthsInBusiness.HasValue ? editMerchant.MonthsInBusiness.Value.ToString("d2") : "";
                            }
                        }

                        if (lblFieldName.Text == "Seasonal Sales")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.SeasonalSales.ToString(), lblNewValue.Text);

                                editMerchant.SeasonalSales = Convert.ToBoolean(lblNewValue.Text);
                            }
                            else
                            {
                                if (editMerchant.SeasonalSales.HasValue)
                                {
                                    rblSeasonal.SelectedValue = editMerchant.SeasonalSales.ToString();
                                }
                                else
                                {
                                    rblSeasonal.SelectedIndex = -1;
                                }
                            }
                        }

                        if (lblFieldName.Text == "Seasonal Months")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.SeasonalMonths, lblNewValue.Text);

                                editMerchant.SeasonalMonths = lblNewValue.Text;
                            }
                            else
                            {
                                if (!String.IsNullOrEmpty(editMerchant.SeasonalMonths))
                                {
                                    foreach (string month in editMerchant.SeasonalMonths.Split('-'))
                                    {
                                        ListItem li = cblSeasonal.Items.FindByValue(month.Replace(" ", ""));

                                        if (li != null)
                                        {
                                            li.Selected = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                string userId = "";

                if (Request.IsAuthenticated)
                {
                    userId = Page.User.Identity.GetUserId();
                }
                _newLogic.WriteExceptionToDB(ex, "SaveBusinessChanges", 0, editMerchant.RecordId, userId);
            }
        }

        private void SavePrincipalChanges(MerchantModel editMerchant, ApplicationDbContext ctx)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrPrincipalChanges.Items)
                {
                    Label lblFieldName = (Label)rItem.FindControl("lblFieldName1");
                    Label lblNewValue = (Label)rItem.FindControl("lblNewValue1");
                    CheckBox cbConfirmed = (CheckBox)rItem.FindControl("cbConfirmed1");

                    if (lblFieldName != null && lblNewValue != null && cbConfirmed != null)
                    {
                        if (lblFieldName.Text == "First Name")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.MerchantPrincipal.Contact.FirstName, lblNewValue.Text);

                                editMerchant.MerchantPrincipal.Contact.FirstName = lblNewValue.Text;
                            }
                            else
                            {
                                txtPrincipalFirstName.Text = editMerchant.MerchantPrincipal.Contact.FirstName;
                            }
                        }

                        if (lblFieldName.Text == "Last Name")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.MerchantPrincipal.Contact.LastName, lblNewValue.Text);

                                editMerchant.MerchantPrincipal.Contact.LastName = lblNewValue.Text;
                            }
                            else
                            {
                                txtPrincipalLastName.Text = editMerchant.MerchantPrincipal.Contact.LastName;
                            }
                        }

                        if (lblFieldName.Text == "M.I.")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.MerchantPrincipal.Contact.MiddleInitial, lblNewValue.Text);

                                editMerchant.MerchantPrincipal.Contact.MiddleInitial = lblNewValue.Text;
                            }
                            else
                            {
                                txtPrincipalMI.Text = editMerchant.MerchantPrincipal.Contact.MiddleInitial;
                            }
                        }

                        if (lblFieldName.Text == "D.O.B.")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.MerchantPrincipal.PrincipalDoB.ToString(), lblNewValue.Text);

                                editMerchant.MerchantPrincipal.PrincipalDoB = Convert.ToDateTime(lblNewValue.Text);
                            }
                            else
                            {
                                if (editMerchant.MerchantPrincipal.PrincipalDoB.HasValue)
                                {
                                    ddlBirthYear.SelectedValue = editMerchant.MerchantPrincipal.PrincipalDoB.Value.Year.ToString();
                                    ddlBirthMonth.SelectedValue = editMerchant.MerchantPrincipal.PrincipalDoB.Value.Month.ToString();
                                    ddlBirthDay.SelectedValue = editMerchant.MerchantPrincipal.PrincipalDoB.Value.Day.ToString();
                                }
                                else
                                {
                                    ddlBirthYear.SelectedIndex = -1;
                                    ddlBirthMonth.SelectedIndex = -1;
                                    ddlBirthDay.SelectedIndex = -1;
                                }
                            }
                        }

                        if (lblFieldName.Text == "S.S.N.")
                        {
                            if (cbConfirmed.Checked)
                            {
                                String maskedOldSsn = MaskSsn(editMerchant.MerchantPrincipal.PrincipalSsn, editMerchant.MerchantPrincipal.RecordId);

                                Logic.Crypt crypto1 = new Logic.Crypt();
                                crypto1.CryptType = "SSN";
                                crypto1.CryptData = Convert.ToBase64String(PWDTK.StringToUtf8Bytes(lblNewValue.Text));
                                crypto1.Purpose = "86d4b5b2-b34a-4968-9fde-f8ed677bca8b";
                                crypto1.AdditionalData = editMerchant.MerchantPrincipal.RecordId.ToString();

                                byte[] newSsn = crypto1.Protect();

                                editMerchant.MerchantPrincipal.PrincipalSsn = newSsn;

                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, maskedOldSsn, MaskSsn(newSsn, editMerchant.MerchantPrincipal.RecordId));

                            }
                            else
                            {
                                if (editMerchant.MerchantPrincipal.PrincipalSsn != null)
                                {
                                    txtPrincipalSSN.Text = MaskSsn(editMerchant.MerchantPrincipal.PrincipalSsn, editMerchant.MerchantPrincipal.RecordId);
                                }
                                else
                                {
                                    txtPrincipalSSN.Text = "";
                                }
                            }

                            txtPrincipalSSN.Enabled = false;
                        }

                        if (lblFieldName.Text == "% Ownership")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.MerchantPrincipal.PrincipalPctOwn.ToString(), lblNewValue.Text);

                                editMerchant.MerchantPrincipal.PrincipalPctOwn = Convert.ToDecimal(lblNewValue.Text.Substring(0, lblNewValue.Text.Length - 2));
                            }
                            else
                            {
                                if (editMerchant.MerchantPrincipal.PrincipalPctOwn.HasValue)
                                {
                                    txtPrincipalPctOwn.Text = editMerchant.MerchantPrincipal.PrincipalPctOwn.ToString();
                                }
                                else
                                {
                                    txtPrincipalPctOwn.Text = "";
                                }
                            }
                        }

                        if (lblFieldName.Text == "Title")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.MerchantPrincipal.Contact.Title, lblNewValue.Text);

                                editMerchant.MerchantPrincipal.Contact.Title = lblNewValue.Text;
                            }
                            else
                            {
                                txtPrincipalTitle.Text = editMerchant.MerchantPrincipal.Contact.Title;
                            }
                        }

                        if (lblFieldName.Text == "DL Number")
                        {
                            if (cbConfirmed.Checked)
                            {
                                String maskedOldDL = MaskDLNumber(editMerchant.MerchantPrincipal.PrincipalDLNumber, editMerchant.MerchantPrincipal.RecordId);

                                Logic.Crypt crypto2 = new Logic.Crypt();
                                crypto2.CryptType = "DL";
                                crypto2.CryptData = Convert.ToBase64String(PWDTK.StringToUtf8Bytes(lblNewValue.Text));
                                crypto2.Purpose = "29dc7202-ae1f-4d38-9694-9d700a94897b";
                                crypto2.AdditionalData = editMerchant.MerchantPrincipal.RecordId.ToString();

                                byte[] newDl = crypto2.Protect();

                                editMerchant.MerchantPrincipal.PrincipalDLNumber = newDl;

                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, maskedOldDL, MaskDLNumber(newDl, editMerchant.MerchantPrincipal.RecordId));

                            }
                            else
                            {
                                txtPrincipalDLNumber.Text = MaskDLNumber(editMerchant.MerchantPrincipal.PrincipalDLNumber, editMerchant.MerchantPrincipal.RecordId);
                            }

                            txtPrincipalDLNumber.Enabled = false;
                        }

                        if (lblFieldName.Text == "DL State")
                        {
                            if (cbConfirmed.Checked)
                            {
                                string stateName = editMerchant.MerchantPrincipal.PrincipalDLState == null ? "" : editMerchant.MerchantPrincipal.PrincipalDLState.Name;

                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, stateName, lblNewValue.Text);

                                editMerchant.MerchantPrincipal.PrincipalDLState = ctx.GeoStates.First(gs => gs.Name == lblNewValue.Text);
                            }
                            else
                            {
                                if (editMerchant.MerchantPrincipal.PrincipalDLState != null)
                                {
                                    ddlPrincipalDLState.SelectedValue = editMerchant.MerchantPrincipal.PrincipalDLState.RecordId.ToString();
                                }
                                else
                                {
                                    ddlPrincipalDLState.SelectedIndex = -1;
                                }
                            }
                        }

                        if (lblFieldName.Text == "Address")
                        {
                            if (cbConfirmed.Checked)
                            {
                                if (editMerchant.MerchantPrincipal.Contact.Address != null)
                                {
                                    AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.MerchantPrincipal.Contact.Address.Address, lblNewValue.Text);

                                    editMerchant.MerchantPrincipal.Contact.Address.Address = lblNewValue.Text;
                                }
                                else
                                {
                                    AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, "", lblNewValue.Text);

                                    AddressModel newContactAddress = new AddressModel();
                                    newContactAddress.Address = lblNewValue.Text;
                                    ctx.Addresses.Add(newContactAddress);
                                    editMerchant.MerchantPrincipal.Contact.Address = newContactAddress;
                                }
                            }
                            else
                            {
                                if (editMerchant.MerchantPrincipal.Contact.Address != null)
                                {
                                    txtPrincipalAddress.Text = editMerchant.MerchantPrincipal.Contact.Address.Address;
                                }
                                else
                                {
                                    txtPrincipalAddress.Text = "";
                                }
                            }
                        }

                        if (lblFieldName.Text == "City")
                        {
                            if (cbConfirmed.Checked)
                            {
                                if (editMerchant.MerchantPrincipal.Contact.Address != null)
                                {
                                    AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.MerchantPrincipal.Contact.Address.City, lblNewValue.Text);

                                    editMerchant.MerchantPrincipal.Contact.Address.City = lblNewValue.Text;
                                }
                                else
                                {
                                    AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, "", lblNewValue.Text);

                                    AddressModel newContactAddress = new AddressModel();
                                    newContactAddress.City = lblNewValue.Text;
                                    ctx.Addresses.Add(newContactAddress);
                                    editMerchant.MerchantPrincipal.Contact.Address = newContactAddress;
                                }
                            }
                            else
                            {
                                if (editMerchant.MerchantPrincipal.Contact.Address != null)
                                {
                                    txtPrincipalCity.Text = editMerchant.MerchantPrincipal.Contact.Address.City;
                                }
                                else
                                {
                                    txtPrincipalCity.Text = "";
                                }
                            }
                        }

                        if (lblFieldName.Text == "Zip")
                        {
                            if (cbConfirmed.Checked)
                            {
                                if (editMerchant.MerchantPrincipal.Contact.Address != null)
                                {
                                    AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.MerchantPrincipal.Contact.Address.Zip, lblNewValue.Text);

                                    editMerchant.MerchantPrincipal.Contact.Address.Zip = lblNewValue.Text;
                                }
                                else
                                {
                                    AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, "", lblNewValue.Text);

                                    AddressModel newContactAddress = new AddressModel();
                                    newContactAddress.Zip = lblNewValue.Text;
                                    ctx.Addresses.Add(newContactAddress);
                                    editMerchant.MerchantPrincipal.Contact.Address = newContactAddress;
                                }
                            }
                            else
                            {
                                if (editMerchant.MerchantPrincipal.Contact.Address != null)
                                {
                                    txtPrincipalZip.Text = editMerchant.MerchantPrincipal.Contact.Address.Zip;
                                }
                                else
                                {
                                    txtPrincipalZip.Text = "";
                                }
                            }
                        }


                        if (lblFieldName.Text == "State")
                        {
                            if (cbConfirmed.Checked)
                            {
                                if (editMerchant.MerchantPrincipal.Contact.Address != null && editMerchant.MerchantPrincipal.Contact.Address.State != null)
                                {
                                    AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.MerchantPrincipal.Contact.Address.State.Name, lblNewValue.Text);

                                    editMerchant.MerchantPrincipal.Contact.Address.State = ctx.GeoStates.First(gs => gs.Name == lblNewValue.Text);
                                }
                                else
                                {
                                    AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, "", lblNewValue.Text);

                                    AddressModel newContactAddress = new AddressModel();
                                    newContactAddress.State = ctx.GeoStates.First(x => x.Name == lblNewValue.Text);
                                    ctx.Addresses.Add(newContactAddress);
                                    editMerchant.MerchantPrincipal.Contact.Address = newContactAddress;
                                }
                            }
                            else
                            {
                                if (editMerchant.MerchantPrincipal.Contact.Address.State != null)
                                {
                                    ddlPrincipalState.SelectedValue = editMerchant.MerchantPrincipal.Contact.Address.State.RecordId.ToString();
                                }
                                else
                                {
                                    ddlPrincipalState.SelectedIndex = -1;
                                }
                            }
                        }

                        if (lblFieldName.Text == "Home Phone")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.MerchantPrincipal.Contact.HomePhone, lblNewValue.Text);

                                editMerchant.MerchantPrincipal.Contact.HomePhone = lblNewValue.Text;
                            }
                            else
                            {
                                txtPrincipalHomePhone.Text = editMerchant.MerchantPrincipal.Contact.HomePhone;
                            }
                        }

                        if (lblFieldName.Text == "Cell Phone")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.MerchantPrincipal.Contact.CellPhone, lblNewValue.Text);

                                editMerchant.MerchantPrincipal.Contact.CellPhone = lblNewValue.Text;
                            }
                            else
                            {
                                txtPrincipalCellPhone.Text = editMerchant.MerchantPrincipal.Contact.CellPhone;
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                string userId = "";

                if (Request.IsAuthenticated)
                {
                    userId = Page.User.Identity.GetUserId();
                }
                _newLogic.WriteExceptionToDB(ex, "SavePrincipalChanges", 0, editMerchant.RecordId, userId);
            }
        }

        private void SaveContactChanges(MerchantModel editMerchant)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrContactChanges.Items)
                {
                    Label lblFieldName = (Label)rItem.FindControl("lblFieldName2");
                    Label lblNewValue = (Label)rItem.FindControl("lblNewValue2");
                    CheckBox cbConfirmed = (CheckBox)rItem.FindControl("cbConfirmed2");

                    if (lblFieldName != null && lblNewValue != null && cbConfirmed != null)
                    {
                        if (lblFieldName.Text == "FirstName")
                        {
                            if (cbConfirmed.Checked)
                            {
                                if (editMerchant.Contact != null)
                                {
                                    AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.Contact.FirstName, lblNewValue.Text);

                                    editMerchant.Contact.FirstName = lblNewValue.Text.Trim();
                                }
                                else
                                {
                                    AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, "", lblNewValue.Text);

                                    ContactModel newContact = new ContactModel();
                                    newContact.FirstName = lblNewValue.Text.Trim();
                                    _globalCtx.Contacts.Add(newContact);
                                    editMerchant.Contact = newContact;
                                }
                            }
                            else
                            {
                                if (editMerchant.Contact != null)
                                {
                                    txtContactFirstName.Text = editMerchant.Contact.FirstName;
                                }
                                else
                                {
                                    txtContactFirstName.Text = "";
                                }
                            }
                        }

                        if (lblFieldName.Text == "LastName")
                        {
                            if (cbConfirmed.Checked)
                            {
                                if (editMerchant.Contact != null)
                                {
                                    AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.Contact.LastName, lblNewValue.Text);

                                    editMerchant.Contact.LastName = lblNewValue.Text.Trim();
                                }
                                else
                                {
                                    AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, "", lblNewValue.Text);

                                    ContactModel newContact = new ContactModel();
                                    newContact.LastName = lblNewValue.Text.Trim();
                                    _globalCtx.Contacts.Add(newContact);
                                    editMerchant.Contact = newContact;
                                }
                            }
                            else
                            {
                                if (editMerchant.Contact != null)
                                {
                                    txtContactLastName.Text = editMerchant.Contact.LastName;
                                }
                                else
                                {
                                    txtContactLastName.Text = "";
                                }
                            }
                        }

                        if (lblFieldName.Text == "Email")
                        {
                            if (cbConfirmed.Checked)
                            {
                                if (editMerchant.Contact != null)
                                {
                                    AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.Contact.Email, lblNewValue.Text);

                                    editMerchant.Contact.Email = lblNewValue.Text.Trim();
                                }
                                else
                                {
                                    AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, "", lblNewValue.Text);

                                    ContactModel newContact = new ContactModel();
                                    newContact.Email = lblNewValue.Text.Trim();
                                    _globalCtx.Contacts.Add(newContact);
                                    editMerchant.Contact = newContact;
                                }
                            }
                            else
                            {
                                if (editMerchant.Contact != null)
                                {
                                    txtContactEmail.Text = editMerchant.Contact.Email;
                                }
                                else
                                {
                                    txtContactEmail.Text = "";
                                }
                            }
                        }

                        if (lblFieldName.Text == "Phone")
                        {
                            if (cbConfirmed.Checked)
                            {
                                if (editMerchant.Contact != null)
                                {
                                    AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.Contact.HomePhone, lblNewValue.Text);

                                    editMerchant.Contact.HomePhone = lblNewValue.Text.Trim();
                                }
                                else
                                {
                                    AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, "", lblNewValue.Text);

                                    ContactModel newContact = new ContactModel();
                                    newContact.HomePhone = lblNewValue.Text.Trim();
                                    _globalCtx.Contacts.Add(newContact);
                                    editMerchant.Contact = newContact;
                                }
                            }
                            else
                            {
                                if (editMerchant.Contact != null)
                                {
                                    txtContactPhone.Text = editMerchant.Contact.HomePhone;
                                }
                                else
                                {
                                    txtContactPhone.Text = "";
                                }
                            }
                        }

                        if (lblFieldName.Text == "Fax")
                        {
                            if (cbConfirmed.Checked)
                            {
                                if (editMerchant.Contact != null)
                                {
                                    AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.Contact.Fax, lblNewValue.Text);

                                    editMerchant.Contact.Fax = lblNewValue.Text.Trim();
                                }
                                else
                                {
                                    AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, "", lblNewValue.Text);

                                    ContactModel newContact = new ContactModel();
                                    newContact.Fax = lblNewValue.Text.Trim();
                                    _globalCtx.Contacts.Add(newContact);
                                    editMerchant.Contact = newContact;
                                }
                            }
                            else
                            {
                                if (editMerchant.Contact != null)
                                {
                                    txtContactFax.Text = editMerchant.Contact.Fax;
                                }
                                else
                                {
                                    txtContactFax.Text = "";
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                string userId = "";

                if (Request.IsAuthenticated)
                {
                    userId = Page.User.Identity.GetUserId();
                }
                _newLogic.WriteExceptionToDB(ex, "SaveContactChanges", 0, editMerchant.RecordId, userId);
            }
        }

        private void SaveBankingChanges(MerchantModel editMerchant, ApplicationDbContext ctx)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrBankingChanges.Items)
                {
                    Label lblFieldName = (Label)rItem.FindControl("lblFieldName3");
                    Label lblNewValue = (Label)rItem.FindControl("lblNewValue3");
                    CheckBox cbConfirmed = (CheckBox)rItem.FindControl("cbConfirmed3");

                    if (lblFieldName != null && lblNewValue != null && cbConfirmed != null)
                    {
                        if (lblFieldName.Text == "Bank Name")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.BankAccount.Bank.BankName, lblNewValue.Text);

                                editMerchant.BankAccount.Bank.BankName = lblNewValue.Text.Trim();
                            }
                            else
                            {
                                txtBankName.Text = editMerchant.BankAccount.Bank.BankName;
                            }
                        }

                        if (lblFieldName.Text == "Bank City")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.BankAccount.Bank.BankCity, lblNewValue.Text);

                                editMerchant.BankAccount.Bank.BankCity = lblNewValue.Text.Trim();
                            }
                            else
                            {
                                txtBankCity.Text = editMerchant.BankAccount.Bank.BankCity;
                            }
                        }

                        if (lblFieldName.Text == "Bank Phone")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.BankAccount.Bank.BankPhone, lblNewValue.Text);

                                editMerchant.BankAccount.Bank.BankPhone = lblNewValue.Text.Trim();
                            }
                            else
                            {
                                txtBankPhone.Text = editMerchant.BankAccount.Bank.BankPhone;
                            }
                        }

                        if (lblFieldName.Text == "Bank State")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.BankAccount.Bank.BankState.Name, lblNewValue.Text);

                                editMerchant.BankAccount.Bank.BankState = ctx.GeoStates.First(gs => gs.Name == lblNewValue.Text);
                            }
                            else
                            {
                                ddlBankState.SelectedValue = editMerchant.BankAccount.Bank.BankState.RecordId.ToString();
                            }
                        }

                        if (lblFieldName.Text == "Debit Card")
                        {
                            if (cbConfirmed.Checked)
                            {
                                string oldDebitCard = MaskDebitCard(editMerchant.DebitCard.DebitCardNumber, editMerchant.DebitCard.RecordId);

                                Logic.Crypt crypto1 = new Logic.Crypt();
                                crypto1.CryptType = "Card";
                                crypto1.CryptData = Convert.ToBase64String(PWDTK.StringToUtf8Bytes(lblNewValue.Text));
                                crypto1.Purpose = "9c443c6a-87d5-47cc-9c4d-abf6a2bd2e06";
                                crypto1.AdditionalData = editMerchant.DebitCard.RecordId.ToString();

                                byte[] newDebitCard = crypto1.Protect();

                                editMerchant.DebitCard.DebitCardNumber = newDebitCard;
                                editMerchant.DebitCard.DebitCardFirst6 = txtDebitCardNumber.Text.Substring(0, 6);
                                editMerchant.DebitCard.DebitCardLast4 = txtDebitCardNumber.Text.Substring(txtDebitCardNumber.Text.Length - 4, 4);
                                editMerchant.DebitCard.RemainingDigits = Convert.ToInt16(txtDebitCardNumber.Text.Length - 10);

                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, oldDebitCard, MaskDebitCard(newDebitCard, editMerchant.DebitCard.RecordId));
                            }
                            else
                            {
                                txtDebitCardNumber.Text = MaskDebitCard(editMerchant.DebitCard.DebitCardNumber, editMerchant.DebitCard.RecordId);
                            }

                            txtDebitCardNumber.Enabled = false;
                        }

                        if (lblFieldName.Text == "Account Number")
                        {
                            if (cbConfirmed.Checked)
                            {
                                string oldAccNum = MaskAccountNumber(editMerchant.BankAccount.AccountNumber, editMerchant.BankAccount.RecordId);

                                Logic.Crypt crypto2 = new Logic.Crypt();
                                crypto2.CryptType = "Account";
                                crypto2.CryptData = Convert.ToBase64String(PWDTK.StringToUtf8Bytes(lblNewValue.Text.Trim()));
                                crypto2.Purpose = "4742b06b-9e12-48cf-9034-aadb15dc7a58";
                                crypto2.AdditionalData = editMerchant.BankAccount.RecordId.ToString();

                                byte[] newAccNum = crypto2.Protect();

                                editMerchant.BankAccount.AccountNumber = newAccNum;
                                editMerchant.BankAccount.AccountNumberLast4 = txtAccountNumber.Text.Substring(txtAccountNumber.Text.Length - 4, 4);
                                editMerchant.BankAccount.RemainingDigits = Convert.ToInt16(txtAccountNumber.Text.Length - 4);

                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, oldAccNum, MaskAccountNumber(newAccNum, editMerchant.BankAccount.RecordId));
                            }
                            else
                            {
                                txtAccountNumber.Text = MaskAccountNumber(editMerchant.BankAccount.AccountNumber, editMerchant.BankAccount.RecordId);
                            }

                            txtAccountNumber.Enabled = false;
                        }

                        if (lblFieldName.Text == "Exp Month")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.DebitCard.DebitCardExpMonth, lblNewValue.Text);

                                editMerchant.DebitCard.DebitCardExpMonth = lblNewValue.Text;
                            }
                            else
                            {
                                ddlExpMonth.SelectedValue = editMerchant.DebitCard.DebitCardExpMonth.ToString();
                            }

                            ddlExpMonth.Enabled = false;
                        }

                        if (lblFieldName.Text == "Exp Year")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.DebitCard.DebitCardExpYear, lblNewValue.Text);

                                editMerchant.DebitCard.DebitCardExpYear = lblNewValue.Text;
                            }
                            else
                            {
                                ddlExpYear.SelectedValue = editMerchant.DebitCard.DebitCardExpYear.ToString();
                            }

                            ddlExpYear.Enabled = false;
                        }

                        if (lblFieldName.Text == "Routing Number")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.BankAccount.RoutingNumber, lblNewValue.Text);

                                editMerchant.BankAccount.RoutingNumber = lblNewValue.Text.Trim();
                            }
                            else
                            {
                                txtRoutingNumber.Text = editMerchant.BankAccount.RoutingNumber;
                            }

                            txtRoutingNumber.Enabled = false;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                string userId = "";

                if (Request.IsAuthenticated)
                {
                    userId = Page.User.Identity.GetUserId();
                }
                _newLogic.WriteExceptionToDB(ex, "SaveBankingChanges", 0, editMerchant.RecordId, userId);
            }
        }

        private void SaveMerchantAccountChanges(MerchantModel editMerchant, ApplicationDbContext ctx)
        {
            try
            {

                foreach (RepeaterItem rItem in rptrMerchantAccountChanges.Items)
                {
                    Label lblFieldName = (Label)rItem.FindControl("lblFieldName4");
                    Label lblNewValue = (Label)rItem.FindControl("lblNewValue4");
                    CheckBox cbConfirmed = (CheckBox)rItem.FindControl("cbConfirmed4");

                    if (lblFieldName != null && lblNewValue != null && cbConfirmed != null)
                    {
                        if (lblFieldName.Text == "Swiped %")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.SwipedPct.Value.ToString() + "%", lblNewValue.Text);

                                editMerchant.SwipedPct = Convert.ToInt32(lblNewValue.Text.Substring(0, lblNewValue.Text.Length - 2));
                            }
                            else
                            {
                                txtSwipedPct.Text = editMerchant.SwipedPct.ToString() ?? "";
                            }
                        }

                        if (lblFieldName.Text == "Avg Monthly Sales")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, "$" + editMerchant.AvgMonthlySales.Value.ToString(), lblNewValue.Text);

                                editMerchant.AvgMonthlySales = Convert.ToDecimal(lblNewValue.Text.Trim().Replace("$", ""));
                            }
                            else
                            {
                                txtAvgMonthlySales.Text = editMerchant.AvgMonthlySales.ToString() ?? "";
                            }
                        }

                        if (lblFieldName.Text == "Highest Monthly Sales")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, "$" + editMerchant.HighestMonthlySales.Value.ToString(), lblNewValue.Text);

                                editMerchant.HighestMonthlySales = Convert.ToDecimal(lblNewValue.Text.Trim().Replace("$", ""));
                            }
                            else
                            {
                                txtHighestMonthlySales.Text = editMerchant.HighestMonthlySales.ToString() ?? "";
                            }
                        }

                        if (lblFieldName.Text == "Avg Weekly Sales")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, "$" + editMerchant.AvgWeeklySales.Value.ToString(), lblNewValue.Text);

                                editMerchant.AvgWeeklySales = Convert.ToDecimal(lblNewValue.Text.Trim().Replace("$", ""));
                            }
                            else
                            {
                                txtAvgWeeklySales.Text = editMerchant.AvgWeeklySales.ToString() ?? "";
                            }
                        }

                        if (lblFieldName.Text == "Processor")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.Processor.ProcessorName, lblNewValue.Text);

                                //Check if merchant processor already exists, if so link to merchant.  If not, create it and add to merchant.
                                if (ctx.Processor.Any(p => p.ProcessorName == txtCardProcessor.Text.Trim()))
                                {
                                    editMerchant.Processor = ctx.Processor.First(p => p.ProcessorName == txtCardProcessor.Text.Trim());
                                }
                                else
                                {
                                    ProcessorModel newProcessor = new ProcessorModel();
                                    newProcessor.ProcessorName = txtCardProcessor.Text;
                                    ctx.Processor.Add(newProcessor);
                                    editMerchant.Processor = newProcessor;
                                }
                            }
                            else
                            {
                                if (editMerchant.Processor != null)
                                {
                                    txtCardProcessor.Text = editMerchant.Processor.ProcessorName;
                                }
                                else
                                {
                                    txtCardProcessor.Text = "";
                                }
                            }
                        }

                        if (lblFieldName.Text == "Merchant Id")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.MerchantId, lblNewValue.Text);

                                editMerchant.MerchantId = lblNewValue.Text.Trim();
                            }
                            else
                            {
                                txtMerchantId.Text = editMerchant.MerchantId ?? "";
                            }
                        }

                        if (lblFieldName.Text == "High Risk")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.HighRisk.Value.ToString(), lblNewValue.Text.Trim());

                                editMerchant.HighRisk = Convert.ToBoolean(lblNewValue.Text.Trim());

                                if (lblNewValue.Text.Trim() == "False")
                                {
                                    editMerchant.HighRiskWho = "";
                                    editMerchant.HighRiskDate = null;
                                    txtHighRiskWho.Text = "";
                                    ddlHighRiskDay.SelectedIndex = -1;
                                    ddlHighRiskMonth.SelectedIndex = -1;
                                    ddlHighRiskYear.SelectedIndex = -1;
                                }
                            }
                            else
                            {
                                if (editMerchant.HighRisk != null)
                                {
                                    rblHighRisk.SelectedValue = editMerchant.HighRisk.ToString();
                                }
                                else
                                {
                                    rblHighRisk.SelectedIndex = -1;
                                }
                            }
                        }

                        if (lblFieldName.Text == "High Risk Who")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.HighRiskWho, lblNewValue.Text.Trim());

                                editMerchant.HighRiskWho = lblNewValue.Text.Trim();
                            }
                            else
                            {
                                txtHighRiskWho.Text = editMerchant.HighRiskWho ?? "";
                            }
                        }

                        if (lblFieldName.Text == "High Risk Date")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.HighRiskDate.Value.ToString(), lblNewValue.Text.Trim());

                                editMerchant.HighRiskDate = Convert.ToDateTime(lblNewValue.Text.Trim());
                            }
                            else
                            {
                                if (editMerchant.HighRiskDate.HasValue)
                                {
                                    ddlHighRiskYear.SelectedValue = editMerchant.HighRiskDate.Value.Year.ToString();
                                    ddlHighRiskMonth.SelectedValue = editMerchant.HighRiskDate.Value.Year.ToString();
                                    ddlHighRiskDay.SelectedValue = editMerchant.HighRiskDate.Value.Year.ToString();
                                }
                                else
                                {
                                    ddlHighRiskYear.SelectedIndex = -1;
                                    ddlHighRiskMonth.SelectedIndex = -1;
                                    ddlHighRiskDay.SelectedIndex = -1;
                                }
                            }
                        }

                        if (lblFieldName.Text == "Bankruptcy")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.Bankruptcy.Value.ToString(), lblNewValue.Text.Trim());

                                editMerchant.Bankruptcy = Convert.ToBoolean(lblNewValue.Text.Trim());

                                if (lblNewValue.Text.Trim() == "False")
                                {
                                    editMerchant.BankruptcyDate = null;
                                    ddlBankruptcyDay.SelectedIndex = -1;
                                    ddlBankruptcyMonth.SelectedIndex = -1;
                                    ddlBankruptcyYear.SelectedIndex = -1;
                                }
                            }
                            else
                            {
                                if (editMerchant.Bankruptcy.HasValue)
                                {
                                    rblBankruptcy.SelectedValue = editMerchant.Bankruptcy.ToString();
                                }
                                else
                                {
                                    rblBankruptcy.SelectedIndex = -1;
                                }
                            }
                        }

                        if (lblFieldName.Text == "Bankruptcy Date")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.BankruptcyDate.Value.ToString(), lblNewValue.Text.Trim());

                                editMerchant.BankruptcyDate = Convert.ToDateTime(lblNewValue.Text.Trim());
                            }
                            else
                            {
                                if (editMerchant.BankruptcyDate.HasValue)
                                {
                                    ddlBankruptcyYear.SelectedValue = editMerchant.BankruptcyDate.Value.Year.ToString();
                                    ddlBankruptcyMonth.SelectedValue = editMerchant.BankruptcyDate.Value.Year.ToString();
                                    ddlBankruptcyDay.SelectedValue = editMerchant.BankruptcyDate.Value.Year.ToString();
                                }
                                else
                                {
                                    ddlBankruptcyYear.SelectedIndex = -1;
                                    ddlBankruptcyMonth.SelectedIndex = -1;
                                    ddlBankruptcyDay.SelectedIndex = -1;
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                string userId = "";

                if (Request.IsAuthenticated)
                {
                    userId = Page.User.Identity.GetUserId();
                }
                _newLogic.WriteExceptionToDB(ex, "SaveMerchantAccountChanges", 0, editMerchant.RecordId, userId);
            }
        }

        private void SaveAdvancePlanChanges(MerchantModel editMerchant, ApplicationDbContext ctx)
        {
            try
            {

                foreach (RepeaterItem rItem in rptrAdvancePlanChanges.Items)
                {
                    Label lblFieldName = (Label)rItem.FindControl("lblFieldName5");
                    Label lblNewValue = (Label)rItem.FindControl("lblNewValue5");
                    CheckBox cbConfirmed = (CheckBox)rItem.FindControl("cbConfirmed5");

                    if (lblFieldName != null && lblNewValue != null && cbConfirmed != null)
                    {
                        if (lblFieldName.Text == "Advance Plan")
                        {
                            if (cbConfirmed.Checked)
                            {
                                AddChangeToAudit(editMerchant.RecordId, lblFieldName.Text, editMerchant.AdvancePlan.PlanName, lblNewValue.Text.Trim());

                                editMerchant.AdvancePlan = ctx.AdvancePlans.First(ap => ap.PlanName == lblNewValue.Text);
                            }
                            else
                            {
                                ddlActivePlan.SelectedValue = editMerchant.AdvancePlan.RecordId.ToString();
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                string userId = "";

                if (Request.IsAuthenticated)
                {
                    userId = Page.User.Identity.GetUserId();
                }
                _newLogic.WriteExceptionToDB(ex, "SaveAdvancePlanChanges", 0, editMerchant.RecordId, userId);
            }

        }





        protected void ddlLicenseDateYear_DataBinding(object sender, EventArgs e)
        {
            if (DataBinder.Eval(DataItem, "BusLicDate") != null && ddlLicenseDateYear.SelectedIndex == -1)
            {
                ddlLicenseDateYear.SelectedValue = DateTime.Parse(DataBinder.Eval(DataItem, "BusLicDate").ToString()).Year.ToString();
            }
        }

        protected void ddlLicenseDateMonth_DataBinding(object sender, EventArgs e)
        {
            if (DataBinder.Eval(DataItem, "BusLicDate") != null && ddlLicenseDateMonth.SelectedIndex == -1)
            {
                ddlLicenseDateMonth.SelectedValue = DateTime.Parse(DataBinder.Eval(DataItem, "BusLicDate").ToString()).Month.ToString();
            }

        }

        protected void ddlLicenseDateDay_DataBinding(object sender, EventArgs e)
        {
            if (DataBinder.Eval(DataItem, "BusLicDate") != null && ddlLicenseDateDay.SelectedIndex == -1)
            {
                ddlLicenseDateDay.SelectedValue = DateTime.Parse(DataBinder.Eval(DataItem, "BusLicDate").ToString()).Day.ToString();
            }

        }

        protected void ddlBirthYear_DataBinding(object sender, EventArgs e)
        {
            if (DataBinder.Eval(DataItem, "MerchantPrincipal.PrincipalDoB") != null && ddlBirthYear.SelectedIndex == -1)
            {
                ddlBirthYear.SelectedValue = DateTime.Parse(DataBinder.Eval(DataItem, "MerchantPrincipal.PrincipalDoB").ToString()).Year.ToString();
            }
        }

        protected void ddlBirthMonth_DataBinding(object sender, EventArgs e)
        {
            if (DataBinder.Eval(DataItem, "MerchantPrincipal.PrincipalDoB") != null && ddlBirthMonth.SelectedIndex == -1)
            {
                ddlBirthMonth.SelectedValue = DateTime.Parse(DataBinder.Eval(DataItem, "MerchantPrincipal.PrincipalDoB").ToString()).Month.ToString();
            }
        }

        protected void ddlBirthDay_DataBinding(object sender, EventArgs e)
        {
            if (DataBinder.Eval(DataItem, "MerchantPrincipal.PrincipalDoB") != null && ddlBirthDay.SelectedIndex == -1)
            {
                ddlBirthDay.SelectedValue = DateTime.Parse(DataBinder.Eval(DataItem, "MerchantPrincipal.PrincipalDoB").ToString()).Day.ToString();
            }
        }

        protected void ddlHighRiskYear_DataBinding(object sender, EventArgs e)
        {
            if (DataBinder.Eval(DataItem, "HighRiskDate") != null && ddlHighRiskYear.SelectedIndex == -1)
            {
                ddlHighRiskYear.SelectedValue = DateTime.Parse(DataBinder.Eval(DataItem, "HighRiskDate").ToString()).Year.ToString();
            }
        }

        protected void ddlHighRiskMonth_DataBinding(object sender, EventArgs e)
        {
            if (DataBinder.Eval(DataItem, "HighRiskDate") != null && ddlHighRiskMonth.SelectedIndex == -1)
            {
                ddlHighRiskMonth.SelectedValue = DateTime.Parse(DataBinder.Eval(DataItem, "HighRiskDate").ToString()).Month.ToString();
            }
        }

        protected void ddlHighRiskDay_DataBinding(object sender, EventArgs e)
        {
            if (DataBinder.Eval(DataItem, "HighRiskDate") != null && ddlHighRiskDay.SelectedIndex == -1)
            {
                ddlHighRiskDay.SelectedValue = DateTime.Parse(DataBinder.Eval(DataItem, "HighRiskDate").ToString()).Day.ToString();
            }
        }

        protected void ddlBankruptcyYear_DataBinding(object sender, EventArgs e)
        {
            if (DataBinder.Eval(DataItem, "BankruptcyDate") != null && ddlBankruptcyYear.SelectedIndex == -1)
            {
                ddlBankruptcyYear.SelectedValue = DateTime.Parse(DataBinder.Eval(DataItem, "BankruptcyDate").ToString()).Year.ToString();
            }
        }

        protected void ddlBankruptcyMonth_DataBinding(object sender, EventArgs e)
        {
            if (DataBinder.Eval(DataItem, "BankruptcyDate") != null && ddlBankruptcyMonth.SelectedIndex == -1)
            {
                ddlBankruptcyMonth.SelectedValue = DateTime.Parse(DataBinder.Eval(DataItem, "BankruptcyDate").ToString()).Month.ToString();
            }
        }

        protected void ddlBankruptcyDay_DataBinding(object sender, EventArgs e)
        {
            if (DataBinder.Eval(DataItem, "BankruptcyDate") != null && ddlBankruptcyDay.SelectedIndex == -1)
            {
                ddlBankruptcyDay.SelectedValue = DateTime.Parse(DataBinder.Eval(DataItem, "BankruptcyDate").ToString()).Day.ToString();
            }
        }



        protected void btnBeginUnderwritingUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                BeginNewUnderwriting();

            }
            catch (System.Exception ex)
            {
                string userId = "";

                if (Request.IsAuthenticated)
                {
                    userId = Page.User.Identity.GetUserId();
                }

                _newLogic.WriteExceptionToDB(ex, "BeginUnderwriting", 0, 0, userId);
            }
        }

        public void BeginNewUnderwriting()
        {
            MerchantModel editMerchant = new MerchantModel();
            UnderwritingResultModel underwritingResult = new UnderwritingResultModel();
            UnderwritingResultModel oldUnderwritingResult = new UnderwritingResultModel();
            String userId = "";
            Int32 merchantRecordId;

            try
            {
                txtUnderwriterInitials.Enabled = false;

                merchantRecordId = Convert.ToInt32(hMerchantRecordId.Value);
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    editMerchant = ctx.Merchants.First(m => m.RecordId == merchantRecordId);

                    if (ctx.UnderwritingResults.Any(ur => ur.Merchant.RecordId == merchantRecordId && ur.Active == true))
                    {
                        oldUnderwritingResult = ctx.UnderwritingResults.Where(ur => ur.Merchant.RecordId == merchantRecordId && ur.Active == true).First();
                        oldUnderwritingResult.Active = false;
                    }

                    underwritingResult.Brand = editMerchant.Brand;
                    underwritingResult.Merchant = editMerchant;
                    underwritingResult.Timestamp = DateTime.UtcNow;
                    underwritingResult.Active = true;
                    underwritingResult.UnderwriterInitials = txtUnderwriterInitials.Text;
                    if (Request.IsAuthenticated)
                    {
                        userId = Page.User.Identity.GetUserId();
                        underwritingResult.UnderwriterUser = ctx.Users.First(u => u.Id == userId);
                    }

                    ctx.UnderwritingResults.Add(underwritingResult);

                    ctx.SaveChanges();
                }

                btnBeginUnderwritingUpdate.Enabled = false;
                pnlUnderwritingButtons.Visible = true;
                btnAddNewUnderwriting.Visible = false;
                btnUnderwritingContinue.Visible = true;
                btnUnderwritingCancel2.Visible = true;

                //Connect to LexisNexis to get automated results
                AutomatedUnderwriting(editMerchant.RecordId, underwritingResult.RecordId);



                ////Enable Radio Button Fields
                //rblUWCorpInfoVerifiedResult.Enabled = true;
                //rblUWBusLicStatusResult.Enabled = true;
                //rblUWEINVerifiedResult.Enabled = true;
                //rblUWPrincipalVerifiedResult.Enabled = true;
                //rblUWCardSalesIndicatorsVerifiedResult.Enabled = true;
                //rblUWBankingInfoVerifiedResult.Enabled = true;
                //rblUWMCCVerifiedResult.Enabled = true;
                //rblUWBVIVerifiedResult.Enabled = true;
                //rblUWTaxLiensVerifiedResult.Enabled = true;
                //rblUWRiskIndicatorVerifiedResult.Enabled = true;


                ////Enable Text Area Fields
                //txtUWCorpInfoVerifiedNotes.Enabled = true;
                //txtUWBUsLicStatusVerifiedNotes.Enabled = true;
                //txtUWEINVerifiedNotes.Enabled = true;
                //txtUWPrincipalVerifiedNotes.Enabled = true;
                //txtUWCardSalesIndicatorsVerifiedNotes.Enabled = true;
                //txtUWBankingInfoVerifiedNotes.Enabled = true;
                //txtUWMCCVerifiedNotes.Enabled = true;
                //txtUWBVIVerifiedNotes.Enabled = true;
                //txtUWTaxLiensVerifiedNotes.Enabled = true;
                //txtUWRiskIndicatorVerifiedNotes.Enabled = true;





            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "BeginUnderwriting", 0, 0, userId);
            }
        }

        public void AutomatedUnderwriting(Int32 MerchantId, Int32 UnderwritingResultId)
        {
            try
            {
                String displayResults = "";

                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    if (ctx.Merchants.Any(m => m.RecordId == MerchantId) && ctx.UnderwritingResults.Any(ur => ur.RecordId == UnderwritingResultId))
                    {
                        var editMerchant = ctx.Merchants.First(m => m.RecordId == MerchantId);
                        var underwritingResults = ctx.UnderwritingResults.FirstOrDefault(ur => ur.RecordId == UnderwritingResultId);
                        var underwritingSchema = editMerchant.AdvancePlan.UnderwritingSchema;

                        if (editMerchant != null && underwritingResults != null)
                        {
                            String baseUrl = "";
                            String apiPath = "api/Underwriting";
                            XmlDocument xmlDoc = new XmlDocument();
                            String riskCodes = "";
                            String isDemo = "0";

                            if (!String.IsNullOrEmpty(_miqroSettings["d9444814-043a-44f2-982c-913c55d84746"]))
                            {
                                baseUrl = _miqroSettings["d9444814-043a-44f2-982c-913c55d84746"];

                                if (!String.IsNullOrEmpty(baseUrl))
                                {
                                    var client = new RestClient(baseUrl);
                                    var request = new RestRequest(apiPath, Method.POST);
                                    request.AddHeader("Accept", "application/xml");
                                    request.Parameters.Clear();

                                    request.AddParameter("CorpName", editMerchant.CorpName ?? "");
                                    request.AddParameter("DbaName", editMerchant.DbaName ?? "");
                                    request.AddParameter("FEIN", editMerchant.FedTaxId ?? "");
                                    if (editMerchant.Business != null)
                                    {
                                        request.AddParameter("CorpPhone", editMerchant.Business.HomePhone ?? "");
                                        if (editMerchant.Business.Address != null)
                                        {
                                            request.AddParameter("CorpAddress", editMerchant.Business.Address.Address ?? "");
                                            request.AddParameter("CorpCity", editMerchant.Business.Address.City ?? "");
                                            request.AddParameter("CorpState", editMerchant.Business.Address.State.Abbreviation ?? "");
                                            request.AddParameter("CorpZip", editMerchant.Business.Address.Zip ?? "");
                                        }
                                    }
                                    else
                                    {
                                        request.AddParameter("CorpPhone", "");
                                        request.AddParameter("CorpAddress", "");
                                        request.AddParameter("CorpCity", "");
                                        request.AddParameter("CorpState", "");
                                        request.AddParameter("CorpZip", "");
                                    }

                                    isDemo = _miqroSettings["3e1acfa5-73dd-4573-8f3c-e7e46aef39dc"] ?? "0";

                                    if (isDemo == "1")
                                    {
                                        request.AddParameter("ARFirst", editMerchant.MerchantPrincipal.Contact.FirstName);
                                        request.AddParameter("ARMiddle", editMerchant.MerchantPrincipal.Contact.MiddleInitial);
                                        request.AddParameter("ARLast", editMerchant.MerchantPrincipal.Contact.LastName);
                                        if (editMerchant.MerchantPrincipal.Contact != null && editMerchant.MerchantPrincipal.Contact.Address != null)
                                        {
                                            request.AddParameter("ARAddress", editMerchant.MerchantPrincipal.Contact.Address.Address ?? "");
                                            request.AddParameter("ARCity", editMerchant.MerchantPrincipal.Contact.Address.City ?? "");
                                            request.AddParameter("ARState", editMerchant.MerchantPrincipal.Contact.Address.State.Abbreviation ?? "");
                                            request.AddParameter("ARZip", editMerchant.MerchantPrincipal.Contact.Address.Zip ?? "");
                                        }
                                        else
                                        {
                                            request.AddParameter("ARAddress", "");
                                            request.AddParameter("ARCity", "");
                                            request.AddParameter("ARState", "");
                                            request.AddParameter("ARZip", "");
                                        }
                                        request.AddParameter("ARAge", "");
                                        if (editMerchant.MerchantPrincipal.PrincipalDoB.HasValue)
                                        {
                                            request.AddParameter("ARDoBYear", editMerchant.MerchantPrincipal.PrincipalDoB.Value.Year.ToString() ?? "");
                                            request.AddParameter("ARDoBMonth", editMerchant.MerchantPrincipal.PrincipalDoB.Value.Month.ToString() ?? "");
                                            request.AddParameter("ARDoBDay", editMerchant.MerchantPrincipal.PrincipalDoB.Value.Day.ToString() ?? "");
                                        }
                                        else
                                        {
                                            request.AddParameter("ARDoBYear", "");
                                            request.AddParameter("ARDoBMonth", "");
                                            request.AddParameter("ARDoBDay", "");
                                        }
                                        if (editMerchant.MerchantPrincipal.PrincipalSsn != null)
                                        {
                                            request.AddParameter("ARSSN", PWDTK.Utf8BytesToString(editMerchant.MerchantPrincipal.PrincipalSsn));
                                        }
                                        else
                                        {
                                            request.AddParameter("ARSSN", "");
                                        }
                                        if (editMerchant.MerchantPrincipal.PrincipalDLNumber != null)
                                        {
                                            request.AddParameter("ARDLNumber", PWDTK.Utf8BytesToString(editMerchant.MerchantPrincipal.PrincipalDLNumber));
                                        }
                                        else
                                        {
                                            request.AddParameter("ARDLNumber", "");
                                        }
                                        if (editMerchant.MerchantPrincipal.PrincipalDLState != null)
                                        {
                                            request.AddParameter("ARDLState", editMerchant.MerchantPrincipal.PrincipalDLState.Abbreviation);
                                        }
                                        else
                                        {
                                            request.AddParameter("ARDLState", "");
                                        }
                                        request.AddParameter("ARPhone", editMerchant.MerchantPrincipal.Contact.HomePhone ?? "");
                                        request.AddParameter("ARFormerLast", "");
                                    }
                                    else
                                    {
                                        request.AddParameter("ARFirst", "");
                                        request.AddParameter("ARMiddle", "");
                                        request.AddParameter("ARLast", "");
                                        request.AddParameter("ARAddress", "");
                                        request.AddParameter("ARCity", "");
                                        request.AddParameter("ARState", "");
                                        request.AddParameter("ARZip", "");
                                        request.AddParameter("ARAge", "");
                                        request.AddParameter("ARDoBYear", "");
                                        request.AddParameter("ARDoBMonth", "");
                                        request.AddParameter("ARDoBDay", "");
                                        request.AddParameter("ARSSN", "");
                                        request.AddParameter("ARDLNumber", "");
                                        request.AddParameter("ARDLState", "");
                                        request.AddParameter("ARPhone", "");
                                        request.AddParameter("ARFormerLast", "");
                                    }

                                    IRestResponse response = client.Execute(request);

                                    if (response != null)
                                    {
                                        var content = response.Content;

                                        XNode node = JsonConvert.DeserializeXNode(content.ToString());

                                        XDocument xDoc = XDocument.Parse(node.ToString());

                                        if (xDoc != null)
                                        {
                                            LexisNexisUnderwritingResultModel autoUnderwritingResults = new LexisNexisUnderwritingResultModel();
                                            Boolean uwComplete = true;

                                            LexisNexisRawResultModel rawXml = new LexisNexisRawResultModel();

                                            rawXml.Timestamp = DateTime.UtcNow;
                                            rawXml.XmlContent = xDoc.ToString();

                                            ctx.LexisNexisRawResults.Add(rawXml);
                                            ctx.SaveChanges();

                                            autoUnderwritingResults.Timestamp = DateTime.UtcNow;
                                            autoUnderwritingResults.LexisNexisRawResult = ctx.LexisNexisRawResults.FirstOrDefault(lnrr => lnrr.RecordId == rawXml.RecordId);
                                            autoUnderwritingResults.Version = "1.7";

                                            if (xDoc.Descendants("Exceptions").Any())
                                            {
                                                autoUnderwritingResults.Error = true;

                                                var exceptions = xDoc.Descendants("Exceptions")
                                                    .Where(x => x.Parent.Name == "Header")
                                                    .Select(x => new
                                                    {
                                                        Code = (String)x.Element("Code") ?? "",
                                                        Source = (String)x.Element("Source") ?? "",
                                                        Message = (String)x.Element("Message") ?? ""
                                                    });

                                                if (exceptions != null)
                                                {
                                                    foreach (var e in exceptions)
                                                    {
                                                        LexisNexisExceptionModel exception = new LexisNexisExceptionModel();

                                                        exception.Timestamp = DateTime.UtcNow;
                                                        exception.Code = e.Code;
                                                        exception.Source = e.Source;
                                                        exception.Message = e.Message;
                                                        exception.UnderwritingResult = underwritingResults;

                                                        ctx.LexisNexisExceptions.Add(exception);
                                                    }

                                                    ctx.SaveChanges();
                                                }
                                            }

                                            if (xDoc.Descendants("Header").Any())
                                            {
                                                var headerInfo = xDoc.Descendants("Header")
                                                    .Select(x => new
                                                    {
                                                        TransactionId = (String)x.Element("TransactionId") ?? "",
                                                        QueryId = (String)x.Element("QueryId") ?? "",
                                                        Status = (Int16?)x.Element("Status") ?? 999,
                                                        Message = (String)x.Element("Message") ?? ""
                                                    })
                                                    .FirstOrDefault();

                                                autoUnderwritingResults.TransactionId = headerInfo.TransactionId;
                                                autoUnderwritingResults.QueryId = headerInfo.QueryId;
                                                autoUnderwritingResults.Status = headerInfo.Status;
                                                autoUnderwritingResults.Message = headerInfo.Message;

                                                displayResults += "Transaction ID: " + headerInfo.TransactionId + "<br />";
                                                displayResults += "Query ID: " + headerInfo.QueryId + "<br />";
                                                displayResults += "Status: " + headerInfo.Status + "<br /><br />";

                                                if (headerInfo.Message != "")
                                                {
                                                    displayResults += "Message: " + headerInfo.Message + "<br /><br />";
                                                }

                                            }
                                            else
                                            {
                                                uwComplete = false;
                                            }

                                            if (xDoc.Descendants("CompanyResults").Any())
                                            {
                                                if (xDoc.Descendants("WatchList").Any())
                                                {
                                                    var watchLists = xDoc.Descendants("WatchList")
                                                         .Where(x => x.Parent.Parent.Name == "CompanyResults")
                                                         .Select(x => new
                                                         {
                                                             Table = (String)x.Element("Table") ?? "",
                                                             RecordNumber = (String)x.Element("RecordNumber") ?? "",
                                                             EntityName = (String)x.Element("EntityName") ?? "",
                                                             Country = (String)x.Element("Country") ?? "",
                                                             Sequence = (String)x.Element("Sequence") ?? ""
                                                         });

                                                    foreach (var wL in watchLists)
                                                    {
                                                        if (wL.Table.Contains("Office of Foreign Asset"))
                                                        {
                                                            autoUnderwritingResults.OFAC = true;
                                                            autoUnderwritingResults.OFACMatchData = "Table: " + wL.Table + " -- RecordNumber: " + wL.RecordNumber + " -- EntityName: " +
                                                                wL.EntityName + " -- Country: " + wL.Country + " -- Sequence: " + wL.Sequence;
                                                        }

                                                        displayResults += "<b>Office of Foreign Asset Control Match</b><br />";
                                                    }
                                                }

                                                var companyResults = xDoc.Descendants("CompanyResults")
                                                    .Select(x => new
                                                    {
                                                        BVI = (int?)x.Element("BusinessVerificationIndicator") ?? 0,
                                                        UnreleasedLienCounter = (int?)x.Element("UnreleasedLienCounter") ?? 0,
                                                        SIC = (String)x.Element("SICCode") ?? "",
                                                        RecentLienType = (String)x.Element("RecentLienType") ?? "",
                                                        BusinessDescription = (String)x.Element("BusinessDescription") ?? ""
                                                    })
                                                    .FirstOrDefault();

                                                autoUnderwritingResults.BVI = companyResults.BVI;
                                                if (companyResults.SIC.Length >= 4)
                                                {
                                                    autoUnderwritingResults.Mcc = companyResults.SIC.Substring(0, 4);
                                                }
                                                else
                                                {
                                                    autoUnderwritingResults.Mcc = companyResults.SIC;
                                                }
                                                autoUnderwritingResults.UnreleasedLienCount = companyResults.UnreleasedLienCounter;
                                                autoUnderwritingResults.BusinessDescription = companyResults.BusinessDescription;

                                                if (companyResults.SIC.Length > 4)
                                                {
                                                    autoUnderwritingResults.AdditionalMccDigits = companyResults.SIC.Substring(4);
                                                }

                                                if (companyResults.RecentLienType != "")
                                                {
                                                    if (ctx.LexisNexisLienTypes.Any(lnlt => lnlt.TypeName == companyResults.RecentLienType))
                                                    {
                                                        autoUnderwritingResults.RecentLienType = ctx.LexisNexisLienTypes.First(lnlt => lnlt.TypeName == companyResults.RecentLienType);
                                                    }
                                                    else
                                                    {
                                                        autoUnderwritingResults.UnmatchedLienType = companyResults.RecentLienType;
                                                    }
                                                }

                                                displayResults += "BVI: " + companyResults.BVI + "<br />";

                                                displayResults += "SIC: ";
                                                if (companyResults.SIC == editMerchant.Mcc.MerchantCategoryCode)
                                                {
                                                    if (underwritingSchema.ApprovedMCCList.Any(x => x.MCC.MerchantCategoryCode == companyResults.SIC))
                                                    {
                                                        displayResults += "Match and Approved MCC<br />";
                                                        autoUnderwritingResults.MccMatch = true;
                                                    }
                                                    else
                                                    {
                                                        displayResults += "Match, but MCC Not Approved<br />";
                                                        autoUnderwritingResults.MccMatch = false;
                                                    }
                                                }
                                                else if (companyResults.SIC == "")
                                                {
                                                    displayResults += "No SIC Found<br />";
                                                    autoUnderwritingResults.MccMatch = false;
                                                }
                                                else
                                                {
                                                    displayResults += "SIC Mismatch (" + companyResults.SIC + ")<br />";
                                                    autoUnderwritingResults.MccMatch = false;
                                                }


                                                if (companyResults.UnreleasedLienCounter > 0)
                                                {
                                                    displayResults += "<br />Unreleased Liens<br />";
                                                    displayResults += "Count: " + companyResults.UnreleasedLienCounter + "<br />";
                                                    displayResults += "Type: " + companyResults.RecentLienType + "<br />";
                                                }

                                                if (xDoc.Descendants("RecentLienFilingDate").Any())
                                                {
                                                    var recentLienDate = xDoc.Descendants("RecentLienFilingDate")
                                                        .Select(x => new
                                                        {
                                                            Year = (int?)x.Element("Year"),
                                                            Month = (int?)x.Element("Month"),
                                                            Day = (int?)x.Element("Day")
                                                        })
                                                        .FirstOrDefault();

                                                    if (recentLienDate != null)
                                                    {
                                                        autoUnderwritingResults.RecentLienDate = DateTime.Parse(recentLienDate.Year + "-" + recentLienDate.Month + "-" + recentLienDate.Day);
                                                        displayResults += "Most Recent Lien: " + recentLienDate.Year + "-" + recentLienDate.Month + "-" + recentLienDate.Day + "<br />";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                uwComplete = false;
                                            }



                                            if (uwComplete == true)
                                            {
                                                ctx.LexisNexisResults.Add(autoUnderwritingResults);
                                                ctx.SaveChanges();

                                                if (xDoc.Descendants("RiskIndicator").Any())
                                                {
                                                    displayResults += "<br />Company Risk Codes <br />";
                                                    var cRiskIndicators = xDoc.Descendants("RiskIndicator")
                                                        .Where(x => x.Parent.Parent.Name == "CompanyResults")
                                                        .Select(x => new { RiskCode = x.Element("RiskCode").Value, Description = x.Element("Description").Value });

                                                    foreach (var r in cRiskIndicators)
                                                    {
                                                        LexisNexisRiskCodeResultModel riskCodeResults = new LexisNexisRiskCodeResultModel();

                                                        riskCodeResults.Timestamp = DateTime.UtcNow;
                                                        riskCodeResults.LexisNexisUnderwritingResult = ctx.LexisNexisResults.FirstOrDefault(lnr => lnr.RecordId == autoUnderwritingResults.RecordId);
                                                        riskCodeResults.RiskCode = ctx.LexisNexisRiskCodes.FirstOrDefault(lnrc => lnrc.RiskCode == r.RiskCode);

                                                        ctx.LexisNexisRiskCodeResults.Add(riskCodeResults);

                                                        displayResults += r.RiskCode + "  --  " + r.Description + "<br />";
                                                    }

                                                    underwritingResults.LexisNexisResults = ctx.LexisNexisResults.FirstOrDefault(lnr => lnr.RecordId == autoUnderwritingResults.RecordId);
                                                    ctx.SaveChanges();

                                                    FinalizeUnderwriting(autoUnderwritingResults);
                                                }
                                                else
                                                {
                                                    displayResults += "No Risk Indicators Found<br />";
                                                }
                                            }
                                            else
                                            {
                                                CancelUnderwriting();

                                                btnUnderwritingCancel.Visible = false;
                                                btnUnderwritingCancel2.Visible = false;
                                                btnUnderwritingContinue.Visible = false;
                                                btnUnderwritingSave.Visible = false;
                                                btnUnderwritingComplete.Visible = false;
                                                btnUnderwritingTryAgain.Visible = true;

                                                displayResults = "Underwriting was unable to be completed.  Please Cancel and try again or contact your System Administrator for more information.";
                                            }
                                        }
                                        else
                                        {
                                            displayResults = "Underwriting was unable to be completed.  Please Cancel and try again or contact your System Administrator for more information.";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                lblUnderwritingMessage.Text = displayResults;

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "AutomatedUnderwriting");
            }
        }

        public Boolean FinalizeUnderwriting(LexisNexisUnderwritingResultModel uwResults)
        {
            try
            {


                return true;
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "AutomatedUnderwriting");
                return false;
            }

        }

        public string GetUnderwritingResults(String fieldName)
        {
            try
            {
                Int32 merchantRecordId = Convert.ToInt32(hMerchantRecordId.Value);

                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    MerchantModel editMerchant = ctx.Merchants.First(m => m.RecordId == merchantRecordId);
                    UnderwritingResultModel underwritingResult = ctx.UnderwritingResults.Where(ur => ur.Merchant.RecordId == editMerchant.RecordId).OrderByDescending(ur => ur.Timestamp).First();

                    switch (fieldName)
                    {
                        case "CorpInfoResult":
                            return underwritingResult.CorpInfoResult.ToString();
                        case "CorpInfoNotes":
                            return underwritingResult.CorpInfoNotes;
                        case "BusLicStatusResult":
                            return underwritingResult.BusLicStatusResult.ToString();
                        case "BusLicStatusNotes":
                            return underwritingResult.BusLicStatusNotes;
                        default:
                            return null;

                    }

                }

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "GetUnderwritingResults");
                return null;
            }
        }

        protected void btnAddNewUnderwriting_Click(object sender, EventArgs e)
        {
            pnlUnderwritingHistory.Visible = false;
            pnlNewUnderwriting.Visible = true;
            pnlUnderwritingAdvancedButtons.Visible = false;
            pnlUnderwritingButtons.Visible = false;

            txtUnderwriterInitials.Text = "";
            txtUnderwriterInitials.Enabled = true;
            btnBeginUnderwritingUpdate.Enabled = true;
            lblUnderwritingMessage.Text = "";
            pnlNewUnderwriting2.Visible = false;
            btnUnderwritingCancel.Visible = false;
            btnUnderwritingCancel2.Visible = false;
            btnUnderwritingTryAgain.Visible = false;
            btnUnderwritingSave.Visible = false;
            btnAddNewUnderwriting.Visible = true;
            btnUnderwritingComplete.Visible = false;

        }

        protected void btnUnderwritingSave_Click(object sender, EventArgs e)
        {
            MerchantModel editMerchant = new MerchantModel();
            UnderwritingResultModel underwritingResult = new UnderwritingResultModel();
            UnderwritingResultModel oldUnderwritingResult = new UnderwritingResultModel();
            Int32 merchantRecordId;

            try
            {
                merchantRecordId = Convert.ToInt32(hMerchantRecordId.Value);
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    editMerchant = ctx.Merchants.First(m => m.RecordId == merchantRecordId);

                    underwritingResult = ctx.UnderwritingResults.Where(ur => ur.Merchant.RecordId == merchantRecordId && ur.Active == true).First();

                    underwritingResult.CorpInfoResult = Convert.ToInt16(rblUWCorpInfoVerifiedResult.SelectedValue);
                    underwritingResult.CorpInfoNotes = txtUWCorpInfoVerifiedNotes.Text;

                    underwritingResult.BusLicStatusResult = Convert.ToInt16(rblUWBusLicStatusResult.SelectedValue);
                    underwritingResult.BusLicStatusNotes = txtUWBUsLicStatusVerifiedNotes.Text;

                    underwritingResult.EINResult = Convert.ToInt16(rblUWEINVerifiedResult.SelectedValue);
                    underwritingResult.EINNotes = txtUWEINVerifiedNotes.Text;

                    underwritingResult.PrincipalResult = Convert.ToInt16(rblUWPrincipalVerifiedResult.SelectedValue);
                    underwritingResult.PrincipalNotes = txtUWPrincipalVerifiedNotes.Text;

                    underwritingResult.CardSalesIndicatorResult = Convert.ToInt16(rblUWCardSalesIndicatorsVerifiedResult.SelectedValue);
                    underwritingResult.CardSalesIndicatorNotes = txtUWCardSalesIndicatorsVerifiedNotes.Text;

                    underwritingResult.BankingInfoResult = Convert.ToInt16(rblUWBankingInfoVerifiedResult.SelectedValue);
                    underwritingResult.BankingInfoNotes = txtUWBankingInfoVerifiedNotes.Text;

                    underwritingResult.MCCResult = Convert.ToInt16(rblUWMCCVerifiedResult.SelectedValue);
                    underwritingResult.MCCNotes = txtUWMCCVerifiedNotes.Text;

                    underwritingResult.BVIResult = Convert.ToInt16(rblUWBVIVerifiedResult.SelectedValue);
                    underwritingResult.BVINotes = txtUWBVIVerifiedNotes.Text;

                    underwritingResult.TaxLiensResult = Convert.ToInt16(rblUWTaxLiensVerifiedResult.SelectedValue);
                    underwritingResult.TaxLiensNotes = txtUWTaxLiensVerifiedNotes.Text;

                    underwritingResult.RiskIndicatorResult = Convert.ToInt16(rblUWRiskIndicatorVerifiedResult.SelectedValue);
                    underwritingResult.RiskIndicatorNotes = txtUWRiskIndicatorVerifiedNotes.Text;

                    underwritingResult.UnderwritingDecision = "Pending";
                    editMerchant.UnderwritingStatus = ctx.UnderwritingStatuses.First(us => us.StatusDescription == "Pending");

                    ctx.SaveChanges();
                    UpdateStatusLabel();

                    lblUnderwritingMessage.Text = "Underwriting results have been successfully saved.";
                }

            }
            catch (System.Exception ex)
            {
                lblUnderwritingMessage.Text = "Underwriting results could not be saved.  Please try again or contact your system administrator.";
                _newLogic.WriteExceptionToDB(ex, "btnUnderwritingSave_Click");
            }
        }

        public void CancelUnderwriting()
        {
            MerchantModel editMerchant = new MerchantModel();
            UnderwritingResultModel underwritingResult = new UnderwritingResultModel();
            UnderwritingResultModel oldUnderwritingResult = new UnderwritingResultModel();
            LexisNexisExceptionModel lnException = new LexisNexisExceptionModel();

            Int32 merchantRecordId;

            try
            {
                merchantRecordId = Convert.ToInt32(hMerchantRecordId.Value);

                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    editMerchant = ctx.Merchants.First(m => m.RecordId == merchantRecordId);

                    underwritingResult = ctx.UnderwritingResults.FirstOrDefault(ur => ur.Merchant.RecordId == merchantRecordId && ur.Active == true);

                    lnException = ctx.LexisNexisExceptions.FirstOrDefault(lne => lne.UnderwritingResult.RecordId == underwritingResult.RecordId);

                    if (lnException != null)
                    {
                        ctx.LexisNexisExceptions.Remove(lnException);
                    }
                    if (underwritingResult != null)
                    {
                        ctx.UnderwritingResults.Remove(underwritingResult);
                    }

                    ctx.SaveChanges();

                    if (ctx.UnderwritingResults.Any(ur => ur.Merchant.RecordId == merchantRecordId))
                    {
                        oldUnderwritingResult = ctx.UnderwritingResults.Where(ur => ur.Merchant.RecordId == merchantRecordId).OrderByDescending(ur => ur.Timestamp).First();

                        oldUnderwritingResult.Active = true;

                        ctx.SaveChanges();
                    }

                    gridUnderwritingHistory.Rebind();


                }

                //Reset Radio Button Fields
                rblUWCorpInfoVerifiedResult.SelectedValue = "0";
                rblUWBusLicStatusResult.SelectedValue = "0";
                rblUWEINVerifiedResult.SelectedValue = "0";
                rblUWPrincipalVerifiedResult.SelectedValue = "0";
                rblUWCardSalesIndicatorsVerifiedResult.SelectedValue = "0";
                rblUWBankingInfoVerifiedResult.SelectedValue = "0";
                rblUWMCCVerifiedResult.SelectedValue = "0";
                rblUWBVIVerifiedResult.SelectedValue = "0";
                rblUWTaxLiensVerifiedResult.SelectedValue = "0";
                rblUWRiskIndicatorVerifiedResult.SelectedValue = "0";

                //Reset Text Area Fields
                txtUWCorpInfoVerifiedNotes.Text = "";
                txtUWBUsLicStatusVerifiedNotes.Text = "";
                txtUWEINVerifiedNotes.Text = "";
                txtUWPrincipalVerifiedNotes.Text = "";
                txtUWCardSalesIndicatorsVerifiedNotes.Text = "";
                txtUWBankingInfoVerifiedNotes.Text = "";
                txtUWMCCVerifiedNotes.Text = "";
                txtUWBVIVerifiedNotes.Text = "";
                txtUWTaxLiensVerifiedNotes.Text = "";
                txtUWRiskIndicatorVerifiedNotes.Text = "";
                txtUnderwriterInitials.Text = "";

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "CancelUnderwriting");
            }
        }

        protected void btnUnderwitingCancel_Click(object sender, EventArgs e)
        {
            CancelUnderwriting();

            txtUnderwriterInitials.Text = "";
            pnlNewUnderwriting.Visible = false;
            pnlNewUnderwriting2.Visible = false;
            pnlUnderwritingHistory.Visible = true;
            btnUnderwritingCancel.Visible = false;
            btnUnderwritingCancel2.Visible = false;
            btnUnderwritingTryAgain.Visible = false;
            btnUnderwritingSave.Visible = false;
            btnAddNewUnderwriting.Visible = true;
            btnUnderwritingComplete.Visible = false;
        }

        protected void btnUnderwritingComplete_Click(object sender, EventArgs e)
        {
            MerchantModel editMerchant = new MerchantModel();
            UnderwritingResultModel underwritingResult = new UnderwritingResultModel();
            UnderwritingResultModel oldUnderwritingResult = new UnderwritingResultModel();
            Int32 merchantRecordId;
            String adminId;

            try
            {
                merchantRecordId = Convert.ToInt32(hMerchantRecordId.Value);
                adminId = Context.User.Identity.GetUserId();

                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    editMerchant = ctx.Merchants.First(m => m.RecordId == merchantRecordId);

                    underwritingResult = ctx.UnderwritingResults.Where(ur => ur.Merchant.RecordId == merchantRecordId && ur.Active == true).First();

                    underwritingResult.CorpInfoResult = Convert.ToInt16(rblUWCorpInfoVerifiedResult.SelectedValue);
                    underwritingResult.CorpInfoNotes = txtUWCorpInfoVerifiedNotes.Text;

                    underwritingResult.BusLicStatusResult = Convert.ToInt16(rblUWBusLicStatusResult.SelectedValue);
                    underwritingResult.BusLicStatusNotes = txtUWBUsLicStatusVerifiedNotes.Text;

                    underwritingResult.EINResult = Convert.ToInt16(rblUWEINVerifiedResult.SelectedValue);
                    underwritingResult.EINNotes = txtUWEINVerifiedNotes.Text;

                    underwritingResult.PrincipalResult = Convert.ToInt16(rblUWPrincipalVerifiedResult.SelectedValue);
                    underwritingResult.PrincipalNotes = txtUWPrincipalVerifiedNotes.Text;

                    underwritingResult.CardSalesIndicatorResult = Convert.ToInt16(rblUWCardSalesIndicatorsVerifiedResult.SelectedValue);
                    underwritingResult.CardSalesIndicatorNotes = txtUWCardSalesIndicatorsVerifiedNotes.Text;

                    underwritingResult.BankingInfoResult = Convert.ToInt16(rblUWBankingInfoVerifiedResult.SelectedValue);
                    underwritingResult.BankingInfoNotes = txtUWBankingInfoVerifiedNotes.Text;

                    underwritingResult.MCCResult = Convert.ToInt16(rblUWMCCVerifiedResult.SelectedValue);
                    underwritingResult.MCCNotes = txtUWMCCVerifiedNotes.Text;

                    underwritingResult.BVIResult = Convert.ToInt16(rblUWBVIVerifiedResult.SelectedValue);
                    underwritingResult.BVINotes = txtUWBVIVerifiedNotes.Text;

                    underwritingResult.TaxLiensResult = Convert.ToInt16(rblUWTaxLiensVerifiedResult.SelectedValue);
                    underwritingResult.TaxLiensNotes = txtUWTaxLiensVerifiedNotes.Text;

                    underwritingResult.RiskIndicatorResult = Convert.ToInt16(rblUWRiskIndicatorVerifiedResult.SelectedValue);
                    underwritingResult.RiskIndicatorNotes = txtUWRiskIndicatorVerifiedNotes.Text;

                    underwritingResult.OFACMatchResult = Convert.ToInt16(rblUWOFACMatchVerifiedResult.SelectedValue);
                    underwritingResult.OFACMatchNotes = txtUWOFACMatchVerifiedNotes.Text;

                    if (underwritingResult.CorpInfoResult == 1
                        && underwritingResult.BusLicStatusResult == 1
                        && underwritingResult.EINResult == 1
                        && underwritingResult.PrincipalResult == 1
                        && underwritingResult.CardSalesIndicatorResult == 1
                        && underwritingResult.BankingInfoResult == 1
                        && underwritingResult.MCCResult == 1
                        && underwritingResult.BVIResult == 1
                        && underwritingResult.TaxLiensResult == 1
                        && underwritingResult.RiskIndicatorResult == 1
                        && underwritingResult.OFACMatchResult == 1
                        )
                    {
                        underwritingResult.UnderwritingDecision = "Pass";
                        editMerchant.UnderwritingStatus = ctx.UnderwritingStatuses.First(us => us.StatusDescription == "Approved");
                    }
                    else
                    {
                        underwritingResult.UnderwritingDecision = "Fail";
                        editMerchant.UnderwritingStatus = ctx.UnderwritingStatuses.First(us => us.StatusDescription == "Denied");

                        if (editMerchant.MerchantStatus.StatusDescription == "Enrolled")
                        {
                            editMerchant.MerchantStatus = ctx.MerchantStatuses.First(ms => ms.StatusDescription == "Denied");
                            editMerchant.DenialNotes = "Underwriting Decline.  See Underwriting Results for more information.";
                            editMerchant.DeniedBy = ctx.Users.First(u => u.Id == adminId);
                            editMerchant.DeniedDate = DateTime.UtcNow;
                            editMerchant.ApprovedAdvanceAmount = 0;
                            editMerchant.AvailableAdvanceAmount = 0;

                            //Send Email
                            Logic.Messaging messaging = new Logic.Messaging();
                            Boolean emailSent = false;
                            var template = messaging.GetTemplate("DeniedEnrollment");

                            if (template != null)
                            {
                                String html = messaging.GetTemplateHtml(template);

                                foreach (EmailTemplateVariableModel variable in messaging.GetRequiredVariables(template))
                                {
                                    if (editMerchant.MerchantPrincipal != null && editMerchant.MerchantPrincipal.Contact != null)
                                    {
                                        if (variable.VariableName == "MERCHANT_FIRST_NAME")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", editMerchant.MerchantPrincipal.Contact.FirstName);
                                            html = html.Replace("<$" + variable.VariableName + ">", editMerchant.MerchantPrincipal.Contact.FirstName);
                                        }
                                        if (variable.VariableName == "MERCHANT_LAST_NAME")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", editMerchant.MerchantPrincipal.Contact.LastName);
                                            html = html.Replace("<$" + variable.VariableName + ">", editMerchant.MerchantPrincipal.Contact.LastName);
                                        }
                                    }
                                    else
                                    {
                                        if (variable.VariableName == "MERCHANT_FIRST_NAME")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                            html = html.Replace("<$" + variable.VariableName + ">", "");
                                        }
                                        if (variable.VariableName == "MERCHANT_LAST_NAME")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                            html = html.Replace("<$" + variable.VariableName + ">", "");
                                        }
                                    }
                                    if (variable.VariableName == "MERCHANT_NAME")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", editMerchant.CorpName);
                                        html = html.Replace("<$" + variable.VariableName + ">", editMerchant.CorpName);
                                    }
                                    if (editMerchant.Business.Address != null)
                                    {
                                        if (variable.VariableName == "MERCHANT_ADDRESS")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", editMerchant.Business.Address.Address ?? "");
                                            html = html.Replace("<$" + variable.VariableName + ">", editMerchant.Business.Address.Address ?? "");
                                        }
                                        if (variable.VariableName == "MERCHANT_CITY")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", editMerchant.Business.Address.City ?? "");
                                            html = html.Replace("<$" + variable.VariableName + ">", editMerchant.Business.Address.City ?? "");
                                        }
                                        if (variable.VariableName == "MERCHANT_ZIP")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", editMerchant.Business.Address.Zip ?? "");
                                            html = html.Replace("<$" + variable.VariableName + ">", editMerchant.Business.Address.Zip ?? "");
                                        }
                                        if (editMerchant.Business.Address.State != null)
                                        {
                                            if (variable.VariableName == "MERCHANT_STATE")
                                            {
                                                html = html.Replace("&lt;$" + variable.VariableName + "&gt;", editMerchant.Business.Address.State.Name ?? "");
                                                html = html.Replace("<$" + variable.VariableName + ">", editMerchant.Business.Address.State.Name ?? "");
                                            }
                                        }
                                        else
                                        {
                                            if (variable.VariableName == "MERCHANT_STATE")
                                            {
                                                html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                                html = html.Replace("<$" + variable.VariableName + ">", "");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (variable.VariableName == "MERCHANT_ADDRESS")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                            html = html.Replace("<$" + variable.VariableName + ">", "");
                                        }
                                        if (variable.VariableName == "MERCHANT_CITY")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                            html = html.Replace("<$" + variable.VariableName + ">", "");
                                        }
                                        if (variable.VariableName == "MERCHANT_STATE")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                            html = html.Replace("<$" + variable.VariableName + ">", "");
                                        }
                                        if (variable.VariableName == "MERCHANT_ZIP")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                            html = html.Replace("<$" + variable.VariableName + ">", "");
                                        }
                                    }
                                    if (variable.VariableName == "DENY_NOTES")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", editMerchant.DenialNotes);
                                        html = html.Replace("<$" + variable.VariableName + ">", editMerchant.DenialNotes);
                                    }
                                }

                                if (ctx.Users.Any(u => u.Merchant.RecordId == editMerchant.RecordId))
                                {
                                    IQueryable<ApplicationUser> userList = ctx.Users.Where(u => u.Merchant.RecordId == editMerchant.RecordId);

                                    if (userList != null)
                                    {
                                        foreach (ApplicationUser thisUser in userList)
                                        {
                                            if (html != null)
                                            {
                                                emailSent = messaging.SendEmail(thisUser.Email, template.EmailSubject, html, template, editMerchant, thisUser);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            editMerchant.MerchantStatus = ctx.MerchantStatuses.First(ms => ms.StatusDescription == "Suspended");

                            MerchantSuspensionModel suspension = new MerchantSuspensionModel();

                            suspension.Merchant = ctx.Merchants.First(m => m.RecordId == editMerchant.RecordId);
                            suspension.OtherReasonNotes = "Failed Underwriting. See Undewriting Results for more information.";
                            suspension.SuspendedBy = ctx.Users.First(u => u.Id == adminId);
                            suspension.SuspensionReason = ctx.MerchantSuspensionReasons.First(msr => msr.ReasonName == "Failed Underwriting Result");
                            suspension.Timestamp = DateTime.UtcNow;

                            ctx.MerchantSuspensions.Add(suspension);

                            //Send Email
                            Logic.Messaging messaging = new Logic.Messaging();
                            Boolean emailSent = false;
                            var template = messaging.GetTemplate("MerchantSuspended");

                            String suspendNotes = "Failed Underwriting.  Contact Customer Service for more information.";

                            if (template != null)
                            {
                                String html = messaging.GetTemplateHtml(template);

                                foreach (EmailTemplateVariableModel variable in messaging.GetRequiredVariables(template))
                                {
                                    if (editMerchant.MerchantPrincipal != null && editMerchant.MerchantPrincipal.Contact != null)
                                    {
                                        if (variable.VariableName == "MERCHANT_FIRST_NAME")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", editMerchant.MerchantPrincipal.Contact.FirstName);
                                            html = html.Replace("<$" + variable.VariableName + ">", editMerchant.MerchantPrincipal.Contact.FirstName);
                                        }
                                        if (variable.VariableName == "MERCHANT_LAST_NAME")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", editMerchant.MerchantPrincipal.Contact.LastName);
                                            html = html.Replace("<$" + variable.VariableName + ">", editMerchant.MerchantPrincipal.Contact.LastName);
                                        }
                                    }
                                    else
                                    {
                                        if (variable.VariableName == "MERCHANT_FIRST_NAME")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                            html = html.Replace("<$" + variable.VariableName + ">", "");
                                        }
                                        if (variable.VariableName == "MERCHANT_LAST_NAME")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                            html = html.Replace("<$" + variable.VariableName + ">", "");
                                        }
                                    }
                                    if (variable.VariableName == "MERCHANT_NAME")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", editMerchant.CorpName);
                                        html = html.Replace("<$" + variable.VariableName + ">", editMerchant.CorpName);
                                    }
                                    if (editMerchant.Business.Address != null)
                                    {
                                        if (variable.VariableName == "MERCHANT_ADDRESS")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", editMerchant.Business.Address.Address ?? "");
                                            html = html.Replace("<$" + variable.VariableName + ">", editMerchant.Business.Address.Address ?? "");
                                        }
                                        if (variable.VariableName == "MERCHANT_CITY")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", editMerchant.Business.Address.City ?? "");
                                            html = html.Replace("<$" + variable.VariableName + ">", editMerchant.Business.Address.City ?? "");
                                        }
                                        if (variable.VariableName == "MERCHANT_ZIP")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", editMerchant.Business.Address.Zip ?? "");
                                            html = html.Replace("<$" + variable.VariableName + ">", editMerchant.Business.Address.Zip ?? "");
                                        }
                                        if (editMerchant.Business.Address.State != null)
                                        {
                                            if (variable.VariableName == "MERCHANT_STATE")
                                            {
                                                html = html.Replace("&lt;$" + variable.VariableName + "&gt;", editMerchant.Business.Address.State.Name ?? "");
                                                html = html.Replace("<$" + variable.VariableName + ">", editMerchant.Business.Address.State.Name ?? "");
                                            }
                                        }
                                        else
                                        {
                                            if (variable.VariableName == "MERCHANT_STATE")
                                            {
                                                html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                                html = html.Replace("<$" + variable.VariableName + ">", "");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (variable.VariableName == "MERCHANT_ADDRESS")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                            html = html.Replace("<$" + variable.VariableName + ">", "");
                                        }
                                        if (variable.VariableName == "MERCHANT_CITY")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                            html = html.Replace("<$" + variable.VariableName + ">", "");
                                        }
                                        if (variable.VariableName == "MERCHANT_STATE")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                            html = html.Replace("<$" + variable.VariableName + ">", "");
                                        }
                                        if (variable.VariableName == "MERCHANT_ZIP")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", "");
                                            html = html.Replace("<$" + variable.VariableName + ">", "");
                                        }
                                    }
                                    if (variable.VariableName == "SUSPEND_NOTES")
                                    {
                                        html = html.Replace("&lt;$" + variable.VariableName + "&gt;", suspendNotes);
                                        html = html.Replace("<$" + variable.VariableName + ">", suspendNotes);
                                    }
                                }

                                if (ctx.Users.Any(u => u.Merchant.RecordId == editMerchant.RecordId))
                                {
                                    IQueryable<ApplicationUser> userList = ctx.Users.Where(u => u.Merchant.RecordId == editMerchant.RecordId);

                                    if (userList != null)
                                    {
                                        foreach (ApplicationUser thisUser in userList)
                                        {
                                            if (html != null)
                                            {
                                                emailSent = messaging.SendEmail(thisUser.Email, template.EmailSubject, html, template, editMerchant, thisUser);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        ctx.SaveChanges();
                    }

                    ctx.SaveChanges();
                    UpdateStatusLabel();
                    gridUnderwritingHistory.Rebind();

                }

                pnlNewUnderwriting.Visible = false;
                pnlNewUnderwriting2.Visible = false;
                pnlUnderwritingHistory.Visible = true;
                pnlUnderwritingButtons.Visible = true;
                pnlUnderwritingAdvancedButtons.Visible = false;

                btnUnderwritingCancel.Visible = false;
                btnUnderwritingCancel2.Visible = false;
                btnUnderwritingTryAgain.Visible = false;
                btnUnderwritingComplete.Visible = false;
                btnUnderwritingSave.Visible = false;
                btnAddNewUnderwriting.Visible = true;
                btnUnderwritingContinue.Visible = false;


            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "btnUnderwritingComplete_Click");
            }
        }

        protected void btnUnderwritingContinue_Click(object sender, EventArgs e)
        {
            pnlUnderwritingHistory.Visible = false;
            pnlNewUnderwriting.Visible = false;
            pnlNewUnderwriting2.Visible = true;
            pnlUnderwritingButtons.Visible = false;
            pnlUnderwritingAdvancedButtons.Visible = true;
            btnUnderwritingComplete.Visible = true;
            btnUnderwritingSave.Visible = true;
            btnUnderwritingCancel.Visible = true;

            MerchantModel editMerchant = new MerchantModel();
            UnderwritingResultModel underwritingResult = new UnderwritingResultModel();
            UnderwritingResultModel oldUnderwritingResult = new UnderwritingResultModel();
            Int32 merchantRecordId;

            try
            {
                merchantRecordId = Convert.ToInt32(hMerchantRecordId.Value);

                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    editMerchant = ctx.Merchants.First(m => m.RecordId == merchantRecordId);
                    var underwritingSchema = editMerchant.AdvancePlan.UnderwritingSchema;

                    underwritingResult = ctx.UnderwritingResults.FirstOrDefault(ur => ur.Merchant.RecordId == merchantRecordId && ur.Active == true);

                    if (underwritingResult != null)
                    {
                        if (underwritingResult.LexisNexisResults != null)
                        {
                            if (underwritingResult.LexisNexisResults.MccMatch != null)
                            {
                                if (underwritingResult.LexisNexisResults.MccMatch == true)
                                {
                                    txtUWMCCVerifiedNotes.Text = "Match and Approved";
                                    rblUWMCCVerifiedResult.SelectedValue = "1";
                                }
                                else if (underwritingResult.LexisNexisResults.MccMatch == false)
                                {
                                    txtUWMCCVerifiedNotes.Text = "Mismatch";
                                    rblUWMCCVerifiedResult.SelectedValue = "2";
                                    rblUWMCCVerifiedResult.Enabled = false;
                                }
                                else
                                {
                                    txtUWMCCVerifiedNotes.Text = "";
                                    rblUWMCCVerifiedResult.SelectedValue = "0";
                                }
                            }

                            if (underwritingResult.LexisNexisResults.OFAC != null)
                            {
                                if (underwritingResult.LexisNexisResults.OFAC == false)
                                {
                                    rblUWOFACMatchVerifiedResult.SelectedValue = "1";
                                    txtUWOFACMatchVerifiedNotes.Text = "No OFAC Match Found";
                                }
                                else
                                {
                                    if (underwritingSchema.OFACDecline == true)
                                    {
                                        rblUWOFACMatchVerifiedResult.SelectedValue = "2";
                                        txtUWOFACMatchVerifiedNotes.Text = "Match Found | " + underwritingResult.LexisNexisResults.OFACMatchData;
                                        rblUWOFACMatchVerifiedResult.Enabled = false;
                                    }
                                    else
                                    {
                                        rblUWOFACMatchVerifiedResult.SelectedValue = "1";
                                        txtUWOFACMatchVerifiedNotes.Text = "Match Found, but Allowed | " + underwritingResult.LexisNexisResults.OFACMatchData;
                                    }
                                }
                            }

                            if (underwritingResult.LexisNexisResults.BVI != null)
                            {
                                if (underwritingResult.LexisNexisResults.BVI < underwritingSchema.BVIDeclineLimit)
                                {
                                    txtUWBVIVerifiedNotes.Text = "Fail: " + underwritingResult.LexisNexisResults.BVI + " < " + underwritingSchema.BVIDeclineLimit;
                                    rblUWBVIVerifiedResult.SelectedValue = "2";
                                    rblUWBVIVerifiedResult.Enabled = false;
                                }
                                else if (underwritingResult.LexisNexisResults.BVI == underwritingSchema.BVIReviewLimit)
                                {
                                    txtUWBVIVerifiedNotes.Text = "Manual Approval Required: " + underwritingResult.LexisNexisResults.BVI + " = " + underwritingSchema.BVIReviewLimit;
                                    rblUWBVIVerifiedResult.SelectedValue = "0";
                                }
                                else
                                {
                                    txtUWBVIVerifiedNotes.Text = "Pass: " + underwritingResult.LexisNexisResults.BVI + " > " + underwritingSchema.BVIReviewLimit;
                                    rblUWBVIVerifiedResult.SelectedValue = "1";
                                }
                            }

                            if (underwritingResult.LexisNexisResults.RecentLienType != null)
                            {
                                if (underwritingResult.LexisNexisResults.RecentLienType.TypeName.ToLower().Contains("tax"))
                                {
                                    if (underwritingSchema.DeclineOnTaxLien)
                                    {
                                        txtUWTaxLiensVerifiedNotes.Text = "Fail: " + underwritingResult.LexisNexisResults.RecentLienType.TypeName;
                                        rblUWTaxLiensVerifiedResult.SelectedValue = "2";
                                        rblUWTaxLiensVerifiedResult.Enabled = false;
                                    }
                                    else
                                    {
                                        txtUWTaxLiensVerifiedNotes.Text = "Tax Lien Found, but Allowed: " + underwritingResult.LexisNexisResults.RecentLienType.TypeName;
                                        rblUWTaxLiensVerifiedResult.SelectedValue = "1";
                                    }
                                }
                            }
                            else
                            {
                                txtUWTaxLiensVerifiedNotes.Text = "No Tax Liens";
                                rblUWTaxLiensVerifiedResult.SelectedValue = "1";
                            }


                            if (ctx.LexisNexisRiskCodeResults.Any(lnrcr => lnrcr.LexisNexisUnderwritingResult.RecordId == underwritingResult.LexisNexisResults.RecordId))
                            {
                                String riskCodeList = "";
                                var riskCodes = ctx.LexisNexisRiskCodeResults.Where(lnrcr => lnrcr.LexisNexisUnderwritingResult.RecordId == underwritingResult.LexisNexisResults.RecordId);

                                foreach (var code in riskCodes)
                                {
                                    if (underwritingSchema.LexisNexisApprovedRiskCodes.Any(x => x.Decline && x.RiskCode.RecordId == code.RiskCode.RecordId))
                                    {
                                        riskCodeList += "Decline: " + code.RiskCode.RiskCodeDescription + " || ";
                                    }
                                    else if (underwritingSchema.LexisNexisApprovedRiskCodes.Any(x => x.ReqeustVerification && x.RiskCode.RecordId == code.RiskCode.RecordId))
                                    {
                                        riskCodeList += "Manual Verification: " + code.RiskCode.RiskCodeDescription + " || ";
                                    }
                                    else
                                    {
                                        riskCodeList += "Found, but Allowed: " + code.RiskCode.RiskCodeDescription + " || ";
                                    }
                                }

                                txtUWRiskIndicatorVerifiedNotes.Text = riskCodeList.Substring(0, riskCodeList.Length - 4);
                                if (riskCodeList.Contains("Decline:"))
                                {
                                    rblUWRiskIndicatorVerifiedResult.SelectedValue = "2";
                                    rblUWRiskIndicatorVerifiedResult.Enabled = false;
                                }
                                else if (riskCodeList.Contains("Manual Verification:"))
                                {
                                    rblUWRiskIndicatorVerifiedResult.SelectedValue = "0";
                                }
                                else
                                {
                                    rblUWRiskIndicatorVerifiedResult.SelectedValue = "1";
                                }

                            }
                        }
                    }
                }

                txtUWCorpInfoVerifiedNotes.Text = "";
                txtUWBUsLicStatusVerifiedNotes.Text = "";
                txtUWEINVerifiedNotes.Text = "";
                txtUWPrincipalVerifiedNotes.Text = "";
                txtUWCardSalesIndicatorsVerifiedNotes.Text = "";
                txtUWBankingInfoVerifiedNotes.Text = "";
                rblUWCorpInfoVerifiedResult.SelectedValue = "0";
                rblUWBusLicStatusResult.SelectedValue = "0";
                rblUWEINVerifiedResult.SelectedValue = "0";
                rblUWPrincipalVerifiedResult.SelectedValue = "0";
                rblUWCardSalesIndicatorsVerifiedResult.SelectedValue = "0";
                rblUWBankingInfoVerifiedResult.SelectedValue = "0";

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "btnUnderwritingContinue_Click");
            }

        }

        protected void btnUnderwritingTryAgain_Click(object sender, EventArgs e)
        {
            txtUnderwriterInitials.Text = "";
            pnlNewUnderwriting.Visible = false;
            pnlNewUnderwriting2.Visible = false;
            pnlUnderwritingHistory.Visible = true;
            btnUnderwritingCancel.Visible = false;
            btnUnderwritingCancel2.Visible = false;
            btnUnderwritingSave.Visible = false;
            btnAddNewUnderwriting.Visible = true;
            btnUnderwritingComplete.Visible = false;
            btnUnderwritingTryAgain.Visible = false;
        }

        protected void BusinessView_PreRender(object sender, EventArgs e)
        {
            if (lblStatusDescription.Text.ToLower() == "denied")
            {
                ddlLegalOrgType.Enabled = false;
                ddlLegalOrgState.Enabled = false;
                rblMerchantType.Enabled = false;
                ddlMCC.Enabled = false;
                txtCorpName.Enabled = false;
                txtDBAName.Enabled = false;
                txtCorpAddress.Enabled = false;
                txtCorpCity.Enabled = false;
                ddlCorpState.Enabled = false;
                txtCorpZip.Enabled = false;
                txtBusLicNumber.Enabled = false;
                txtBusLicType.Enabled = false;
                txtBusLicIssuer.Enabled = false;
                ddlLicenseDateYear.Enabled = false;
                ddlLicenseDateMonth.Enabled = false;
                ddlLicenseDateDay.Enabled = false;
                txtFedTaxId.Enabled = false;
                txtBusEmail.Enabled = false;
                txtBusPhone.Enabled = false;
                txtBusFax.Enabled = false;
                txtMerchandiseSold.Enabled = false;
                txtYearsInBus.Enabled = false;
                txtMonthsInBus.Enabled = false;
                rblSeasonal.Enabled = false;
                cblSeasonal.Enabled = false;
            }
            else
            {
                ddlLegalOrgType.Enabled = true;
                ddlLegalOrgState.Enabled = true;
                rblMerchantType.Enabled = true;
                ddlMCC.Enabled = true;
                txtCorpName.Enabled = true;
                txtDBAName.Enabled = true;
                txtCorpAddress.Enabled = true;
                txtCorpCity.Enabled = true;
                ddlCorpState.Enabled = true;
                txtCorpZip.Enabled = true;
                txtBusLicNumber.Enabled = true;
                txtBusLicType.Enabled = true;
                txtBusLicIssuer.Enabled = true;
                ddlLicenseDateYear.Enabled = true;
                ddlLicenseDateMonth.Enabled = true;
                ddlLicenseDateDay.Enabled = true;
                txtFedTaxId.Enabled = true;
                txtBusEmail.Enabled = true;
                txtBusPhone.Enabled = true;
                txtBusFax.Enabled = true;
                txtMerchandiseSold.Enabled = true;
                txtYearsInBus.Enabled = true;
                txtMonthsInBus.Enabled = true;
                rblSeasonal.Enabled = true;
                cblSeasonal.Enabled = true;

            }
        }

        protected void PrincipalView_PreRender(object sender, EventArgs e)
        {
            if (lblStatusDescription.Text.ToLower() == "denied")
            {
                txtPrincipalFirstName.Enabled = false;
                txtPrincipalMI.Enabled = false;
                txtPrincipalLastName.Enabled = false;
                ddlBirthYear.Enabled = false;
                ddlBirthMonth.Enabled = false;
                ddlBirthDay.Enabled = false;
                lbEditSSN.Visible = false;
                txtPrincipalPctOwn.Enabled = false;
                txtPrincipalTitle.Enabled = false;
                lbEditDL.Enabled = false;
                ddlPrincipalDLState.Enabled = false;
                txtPrincipalAddress.Enabled = false;
                txtPrincipalCity.Enabled = false;
                ddlPrincipalState.Enabled = false;
                txtPrincipalZip.Enabled = false;
                txtPrincipalHomePhone.Enabled = false;
                txtPrincipalCellPhone.Enabled = false;
            }
            else
            {
                txtPrincipalFirstName.Enabled = true;
                txtPrincipalMI.Enabled = true;
                txtPrincipalLastName.Enabled = true;
                ddlBirthYear.Enabled = true;
                ddlBirthMonth.Enabled = true;
                ddlBirthDay.Enabled = true;
                lbEditSSN.Visible = true;
                txtPrincipalPctOwn.Enabled = true;
                txtPrincipalTitle.Enabled = true;
                lbEditDL.Enabled = true;
                ddlPrincipalDLState.Enabled = true;
                txtPrincipalAddress.Enabled = true;
                txtPrincipalCity.Enabled = true;
                ddlPrincipalState.Enabled = true;
                txtPrincipalZip.Enabled = true;
                txtPrincipalHomePhone.Enabled = true;
                txtPrincipalCellPhone.Enabled = true;
            }
        }

        protected void ContactView_PreRender(object sender, EventArgs e)
        {
            if (lblStatusDescription.Text.ToLower() == "denied")
            {
                txtContactFirstName.Enabled = false;
                txtContactLastName.Enabled = false;
                txtContactEmail.Enabled = false;
                txtContactPhone.Enabled = false;
                txtContactFax.Enabled = false;
            }
            else
            {
                txtContactFirstName.Enabled = true;
                txtContactLastName.Enabled = true;
                txtContactEmail.Enabled = true;
                txtContactPhone.Enabled = true;
                txtContactFax.Enabled = true;
            }
        }

        protected void BankingView_PreRender(object sender, EventArgs e)
        {
            if (lblStatusDescription.Text.ToLower() == "denied")
            {
                txtBankName.Enabled = false;
                txtBankCity.Enabled = false;
                ddlBankState.Enabled = false;
                txtBankPhone.Enabled = false;
                txtDebitCardNumber.Enabled = false;
                lbEditDebitCard.Enabled = false;
                txtAccountNumber.Enabled = false;
                lbEditBankAccount.Enabled = false;
                ddlExpYear.Enabled = false;
                ddlExpMonth.Enabled = false;
                lbEditExpDate.Enabled = false;
                txtRoutingNumber.Enabled = false;
                lbEditRoutingNumber.Enabled = false;
            }
            else
            {
                txtBankName.Enabled = true;
                txtBankCity.Enabled = true;
                ddlBankState.Enabled = true;
                txtBankPhone.Enabled = true;
                lbEditDebitCard.Enabled = true;
                lbEditBankAccount.Enabled = true;
                lbEditExpDate.Enabled = true;
                lbEditRoutingNumber.Enabled = true;
            }
        }

        protected void UsersView_PreRender(object sender, EventArgs e)
        {
            pnlUserButtons.Visible = true;
        }

        protected void MerchantAccountView_PreRender(object sender, EventArgs e)
        {
            if (lblStatusDescription.Text.ToLower() == "denied")
            {
                txtSwipedPct.Enabled = false;
                txtAvgMonthlySales.Enabled = false;
                txtHighestMonthlySales.Enabled = false;
                txtAvgWeeklySales.Enabled = false;
                txtCardProcessor.Enabled = false;
                txtMerchantId.Enabled = false;
                rblHighRisk.Enabled = false;
                txtHighRiskWho.Enabled = false;
                ddlHighRiskYear.Enabled = false;
                ddlHighRiskMonth.Enabled = false;
                ddlHighRiskDay.Enabled = false;
                rblBankruptcy.Enabled = false;
                ddlBankruptcyYear.Enabled = false;
                ddlBankruptcyMonth.Enabled = false;
                ddlBankruptcyDay.Enabled = false;
            }
            else
            {
                txtSwipedPct.Enabled = true;
                txtAvgMonthlySales.Enabled = true;
                txtHighestMonthlySales.Enabled = true;
                txtAvgWeeklySales.Enabled = true;
                txtCardProcessor.Enabled = true;
                txtMerchantId.Enabled = true;
                rblHighRisk.Enabled = true;
                txtHighRiskWho.Enabled = true;
                ddlHighRiskYear.Enabled = true;
                ddlHighRiskMonth.Enabled = true;
                ddlHighRiskDay.Enabled = true;
                rblBankruptcy.Enabled = true;
                ddlBankruptcyYear.Enabled = true;
                ddlBankruptcyMonth.Enabled = true;
                ddlBankruptcyDay.Enabled = true;
            }
        }

        protected void AdvancePlanView_PreRender(object sender, EventArgs e)
        {
            if (lblStatusDescription.Text.ToLower() == "denied")
            {
                ddlActivePlan.Enabled = false;
            }
            else
            {
                ddlActivePlan.Enabled = true;
            }
        }

        protected void UnderwritingView_PreRender(object sender, EventArgs e)
        {
            if (lblStatusDescription.Text.ToLower() == "denied")
            {
                btnAddNewUnderwriting.Enabled = false;
            }
            else
            {
                btnAddNewUnderwriting.Enabled = true;
            }
        }

        protected void cvSSN_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtPrincipalSSN.Enabled == true)
            {
                if (txtPrincipalSSN.Text.Length > 0 && txtPrincipalSSN.Text.Length != 9)
                {
                    args.IsValid = false;
                }
                else
                {
                    args.IsValid = true;
                }
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void cvHomePhone_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtPrincipalHomePhone.Text.Length > 0 && txtPrincipalHomePhone.Text.Length != 10)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void cvCellPhone_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtPrincipalCellPhone.Text.Length > 0 && txtPrincipalCellPhone.Text.Length != 10)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void cvContactPhone_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtContactPhone.Text.Length > 0 && txtContactPhone.Text.Length != 10)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void cvContactFax_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtContactFax.Text.Length > 0 && txtContactFax.Text.Length != 10)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void cvDebitCard_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (txtDebitCardNumber.Enabled == true)
                {
                    args.IsValid = _newLogic.IsCardValid(args.Value);
                }
                else
                {
                    args.IsValid = true;
                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "cvDebitCard_ServerValidate");
            }
        }

        protected void cvAccountNumber_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Regex regex = new Regex(@"\d+");

            if (txtAccountNumber.Enabled == true)
            {
                if (txtAccountNumber.Text.Length < 6)
                {
                    args.IsValid = false;
                }
                else
                {
                    args.IsValid = DigitsOnly(args.Value);
                }
            }
            else
            {
                args.IsValid = true;
            }
        }

        static bool DigitsOnly(string s)
        {
            int len = s.Length;
            for (int i = 0; i < len; ++i)
            {
                char c = s[i];
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        public IQueryable<MiqroMoney.Models.StatusChangeModel> GetStatusChanges()
        {
            try
            {
                int merchantId;
                Int32.TryParse(hMerchantRecordId.Value, out merchantId);
                string adminId = Context.User.Identity.GetUserId();

                if (_globalCtx.StatusChanges.Any(x => x.Merchant.RecordId == merchantId))
                {
                    return _globalCtx.StatusChanges.Where(x => x.Merchant.RecordId == merchantId);
                }

                return null;
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "FillStatusChanges");
                return null;
            }

        }

        public string ParsePaymentDate(object paymentDate)
        {
            if (paymentDate != null)
            {
                return Convert.ToDateTime(paymentDate).ToShortDateString();
            }
            else
            {
                return "";
            }
        }


        protected void gridUserList_UpdateCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void gridUserList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GetUsers();
        }

        protected void gridUserList_ItemDataBound(object sender, GridItemEventArgs e)
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
                        case "active":
                            statusCell.BackColor = System.Drawing.Color.Green;
                            break;
                        case "inactive":
                            statusCell.BackColor = System.Drawing.Color.Red;
                            break;
                        default:
                            statusCell.BackColor = System.Drawing.Color.Gray;
                            break;
                    }

                }

                if (e.Item is GridFilteringItem)
                {
                    GridFilteringItem filterItem = (GridFilteringItem)e.Item;
                    filterItem["Id"].HorizontalAlign = HorizontalAlign.Center;
                    filterItem["UserName"].HorizontalAlign = HorizontalAlign.Center;
                    filterItem["FirstName"].HorizontalAlign = HorizontalAlign.Center;
                    filterItem["LastName"].HorizontalAlign = HorizontalAlign.Center;
                    filterItem["Email"].HorizontalAlign = HorizontalAlign.Center;
                    filterItem["StatusDescription"].HorizontalAlign = HorizontalAlign.Center;

                }
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "gridUserList_ItemDataBound");
            }
        }

        protected void gridUserList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName)
            {
                gridUserList.ExportSettings.ExportOnlyData = false;
                gridUserList.MasterTableView.ExportToCSV();
            }

            if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName)
            {
                isExport = true;

                gridUserList.MasterTableView.ExportToExcel();
            }

            if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToPdfCommandName)
            {
                isExport = true;

                gridUserList.MasterTableView.ExportToPdf();
            }

            if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName)
            {
                isExport = true;

                gridUserList.MasterTableView.ExportToWord();
            }
        }

        protected void gridUserList_ExportCellFormatting(object sender, ExportCellFormattingEventArgs e)
        {
            if (e.FormattedColumn.UniqueName == "StatusDescription")
            {
                e.Cell.Style["Color"] = "#FFF";
            }
        }

        protected void gridUserList_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFilteringItem && isExport)
            {
                e.Item.Visible = false;
            }
        }

        protected void gridUserList_CancelCommand(object sender, GridCommandEventArgs e)
        {
            gridUserList.MasterTableView.ClearEditItems();
            gridUserList.MasterTableView.ClearChildEditItems();
        }

        public IQueryable<ApplicationUser> GetUsers()
        {
            try
            {
                Int32 merchantRecordId = Convert.ToInt32(hMerchantRecordId.Value);
                MerchantModel editMerchant = _globalCtx.Merchants.First(m => m.RecordId == merchantRecordId);

                if (editMerchant != null)
                {
                    IQueryable<ApplicationUser> userList = _globalCtx.Users.Where(x => x.Merchant.RecordId == editMerchant.RecordId).OrderBy(x => x.UserName);

                    return userList;
                }

                return null;
            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "GetUsers");
                return null;
            }
        }

        protected void btnAddUserToMerchant_Click(object sender, EventArgs e)
        {
            pnlUserList.Visible = false;
            pnlAddUserToMerchant.Visible = true;

            btnAddUserToMerchant.Visible = false;
            btnConfirmUsers.Visible = true;
            btnCancelUserChanges.Visible = true;

            rlvAvailableUsers.DataBind();
            rlvAvailableUsers.ClearSelectedItems();
            Session.Clear();

        }

        protected void btnConfirmUsers_Click(object sender, EventArgs e)
        {
            pnlUserList.Visible = true;
            pnlAddUserToMerchant.Visible = false;

            pnlUserButtons.Visible = true;
            btnAddUserToMerchant.Visible = true;
            btnConfirmUsers.Visible = false;
            btnCancelUserChanges.Visible = false;

            Hashtable values = new Hashtable();

            if (rlvAvailableUsers.SelectedItems.Any())
            {
                foreach (RadListViewDataItem item in rlvAvailableUsers.SelectedItems)
                {
                    using (ApplicationDbContext ctx = new ApplicationDbContext())
                    {
                        LinkButton btn = (LinkButton)item.FindControl("btnUserSelection");

                        if (ctx.Users.Any(x => x.UserName == btn.Text))
                        {
                            ApplicationUser user = ctx.Users.Include(x => x.Merchant).FirstOrDefault(x => x.UserName == btn.Text);

                            Int32 merchantId = Convert.ToInt32(hMerchantRecordId.Value);
                            MerchantModel merchant = ctx.Merchants.FirstOrDefault(x => x.RecordId == merchantId);

                            if (merchant != null)
                            {
                                user.Merchant = merchant;
                                user.DisassociatedMerchant = null;
                                user.HasBeenDisassociated = false;
                                ctx.SaveChanges();

                                Logic.Messaging messaging = new Logic.Messaging();
                                Boolean emailSent = false;
                                var template = messaging.GetTemplate("UserAssociated");

                                if (template != null)
                                {
                                    String html = messaging.GetTemplateHtml(template);

                                    foreach (EmailTemplateVariableModel variable in messaging.GetRequiredVariables(template))
                                    {
                                        if (variable.VariableName == "MERCHANT_NAME")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", merchant.CorpName);
                                            html = html.Replace("<$" + variable.VariableName + ">", merchant.CorpName);
                                        }
                                        if (variable.VariableName == "USERNAME")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", user.UserName);
                                            html = html.Replace("<$" + variable.VariableName + ">", user.UserName);
                                        }
                                        if (variable.VariableName == "USER_FIRST_NAME")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", user.FirstName);
                                            html = html.Replace("<$" + variable.VariableName + ">", user.FirstName);
                                        }
                                        if (variable.VariableName == "USER_LAST_NAME")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", user.LastName);
                                            html = html.Replace("<$" + variable.VariableName + ">", user.LastName);
                                        }
                                        if (variable.VariableName == "USER_PHONE")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", user.PhoneNumber);
                                            html = html.Replace("<$" + variable.VariableName + ">", user.PhoneNumber);
                                        }
                                        if (variable.VariableName == "USER_EMAIL")
                                        {
                                            html = html.Replace("&lt;$" + variable.VariableName + "&gt;", user.Email);
                                            html = html.Replace("<$" + variable.VariableName + ">", user.Email);
                                        }

                                    }
                                    if (html != null && user != null)
                                    {
                                        emailSent = messaging.SendEmail(user.Email, template.EmailSubject, html, template, merchant, user);
                                    }
                                }

                            }
                        }
                    }
                }
            }



            rlvAvailableUsers.ClearSelectedItems();
            Session.Clear();
            gridUserList.Rebind();
            rlvAvailableUsers.Rebind();

        }

        protected void btnCancelUserChanges_Click(object sender, EventArgs e)
        {
            pnlUserList.Visible = true;
            pnlAddUserToMerchant.Visible = false;

            pnlUserButtons.Visible = true;
            btnAddUserToMerchant.Visible = true;
            btnConfirmUsers.Visible = false;
            btnCancelUserChanges.Visible = false;

            rlvAvailableUsers.ClearSelectedItems();
            Session.Clear();
            gridUserList.Rebind();
            rlvAvailableUsers.Rebind();

        }

        protected void rlvAvailableUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public IQueryable<ApplicationUser> GetAvailableUsers()
        {
            try
            {
                String merchantRole = _globalCtx.Roles.FirstOrDefault(x => x.Name == "Merchant").Id;

                IQueryable<ApplicationUser> userList = _globalCtx.Users.Where(x => x.Merchant == null && x.Roles.Any(y => y.RoleId == merchantRole));

                return userList;

            }
            catch (System.Exception ex)
            {
                _newLogic.WriteExceptionToDB(ex, "GetAvailableUsers");
                return null;
            }
        }

    }
}