using Bogus;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {

        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.PostId);
            builder.Property(p => p.Title).IsRequired();
            builder.Property(p => p.Content).IsRequired();

            /*     var faker = new Faker<Post>("tr")
            .StrictMode(false)
            .RuleFor(p => p.PostId, f => f.IndexVariable++)
            .RuleFor(p => p.Title, f => f.Lorem.Sentence())
            .RuleFor(p => p.Content, f => f.Lorem.Paragraphs(40))
            .RuleFor(p => p.PublishDate, f => f.Date.Past())
            .RuleFor(p => p.Author, f => f.Person.FullName)
            .RuleFor(p => p.ImageURL, f => $"https://picsum.photos/650/200?random={f.IndexVariable}")
            .RuleFor(p => p.CategoryId, f => f.Random.Number(1, 7));

            */
            //  var posts = faker.Generate(20);
            var posts = GeneratePosts(100);
            builder.HasData(posts);

        }
        private List<Post> GeneratePosts(int count)
        {

            var posts = new List<Post>();
            var faker = new Bogus.Faker("en");
            for (int i = 1; i <= count; i++)
            {
                var post = new Post
                {
                    PostId = i,
                    Title = faker.Lorem.Sentence(),
                    Content = faker.Lorem.Paragraphs(30),
                    PublishDate = faker.Date.Past(),
                    AuthorId = $"TestUserId{faker.Random.Number(1, 40)}",
                    ImageURL = $"https://picsum.photos/650/200?random={i}",
                    CategoryId = faker.Random.Number(1, 7)
                };


                posts.Add(post);
            }

            return posts;
        }
    }
}
