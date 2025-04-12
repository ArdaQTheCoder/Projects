using System.ComponentModel.DataAnnotations;

namespace BlogApp1.Models{
    public class RegisterViewModel{
        [Required(ErrorMessage = "Kullanıcı adı gereklidir")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "E-posta adresi gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin")]
        public required string Email { get; set; }

         [Required(ErrorMessage = "Şifre gereklidir")]
         public required string Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrar gereklidir")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        public required string ConfirmPassword { get; set; }
    }
}