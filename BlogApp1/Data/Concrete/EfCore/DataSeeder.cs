using BlogApp1.Entity;
using BlogApp1.Helpers;
namespace BlogApp1.Data.Concrete.EfCore
{
    public class DataSeeder
    {
        public static void Seed(BlogContext context)
        {
            // Etiketler yoksa ekle
            if (!context.Tags.Any())
            {
                var tags = new List<Tag>
                {
                    new Tag { Text = "Artificial Intelligence", Url = SlugHelper.ToSlug("Artificial Intelligence") },
                    new Tag { Text = "Machine Learning", Url = SlugHelper.ToSlug("Machine Learning") },
                    new Tag { Text = "NLP (Natural Language Processing)", Url = SlugHelper.ToSlug("NLP (Natural Language Processing)") },
                    new Tag { Text = "Deep Learning", Url = SlugHelper.ToSlug("Deep Learning") },
                    new Tag { Text = "Containers", Url = SlugHelper.ToSlug("Containers") },
                };

                context.Tags.AddRange(tags);
                context.SaveChanges();
            }

            // Kullanıcı yoksa ekle
            if (!context.Users.Any())
            {
                var user = new User
                {
                    UserName = "testuser",
                    Email = "test@example.com",
                    Password = "1234" // Şifre düz metin olarak tutuluyor
                };

                context.Users.Add(user);
                context.SaveChanges();
            }

            // Örnek blog yazıları yoksa ekle
            if (!context.Posts.Any())
            {
                var user = context.Users.First(u => u.UserName == "testuser");
                var tags = context.Tags.ToList();

                var post1 = new Post
                {
                    Title = "AI in Healthcare",
                    Content = "Artificial Intelligence is transforming healthcare rapidly.",
                    CreatedDate = DateTime.Now.AddDays(-5),
                    UserId = user.UserId,
                    Tags = new List<Tag> { tags[0], tags[1] }
                };

                var post2 = new Post
                {
                    Title = "Understanding Containers",
                    Content = "Containers revolutionized application deployment and scaling.",
                    CreatedDate = DateTime.Now.AddDays(-2),
                    UserId = user.UserId,
                    Tags = new List<Tag> { tags[4] }
                };

                context.Posts.AddRange(post1, post2);
                context.SaveChanges();
            }
        }
    }
}
