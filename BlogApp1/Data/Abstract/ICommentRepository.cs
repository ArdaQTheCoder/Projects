using BlogApp1.Entity;

namespace BlogApp1.Data.Abstract{

    public interface ICommentRepository{
        IQueryable<Comment> Comments {get;}
        void CreateComment(Comment Comment);
    }
}