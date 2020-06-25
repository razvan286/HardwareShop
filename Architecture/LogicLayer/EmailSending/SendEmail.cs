using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Media_Bazaar
{
     public class SendEmail
    {
        public void Send(string receiverEmail, string username, string password)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                MailMessage message = new MailMessage();

                message.From = new MailAddress("media.bazaar2020@gmail.com");
                message.To.Add(receiverEmail);
                message.Body = $"Welcome to Media Bazaar company. \n" + "\n" + "\n" +
                    $"Below you can find your account details which you will have to use when accessing your account. \n" +
                    $"Username: {username} \n" +
                    $"Password: {password} \n" + "\n" +
                    $"You can change your password using the website.";
                message.Subject = "Login Credentials";
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;

                client.Credentials = new System.Net.NetworkCredential("media.bazaar2020@gmail.com", "SpankLee12.");
                client.Send(message);
                message = null;
            }
            catch (Exception s)
            {

            }
        }
    }
}
