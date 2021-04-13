using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.WalletType
{
    public class WalletTypeUpdateDto
    {
        [Required(ErrorMessage = "Id is required to update")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required to update")]
        public string Name { get; set; }
    }
}
