using ADB.Blogger.Domain.Models;
using ADB.Blogger.Infrastructure.Identity;
using Carter;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

            app.MapGet("/roles", (HttpContext ctx) =>
            {
                if (ctx.User is null)
                {
                    return Results.Ok<string[]>([]);
                }
                return Results.Ok(ctx.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray());
            })
                .WithOpenApi()
            .RequireAuthorization(); ;
        }

    }
}
