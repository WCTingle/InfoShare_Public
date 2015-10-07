using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using Tingle_WebForms.Models;
using Tingle_WebForms.Logic;
using System.Data.Entity;

namespace Tingle_WebForms
{
    public partial class TestEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage completeMessage = new MailMessage(txtFrom.Text, txtTo.Text, txtSubject.Text, txtBody.Text);
                completeMessage.IsBodyHtml = true;

                SmtpClient client = new SmtpClient("TingleNT30.wctingle.com");
                client.UseDefaultCredentials = true;

                client.Send(completeMessage);

                lblResult.Text = "Success";

            }
            catch (Exception ex)
            {
                lblResult.Text = ex.Message;
            }

        }
    }
}