using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Entity
{
    public class EntityUpdateDto
    {
        [Required(ErrorMessage = "Id is required to update")]
        public Guid Id { get; set; }
    }
}
