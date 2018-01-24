using System.ComponentModel.DataAnnotations;

namespace Kontrolmatik.Models
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Kullanıcı adı boş bırakılamaz")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Şifre boş bırakılamaz")]
        public string Password { get; set; }
    }
}