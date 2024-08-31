using Microsoft.AspNetCore.Mvc;
using Moq;
using RestApi.Controllers;
using RestApi.Models;
using RestAPI.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.RestAPITest
{
    public class WorkstationBookingControllerTests
    {
        private readonly WorkstationBookingController _controller;
        private readonly Mock<IWorkstationBookingService> _mockService;

        public WorkstationBookingControllerTests()
        {
            _mockService = new Mock<IWorkstationBookingService>();
            _controller = new WorkstationBookingController(_mockService.Object);
        }

        [Fact]
        public void Get_ReturnsAllBookings()
        {
            // Arrange
            var bookings = new List<WorkstationBooking>
            {
                new WorkstationBooking { Id = 1, EmployeeName = "John", Seat = "A1", Start = DateTime.Now, End = DateTime.Now.AddHours(1) }
            };
            _mockService.Setup(service => service.GetAll()).Returns(bookings);

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var items = Assert.IsType<List<WorkstationBooking>>(okResult.Value);
            Assert.Single(items);
        }
        [Fact]
        public void Get_ById_ReturnsBooking()
        {
            // Arrange
            var booking = new WorkstationBooking { Id = 1, EmployeeName = "John", Seat = "A1", Start = DateTime.Now, End = DateTime.Now.AddHours(1) };
            _mockService.Setup(service => service.GetById(1)).Returns(booking);

            // Act
            var result = _controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedBooking = Assert.IsType<WorkstationBooking>(okResult.Value);
            Assert.Equal(1, returnedBooking.Id);
        }
        [Fact]
        public void Get_ById_ReturnsNotFound_WhenIdDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetById(It.IsAny<int>())).Returns((WorkstationBooking)null);

            // Act
            var result = _controller.Get(-1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public void Post_CreatesBooking()
        {
            // Arrange
            var newBooking = new WorkstationBooking { EmployeeName = "Jane", Seat = "B2", Start = DateTime.Now, End = DateTime.Now.AddHours(2) };
            _mockService.Setup(service => service.Add(It.IsAny<WorkstationBooking>())).Returns(newBooking);
            _mockService.Setup(service => service.GetById(It.IsAny<int>())).Returns(newBooking);

            // Act
            var result = _controller.Post(newBooking);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var booking = Assert.IsType<WorkstationBooking>(createdAtActionResult.Value);
            Assert.Equal(newBooking.EmployeeName, booking.EmployeeName);
        }

        [Fact]
        public void Post_ReturnsBadRequest_WhenRequiredFieldsAreMissing()
        {
            // Arrange
            var incompleteBooking = new WorkstationBooking { EmployeeName = "Jane" }; // Missing Seat, Start, End

            // Act
            var result = _controller.Post(incompleteBooking);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Put_UpdatesBooking()
        {
            // Arrange
            var booking = new WorkstationBooking { Id = 1, EmployeeName = "John", Seat = "A1", Start = DateTime.Now, End = DateTime.Now.AddHours(1) };
            var updatedBooking = new WorkstationBooking { Id = 1, EmployeeName = "Jane", Seat = "B2", Start = DateTime.Now, End = DateTime.Now.AddHours(2) };
            _mockService.Setup(service => service.GetById(1)).Returns(booking);
            _mockService.Setup(service => service.Update(It.IsAny<int>(), It.IsAny<WorkstationBooking>())).Verifiable();

            // Act
            var result = _controller.Put(1, updatedBooking);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _mockService.Verify(service => service.Update(1, updatedBooking), Times.Once);
        }

        [Fact]
        public void Put_ReturnsNotFound_WhenIdDoesNotExist()
        {
            // Arrange
            var updatedBooking = new WorkstationBooking { Id = 2, EmployeeName = "Jane", Seat = "B2", Start = DateTime.Now, End = DateTime.Now.AddHours(2) };
            _mockService.Setup(service => service.GetById(2)).Returns((WorkstationBooking)null);

            // Act
            var result = _controller.Put(2, updatedBooking);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_RemovesBooking()
        {
            // Arrange
            var booking = new WorkstationBooking { Id = 1, EmployeeName = "John", Seat = "A1", Start = DateTime.Now, End = DateTime.Now.AddHours(1) };
            _mockService.Setup(service => service.GetById(1)).Returns(booking);
            _mockService.Setup(service => service.Delete(1)).Verifiable();

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _mockService.Verify(service => service.Delete(1), Times.Once);
        }

        [Fact]
        public void Delete_ReturnsNotFound_WhenIdDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetById(It.IsAny<int>())).Returns((WorkstationBooking)null);

            // Act
            var result = _controller.Delete(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}