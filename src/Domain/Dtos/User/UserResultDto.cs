using System;
using System.Collections.Generic;
using Domain.Dtos.Category;
using Domain.Dtos.Entity;
using Domain.Dtos.Wallet;

namespace Domain.Dtos.User
{
    public class UserResultDto : EntityResultDto
    {   
        public string Name { get; set; }
        
        public string Email { get; set; }

        public IEnumerable<WalletResultDto> Wallets { get; set; }

        public IEnumerable<CategoryResultDto> Categories { get; set; }
    }
}
