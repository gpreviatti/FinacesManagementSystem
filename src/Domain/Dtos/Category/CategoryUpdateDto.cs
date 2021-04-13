using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Category
{
    public class CategoryUpdateDto
    {
        [Required(ErrorMessage = "Id is required to update")]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid UserId { get; set; }

        public Guid CategoryId { get; set; }
    }
}
