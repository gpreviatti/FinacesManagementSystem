using System;
using Domain.Dtos.User;

namespace Domain.Dtos.Category
{
    public class CategoryResultDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public UserResultDto User { get; set; }

        public Guid CategoryId { get; set; }
    }
}
