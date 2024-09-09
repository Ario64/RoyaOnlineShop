using System.Net.Mail;

namespace OnlineShop.Core.Senders;

public class SendEmail
{
    public static void Send(string to, string subject, string body)
    {
        MailMessage mail = new MailMessage();
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        mail.From = new MailAddress("royacollection9@gmail.com", "فروشگاه آنلاین رویا");
        mail.To.Add(to);
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = true;

        //System.Net.Mail.Attachment attachment;
        // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
        // mail.Attachments.Add(attachment);

        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("royacollection9@gmail.com", "luqk edmv yghh aspe");
        smtpServer.EnableSsl = true;

        smtpServer.Send(mail);
    }
}