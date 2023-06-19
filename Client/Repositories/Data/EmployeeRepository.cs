using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModel;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, Guid>, IEmployeeRepository
    {
        private readonly HttpClient httpClient;
        private readonly string request;

        public EmployeeRepository(string request = "Employee/") : base(request)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7264/api/")
            };
            this.request = request;
        }

        public async Task<ResponseListVM<MasterEmployeeVM>> GetMasterEmployee()
        {
            ResponseListVM<MasterEmployeeVM> entityVM = null;
            using (var response = httpClient.GetAsync(request + "GetAllMasterEmployee").Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseListVM<MasterEmployeeVM>>(apiResponse);
            }
            return entityVM;
        }
    }
}
