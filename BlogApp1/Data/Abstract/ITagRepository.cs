using BlogApp1.Entity;

namespace BlogApp1.Data.Abstract{

    public interface ITagRepository{
        IQueryable<Tag> Tags {get;}
        void CreateTag(Tag Tag);
    }
}