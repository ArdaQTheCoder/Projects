using BlogApp1.Data.Abstract;
using BlogApp1.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BlogApp1.Data.Concrete.EfCore;

namespace BlogApp1.Controllers
{
   public class HomeController : Controller
{
    private readonly IPostRepository _postRepository;
    private readonly BlogContext _context;

    public HomeController(IPostRepository postRepository, BlogContext context)
    {
        _postRepository = postRepository;
        _context = context;
    }

    // Misafir olarak devam etme sayfası
    public IActionResult Guest()
    {
        var posts = _postRepository.Posts //tüm postları çekiyoruz
            .Include(p => p.User)
            .Include(p => p.Tags) // Etiketleri de dahil ediyoruz
            .OrderByDescending(p => p.CreatedDate)
            .ToList();

        return View(posts); // Misafire gösterilecek sayfaya gönderiyoruz
    }

    
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult RegisteredUser()
{
    var posts = _context.Posts
        .Include(p => p.User)
        .Include(p => p.Tags)
        .OrderByDescending(p => p.CreatedDate)
        .ToList();

    ViewBag.Tags = _context.Tags.ToList();

    return View(posts);
}

}

}
