using System.ComponentModel.DataAnnotations;

namespace DiceGaming.WebApi.Requests
{
    public class DeleteAccountRequest
    {
        [Required]
        [MinLength(6)]
        [MaxLength(32)]
        public string Password { get; set; }
    }
}