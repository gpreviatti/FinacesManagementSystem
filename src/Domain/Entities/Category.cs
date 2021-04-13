using System;

namespace Domain.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid CategoryId { get; set; }
    }
}