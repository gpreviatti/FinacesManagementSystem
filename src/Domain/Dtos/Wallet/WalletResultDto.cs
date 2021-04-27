using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Dtos.Entity;
using Domain.Dtos.Entrace;
using Domain.Dtos.WalletType;

namespace Domain.Dtos.Wallet
{
    public class WalletResultDto : EntityResultDto
    {   
        public string Name { get; set; }
        
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public double CurrentValue { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CloseDate { get; set; }

        public WalletTypeResultDto WalletType { get; set; }

        public IEnumerable<EntraceResultDto> Entraces { get; set; }
    }
}
