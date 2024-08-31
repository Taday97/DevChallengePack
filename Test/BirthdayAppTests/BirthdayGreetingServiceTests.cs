using Moq;
using BirthdayApp.Models;
using BirthdayApp.Interfaces;
using BirthdayApp.Services;
using BirthdayApp.Implementations.DataProvider;
using BirthdayApp.Implementations.MessageSender;
namespace Test.BirthdayAppTests
{
    public class BirthdayGreetingServiceTests
    {

        [Fact]
        public void GetTodaysBirthdays_WithListDataProvider_ReturnsCorrectFriends()
        {
            // Arrange
            var friendsList = new List<Friend>
            {
                new Friend { Name = "John Doe", Birthday = new DateTime(1990, 5, 15), Email = "john.doe@example.com" },
                new Friend { Name = "Jane Smith", Birthday = new DateTime(1985, DateTime.Today.Month, DateTime.Today.Day), Email = "jane.smith@example.com" },
                new Friend { Name = "Alice Johnson", Birthday = new DateTime(1992, 3, 10), Email = "alice.johnson@example.com" },
                new Friend { Name = "Bob Brown", Birthday = new DateTime(1988,DateTime.Today.Month, DateTime.Today.Day), Email = "bob.brown@example.com" }
            };
            IDataProvider dataProviderList = new ListDataProvider(friendsList);
            var messageSender = new Mock<IMessageSender>();
            var service = new BirthdayGreetingService(dataProviderList, messageSender.Object);

            var expectedFriends = new List<Friend>
            {
                new Friend { Name = "Jane Smith", Birthday = new DateTime(1985, DateTime.Today.Month, DateTime.Today.Day), Email = "jane.smith@example.com" },
                new Friend { Name = "Bob Brown", Birthday = new DateTime(1988,DateTime.Today.Month, DateTime.Today.Day), Email = "bob.brown@example.com" }
            };

            // Act
            var result = service.GetTodaysBirthdays();

            // Assert
            Assert.Equal(expectedFriends.Count, result.Count);
            for (int i = 0; i < expectedFriends.Count; i++)
            {
                Assert.Equal(expectedFriends[i].Name, result[i].Name);
                Assert.Equal(expectedFriends[i].Birthday, result[i].Birthday);
                Assert.Equal(expectedFriends[i].Email, result[i].Email);
            }
        }
       
        [Fact]
        public void GetTodaysBirthdays_WithCsvDataProvider_ReturnsCorrectFriends()
        {
            // Arrange
            var filePath = "C:\\Users\\taday\\source\\repos\\GETECAssessment\\BirthdayApp\\friendsCSV.csv";
            IDataProvider dataProviderCsv = new CsvDataProvider(filePath);
            var messageSender = new Mock<IMessageSender>();
            var service = new BirthdayGreetingService(dataProviderCsv, messageSender.Object);

            var expectedFriends = new List<Friend>();

            // Act
            var result = service.GetTodaysBirthdays();

            // Assert
            Assert.Equal(expectedFriends.Count, result.Count);
            
        }

        [Fact]
        public void GetTodaysBirthdays_WithTxtDataProvider_ReturnsCorrectFriends()
        {
            // Arrange
            var filePath = "C:\\Users\\taday\\source\\repos\\GETECAssessment\\BirthdayApp\\friendsTXT.txt";
            IDataProvider dataProviderTxt = new TxtDataProvider(filePath);
            var messageSender = new Mock<IMessageSender>();
            var service = new BirthdayGreetingService(dataProviderTxt, messageSender.Object);

            var expectedFriends = new List<Friend>();

            // Act
            var result = service.GetTodaysBirthdays();

            // Assert
            Assert.Equal(expectedFriends.Count, result.Count);
        }

        [Fact]
        public void SendMessage_WithEmailSender_SendsCorrectMessage()
        {
            var smtpServer = "smtp.example.com";
            var smtpPort = 587;
            var smtpUser = "user@example.com";
            var smtpPass = "password";
            var emailSender = new EmailSender(smtpServer, smtpPort, smtpUser, smtpPass);

            var friend = new Friend { Name = "Jane Smith", Birthday = DateTime.Today, Email = "jane.smith@example.com" };
            var message = "Happy Birthday, Jane!";


            // Act
            var exception = Record.Exception(() => emailSender.SendMessage(friend, message));

            // Assert
            Assert.Null(exception); // Assert that no exception was thrown
        }

        [Fact]
        public void SendMessage_WithCustomMessage_SendsCorrectMessage()
        {
            // Arrange
            var dataProvider = new ListDataProvider(new List<Friend>
             {
               new Friend { Name = "Alice Johnson", Birthday = DateTime.Today, Email = "alice.johnson@example.com" }
            });
            var messageSender = new Mock<IMessageSender>();
            var service = new BirthdayGreetingService(dataProvider, messageSender.Object);

            var customMessage = "Wishing you a fantastic day, Alice!";

            // Act
            messageSender.Object.SendMessage(new Friend { Name = "Alice Johnson", Birthday = DateTime.Today, Email = "alice.johnson@example.com" },customMessage);

            // Assert
            messageSender.Verify(m => m.SendMessage(It.Is<Friend>(f => f.Name == "Alice Johnson"), customMessage), Times.Once);
        }

    }
}
