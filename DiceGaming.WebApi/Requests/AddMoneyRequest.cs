using System.ComponentModel.DataAnnotations;

namespace DiceGaming.WebApi.Requests
{
    public class AddMoneyRequest
    {
        [Required]
        [Range(0.01, double.PositiveInfinity)]
        public decimal AddMoney { get; set; }
    }
}