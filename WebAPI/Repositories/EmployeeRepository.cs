using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;

namespace WebAPI.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly BookingManagementDbContext _context;
        public EmployeeRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        /*
         * <summary>
         * Create a new university
         * </summary>
         * <param name="university">University object</param>
         * <returns>University object</returns>
         */
        public Employee Create(Employee employee)
        {
            try
            {
                _context.Set<Employee>().Add(employee);
                _context.SaveChanges();
                return employee;
            }
            catch
            {
                return new Employee();
            }
        }

        /*
         * <summary>
         * Update a university
         * </summary>
         * <param name="university">University object</param>
         * <returns>true if data updated</returns>
         * <returns>false if data not updated</returns>
         */
        public bool Update(Employee employee)
        {
            try
            {
                _context.Set<Employee>().Update(employee);
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
                var employee = GetByGuid(guid);
                if (employee == null)
                {
                    return false;
                }

                _context.Set<Employee>().Remove(employee);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Set<Employee>().ToList();
        }

        public Employee? GetByGuid(Guid guid)
        {
            return _context.Set<Employee>().Find(guid);
        }
    }
}
