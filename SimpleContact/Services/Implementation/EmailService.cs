using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace SimpleContact.Services.Implementation;

public class EmailService : IEmailService
{
    public void SendContactEmail(string name, string email, string message)
    {
        //Check nulls
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email));

        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentNullException(nameof(message));


        if (name.Length > 128)
            throw new InvalidOperationException("Name is bigger than 128 characters");
        
        if (email.Length > 256)
            throw new InvalidOperationException("Email is bigger than 256 characters");

        if (!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            throw new InvalidOperationException("Email is not valid");

        if (message.Length > 2048)
            throw new InvalidOperationException("Message is bigger than 2048 characters");

        

        //This function would then send an email to optoma.
        //You do NOT need to complete this method.


        using (MailMessage mail = new MailMessage())
        {
            mail.From = new MailAddress(email);
            mail.To.Add("optomainfo@xyz.com");
            mail.Subject = "Customer Enquiry - " + name;
            mail.Body = message;
            mail.IsBodyHtml = true;

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("optoma@xyz.com", "password");
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
        }

    }
}