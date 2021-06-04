using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Dtos.Entity;
using Domain.Dtos.Entrance;
using Domain.Dtos.WalletType;

namespace Domain.Dtos.Wallet
{
    public class WalletResultDto : EntityResultDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Current Value")]
        [DataType(DataType.Currency)]
        public double CurrentValue { get; set; }

        [Display(Name = "Due Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }

        [Display(Name = "Close Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CloseDate { get; set; }

        [Display(Name = "Wallet Type")]
        public WalletTypeResultDto WalletType { get; set; }

        public IEnumerable<EntranceResultDto> Entrances { get; set; }
    }
}
