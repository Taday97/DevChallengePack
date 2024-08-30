using System.ComponentModel.DataAnnotations;

namespace RestApi.Models
{
    public class WorkstationBooking
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string Seat { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public WorkstationBooking()
        {
            EmployeeName = string.Empty;
            Seat = string.Empty;
        }
    }
}
