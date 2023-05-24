using WebAPI.Model;

namespace WebAPI.Contracts
{
    public interface IBookingRepository : IGeneralRepository<Booking>
    {
        IEnumerable<Booking> GetByEmployeeId(Guid employeeId);
        IEnumerable<Booking> GetByRoomId(Guid roomId);
        Booking GetByDate(DateTime dateTime);
    }
}
