using Client.ViewModel;
using WebAPI.Others;

namespace Client.Repositories
{
    public interface IRepository<T, X>
        where T : class
    {
        Task<ResponseListVM<T>> Get();
        Task<ResponseViewModel<T>> Get(X id);
        Task<ResponseMessageVM> Post(T entity);
        Task<ResponseMessageVM> Put(T entity);
        Task<ResponseMessageVM> Deletes(X id);
    }
}
