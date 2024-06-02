using ADB.Blogger.Domain.Models;
using ADB.Blogger.Domain.Results;

namespace ADB.Blogger.Application.Strategies
{
    public interface IActionStrategy
    {
        string[] GenerateActionsForPostRoute();
        string[] GenerateActionsForPostResource();
    }
}
