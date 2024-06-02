using ADB.Blogger.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ADB.Blogger.Infrastructure.Persistence.Configuration
{
    public class BlogPostTagConfiguration : BaseEntityConfiguration<BlogPostTag>
    {
        public override void Configure(EntityTypeBuilder<BlogPostTag> builder)
        {

            base.Configure(builder);
        }
    }
}
