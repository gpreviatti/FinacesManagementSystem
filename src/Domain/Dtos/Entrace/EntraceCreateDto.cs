using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Entrace
{
    public class EntraceCreateDto
    {
        [Required(ErrorMessage = "Descrption is required")]
        [StringLength(60, ErrorMessage = "Name should have {1} caracters")]
        public string Description { get; set; }

        public string Ticker { get; set; }

        [Required(ErrorMessage = "Type is required, you should choose to income or expanse")]
        public int Type { get; set; }

        public string Observation { get; set; }

        [Required(ErrorMessage = "Value is required")]
        public string Value { get; set; }

        [Required(ErrorMessage = "Wallet Id is required")]
        public Guid WalletId { get; set; }

        [Required(ErrorMessage = "Category Id is required")]
        public Guid CategoryId { get; set; }
    }
}
