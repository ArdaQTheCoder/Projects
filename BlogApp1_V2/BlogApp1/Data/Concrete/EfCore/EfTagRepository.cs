using BlogApp1.Data.Abstract;
using BlogApp1.Data.Concrete.EfCore;
using BlogApp1.Entity;

namespace BlogApp1.Data.Concrete{
    public class EfTagRepository : ITagRepository
    {
        private BlogContext _context;
        public EfTagRepository(BlogContext context){
            _context = context;
        }
        public IQueryable<Tag> Tags => _context.Tags;

        public void CreateTag(Tag Tag)
        {
            _context.Tags.Add(Tag);
            _context.SaveChanges();
        }
    }
}