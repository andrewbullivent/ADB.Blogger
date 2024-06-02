using Microsoft.EntityFrameworkCore;

namespace ADB.Blogger.Infrastructure.Persistence.Configuration
{
    public interface IBaseEntityConfiguration<TBaseEntity> : IEntityTypeConfiguration<TBaseEntity> where TBaseEntity : class
    {
    }
}