using BlogApp1.Data.Abstract;
using BlogApp1.Data.Concrete.EfCore;
using BlogApp1.Entity;

namespace BlogApp1.Data.Concrete{

    public class EfCommentRepository : ICommentRepository
    {
        private BlogContext _context;
        public EfCommentRepository(BlogContext context){
            _context = context;
        }
        public IQueryable<Comment> Comments => _context.Comments;

        public void CreateComment(Comment Comment)
        {
            _context.Comments.Add(Comment);
            _context.SaveChanges();
        }
    }
}