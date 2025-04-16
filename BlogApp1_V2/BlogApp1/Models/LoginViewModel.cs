using System.ComponentModel.DataAnnotations;

namespace BlogApp1.Models{
    public class LoginViewModel
{
    public string? Email{get;set;}
    public required string UserName { get; set; }
    public required string Password { get; set; }
}

}