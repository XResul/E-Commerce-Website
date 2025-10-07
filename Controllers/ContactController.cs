using haliEntityLayer.Entity;
using haliWebProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace haliWebProje.Controllers
{
    public class ContactController : Controller
    {
        EntityModelContext db = new EntityModelContext();

        // GET: Contact

        public ActionResult Index()
        {
            ContactViewModel model = new ContactViewModel();
            model.Adresses__ = db.Adresses.ToList();
            model.contact_ = db.Contacts.Take(1).ToList().FirstOrDefault();
            model.social_ = db.Socials.Take(1).ToList().FirstOrDefault();

            return View(model);
        }

        public ActionResult iletisimForm(string email, string ad, string mesaj, string konu)
        {

            var mailModel = db.mailSetting.FirstOrDefault();

            //Gönderenin mail adresi ve şifresi
            string fromAddress = mailModel.SenderEmail;
            string password = mailModel.SenderPassword;

            //Alıcının e-posta adresi
            string toAddress = mailModel.ReceiverMail;

            //E-posta Konusu
            string subject = konu;

            //E-posta İçeriği
            string body = $"<p>Ad Soyad: {ad}</p><p>Email: <a href='mailto:{email}'>{email}</a></p><p>Konu: {konu}</p><p>Mesaj: {mesaj}</p>";

            //Gmail SMTP sunucusu ve portu
            string smtpServer = mailModel.Smtp;
            int port = mailModel.Port;


            //Gönderen e-posta adresi oluşturma
            MailAddress fromMailAddress = new MailAddress(fromAddress);

            //Alıcı e-posta adresi oluşturma
            MailAddress toMailAddress = new MailAddress(toAddress);

            //SMTP istemcisi oluşturma ve ayarlama 
            SmtpClient smtpClient = new SmtpClient(smtpServer, port);
            smtpClient.EnableSsl = mailModel.EnableSsl;// SSL/TLS kullanarak güvenli bağlantı sağlar
                                                       //smtpClient.UseDefaultCredentials = true; // Gmail kimlik bilgileri kullanılacak


            smtpClient.Credentials = new NetworkCredential(fromAddress, password); //Gönderenin kimlik bilgileri


            //E-posta oluşturma ve gönderme
            using (MailMessage mailMessage = new MailMessage(fromMailAddress,toMailAddress))
            {
                mailMessage.Subject = subject;  
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                try
                {
                    smtpClient.Send(mailMessage);
                }
                catch (Exception)
                {
                     
                }


            }


            return RedirectToAction("index");
        }



    }
}