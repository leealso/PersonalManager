using PersonalManager.Domain.Common;

namespace PersonalManager.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public Category Parent { get; set; }

        public string IconUrl { get; set; }
    }
}
