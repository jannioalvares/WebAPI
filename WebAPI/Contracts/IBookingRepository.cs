using WebAPI.Model;
using WebAPI.ViewModels.Bookings;

namespace WebAPI.Contracts
{
    public interface IBookingRepository : IGeneralRepository<Booking>
    {
        IEnumerable<BookingDurationVM> GetBookingDuration();
        BookingDetailVM GetBookingDetailByGuid(Guid guid);
        IEnumerable<BookingDetailVM> GetAllBookingDetail();
    }
}
