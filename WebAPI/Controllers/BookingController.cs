using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Others;
using WebAPI.Repositories;
using WebAPI.ViewModels.Bookings;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : BaseController<Booking, BookingVM>
    {

        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper<Booking, BookingVM> _mapper;
        public BookingController(IBookingRepository bookingRepository, 
            IMapper<Booking, BookingVM> mapper) : base(bookingRepository, mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("BookingLength")]
        public IActionResult GetDuration()
        {
            var response = new ResponseVM<IEnumerable<BookingDurationVM>>();
            var bookingLengths = _bookingRepository.GetBookingDuration();
            if (!bookingLengths.Any())
            {
                return NotFound(response.NotFound("BookingLength Not Found"));
            }

            return Ok(response.Success(bookingLengths, "BookingLength Found"));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("BookingDetail")]
        public IActionResult GetAllBookingDetail()
        {
            var response = new ResponseVM<IEnumerable<BookingDetailVM>>();
            try
            {
                var results = _bookingRepository.GetAllBookingDetail();
                return Ok(response.Success(results, "BookingDetail Found"));
            }
            catch
            {
                return Ok(response.NotFound("BookingDetail Not Found"));
            }
        }

        [HttpGet("BookingDetail/{guid}")]
        public IActionResult GetDetailByGuid(Guid guid)
        {
            var response = new ResponseVM<BookingDetailVM>();
            try
            {
                var bookingDetailVM = _bookingRepository.GetBookingDetailByGuid(guid);

                if (bookingDetailVM is null)
                {
                    return Ok(response.NotFound("BookingDetail Not Found"));
                }


                return Ok(response.Success(bookingDetailVM, "BookingDetail Found"));
            }
            catch
            {
                return Ok(response.NotFound("BookingDetail Not Found"));
            }
        }
    }
}
