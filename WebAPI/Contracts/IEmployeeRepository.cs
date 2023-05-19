using WebAPI.Model;

namespace WebAPI.Contracts
{
    public interface IEmployeeRepository
    {
        Employee Create(Employee employee);
        bool Update(Employee employee);
        bool Delete(Guid guid);
        IEnumerable<Employee> GetAll();
        Employee? GetByGuid(Guid guid);
    }
}
