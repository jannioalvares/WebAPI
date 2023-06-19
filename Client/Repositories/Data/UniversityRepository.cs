using Client.Models;
using Client.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Client.Repositories
{
    public class UniversityRepository : GeneralRepository<University, Guid>, IUniversityRepository
    {
        public UniversityRepository(string request = "University/") : base(request)
        {

        }
    }
}
