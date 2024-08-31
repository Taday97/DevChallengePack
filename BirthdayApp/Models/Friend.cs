using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayApp.Models
{
    public class Friend
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }

        public Friend()
        {
        }
        public Friend(string name, DateTime birthday, string email)
        {
            Name = name;
            Birthday = birthday;
            Email = email;
        }
    }
}
