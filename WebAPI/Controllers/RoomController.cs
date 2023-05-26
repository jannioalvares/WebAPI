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
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper<Room, RoomVM> _mapper;
        public RoomController(IRoomRepository roomRepository, IMapper<Room, RoomVM> mapper, IBookingRepository bookingRepository, IEmployeeRepository employeeRepository)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new ResponseVM<IEnumerable<RoomVM>>();
            var rooms = _roomRepository.GetAll();
            if (!rooms.Any())
            {
                return NotFound(response.NotFound("Room Not Found"));
            }

            var data = rooms.Select(_mapper.Map).ToList();
            return Ok(response.Success(data, "Room Found"));
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var response = new ResponseVM<RoomVM>();
            var room = _roomRepository.GetByGuid(guid);
            if (room is null)
            {
                return NotFound(response.NotFound("Room Not Found"));
            }

            var data = _mapper.Map(room);
            return Ok(response.Success(data, "Room Found"));
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

        [HttpPost]
        public IActionResult Create(RoomVM roomVM)
        {
            var response = new ResponseVM<Room>();
            var roomConverted = _mapper.Map(roomVM);
            var result = _roomRepository.Create(roomConverted);
            if (result is null)
            {
                return BadRequest(response.Failed("Room Create Failed"));
            }

            return Ok(response.Success(result, "Room Create Success"));
        }

        [HttpPut]
        public IActionResult Update(RoomVM roomVM)
        {
            var response = new ResponseVM<RoomVM>();
            var roomConverted = _mapper.Map(roomVM);
            var isUpdated = _roomRepository.Update(roomConverted);
            if (!isUpdated)
            {
                return BadRequest(response.Failed("Room Update Failed"));
            }

            return Ok(response.Success("Room Update Success"));
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var response = new ResponseVM<RoomVM>();
            var isDeleted = _roomRepository.Delete(guid);
            if (!isDeleted)
            {
                return BadRequest(response.Failed("Room Delete Failed"));
            }

            return Ok(response.Success("Room Delete Success"));
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
