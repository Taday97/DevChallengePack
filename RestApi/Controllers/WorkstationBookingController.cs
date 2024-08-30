using Microsoft.AspNetCore.Mvc;
using RestApi.Models;
using System.Collections.Generic;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkstationBookingController : ControllerBase
    {
        List<WorkstationBooking> buchungen = new List<WorkstationBooking>();

        public WorkstationBookingController()
        {
            buchungen = buchungen ?? new List<WorkstationBooking>();
        }

        [HttpGet]
        public ActionResult<IEnumerable<WorkstationBooking>> Get()
        {
            return Ok(buchungen);
        }

        [HttpGet("{id}")]
        public ActionResult<WorkstationBooking> Get(int id)
        {
            var buchung = buchungen.FirstOrDefault(b => b.Id == id);
            if (buchung == null)
            {
                return NotFound();
            }
            return Ok(buchung);
        }

        [HttpPost]
        public ActionResult Post([FromBody] WorkstationBooking neueBuchung)
        {
            if (string.IsNullOrEmpty(neueBuchung.EmployeeName) || string.IsNullOrEmpty(neueBuchung.Seat) || neueBuchung.Start == default ||
                neueBuchung.End == default)
            {
                return BadRequest("Missing required fields.");
            }

            neueBuchung.Id = buchungen.Count > 0 ? buchungen.Max(b => b.Id) + 1 : 1;
            buchungen.Add(neueBuchung);
            return CreatedAtAction(nameof(Get), new { id = neueBuchung.Id }, neueBuchung);

        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] WorkstationBooking aktualisierteBuchung)
        {
            var index = buchungen.FindIndex(b => b.Id == id);
            if (index == -1)
            {
                return NotFound();
            }
            buchungen[index] = aktualisierteBuchung;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var buchung = buchungen.FirstOrDefault(b => b.Id == id);
            if (buchung == null)
            {
                return NotFound();
            }
            buchungen.Remove(buchung);
            return NoContent();
        }
    }
}