using Microsoft.AspNetCore.Mvc;
using Nest;
using System;
using System.Net;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Others;
using WebAPI.Repositories;
using WebAPI.ViewModels.Employees;
using WebAPI.ViewModels.Roles;
using WebAPI.ViewModels.Rooms;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : BaseController<Room, RoomVM>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper<Room, RoomVM> _mapper;
        public RoomController(IRoomRepository roomRepository, IMapper<Room, RoomVM> mapper, 
            IBookingRepository bookingRepository, 
            IEmployeeRepository employeeRepository) : base (roomRepository, mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpGet("CurrentlyUsedRooms")]
        public IActionResult GetCurrentlyUsedRooms()
        {
            var room = _roomRepository.GetCurrentlyUsedRooms();
            if (room is null)
            {
                return NotFound(new ResponseVM<string>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Room Not Found"
                });
            }

            return Ok(new ResponseVM<IEnumerable<RoomUsedVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Room Found",
                Data = room
            });
        }

        [HttpGet("CurrentlyUsedRoomsByDate")]
        public IActionResult GetCurrentlyUsedRooms(DateTime dateTime)
        {
            var room = _roomRepository.GetByDate(dateTime);
            if (room is null)
            {
                return NotFound(new ResponseVM<string>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Room Not Found"
                });
            }

            return Ok(new ResponseVM<IEnumerable<MasterRoomVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Room Found",
                Data = room
            });
        }

        [HttpGet("RoomAvailable")]
        public IActionResult GetRoomByDate()
        {
            var response = new ResponseVM<IEnumerable<EmptyRoomVM>>();
            try
            {
                var room = _roomRepository.GetRoomByDate();
                if (room is null)
                {
                    return Ok(response.NotFound("Available Room Not Found"));
                }

                return Ok(response.Success(room, "Available Room Found"));
            }
            catch
            {
                return Ok(response.Error);
            }
        }
    }
}
