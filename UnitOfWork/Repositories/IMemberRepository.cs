using Domain.Entities;
using System.Collections.Generic;

namespace UnitOfWorks.Repositories
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        IEnumerable<Member> FindName(string name);
        Member FindFirstOrDefaultName(string name);
    }
}
