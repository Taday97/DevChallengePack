using BirthdayApp.Interfaces;
using BirthdayApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayApp.Implementations.MessageSender
{
    public class WhatsAppSender : IMessageSender
    {
        public void SendMessage(string phone, string message)
        {
            //Logic for to send watapp
        }
    }
}