using System;

namespace Domain.Dtos.Entity
{
    public class EntityResultDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
