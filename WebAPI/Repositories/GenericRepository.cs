using WebAPI.Model;
using WebAPI.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAPI.Context;

namespace WebAPI.Repositories
{
    public class GenericRepository<Entity> : IRepository<Entity> where Entity : class
    {
        private readonly BookingManagementDbContext _context;

        public GenericRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public Entity Create(Entity entity)
        {
            try
            {
                _context.Set<Entity>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }

        public bool Update(Entity entity)
        {
            try
            {
                _context.Set<Entity>().Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Guid guid)
        {
            try
            {
                var entity = GetByGuid(guid);
                if (entity == null)
                {
                    return false;
                }

                _context.Set<Entity>().Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Entity> GetAll()
        {
            return _context.Set<Entity>().ToList();
        }

        public Entity GetByGuid(Guid guid)
        {
            return _context.Set<Entity>().Find(guid);
        }
    }
}
