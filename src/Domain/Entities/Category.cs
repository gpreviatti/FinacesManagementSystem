using Domain.Entities;
using System;

namespace Domain
{
    public class Category : Entity
    {
        public string Name { get; set; }

        public Guid UserId { get; set; }
        public Category CategoryId { get; set; }
    }
}