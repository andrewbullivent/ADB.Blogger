using ADB.Blogger.Domain.Models;

namespace ADB.Blogger.Application.Strategies
{
    public class UserActionsStrategy : IActionStrategy
    {

        public string[] GenerateActionsForPostResource()
        {
            return [AppAction.READ];
        }

        public string[] GenerateActionsForPostRoute()
        {
            return [AppAction.READ];
        }
    }
}
