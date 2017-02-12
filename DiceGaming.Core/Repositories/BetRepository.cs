using DiceGaming.Core.Exceptions;
using DiceGaming.Core.Models;
using DiceGaming.Data;
using DiceGaming.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DiceGaming.Core.Repositories
{
    public class BetRepository : IBetRepository
    {
        private BetDto CreateBetDTO(Bet bet)
        {
            return new BetDto()
            {
                Id = bet.Id,
                UserId = bet.UserId,
                DiceSumBet = bet.DiceSumBet,
                DiceSumResult = bet.DiceSumResult,
                Stake = bet.Stake,
                Win = bet.Win,
                CreationDate = bet.CreationDate
            };
        }

        public BetDto Create(BetDto bet)
        {
            var newBet = new Bet()
            {
                UserId = bet.UserId,
                DiceSumBet = bet.DiceSumBet,
                DiceSumResult = bet.DiceSumResult,
                Stake = bet.Stake,
                Win = bet.Win,
                CreationDate = bet.CreationDate
            };

            using (var db = new DiceGamingDb())
            {
                db.Bets.Add(newBet);
                db.SaveChanges();
            }

            return CreateBetDTO(newBet);
        }

        public void Delete(int id)
        {
            using (var db = new DiceGamingDb())
            {
                var bet = db.Bets.FirstOrDefault(b => b.Id == id);
                if (bet == null)
                    throw new NotFoundException();

                var user = bet.User;
                user.Money = user.Money - bet.Win + bet.Stake;

                db.Users.AddOrUpdate(user);
                db.Bets.Remove(bet);
                db.SaveChanges();
            }
        }

        public BetDto Get(int id)
        {
            Bet bet;

            using (var db = new DiceGamingDb())
            {
                bet = db.Bets.FirstOrDefault(b => b.Id == id);

                if (bet == null)
                    throw new NotFoundException();
            }

            return CreateBetDTO(bet);
        }

        public IEnumerable<BetDto> GetBets(int userId, int skip, int take, string orderby, string filter)
        {
            Func<Bet, bool> betFilter;
            if (object.Equals(filter, "win"))
                 betFilter = new Func<Bet, bool>(b => b.UserId == userId && b.Win != 0);
            else if (object.Equals(filter, "lose"))
                betFilter = new Func<Bet, bool>(b => b.UserId == userId && b.Win == 0);
            else betFilter = new Func<Bet, bool>(b => b.UserId == userId);

            List<Bet> bets;
            using (var db = new DiceGamingDb())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                    throw new NotFoundException();

                if (object.Equals(orderby, "win"))
                {
                    bets = (from b in db.Bets.Where(betFilter)
                            orderby b.Win
                            select b).Skip(skip).Take(take).ToList();
                }
                else
                {
                    bets = (from b in db.Bets.Where(betFilter)
                            orderby b.CreationDate
                            select b).Skip(skip).Take(take).ToList();
                }
            }

            foreach (var bet in bets)
            {
                yield return CreateBetDTO(bet);
            }
        }
    }
}