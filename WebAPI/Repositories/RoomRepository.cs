﻿using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.ViewModels.Bookings;
using WebAPI.ViewModels.Employees;
using WebAPI.ViewModels.Rooms;

namespace WebAPI.Repositories
{
    public class RoomRepository : GeneralRepository<Room>, IRoomRepository
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public RoomRepository(BookingManagementDbContext context,
            IBookingRepository bookingRepository,
            IEmployeeRepository employeeRepository) : base(context)
        {
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<MasterRoomVM> GetByDate(DateTime dateTime)
        {
            var rooms = GetAll();
            var bookings = _bookingRepository.GetAll();
            var employees = _employeeRepository.GetAll();

            var usedRooms = new List<MasterRoomVM>();

            foreach (var room in rooms)
            {
                var booking = bookings.FirstOrDefault(b => b.RoomGuid == room.Guid && b.StartDate <= dateTime && b.EndDate >= dateTime);
                if (booking != null)
                {
                    var employee = employees.FirstOrDefault(e => e.Guid == booking.EmployeeGuid);

                    var result = new MasterRoomVM
                    {
                        BookedBy = employee.FirstName + " " + employee?.LastName,
                        Status = booking.Status.ToString(),
                        RoomName = room.Name,
                        Floor = room.Floor,
                        Capacity = room.Capacity,
                        StartDate = booking.StartDate,
                        EndDate = booking.EndDate,
                    };

                    usedRooms.Add(result);
                }
            }

            return usedRooms;
        }

        public IEnumerable<RoomUsedVM> GetCurrentlyUsedRooms()
        {
            var rooms = GetAll();
            var bookings = _bookingRepository.GetAll();
            var employees = _employeeRepository.GetAll();

            var usedRooms = new List<RoomUsedVM>();

            var currentTime = DateTime.Now;

            foreach (var room in rooms)
            {
                var booking = bookings.FirstOrDefault(b => b.RoomGuid == room.Guid && b.StartDate <= currentTime && b.EndDate >= currentTime);
                if (booking != null)
                {
                    var employee = employees.FirstOrDefault(e => e.Guid == booking.EmployeeGuid);

                    var result = new RoomUsedVM
                    {
                        RoomName = room.Name,
                        Status = booking.Status.ToString(),
                        Floor = room.Floor,
                        BookedBy = employee.FirstName + " " + employee?.LastName,
                    };

                    usedRooms.Add(result);
                }
            }
            return usedRooms;
        }

        public IEnumerable<RoomUsedVM> GetCurrentlyUsed()
        {
            var rooms = GetAll();
            var bookings = _bookingRepository.GetAll();
            var employees = _employeeRepository.GetAll();

            var usedRooms = new List<RoomUsedVM>();

            foreach (var room in rooms)
            {
                var booking = bookings.FirstOrDefault(b => b.RoomGuid == room.Guid);
                var employee = employees.FirstOrDefault(e => e.Guid == booking?.EmployeeGuid);

                var result = new RoomUsedVM
                {
                    RoomName = room.Name,
                    Status = booking.Status.ToString(),
                    Floor = room.Floor,
                    BookedBy = employee.FirstName + " " + employee?.LastName,
                };

                usedRooms.Add(result);
            }
            return usedRooms;
        }
    }
}
