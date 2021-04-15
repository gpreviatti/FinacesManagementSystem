using System;
using System.Collections.Generic;
using Domain.Dtos.Entrace;
using Domain.Dtos.User;

namespace Domain.Dtos.Category
{
    public class CategoryResultDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public IEnumerable<EntraceResultDto> Entraces { get; set; }
    }
}
