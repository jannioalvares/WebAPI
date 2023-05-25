using WebAPI.Model;

namespace WebAPI.Contracts
{
    public interface IUniversityRepository : IGeneralRepository<University>
    {
        University CreateWithValidate(University university);
    }
}
