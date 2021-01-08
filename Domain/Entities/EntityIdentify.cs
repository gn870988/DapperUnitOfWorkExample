using Dapper;

namespace Domain.Entities
{
    public abstract class EntityIdentify
    {
        [Key]
        public int Id { get; set; }
    }
}
