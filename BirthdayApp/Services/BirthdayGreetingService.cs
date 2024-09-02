using BirthdayApp.Interfaces;
using BirthdayApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayApp.Services
{
    public class BirthdayGreetingService
    {
        private readonly IDataProvider _dataProvider;
        private readonly IMessageSender _messageSender;
        private readonly string _mensajeTemplate;

        public BirthdayGreetingService(IDataProvider dataProvider, IMessageSender messageSender, string mensajeTemplate)
        {
            _dataProvider = dataProvider;
            _messageSender = messageSender;
            _mensajeTemplate = mensajeTemplate;   
        }

        public void SendBirthdayGreeting()
        {
            var friends = _dataProvider.GetFriends();
            var date = DateTime.Today;

            foreach (var friend in friends)
            {
                if (friend.Birthday.Month == date.Month && friend.Birthday.Day == date.Day)
                {
                    var mensaje = string.Format(_mensajeTemplate, friend.Name);
                    _messageSender.SendMessage(friend, mensaje);
                }
            }
        }
    }
}