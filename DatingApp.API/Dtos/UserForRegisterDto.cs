using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {   [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = " Username must be between 3 - 15 characters")]
        public string UserName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Password must be between 5 - 15 characters")]
        public string UserPassword { get; set; }
    }
}