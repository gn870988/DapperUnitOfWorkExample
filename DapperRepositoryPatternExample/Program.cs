using Domain.Entities;
using System;
using System.Linq;
using UnitOfWorks;
using UnitOfWorks.Infrastructure;

namespace DapperRepositoryPatternExample
{
    class Program
    {
        static void Main()
        {
            using (var unitOfWork = new UnitOfWork(Dialect.SqlLite, ConnectionSelect.SqlLiteLocalDb))
            {
                // 【Get】
                var getData = new Member { Id = 1 };
                var member1 = unitOfWork.MemberRepository.Get(1);
                var member2 = unitOfWork.MemberRepository.Get(getData);
                var memberAll = unitOfWork.MemberRepository.GetAll();

                // 【Add】
                // (SqlLite會有問題 不支援SCOPE_IDENTITY，請自行擴充SqlLite的Add)

                // 【Update】
                //var updateData = new Member { Id = 2, Name = "UpdateName", DisplayName = "Update" };
                //unitOfWork.MemberRepository.Update(updateData);

                // 【Remove】
                //unitOfWork.MemberRepository.Remove(3);

                // FindName
                var findnameData = unitOfWork.MemberRepository.FindName("Vincent");
                var findFirstOrDefaultName = unitOfWork.MemberRepository.FindFirstOrDefaultName("Jasmine");

                Console.WriteLine("Get範例(GenericRepository)");
                Console.WriteLine("------------------------------");
                Console.WriteLine($"member1：{member1.FullName}");
                Console.WriteLine($"member2：{member2.FullName}");
                Console.WriteLine($"member：{string.Join(", ", memberAll.Select(m => m.FullName))}");
                Console.WriteLine("------------------------------");

                Console.WriteLine();

                // FindName (MemberRepository)
                Console.WriteLine("FindName範例(MemberRepository)");
                Console.WriteLine("------------------------------");
                Console.WriteLine($"member：{string.Join(", ", findnameData.Select(m => m.FullName))}");
                Console.WriteLine($"member：{findFirstOrDefaultName.FullName}");
                Console.WriteLine("------------------------------");
                unitOfWork.Complete();
            }

            Console.ReadKey();
        }
    }
}
