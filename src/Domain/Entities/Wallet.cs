using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Wallet : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double CurrentValue { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CloseDate { get; set; }

        public User User { get; set; }

        public WalletType WalletType { get; set; }

        public IEnumerable<Entrace> Entraces { get; set; }
    }
}
