using System;
using System.ComponentModel.DataAnnotations;
using Domain.Dtos.Entity;

namespace Domain.Dtos.Entrace
{
    public class EntraceResultDto : EntityResultDto
    {
        public string Description { get; set; }

        public string Ticker { get; set; }

        public int Type { get; set; }

        public string Observation { get; set; }

        [DataType(DataType.Currency)]
        public double Value { get; set; }

        public Guid WalletId { get; set; }

        public Guid CategoryId { get; set; }
    }
}
