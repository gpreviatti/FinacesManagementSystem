using System;
using Domain.Dtos.Category;
using Domain.Dtos.Wallet;

namespace Domain.Dtos.Entrace
{
    public class EntraceResultDto
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Ticker { get; set; }

        public int Type { get; set; }

        public string Observation { get; set; }

        public string Value { get; set; }

        public WalletResultDto Wallet { get; set; }

        public CategoryResultDto Category { get; set; }
    }
}
