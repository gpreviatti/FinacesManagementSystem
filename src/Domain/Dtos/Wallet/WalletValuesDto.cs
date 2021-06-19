using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Wallet
{
    public class WalletValuesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Current Value")]
        [DataType(DataType.Currency)]
        public double CurrentValue { get; set; }

        [Display(Name = "Total Income")]
        [DataType(DataType.Currency)]
        public double TotalIncomes { get; set; }

        [Display(Name = "Total Expanse")]
        [DataType(DataType.Currency)]
        public double TotalExpanses { get; set; }
    }
}
