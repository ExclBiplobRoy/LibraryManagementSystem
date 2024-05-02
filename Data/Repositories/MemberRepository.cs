using Data.Contracts;
using Domain.Entities;

namespace Data.Repositories
{
    public class MemberRepository : Repository<Member>, IMemberRepository
    {
        public MemberRepository(DataContext context) : base(context)
        {

        }

        async Task<Member> IMemberRepository.GetMemberByKey(int key)
        {
            try
            {
                return await FirstOrDefaultAsync(b => b.MemberID == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<IEnumerable<Member>> IMemberRepository.GetMembers()
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