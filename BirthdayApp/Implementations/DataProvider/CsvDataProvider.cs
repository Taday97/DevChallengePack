using BirthdayApp.Interfaces;
using BirthdayApp.Models;
using System.Globalization;

namespace BirthdayApp.Implementations.DataProvider
{
    public class CsvDataProvider : IDataProvider
    {
        private readonly string _filePath;

        public CsvDataProvider(string filePath)
        {
            _filePath = filePath;
        }

        public List<Friend> GetFriends()
        {
            var csvContent = File.ReadAllText(_filePath);
            var friends = new List<Friend>();

            foreach (var line in csvContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                var parts = line.Split(',');

                if (parts.Length == 3)
                {
                    if (DateTime.TryParseExact(parts[1], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime birthday))
                    {
                        friends.Add(new Friend
                        {
                            Name = parts[0],
                            Birthday = birthday,
                            Email = parts[2]
                        });
                    }
                }
            }

            return friends;
        }
    }
}