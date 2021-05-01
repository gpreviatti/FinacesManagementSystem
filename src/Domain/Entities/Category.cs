using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }

        public Guid? UserId { get; set; }
        public User User { get; set; }

        public Guid? CategoryId { get; set; }
        public Category CustomCategory { get; set; }

        public IEnumerable<Entrance> Entrances { get; set; }
    }
}
