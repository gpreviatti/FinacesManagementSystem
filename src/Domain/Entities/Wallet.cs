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

        public virtual User User { get; set; }

        public virtual WalletType WalletType { get; set; }

        public virtual IEnumerable<Entrace> Entraces { get; set; }
    }
}
