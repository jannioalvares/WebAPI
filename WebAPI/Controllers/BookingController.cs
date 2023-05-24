using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Repositories;
using WebAPI.ViewModels.Bookings;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {

        private readonly IBookingRepository _bookingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper<Booking, BookingVM> _mapper;
        public BookingController(IBookingRepository bookingRepository, IMapper<Booking, BookingVM> mapper, IEmployeeRepository employeeRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _roomRepository = roomRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var bookings = _bookingRepository.GetAll();
            if (!bookings.Any())
            {
                return NotFound();
            }

            var data = bookings.Select(_mapper.Map).ToList();
            return Ok(data);

        }

        [HttpGet("GetRoomByDate")]
        public IActionResult GetByGuidWithEducation(DateTime dateNow)
        {
            try
            {
                var bookings = _bookingRepository.GetByDate(dateNow);
                if (bookings is null)
                {
                    return NotFound();
                }
                var room = _roomRepository.GetByGuid(bookings.RoomGuid);
                if (room is null)
                {
                    return NotFound();
                }
                var employee = _employeeRepository.GetByGuid(bookings.Guid);
                if (employee is null)
                {
                    return NotFound();
                }

                var data = new
                {
                    BookedBy = employee.FirstName+" "+ employee.LastName,
                    Status = bookings.Status.ToString(),
                    RoomName = room.Name,
                    Floor = room.Floor,
                    Capacity = room.Capacity,
                    StartDate = bookings.StartDate,
                    EndDate = bookings.EndDate
                };

                return Ok(data);

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var booking = _bookingRepository.GetByGuid(guid);
            if (booking is null)
            {
                return NotFound();
            }

            var data = _mapper.Map(booking);
            return Ok(data);
        }

        [HttpGet("GetByDate")]
        public IActionResult GetByDate(DateTime StartDate)
        {
            var booked = _bookingRepository.GetByDate(StartDate);
            if (booked is null)
            {
                return NotFound();
            }

            var data = _mapper.Map(booked);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Create(BookingVM bookingVM)
        {
            var bookingConverted = _mapper.Map(bookingVM);
            var result = _bookingRepository.Create(bookingConverted);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(BookingVM bookingVM)
        {
            var bookingConverted = _mapper.Map(bookingVM);
            var isUpdated = _bookingRepository.Update(bookingConverted);
            if (isUpdated)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var isDeleted = _bookingRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
