using System.ComponentModel.DataAnnotations;

namespace Auth.common.Dto
{
    public class RegisterUser
    {
        [Required]
        public string username { get; set; } = null!;
        [Required]
        public string password { get; set; } = null!;
        [Required]
        public string email { get; set; } = null!;
    }
}
