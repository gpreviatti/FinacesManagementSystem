using System;
using Domain.Dtos.Category;
using Domain.Dtos.Entity;
using Domain.Dtos.Wallet;

namespace Domain.Dtos.Entrace
{
    public class EntraceResultDto : EntityResultDto
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
