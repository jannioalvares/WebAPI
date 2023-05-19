﻿using WebAPI.Model;

namespace WebAPI.Contracts
{
    public interface IRoomRepository
    {
        Room Create(Room room);
        bool Update(Room room);
        bool Delete(Guid guid);
        IEnumerable<Room> GetAll();
        Room? GetByGuid(Guid guid);
    }
}
