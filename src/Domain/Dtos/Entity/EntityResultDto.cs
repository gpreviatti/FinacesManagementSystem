using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Entity
{
    public class EntityResultDto
    { 
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedAt { get; set; }
    }
}
