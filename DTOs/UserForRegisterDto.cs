using System.ComponentModel.DataAnnotations;

namespace RednitDating.Api.DTOs
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage="Specify the password between 6 and 20 characters")]
        public string Password { get; set; }

        
    }
}