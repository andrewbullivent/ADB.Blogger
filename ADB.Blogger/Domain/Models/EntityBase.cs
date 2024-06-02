using ADB.Blogger.Infrastructure.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADB.Blogger.Domain.Models;

public abstract class EntityBase
{
    public bool Deleted { get; set; } = false;
    public ApplicationUser? CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now.UtcDateTime;
    public DateTimeOffset ModifiedAt { get; set; } = DateTimeOffset.Now.UtcDateTime;
}
