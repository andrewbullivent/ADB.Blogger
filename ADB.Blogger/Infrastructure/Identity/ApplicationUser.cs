using Microsoft.AspNetCore.Identity;

namespace ADB.Blogger.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? Surname { get; set; }
    public DateTimeOffset? DateJoined { get; set; }
}


public record ApplicationUserViewModel
{
    public ApplicationUserViewModel(ApplicationUser user)
    {
        ArgumentNullException.ThrowIfNull(user);

        Id = user.Id;
        Username = user.UserName;
        FirstName = user.FirstName;
        Surname = user.Surname;
        DateJoined = user.DateJoined;
    }

    public string Id { get; }
    public string? Username { get; }
    public string? FirstName { get; }
    public string? Surname { get; }
    public DateTimeOffset? DateJoined { get; }
}
