using ADB.Blogger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ADB.Blogger.Infrastructure.Persistence.Configuration;

public class TagConfiguration : BaseEntityConfiguration<Tag>
{
    public override void Configure(EntityTypeBuilder<Tag> builder)
    {
        base.Configure(builder);
    }
}
