﻿using WebAPI.Utility;

namespace WebAPI.ViewModels.Rooms
{
    public class MasterRoomVM
    {
        public string BookedBy { get; set; }
        public string Status { get; set; }
        public string RoomName { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
