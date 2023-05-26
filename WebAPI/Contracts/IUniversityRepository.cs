using WebAPI.Model;
using WebAPI.ViewModels.Universities;

namespace WebAPI.Contracts
{
    public interface IUniversityRepository : IGeneralRepository<University>
    {
        University CreateWithValidate(University university);

        IEnumerable<UniversityEducationVM> GetUniversityEducation();
    }
}
