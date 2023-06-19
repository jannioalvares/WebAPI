using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Others;
using WebAPI.ViewModels.Rooms;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : BaseController<Room, RoomVM>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper<Room, RoomVM> _mapper;
        public RoomController(IRoomRepository roomRepository, IMapper<Room, RoomVM> mapper) : base (roomRepository, mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        [HttpGet("CurrentlyUsedRooms")]
        public IActionResult GetCurrentlyUsedRooms()
        {
            var room = _roomRepository.GetCurrentlyUsedRooms();
            if (room is null)
            {
                return NotFound(new ResponseMessageVM<string>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Room Not Found"
                });
            }

            return Ok(new ResponseMessageVM<IEnumerable<RoomUsedVM>>
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
                return NotFound(new ResponseMessageVM<string>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Room Not Found"
                });
            }

            return Ok(new ResponseMessageVM<IEnumerable<MasterRoomVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Room Found",
                Data = room
            });
        }

        [HttpGet("AvailableRoom")]
        public IActionResult GetAvailableRoom()
        {
            try
            {
                var room = _roomRepository.GetAvailableRoom();
                if (room is null)
                {
                    return NotFound(new ResponseMessageVM<EmptyRoomVM>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "AvailableRoom Not Found"
                    });
                }

                return Ok(new ResponseMessageVM<IEnumerable<EmptyRoomVM>>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "AvailableRoom Found",
                    Data = room
                });
            }
            catch
            {
                return Ok(new ResponseMessageVM<EmptyRoomVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Terjadi Error"
                });
            }
        }
    }
}
