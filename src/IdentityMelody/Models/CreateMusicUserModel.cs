using System.ComponentModel.DataAnnotations;

namespace IdentityMelody.Models
{
    public class CreateMusicUserModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}