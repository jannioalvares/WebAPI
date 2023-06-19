using Client.Models;
using Client.ViewModel;

namespace Client.Repositories.Interface
{
    public interface IEmployeeRepository : IRepository<Employee, Guid>
    {
        public Task<ResponseListVM<MasterEmployeeVM>> GetMasterEmployee();
    }
}
