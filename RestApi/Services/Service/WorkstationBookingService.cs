using RestApi.Models;
using RestAPI.Repositories.IRepository;

namespace RestAPI.Repositories.Repository
{
    public class WorkstationBookingService : IWorkstationBookingService
    {
        private readonly List<WorkstationBooking> _bookings;

        public WorkstationBookingService()
        {
            _bookings = new List<WorkstationBooking>();
        }

        public IEnumerable<WorkstationBooking> GetAllBookings()
        {
            return _bookings;
        }

        public WorkstationBooking GetBookingById(int id)
        {
            return _bookings.FirstOrDefault(b => b.Id == id);
        }

        public WorkstationBooking AddBooking(WorkstationBooking newBooking)
        {
            if (string.IsNullOrEmpty(newBooking.EmployeeName) || string.IsNullOrEmpty(newBooking.Seat) ||
                newBooking.Start == default || newBooking.End == default)
            {
                throw new ArgumentException("Missing required fields.");
            }

            newBooking.Id = _bookings.Count > 0 ? _bookings.Max(b => b.Id) + 1 : 1;
            _bookings.Add(newBooking);
            return newBooking;
        }

        public bool UpdateBooking(int id, WorkstationBooking updatedBooking)
        {
            var index = _bookings.FindIndex(b => b.Id == id);
            if (index == -1)
            {
                return false;
            }

            _bookings[index] = updatedBooking;
            return true;
        }

        public bool DeleteBooking(int id)
        {
            var booking = _bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null)
            {
                return false;
            }

            _bookings.Remove(booking);
            return true;
        }
    }
}