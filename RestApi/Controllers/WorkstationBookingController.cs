using Microsoft.AspNetCore.Mvc;
using RestApi.Models;
using RestAPI.Repositories.IRepository;
using System.Collections.Generic;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkstationBookingController : ControllerBase
    {
        private readonly IWorkstationBookingService _bookingService;

        public WorkstationBookingController(IWorkstationBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WorkstationBooking>> Get()
        {
            return Ok(_bookingService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<WorkstationBooking> Get(int id)
        {
            var booking = _bookingService.GetById(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        [HttpPost]
        public ActionResult Post([FromBody] WorkstationBooking newBooking)
        {
            if (string.IsNullOrEmpty(newBooking.EmployeeName) || string.IsNullOrEmpty(newBooking.Seat) || newBooking.Start == default ||
                newBooking.End == default)
            {
                return BadRequest("Missing required fields.");
            }

            _bookingService.Add(newBooking);
            return Ok(newBooking);
        }

        [HttpPut]
        public ActionResult Put([FromBody] WorkstationBooking updatedBooking)
        {
            var booking = _bookingService.GetById(updatedBooking.Id);
            if (booking == null)
            {
                return NotFound();
            }

            _bookingService.Update(updatedBooking);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var booking = _bookingService.GetById(id);
            if (booking == null)
            {
                return NotFound();
            }

            _bookingService.Delete(id);
            return NoContent();
        }
    }
}