using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Category
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name should have {1} caracters")]
        public string Name { get; set; }

        public Guid CategoryId { get; set; }
    }
}
