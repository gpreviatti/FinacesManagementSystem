using System;
using Domain.Dtos.Entity;

namespace Domain.Dtos.Category
{
    public class CategoryUpdateDto : EntityUpdateDto
    {
        public string Name { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
