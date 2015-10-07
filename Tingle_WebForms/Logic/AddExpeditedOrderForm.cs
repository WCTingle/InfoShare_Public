using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tingle_WebForms.Models;

namespace Tingle_WebForms.Logic
{
    public class AddExpeditedOrderForms
    {

        public bool AddExpeditedOrderForm(string oowOrderNumber, string customer, string accountNumber, ExpediteCode expediteCode, string purchaseOrderNumber, string materialSku, string quantityOrdered,
            Nullable<DateTime> installDate, string sM, string contactName, string phoneNumber, string shipToName, string shipToAddress, string shipToCity, string shipToState, string shipToZip, 
            string additionalInfo, Status status, string submittedByUser, string ccFormToEmail, string company, out Int32 formId)
        {
            try
            {
                using (FormContext _db = new FormContext())
                {
                    var expCode = _db.ExpediteCodes.SingleOrDefault(ec => ec.ExpediteCodeID == expediteCode.ExpediteCodeID);
                    var submissionStatus = _db.Statuses.SingleOrDefault(s => s.StatusId == status.StatusId);

                    var newForm = new ExpeditedOrderForm();
                    newForm.Timestamp = DateTime.Now;
                    newForm.OowOrderNumber = oowOrderNumber;
                    newForm.Customer = customer;
                    newForm.AccountNumber = accountNumber;
                    newForm.ExpediteCode = expCode;
                    newForm.PurchaseOrderNumber = purchaseOrderNumber;
                    newForm.InstallDate = installDate;
                    newForm.SM = sM;
                    newForm.ContactName = contactName;
                    newForm.PhoneNumber = phoneNumber;
                    newForm.ShipToName = shipToName;
                    newForm.ShipToAddress = shipToAddress;
                    newForm.ShipToCity = shipToCity;
                    newForm.ShipToState = shipToState;
                    newForm.ShipToZip = shipToZip;
                    newForm.AdditionalInfo = additionalInfo;
                    newForm.Status = submissionStatus;
                    newForm.SubmittedByUser = submittedByUser;
                    newForm.CCFormToEmail = ccFormToEmail;
                    newForm.Company = company;

                    _db.ExpeditedOrderForms.Add(newForm);
                    _db.SaveChanges();

                    formId = newForm.RecordId;
                }
                return true;
            }
            catch (Exception ex)
            {
                formId = 0;
                return false;
                //throw ex;

            }

            
        }
    }
}