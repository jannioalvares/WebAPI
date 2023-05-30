using WebAPI.Model;
using WebAPI.ViewModels.Rooms;

namespace WebAPI.Contracts
{
    public interface IRoomRepository : IGeneralRepository<Room>
    {
        bool CheckRoomName(string value);
        IEnumerable<MasterRoomVM> GetByDate(DateTime dateTime);
        IEnumerable<RoomUsedVM> GetCurrentlyUsedRooms();

        IEnumerable<EmptyRoomVM> GetRoomByDate();

    }
}
