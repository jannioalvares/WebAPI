using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.ViewModels.Bookings;
using WebAPI.ViewModels.Rooms;

namespace WebAPI.Repositories
{
    public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
    {
        public BookingRepository(BookingManagementDbContext context) : base(context){}

        private int CalculateBookingDuration(DateTime startDate, DateTime endDate)
        {
            int totalDays = 0; // Untuk menghitung hari
            DateTime currentDate = startDate.Date; // Perhitungan tergantung dari tanggal yang digunakan

            while (currentDate <= endDate.Date)
            {
                // Mengecek apakah currentDate adalah hari kerja (Selain sabtu dan minggu) 
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    // Jika currentDate hari kerja, totalDays bertambah
                    totalDays++;
                }
                currentDate = currentDate.AddDays(1); // untuk maju ke tanggal berikutnya.
            }

            return totalDays;
        }

        public IEnumerable<BookingDurationVM> GetBookingDuration()
        {
            /* Menampung semua data room di var rooms*/
            var rooms = _context.Rooms.ToList();

            /* Mengambil semua data booking*/
            var bookings = GetAll();

            /* Melakukan instance/membuat object untuk setiap pemesanan yang memenuhi kondisi diatas..
               Pada part ini, value RoomName akan diisi dengan nama Room yang dicari berdasarkan RoomGuid. */
            var bookingduration = bookings.Select(b => new BookingDurationVM
            {
                RoomName = rooms.FirstOrDefault(r => r.Guid == b.RoomGuid)?.Name, // Di set menjadi (?) untuk memastikan tidak terjadi kesalahan ketika objek tidak ditemukan, sehingga valuenya akan otomatis NULL
                DurationOfBooking = CalculateBookingDuration(b.StartDate, b.EndDate)
            });

            return bookingduration;
        }
    }
}
