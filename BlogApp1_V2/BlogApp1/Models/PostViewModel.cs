using BlogApp1.Entity;

namespace BlogApp1.Models
{
    public class PostViewModel
    {
        public string? Title { get; set; }
        public string? Content { get; set; }

        public List<int> SelectedTagIds { get; set; } = new List<int>();
        public List<Tag>? AvailableTags { get; set; }

    }
}
