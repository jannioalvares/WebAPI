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
    public class BookingController : ControllerBase
    {

        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper<Booking, BookingVM> _mapper;
        public BookingController(IBookingRepository bookingRepository, IMapper<Booking, BookingVM> mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new ResponseVM<IEnumerable<BookingVM>>();
            var bookings = _bookingRepository.GetAll();
            if (!bookings.Any())
            {
                return NotFound(response.NotFound("Booking Not Found"));
            }

            var data = bookings.Select(_mapper.Map).ToList();
            return Ok(response.Success(data, "Booking Found"));

        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var response = new ResponseVM<BookingVM>();
            var booking = _bookingRepository.GetByGuid(guid);
            if (booking is null)
            {
                return NotFound(response.NotFound("Booking Not Found"));
            }

            var data = _mapper.Map(booking);
            return Ok(response.Success(data, "Booking Found"));
        }

        [HttpPost]
        public IActionResult Create(BookingVM bookingVM)
        {
            var response = new ResponseVM<BookingVM>();
            var bookingConverted = _mapper.Map(bookingVM);
            var result = _bookingRepository.Create(bookingConverted);
            if (result is null)
            {
                return BadRequest(response.Failed("Booking Create Failed"));
            }
            return Ok(response.Success("Booking Create Success"));
        }

        [HttpPut]
        public IActionResult Update(BookingVM bookingVM)
        {
            var response = new ResponseVM<BookingVM>();
            var bookingConverted = _mapper.Map(bookingVM);
            var isUpdated = _bookingRepository.Update(bookingConverted);
            if (isUpdated)
            {
                return BadRequest(response.Failed("Booking Update Failed"));
            }
            return Ok(response.Success("Booking Update Success"));
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var response = new ResponseVM<BookingVM>();
            var isDeleted = _bookingRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(response.Failed("Booking Delete Failed"));
            }

            return Ok(response.Success("Booking Delete Success"));
        }

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
