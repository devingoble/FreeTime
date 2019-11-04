using System.ComponentModel.DataAnnotations;

namespace FreeTime.Models
{
    public class CredentialsOptions
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Domain { get; set; }
    }
}
