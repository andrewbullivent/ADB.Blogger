using ADB.Blogger.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ADB.Blogger.Infrastructure.Persistence.Configuration
{
    public abstract class BaseEntityConfiguration<TBaseEntity> : IBaseEntityConfiguration<TBaseEntity> where TBaseEntity : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<TBaseEntity> builder)
        {
            builder.HasQueryFilter(c => !c.Deleted);
            builder
                .HasIndex(p => p.CreatedAt)
                .IsClustered();

        }
    }
}
