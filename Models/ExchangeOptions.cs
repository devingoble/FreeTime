using System.ComponentModel.DataAnnotations;

namespace FreeTime.Models
{
    public class ExchangeOptions
    {
        [Required]
        public string? Protocol { get; set; }
        [Required]
        public string? Server { get; set; }
        [Required]
        public string? APIPath { get; set; }
        [Required]
        public string? Resource { get; set; }
        [Required]
        public string? CalendarSuffix { get; set; }
    }
}
