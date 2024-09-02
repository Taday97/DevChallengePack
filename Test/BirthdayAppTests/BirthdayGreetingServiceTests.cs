using Moq;
using BirthdayApp.Models;
using BirthdayApp.Interfaces;
using BirthdayApp.Services;
using BirthdayApp.Implementations.DataProvider;
using BirthdayApp.Implementations.MessageSender;
using System.Reflection.Metadata;
namespace Test.BirthdayAppTests
{
    public class BirthdayGreetingServiceTests
    {
        [Fact]
        public void SendBirthdayGreetings_SendMessageCorrect()
        {
            // Arrange
            var mockDataProvider = new Mock<IDataProvider>();
            var mockMessageSender = new Mock<IMessageSender>();
            var mensajeTemplate = "Happy Birthday, {0}!";

            // Simula Friends que cumplen años hoy
            var friendJuan = new Friend { Name = "Juan", Birthday = DateTime.Today, Email = "juan@example.com" };
            var friendAna = new Friend { Name = "Ana", Birthday = DateTime.Today, Email = "ana@example.com" };
            var Friends = new List<Friend>
           {
            friendJuan,
            friendAna
           };

            mockDataProvider.Setup(dp => dp.GetFriends()).Returns(Friends);

            var servicio = new BirthdayGreetingService(mockDataProvider.Object, mockMessageSender.Object, mensajeTemplate);

            // Act
            servicio.SendBirthdayGreeting();

            // Assert
            mockMessageSender.Verify(ms => ms.SendMessage(friendJuan, "Happy Birthday, Juan!"), Times.Once);
            mockMessageSender.Verify(ms => ms.SendMessage(friendAna, "Happy Birthday, Ana!"), Times.Once);
        }

        [Fact]
        public void SendBirthdayGreetings_UsesCorrectGreetingTemplate()
        {
            // Arrange
            var mockDataProvider = new Mock<IDataProvider>();
            var mockMessageSender = new Mock<IMessageSender>();
            var messageTemplate = "Happy Birthday, {0}!";
            var expectedMessage = "Happy Birthday, Juan!";

            var friendBirthday = new Friend { Name = "Juan", Birthday = DateTime.Today, Email = "juan@example.com" };

            mockDataProvider.Setup(dp => dp.GetFriends()).Returns(new List<Friend>
           {
             new Friend { Name = "Juan", Birthday = DateTime.Today, Email = "juan@example.com" }
           });

            var service = new BirthdayGreetingService(mockDataProvider.Object, mockMessageSender.Object, messageTemplate);

            // Act
            service.SendBirthdayGreeting();

            // Assert
            mockMessageSender.Verify(ms => ms.SendMessage(friendBirthday, expectedMessage), Times.Once);
        }
        [Fact]
        public void GetFriends_FromDifferentSources_ShouldWorkCorrectly()
        {
            // Arrange
            var mockCsvProvider = new Mock<IDataProvider>();
            var mockTxtProvider = new Mock<IDataProvider>();
           
            var csvFriends = new List<Friend> {
            new Friend { Name = "Ana", Birthday = DateTime.Today,Email = "ana@example.com"},
            new Friend { Name = "John Doe", Birthday = new DateTime(1990, 5, 15), Email = "john.doe@example.com"},
            new Friend { Name = "Jane Smith", Birthday = new DateTime(1985, 9, 22), Email = "jane.smith@example.com" },
            new Friend { Name = "Alice Johnson", Birthday = new DateTime(1992, 3, 10),Email = "alice.johnson@example.com" },
            new Friend { Name = "Bob Brown", Birthday = new DateTime(1988, 12, 1),Email = "bob.brown@example.com" } };
           
            var txtFriends = new List<Friend> {
            new Friend { Name = "Ana", Birthday = DateTime.Today,Email = "ana@example.com"},
            new Friend { Name = "John Doe", Birthday = new DateTime(1990, 5, 15), Email = "john.doe@example.com"},
            new Friend { Name = "Jane Smith", Birthday = new DateTime(1985, 9, 22), Email = "jane.smith@example.com" },
            new Friend { Name = "Alice Johnson", Birthday = new DateTime(1992, 3, 10),Email = "alice.johnson@example.com" },
            new Friend { Name = "Bob Brown", Birthday = new DateTime(1988, 12, 1),Email = "bob.brown@example.com" } };



            // Act & Assert
            Assert.Equal(csvFriends, mockCsvProvider.Object.GetFriends());
            Assert.Equal(txtFriends, mockTxtProvider.Object.GetFriends());
        }

        [Fact]
        public void SendMessage_UsingDifferentMethods_ShouldWorkCorrectly()
        {
            // Arrange
            var mockEmailSender = new Mock<IMessageSender>();
            var mockWhatsAppSender = new Mock<IMessageSender>();
            var message = "Happy Birthday, Juan!";
            
            var friendBirthday = new Friend { Name = "Juan", Birthday = DateTime.Today, Email = "juan@example.com" };


            // Act
            mockEmailSender.Object.SendMessage(friendBirthday, message);
            mockWhatsAppSender.Object.SendMessage(friendBirthday, message);

            // Assert
            mockEmailSender.Verify(ms => ms.SendMessage(friendBirthday, message), Times.Once);
            mockWhatsAppSender.Verify(ms => ms.SendMessage(friendBirthday, message), Times.Once);
        }
    }
}
