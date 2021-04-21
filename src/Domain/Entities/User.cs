using System.Collections.Generic;

namespace Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual IEnumerable<Wallet> Wallets { get; set; }

        public virtual IEnumerable<Category> Categories { get; set; }
    }
}
