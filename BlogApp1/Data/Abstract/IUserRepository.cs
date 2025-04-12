using BlogApp1.Entity;

namespace BlogApp1.Data.Abstract{

    public interface IUserRepository{
        IQueryable<User> Users {get;}
        void CreateUser(User User);
    }
}