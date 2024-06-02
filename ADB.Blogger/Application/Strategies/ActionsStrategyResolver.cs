namespace ADB.Blogger.Application.Strategies
{
    public class ActionsStrategyResolver : IActionsStrategyResolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ActionsStrategyResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionStrategy ResolveStrategy()
        {
            if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User.IsInRole("Admin"))
            {
                return new AdminActionsStrategy();
            }

            return new UserActionsStrategy();
        }
    }
}
