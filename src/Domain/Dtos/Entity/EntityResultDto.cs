using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Entity
{
    public class EntityResultDto
    {
        public Guid Id { get; set; }

        [Display(Name = "Created At")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Updated At")]
        [DataType(DataType.Date)]
        public DateTime UpdatedAt { get; set; }
    }
}
