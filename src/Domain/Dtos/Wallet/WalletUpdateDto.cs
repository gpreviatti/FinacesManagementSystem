using System;
using System.ComponentModel.DataAnnotations;
using Domain.Dtos.Entity;

namespace Domain.Dtos.Wallet
{
    public class WalletUpdateDto : EntityUpdateDto
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        [Display(Name = "Current Value")]
        public double CurrentValue { get; set; }

        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Close Date")]
        public DateTime CloseDate { get; set; }

        [Display(Name = "Wallet Type")]
        public Guid WalletTypeId { get; set; }
    }
}
