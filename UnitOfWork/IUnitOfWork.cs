using System;
using UnitOfWorks.Repositories;

namespace UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IMemberRepository MemberRepository { get; }

        void Complete();
    }
}
