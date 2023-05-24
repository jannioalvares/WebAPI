using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.ViewModels.Rooms;

namespace WebAPI.Repositories
{
    public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
    {
        public BookingRepository(BookingManagementDbContext context) : base(context) { }

    }
}
