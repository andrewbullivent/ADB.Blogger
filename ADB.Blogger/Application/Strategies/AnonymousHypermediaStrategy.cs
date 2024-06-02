using ADB.Blogger.Domain.Models;

namespace ADB.Blogger.Application.Strategies
{
    //public class AnonymousHypermediaStrategy : IHypermediaStrategy
    //{
    //    public List<Link> GenerateLinksForRoute(string route, string resource = "")
    //    {
    //        string baseRoute = route;

    //        // remove the resource to get the baseroute for the request, e.g. /post/1234 becomes /post
    //        if (!string.IsNullOrEmpty(resource))
    //        {
    //            baseRoute = baseRoute.Replace(resource, string.Empty);
    //        }
    //        string relString = baseRoute.TrimEnd('/').Split("/").Last();
    //        // Logic to generate links for an anonymous user
    //        return
    //        [
    //            new Link(route, relString, "GET")];
    //    }
    //}
}
