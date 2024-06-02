using ADB.Blogger.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ADB.Blogger.Infrastructure.Persistence.Configuration;

public class BlogPostConfiguration : BaseEntityConfiguration<Post>
{
    public override void Configure(EntityTypeBuilder<Post> builder)
    {
        base.Configure(builder);
    }
}
