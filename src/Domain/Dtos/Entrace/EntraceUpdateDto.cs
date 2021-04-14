using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Entrace
{
    public class EntraceUpdateDto
    {
        [Required(ErrorMessage = "Id is required to update")]
        public Guid Id { get; set; }
        
        public string Description { get; set; }
        
        public string Ticker { get; set; }
        
        public int Type { get; set; }
        
        public string Observation { get; set; }
        
        public string Value { get; set; }

        public Guid WalletId { get; set; }

        public Guid CategoryId { get; set; }
    }
}
