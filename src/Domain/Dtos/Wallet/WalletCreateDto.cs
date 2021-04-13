using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Wallet
{
    public class WalletCreateDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name should have {1} caracters")]
        public string Name { get; set; }
        
        public string Description { get; set; }

        [Required(ErrorMessage = "Current value is required")]
        public double CurrentValue { get; set; }
        
        public DateTime DueDate { get; set; }
        
        public DateTime CloseDate { get; set; }

        [Required(ErrorMessage = "Wallet type id is required")]
        public Guid WalletTypeId { get; set; }
    }
}
