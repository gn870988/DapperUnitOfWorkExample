using Dapper;
using Domain.Entities;
using System.Collections.Generic;
using System.Data;

namespace UnitOfWorks.Repositories
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        private string TableName => GetTableNameMapper();

        public MemberRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public IEnumerable<Member> FindName(string name)
        {
            return Connection.GetList<Member>(new { Name = name }, Transaction);
        }

        public Member FindFirstOrDefaultName(string name)
        {
            // 此範例是為了展示SQL，當邏輯太複雜可用Sql來撰寫
            string sql = $"Select * From {TableName} Where Name = @Name";

            return Connection.QueryFirstOrDefault<Member>(sql, new Member {Name = name}, Transaction);
        }
    }
}
