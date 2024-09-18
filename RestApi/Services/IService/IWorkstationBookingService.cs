using RestApi.Models;

namespace RestAPI.Services.IService
{
    public interface IWorkstationBookingService
    {
        List<WorkstationBooking> GetAll();
        WorkstationBooking GetById(int id);
        WorkstationBooking Add(WorkstationBooking booking);
        void Update(WorkstationBooking booking);
        void Delete(int id);
    }
}
