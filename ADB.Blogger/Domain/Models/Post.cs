using ADB.Blogger.Application.Strategies;
using ADB.Blogger.Infrastructure.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace ADB.Blogger.Domain.Models
{
    public class Post : EntityBase, IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public ICollection<Tag>? Tags { get; set; }
    }

    public class PostViewModel : Post, IActionItems
    {
        public new ApplicationUserViewModel CreatedBy { get; set; }
        public string[] Actions { get; set; } = [];

        public Post ToDto()
        {
            return new Post { Title = Title, Body = Body };
        }

        public static PostViewModel FromDto(Post post)
        {
            return new()
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                Tags = post.Tags,
                CreatedBy = new ApplicationUserViewModel(post.CreatedBy),
                CreatedAt = post.CreatedAt

            };
        }
    }


    public class PostValidator : AbstractValidator<PostViewModel>
    {
        public PostValidator()
        {
            RuleFor(r => r.Title).NotEmpty();
            RuleFor(r => r.Body).NotEmpty();
        }
    }
}
