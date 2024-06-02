using FluentValidation;

namespace ADB.Blogger.Domain.Models
{
    public class Tag : EntityBase, IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Post>? Posts { get; set; }
    }

    public class TagViewModel
    {
        public string Name { get; set; } = null!;
        public ICollection<Guid>? Posts { get; set; }

        internal Tag ToDto()
        {
            return new()
            {
                Name = Name
            };
        }
    }


    public class TagValidator : AbstractValidator<TagViewModel>
    {
        public TagValidator()
        {
            RuleFor(r => r.Name).NotEmpty();
        }
    }
}