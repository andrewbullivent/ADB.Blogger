using ADB.Blogger.Domain.Models;
using ADB.Blogger.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ADB.Blogger.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly IHttpContextAccessor _contextAccessor;
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpCtx) : base(options)
    {
        _contextAccessor = httpCtx;
        //Database.EnsureCreated();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Access current user's identity
        var currentUser = _contextAccessor.HttpContext?.User;

        // Set current user's record on new records
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added && entry.Entity is EntityBase createdByUser)
            {
                // Fetch the user record from the database
                var user = Users.SingleOrDefault(u => u.UserName == currentUser.Identity.Name);
                createdByUser.CreatedBy = user as ApplicationUser;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
