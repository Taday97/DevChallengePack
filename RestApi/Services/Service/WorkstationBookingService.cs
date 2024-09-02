using RestApi.Models;
using RestAPI.Repositories.IRepository;

namespace RestAPI.Repositories.Repository
{
    public class WorkstationBookingService : IWorkstationBookingService
    {

        private readonly List<WorkstationBooking> _bookings = new List<WorkstationBooking>();

        public WorkstationBookingService()
        {
            _bookings = new List<WorkstationBooking>();
        }

        public List<WorkstationBooking> GetAll()
        {
            return _bookings;
        }

        public WorkstationBooking GetById(int id)
        {
            return _bookings.FirstOrDefault(b => b.Id == id);
        }

        public WorkstationBooking Add(WorkstationBooking booking)
        {
            booking.Id = _bookings.Count > 0 ? _bookings.Max(b => b.Id) + 1 : 1;
            _bookings.Add(booking);
            return booking;
        }

        public void Update(int id, WorkstationBooking booking)
        {
            var index = _bookings.FindIndex(b => b.Id == id);
            if (index != -1)
            {
                _bookings[index] = booking;
            }
        }

        public void Delete(int id)
        {
            var booking = _bookings.FirstOrDefault(b => b.Id == id);
            if (booking != null)
            {
                _bookings.Remove(booking);
            }
        }
    }
}