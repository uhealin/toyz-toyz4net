using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;


namespace Toyz4net.Core.Util
{
     public  class EMailUtil
    {

         public static bool SendMail(string subject,string body,string[] addresses) {
             MailMessage mail = new MailMessage();
             foreach (string address in addresses) {
                 mail.To.Add(address);
             }
             mail.Body = body;
             mail.Subject = subject;
             mail.BodyEncoding=Encoding.UTF8;
             mail.SubjectEncoding=Encoding.UTF8;
             mail.From=new MailAddress("smartken0824@gmail.com","smartken",Encoding.UTF8);
             SmtpClient smtp = new SmtpClient();
             smtp.Credentials = new NetworkCredential("smartken0824","chipchina");
             smtp.Host = "smtp.gmail.com";
             smtp.EnableSsl = true;
             try
             {
                 smtp.Send(mail);
             }
             catch (Exception ex) { return false; }
             return true;
         }

    }
}
