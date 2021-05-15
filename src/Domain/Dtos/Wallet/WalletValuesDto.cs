using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Wallet
{
    public class WalletValuesDto
    {
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public double TotalIncomes { get; set; }

        [DataType(DataType.Currency)]
        public double TotalExpanses { get; set; }
    }
}
