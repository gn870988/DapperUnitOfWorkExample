using Dapper;

namespace Domain.Entities
{
    public class Member : EntityIdentify
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }

        [Editable(false)]
        public string FullName => $"{Name}({DisplayName})";
    }
}
