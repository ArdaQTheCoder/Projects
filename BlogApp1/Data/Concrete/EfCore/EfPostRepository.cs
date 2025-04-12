using BlogApp1.Data.Abstract;
using BlogApp1.Data.Concrete.EfCore;
using BlogApp1.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp1.Data.Concrete{

    public class EfPostRepository : IPostRepository
    {
        private BlogContext _context;
        public EfPostRepository(BlogContext context){
            _context = context;
        }
        public IQueryable<Post> Posts => _context.Posts.Include(p=>p.User);

        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }
    }
}