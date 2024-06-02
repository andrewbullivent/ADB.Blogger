using ADB.Blogger.Domain.Models;

namespace ADB.Blogger.Application.Strategies
{
    public class AdminActionsStrategy : IActionStrategy
    {
        public string[] GenerateActionsForPostRoute()
        {
            return [AppAction.READ, AppAction.WRITE];
        }

        public string[] GenerateActionsForPostResource()
        {
            return [AppAction.READ, AppAction.WRITE, AppAction.DELETE];

        }

    }
}
