using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.WalletType
{
    public class WalletTypeCreateDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name should have {1} caracters")]
        public string Name { get; set; }
    }
}
