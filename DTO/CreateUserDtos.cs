using System.ComponentModel.DataAnnotations;

namespace Lab_Results.DTO
{
    public class CreateUserDtos
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } =string.Empty;
    }
}
