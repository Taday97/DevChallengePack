using BirthdayApp.Implementations.DataProvider;
using BirthdayApp.Implementations.MessageSender;
using BirthdayApp.Interfaces;
using BirthdayApp.Models;
using BirthdayApp.Services;
class Program
{
    static void Main(string[] args)
    {
        var filePath = "C:\\Users\\taday\\source\\repos\\GETECAssessment\\BirthdayApp\\friendsCSV.csv";

        #region Data Provider CSV

        IDataProvider dataProvider = new CsvDataProvider(filePath);

        #endregion


        #region Data Provider Text

        IDataProvider dataProviderTxt = new TxtDataProvider(filePath);

        #endregion


        #region Data Provider List

        var friendsList = new List<Friend>
        {
            new Friend { Name = "John Doe", Birthday = new DateTime(1990, 5, 15), Email = "john.doe@example.com" },
            new Friend { Name = "Jane Smith", Birthday = new DateTime(1985, DateTime.Today.Month, DateTime.Today.Day), Email = "jane.smith@example.com" },
            new Friend { Name = "Alice Johnson", Birthday = new DateTime(1992, 3, 10), Email = "alice.johnson@example.com" },
            new Friend { Name = "Bob Brown", Birthday = new DateTime(1988,DateTime.Today.Month, DateTime.Today.Day), Email = "bob.brown@example.com" }
        };
        IDataProvider dataProviderList = new ListDataProvider(friendsList);

        #endregion


        #region Email Sender

        string smtpServer = "smtp.example.com";
        int smtpPort = 587;
        string smtpUser = "user@example.com";
        string smtpPass = "password";
        IMessageSender messageSender = new EmailSender(smtpServer, smtpPort, smtpUser, smtpPass);

        #endregion



        var birthdayGreetingService = new BirthdayGreetingService(dataProviderList, messageSender);

        while (true)
        {
            // Get the friends who have a birthday today
            var todaysBirthdays = birthdayGreetingService.GetTodaysBirthdays();

            if (todaysBirthdays.Any())
            {
                Console.WriteLine("Today is the birthday of the following friends:");
                for (int i = 0; i < todaysBirthdays.Count; i++)
                {
                    var friend = todaysBirthdays[i];
                    Console.WriteLine($"{i + 1}. {friend.Name} ({friend.Email})");
                }

                Console.WriteLine("Select a person by number to send a birthday message or type '0' to exit:");
                if (int.TryParse(Console.ReadLine(), out int selectedIndex))
                {
                    if (selectedIndex == 0)
                    {
                        // Exit the application
                        break;
                    }
                    else if (selectedIndex > 0 && selectedIndex <= todaysBirthdays.Count)
                    {
                        var selectedFriend = todaysBirthdays[selectedIndex - 1];

                        Console.WriteLine("Please enter your birthday message:");
                        var message = Console.ReadLine();

                        // Send Message
                        //messageSender.SendMessage(selectedFriend, message);
                        birthdayGreetingService.DeleteFriendTodaysBirthdays(selectedFriend);
                        Console.WriteLine("Message has been sent!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
            else
            {
                Console.WriteLine("No friends have a birthday today.");
                Console.WriteLine("Press '0' to exit.");

                var input = Console.ReadLine()?.Trim().ToLower();

                if (input == "0")
                {
                    break;
                }
            }
        }

        Console.WriteLine("Thank you for using the Birthday Greeting App!");
    }
}