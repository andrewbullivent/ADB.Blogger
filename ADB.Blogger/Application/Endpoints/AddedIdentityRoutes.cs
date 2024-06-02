using ADB.Blogger.Domain.Models;
using ADB.Blogger.Infrastructure.Identity;
using Carter;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ADB.Blogger.Application.Endpoints
{
    public class AddedIdentityRoutes : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/logout", async (HttpContext ctx, SignInManager<ApplicationUser> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return Results.Ok();
            })
            .WithOpenApi()
            .RequireAuthorization();
        }
    }
}
