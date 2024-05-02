using Domain.Entities;

namespace Data.Contracts
{
    public interface IMemberRepository : IRepository<Member>
    {
        public Task<Member> GetMemberByKey(int key);

        public Task<IEnumerable<Member>> GetMembers();
    }
}