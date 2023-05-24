using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.ViewModels.Rooms;

namespace WebAPI.Repositories
{
    public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
    {
        public BookingRepository(BookingManagementDbContext context) : base(context) { }

        public IEnumerable<Booking> GetByEmployeeId(Guid employeeId)
        {
            return _context.Set<Booking>().Where(b => b.EmployeeGuid == employeeId);
        }

        public IEnumerable<Booking> GetByRoomId(Guid roomId)
        {
            return _context.Set<Booking>().Where(b => b.RoomGuid == roomId);
        }

        public Booking? GetByDate(DateTime dateTime)
        {
            return _context.Set<Booking>().Find(dateTime);
        }
    }
}
