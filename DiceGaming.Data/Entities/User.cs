using System.Collections.Generic;

namespace DiceGaming.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public decimal Money { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }

        public User() { }
    }
}
