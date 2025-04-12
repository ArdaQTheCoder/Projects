using BlogApp1.Data.Abstract;
using BlogApp1.Data.Concrete.EfCore;
using BlogApp1.Entity;

namespace BlogApp1.Data.Concrete{

    public class EfUserRepository : IUserRepository
    {
        private BlogContext _context;
        public EfUserRepository(BlogContext context){
            _context = context;
        }
        public IQueryable<User> Users => _context.Users;

        public void CreateUser(User User)
        {
            _context.Users.Add(User);
            _context.SaveChanges();
        }
    }
}