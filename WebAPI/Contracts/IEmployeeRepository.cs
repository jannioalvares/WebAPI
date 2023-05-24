using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;
using WebAPI.Repositories;
using WebAPI.ViewModels.Employees;

namespace WebAPI.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        IEnumerable<MasterEmployeeVM> GetAllMasterEmployee();

        MasterEmployeeVM? GetEmployeeById(Guid guid);

        object GetEmployeeAll(Guid guid);


    }
}
