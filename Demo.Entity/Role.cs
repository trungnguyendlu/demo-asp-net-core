using System.Collections.Generic;

namespace Demo.Entity
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
