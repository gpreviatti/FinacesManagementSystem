using Domain.Entities;
using System;

namespace Domain
{
    public class Entrace : Entity
    {
        public string Description { get; set; }
        public string Ticker { get; set; }
        public string Type { get; set; }
        public string Observation { get; set; }
        public string Value { get; set; }

        public Guid WalletId { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}