using System.ComponentModel.DataAnnotations;
using Domain.Dtos.Entity;

namespace Domain.Dtos.WalletType
{
    public class WalletTypeUpdateDto : EntityUpdateDto
    {
        [Required(ErrorMessage = "Name is required to update")]
        public string Name { get; set; }
    }
}
