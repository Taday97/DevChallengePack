using BirthdayApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayApp.Interfaces
{
    public interface IMessageSender
    {
        void SendMessage(Friend friend, string message);
    }
}
