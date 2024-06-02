namespace ADB.Blogger.Application.Strategies
{
    public interface IActionsStrategyResolver
    {
        IActionStrategy ResolveStrategy();
    }
}
