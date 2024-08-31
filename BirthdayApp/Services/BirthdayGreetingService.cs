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
        private readonly List<Friend> _friends;
        private readonly IDataProvider _dataProvider;
        private readonly IMessageSender _messageSender;

        public BirthdayGreetingService(IDataProvider dataProvider, IMessageSender messageSender)
        {
            _dataProvider = dataProvider;
            _messageSender = messageSender; 
            _friends = _dataProvider.GetFriends();  
        }

        public List<Friend> GetTodaysBirthdays()
        {
            var today = DateTime.Today;
            return _friends.Where(f => f.Birthday.Month == today.Month && f.Birthday.Day == today.Day).ToList();
        }

        public Friend FindFriendByEmail(string email)
        {
            return _friends.FirstOrDefault(f => f.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public bool DeleteFriendTodaysBirthdays(Friend friend)
        {
            try
            {
                _friends.Remove(friend);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}