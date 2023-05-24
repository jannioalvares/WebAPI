using WebAPI.Model;

namespace WebAPI.Contracts
{
    public interface IEducationRepository : IGeneralRepository<Education>
    {
        IEnumerable<Education> GetByUniversityId(Guid universityId);
    }
}
