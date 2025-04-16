using BlogApp1.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp1.Controllers
{
    public class TagsController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IPostRepository _postRepository;

        public TagsController(ITagRepository tagRepository, IPostRepository postRepository)
        {
            _tagRepository = tagRepository;
            _postRepository = postRepository;
        }

        public IActionResult Details(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return NotFound();
            }

            var tag = _tagRepository.Tags.FirstOrDefault(t => t.Url == url);

            if (tag == null)
            {
                return NotFound();
            }

            var posts = _postRepository.Posts
                .Include(p => p.User)
                .Include(p => p.Tags)
                .Where(p => p.Tags.Any(t => t.Url == url))
                .OrderByDescending(p => p.CreatedDate)
                .ToList();

            ViewBag.Tag = tag; // Detay sayfasında tag başlığı vs. göstermek için
            return View(posts);
        }
    }
}
