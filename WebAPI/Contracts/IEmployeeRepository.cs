using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;
using WebAPI.Repositories;
using WebAPI.ViewModels.Employees;

namespace WebAPI.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        IEnumerable<MasterEmployeeVM> GetAllMasterEmployee();

        MasterEmployeeVM? GetMasterEmployeeByGuid(Guid guid);

        int CreateWithValidate(Employee employee);
        public Guid? FindGuidByEmail(string email);

    }
}
