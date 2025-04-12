using Microsoft.AspNetCore.Identity;

namespace BlogApp1.Entity
{
    public class User 
    {
        public int UserId{get;set;}
        public string? UserName {get;set;}
        public string? Email {get;set;}
        public string? Name { get; set; } 
        public string? Image { get; set; } 
        public required string Password {set;get;}
        // Kullanıcıya ait blog yazıları ve yorumlar
        public List<Post> Posts { get; set; } = new List<Post>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
