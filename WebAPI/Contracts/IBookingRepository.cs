using WebAPI.Model;

namespace WebAPI.Contracts
{
    public interface IBookingRepository
    {
        Booking Create(Booking booking);
        bool Update(Booking booking);
        bool Delete(Guid guid);
        IEnumerable<Booking> GetAll();
        Booking? GetByGuid(Guid guid);
    }
}
