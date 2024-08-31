using BirthdayApp.Interfaces;
using BirthdayApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayApp.Implementations.DataProvider
{
    public class ListDataProvider : IDataProvider
    {
        private readonly List<Friend> _friends;

        public ListDataProvider(List<Friend> friends)
        {
            _friends = friends;
        }

        public List<Friend> GetFriends()
        {
            return _friends;
        }
    }
}