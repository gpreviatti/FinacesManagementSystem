using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }

        public IEnumerable<Entrace> Entraces { get; set; }

        public Guid UserId { get; set; }
    }
}