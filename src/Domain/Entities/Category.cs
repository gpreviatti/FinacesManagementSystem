using System.Collections.Generic;

namespace Domain.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }

        public virtual User User { get; set; }

        public virtual IEnumerable<Entrace> Entraces { get; set; }
    }
}
