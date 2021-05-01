using System;
using Domain.Dtos.Entity;

namespace Domain.Dtos.Entrance
{
    public class EntranceUpdateDto : EntityUpdateDto
    {   
        public string Description { get; set; }
        
        public string Ticker { get; set; }
        
        public int Type { get; set; }
        
        public string Observation { get; set; }
        
        public double Value { get; set; }

        public Guid WalletId { get; set; }

        public Guid CategoryId { get; set; }
    }
}
