using System.ComponentModel.DataAnnotations;

namespace Chat_Application_ServerAPI.Data.Models.DTO.User
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
