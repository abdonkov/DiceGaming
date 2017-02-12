using DiceGaming.Data.Configurations;
using DiceGaming.Data.Entities;
using System.Data.Entity;

namespace DiceGaming.Data
{
    public class DiceGamingDb : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Bet> Bets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserEntityConfiguration());
            modelBuilder.Configurations.Add(new LoginEntityConfiguration());
            modelBuilder.Configurations.Add(new BetEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
