using BirthdayApp.Interfaces;
using BirthdayApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayApp.Implementations.MessageSender
{
    public class EmailSender : IMessageSender
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;

        public EmailSender(string smtpServer, int smtpPort, string smtpUser, string smtpPass)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
        }

        public void SendMessage(Friend to, string message)
        {
            using (var client = new SmtpClient(_smtpServer, _smtpPort))
            {
                //client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                //client.EnableSsl = true;

                //var mailMessage = new MailMessage
                //{
                //    From = new MailAddress(_smtpUser),
                //    Subject = "Happy Birthday!",
                //    Body = message,
                //    IsBodyHtml = true
                //};
                //mailMessage.To.Add(to.Email);

                //client.Send(mailMessage);
            }
        }
    }
}