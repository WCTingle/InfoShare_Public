using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using Tingle_WebForms.Models;
using Tingle_WebForms.Logic;
using System.Data.Entity;

namespace Tingle_WebForms.Logic
{
    public class SendEmail
    {
        public Boolean SendMail(string from, List<string> emailList, string subject, string bodyHtml, TForm newForm, Int32 formId, SystemUsers sentUser)
        {
            try
            {
                MailMessage completeMessage = new MailMessage();
                completeMessage.From = new MailAddress(from);
                completeMessage.Subject = subject;
                completeMessage.Body = bodyHtml;
                completeMessage.IsBodyHtml = true;

                //SmtpClient client = new SmtpClient("TingleNT30.wctingle.com");
                //client.UseDefaultCredentials = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new NetworkCredential("rzordel@gmail.com", "ZXCasdQWE123!");
                client.EnableSsl = true;

                using (FormContext ctx = new FormContext())
                {

                    foreach (string email in emailList)
                    {

                        RequestNotifications newRN = new RequestNotifications
                        {
                            Form = ctx.TForms.FirstOrDefault(x => x.FormID == newForm.FormID),
                            BodyHtml = bodyHtml,
                            RequestedFormId = formId,
                            SentBy = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == sentUser.SystemUserID),
                            Status = 0,
                            Timestamp = DateTime.Now,
                            ToEmailAddress = email
                        };

                        ctx.RequestNotifications.Add(newRN);
                        ctx.SaveChanges();

                        try
                        {
                            completeMessage.To.Clear();
                            completeMessage.To.Add(email);
                            client.Send(completeMessage);

                            newRN.Status = 1;

                            ctx.SaveChanges();
                        }
                        catch(Exception exc)
                        {}

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw ex;
            }
        }

        public Boolean SendInventoryNotification(List<string> emailList, string bodyHtml, SystemUsers sentUser)
        {
            try
            {
                MailMessage completeMessage = new MailMessage();
                completeMessage.From = new MailAddress("InfoShare@wctingle.com");
                completeMessage.Subject = "Inventory Approval Notification";
                completeMessage.Body = bodyHtml;
                completeMessage.IsBodyHtml = true;

                //SmtpClient client = new SmtpClient("TingleNT30.wctingle.com");
                //client.UseDefaultCredentials = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new NetworkCredential("rzordel@gmail.com", "ZXCasdQWE123!");
                client.EnableSsl = true;

                using (FormContext ctx = new FormContext())
                {
                    foreach (string email in emailList)
                    {
                        InventoryApprovalNotifications newRN = new InventoryApprovalNotifications
                        {
                            BodyHtml = bodyHtml,
                            SentBy = ctx.SystemUsers.FirstOrDefault(x => x.SystemUserID == sentUser.SystemUserID),
                            Status = 0,
                            Timestamp = DateTime.Now,
                            ToEmailAddress = email
                        };

                        ctx.InventoryApprovalNotifications.Add(newRN);
                        ctx.SaveChanges();

                        try
                        {
                            completeMessage.To.Clear();
                            completeMessage.To.Add(email);
                            client.Send(completeMessage);

                            newRN.Status = 1;

                            ctx.SaveChanges();
                        }
                        catch (Exception exc)
                        { }

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}