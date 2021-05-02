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

        [Display(Name = "Current Value")]
        [Required(ErrorMessage = "Current value is required")]
        public double CurrentValue { get; set; }

        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Close Date")]
        public DateTime CloseDate { get; set; }

        [Display(Name = "Wallet Type")]
        [Required(ErrorMessage = "Wallet type id is required")]
        public Guid WalletTypeId { get; set; }

        [Required(ErrorMessage = "User id is required")]
        public Guid UserId { get; set; }
    }
}
