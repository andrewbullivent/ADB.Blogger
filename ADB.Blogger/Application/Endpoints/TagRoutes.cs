using ADB.Blogger.Domain.Models;
using ADB.Blogger.Infrastructure.Persistence;
using Carter;
using Carter.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace ADB.Blogger.Application.Endpoints;

public class TagRoutes : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Set up API group for adding new posts
        var tags = app.MapGroup("/api/v1/tags")
            .RequireAuthorization();

        // Map the Get All
        tags.MapGet("/", async (
            ApplicationDbContext dbCtx,
            string? searchTerm,
            int top = 20) =>
        {
            try
            {
                // Check for query term and use if available
                IQueryable<Tag> tagsQuery = dbCtx.Tags;
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    tagsQuery = tagsQuery.Where(t => t.Name.Contains(searchTerm));
                }

                // get the results ordered by Tag name, only return the top 'n' maximum
                var result = tagsQuery.OrderBy(t => t.Name).Take(top);
                return TypedResults.Ok(await result.ToListAsync());
            }
            catch (Exception ex)
            {
                return Results.StatusCode(500);
            }
        });

        // Map the "Get One"
        tags.MapGet("/{id}", async (Guid id, ApplicationDbContext dbCtx) =>
        {
            try
            {
                var result = await dbCtx.Tags.FindAsync(id);
                return result == null
                    ? Results.NotFound() : Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.StatusCode(500);
            }
        });

        // Map the new post
        tags.MapPost("/", async (HttpContext ctx, TagViewModel newTag, ApplicationDbContext dbCtx) =>
        {
            try
            {
                if (newTag == null)
                {
                    return Results.BadRequest();
                }

                var result = ctx.Request.Validate(newTag);
                if (!result.IsValid)
                {
                    return Results.UnprocessableEntity(result.GetFormattedErrors());
                }

                var tagDto = newTag.ToDto();
                await dbCtx.Tags.AddAsync(tagDto);
                await dbCtx.SaveChangesAsync();
                return TypedResults.Ok(tagDto);

            }
            catch (Exception ex)
            {
                return Results.UnprocessableEntity(ex.Message);
            }
        });


        // Map the delete post
        tags.MapDelete("/", async (Guid Id, ApplicationDbContext dbCtx) =>
        {
            var post = await dbCtx.Posts.FindAsync(Id);
            if (post == null)
            {
                return Results.NotFound();
            }

            if (dbCtx.Entry(post).State == EntityState.Detached)
            {
                dbCtx.Attach(post);
            }
            dbCtx.Remove(post);
            await dbCtx.SaveChangesAsync();

            return Results.Ok();

        });
    }
}
