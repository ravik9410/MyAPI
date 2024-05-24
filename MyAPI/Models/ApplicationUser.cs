using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class ApplicationUser
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? Address { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
