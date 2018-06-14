using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CustomLogs
{
   public class SendMails
    {
       internal string FromEmailID = string.Empty;
       internal string ToEmailId = string.Empty;
       internal string Subject = string.Empty;
       internal string UserName = string.Empty;
       internal string Password = string.Empty;
      
       public SendMails()
       {
           //throw new NotImplementedException("Requried Email Address");
       }
       public SendMails(string userName,string passowrd, string fromEmailId,string toEmailAddress)
       {
           this.UserName = userName;
           this.Password = passowrd;
           this.FromEmailID = fromEmailId;
           this.ToEmailId = toEmailAddress;

       }
       public SendMails(string userName, string passowrd, string fromEmailId, string toEmailAddress, string subject)
       {
           this.UserName = userName;
           this.Password = passowrd;
           this.FromEmailID = fromEmailId;
           this.ToEmailId = toEmailAddress;
           this.Subject = subject;

       }
       /// <summary>
       /// Email Send Functionality
       /// </summary>
       /// <param name="FromEmailId">From Email ID</param>
       /// <param name="ToEmailId"> To Email ID</param>
       /// <param name="Subject"> Subject </param>
       /// <param name="Body">Body...</param>
       public void SendMail(string body)
       {

            MailMessage mail = new MailMessage(this.FromEmailID, this.ToEmailId);
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
           
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(this.UserName,this.Password);
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            mail.Subject = this.Subject;
            mail.Body = body;
            client.Send(mail);
       }

    }
}
