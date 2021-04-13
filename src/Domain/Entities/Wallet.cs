using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;

namespace Domain
{
    public class Wallet : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double CurrentValue { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CloseDate { get; set; }

        public Guid WalletTypeId { get; set; }
        public WalletType WalletType { get; set; }

        public IEnumerable<Entrace> Entraces { get; set; }
    }
}