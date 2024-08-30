using RestApi.Models;

namespace RestAPI.Repositories.IRepository
{
    public interface IWorkstationBookingService
    {
        IEnumerable<WorkstationBooking> GetAllBookings();
        WorkstationBooking GetBookingById(int id);
        WorkstationBooking AddBooking(WorkstationBooking booking);
        bool UpdateBooking(int id, WorkstationBooking updatedBooking);
        bool DeleteBooking(int id);
    }
}
