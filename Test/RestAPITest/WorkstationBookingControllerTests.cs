using Microsoft.AspNetCore.Mvc;
using RestApi.Controllers;
using RestApi.Models;
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

        public WorkstationBookingControllerTests()
        {
            _controller = new WorkstationBookingController();
        }

        [Fact]
        public void Get_ReturnsAllBookings()
        {
            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var items = Assert.IsType<List<WorkstationBooking>>(okResult.Value);
            Assert.Equal(0, items?.Count); // For default, the list is empty
        }

        [Fact]
        public void Get_ById_ReturnsBooking()
        {
            // Arrange
            var booking = new WorkstationBooking { Id = 1, EmployeeName = "John", Seat = "A1", Start = DateTime.Now, End = DateTime.Now.AddHours(1) };
            _controller.Post(booking);

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
            var nonExistentId = -1;

            // Act
            var result = _controller.Get(nonExistentId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
        
        [Fact]
        public void Post_CreatesBooking()
        {
            // Arrange
            var newBooking = new WorkstationBooking { EmployeeName = "Jane", Seat = "B2", Start = DateTime.Now, End = DateTime.Now.AddHours(2) };
            
            // Act
            var result = _controller.Post(newBooking);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var booking = Assert.IsType<WorkstationBooking>(createdAtActionResult.Value);
           
            Assert.Equal(1, booking.Id);
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
        public void Post_GeneratesUniqueID_EvenWhenIDIsProvided()
        {
            // Arrange
            var existingBooking = new WorkstationBooking
            {
                Id = 1,
                EmployeeName = "John",
                Seat = "A1",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(1)
            };

            _controller.Post(existingBooking); // Adding the initial booking

            var newBookingWithSameId = new WorkstationBooking
            {
                Id = 1, // Same ID as the existing booking
                EmployeeName = "Jane",
                Seat = "B2",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(2)
            };

            // Act
            var result = _controller.Post(newBookingWithSameId); // Trying to post with an existing ID

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var booking = Assert.IsType<WorkstationBooking>(createdAtActionResult.Value);

            Assert.Equal(2, booking.Id);
        }

        [Fact]
        public void Put_UpdatesBooking()
        {
            // Arrange
            var booking = new WorkstationBooking { Id = 1, EmployeeName = "John", Seat = "A1", Start = DateTime.Now, End = DateTime.Now.AddHours(1) };
            _controller.Post(booking);

            var updatedBooking = new WorkstationBooking { Id = 1, EmployeeName = "Jane", Seat = "B2", Start = DateTime.Now, End = DateTime.Now.AddHours(3) };

            // Act
            var result = _controller.Put(1, updatedBooking);

            // Assert
            Assert.IsType<NoContentResult>(result);
            var resultBooking = _controller.Get(1).Result as OkObjectResult;
            var updated = Assert.IsType<WorkstationBooking>(resultBooking?.Value);
            Assert.Equal("Jane", updated.EmployeeName);
        }
        [Fact]
        public void Put_ReturnsNotFound_WhenIdDoesNotExist()
        {
            // Arrange
            var booking = new WorkstationBooking
            {
                Id = 1,
                EmployeeName = "John",
                Seat = "A1",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(1)
            };
            _controller.Post(booking);

            var updatedBooking = new WorkstationBooking
            {
                Id = 2, // ID that doesn't exist in the list
                EmployeeName = "Jane",
                Seat = "B2",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(3)
            };

            // Act
            var result = _controller.Put(2, updatedBooking); // ID 2 doesn't exist

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_RemovesBooking()
        {
            // Arrange
            var booking = new WorkstationBooking { Id = 1, EmployeeName = "John", Seat = "A1", Start = DateTime.Now, End = DateTime.Now.AddHours(1) };
            _controller.Post(booking);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
            var getResult = _controller.Get(1);
            Assert.IsType<NotFoundResult>(getResult.Result);
        }
        [Fact]
        public void Delete_ReturnsNotFound_WhenIdDoesNotExist()
        {
            // Act
            // Try to delete a booking with an ID that does not exist
            var result = _controller.Delete(999); // Assuming 999 is a non-existent ID

            // Assert
            // Verify that the result is NotFoundResult
            Assert.IsType<NotFoundResult>(result);

        }
    }
}
