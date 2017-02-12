using System;

namespace DiceGaming.Core.Models
{
    public class BetDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DiceSumBet { get; set; }
        public int DiceSumResult { get; set; }
        public decimal Stake { get; set; }
        public decimal Win { get; set; }
        public DateTime CreationDate { get; set; }
    }
}