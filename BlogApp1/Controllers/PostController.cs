using BlogApp1.Data.Abstract;
using BlogApp1.Entity;
using BlogApp1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogApp1.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public PostsController(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var posts = _postRepository.Posts
                .Include(p => p.User)
                .OrderByDescending(p => p.CreatedDate)
                .ToList();

            return View(posts);
        }

        [HttpPost]
public IActionResult Create(string title, string content)
{
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (userId == null)
    {
        return RedirectToAction("Login", "Account");
    }

    var newPost = new Post
    {
        Title = title,
        Content = content,
        CreatedDate = DateTime.Now,
        UserId = int.Parse(userId)
    };

    _postRepository.CreatePost(newPost);

    return RedirectToAction("Index","Posts");
}

    }
}
