using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;

namespace WebAPI.Repositories
{
    public class UniversityRepository : GeneralRepository<University>, IUniversityRepository
    {
        public UniversityRepository(BookingManagementDbContext context) : base(context) { }

        public University CreateWithValidate(University university)
        {
            try
            {
                var existingUniversityWithCode = _context.Universities.FirstOrDefault(u => u.Code == university.Code);
                var existingUniversityWithName = _context.Universities.FirstOrDefault(u => u.Name == university.Name);

                if (existingUniversityWithCode != null & existingUniversityWithName != null)
                {
                    university.Guid = existingUniversityWithCode.Guid;

                    _context.SaveChanges();

                }
                else
                {
                    _context.Universities.Add(university);
                    _context.SaveChanges();
                }

                return university;

            }
            catch
            {
                return null;
            }
        }
    }
}
