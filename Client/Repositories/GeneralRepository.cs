using Client.ViewModel;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Client.Repositories
{
    public class GeneralRepository<Entity, TId> : IRepository<Entity, TId>
        where Entity : class
    {
        private readonly string request;
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor contextAccessor;

        public GeneralRepository(string request)
        {
            this.request = request;
            contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7264/api/")
            };
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", contextAccessor.HttpContext?.Session.GetString("JWToken"));
        }

        public async Task<ResponseMessageVM> Deletes(TId Guid)
        {
            ResponseMessageVM entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(Guid), Encoding.UTF8, "application/json");
            using (var response = httpClient.DeleteAsync(request + Guid).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseMessageVM>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseMessageVM> Put(Entity entity)
        {
            ResponseMessageVM entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PutAsync(request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseMessageVM>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseMessageVM> Post(Entity entity)
        {
            ResponseMessageVM entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseMessageVM>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseListVM<Entity>> Get()
        {
            ResponseListVM<Entity> entityVM = null;
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseListVM<Entity>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseViewModel<Entity>> Get(TId Guid)
        {
            ResponseViewModel<Entity> entity = null;

            using (var response = await httpClient.GetAsync(request + Guid))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<ResponseViewModel<Entity>>(apiResponse);
            }
            return entity;
        }
    }
}
