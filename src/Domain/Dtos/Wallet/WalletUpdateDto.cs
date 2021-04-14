using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Wallet
{
    public class WalletUpdateDto
    {
        [Required(ErrorMessage = "Id is required to update")]
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public double CurrentValue { get; set; }
        
        public DateTime DueDate { get; set; }
        
        public DateTime CloseDate { get; set; }
        
        public Guid WalletTypeId { get; set; }
    }
}
