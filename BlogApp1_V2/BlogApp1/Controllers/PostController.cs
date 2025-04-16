using BlogApp1.Data.Abstract;
using BlogApp1.Entity;
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
        private readonly ITagRepository _tagRepository;
        private readonly ICommentRepository _commentRepository;

        public PostsController(IPostRepository postRepository, IUserRepository userRepository, ITagRepository tagRepository,ICommentRepository commentRepository)
        {
            _tagRepository = tagRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _commentRepository=commentRepository;
        }
        

        [HttpPost]
        public IActionResult Create(string title, string content, List<int> selectedTagIds)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var selectedTags = _tagRepository.Tags
                .Where(t => selectedTagIds.Contains(t.TagId))
                .ToList();

            var newPost = new Post
            {
                Title = title,
                Content = content,
                CreatedDate = DateTime.Now,
                UserId = int.Parse(userId),
                Tags = selectedTags
            };

            _postRepository.CreatePost(newPost);

            return RedirectToAction("RegisteredUser", "Home");
        }

        public IActionResult Details(int id)
        {
            var post = _postRepository.Posts
            .Include(p => p.User)
            .Include(p => p.Tags)
            .Include(p => p.Comments)
                .ThenInclude(c => c.User)
            .FirstOrDefault(p => p.PostId == id);

                if (post == null)
                {
                    return NotFound();
                }

                return View(post);
        }

        


[HttpPost]
public IActionResult AddComment(int postId, string text)
{
    // Giriş yapmış kullanıcıyı kontrol et
    if (User.Identity.IsAuthenticated)
    {
        // User'dan Claim almak
        var userClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (userClaim != null && !string.IsNullOrEmpty(userClaim.Value))
        {
            var userId = int.Parse(userClaim.Value);

            var comment = new Comment
            {
                PostId = postId,
                Text = text,
                CreatedDate = DateTime.Now,
                UserId = userId
            };

            _commentRepository.CreateComment(comment);

            return RedirectToAction("Details", "Post", new { id = postId });
        }

        return RedirectToAction("Details", "Post", new { id = postId });
    }

    return RedirectToAction("Login", "Account");
}

}
}
