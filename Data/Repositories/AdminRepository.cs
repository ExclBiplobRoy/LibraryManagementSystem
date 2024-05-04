using Data.Contracts;
using Domain.Entities;

namespace Data.Repositories
{
    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        public AdminRepository(DataContext context) : base(context)
        {

        }

        async Task<Admin> IAdminRepository.GetAdminByKey(int key)
        {
            try
            {
                return await FirstOrDefaultAsync(b => b.AdminID == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<Admin> IAdminRepository.GetAdminByEmail(string key)
        {
            string[] EmailPass = key.Split(',');
            try
            {
                var admin = await FirstOrDefaultAsync(b => b.Email == EmailPass[0] && b.Password == EmailPass[1]);
                if (admin != null)
                    return admin;
                else
                    return new Admin();
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<IEnumerable<Admin>> IAdminRepository.GetAdmins()
        {
            try
            {
                return await GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}