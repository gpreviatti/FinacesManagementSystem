using System;

namespace Domain.Entities
{
    public class Entrace : Entity
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