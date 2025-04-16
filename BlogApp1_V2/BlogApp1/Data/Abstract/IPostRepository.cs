using BlogApp1.Entity;

namespace BlogApp1.Data.Abstract{

    public interface IPostRepository{
        IQueryable<Post> Posts {get;}
        void CreatePost(Post post);
    }
}