using ADB.Blogger.Application.Strategies;
using ADB.Blogger.Domain.Models;
using ADB.Blogger.Domain.Results;
using ADB.Blogger.Infrastructure.Persistence;
using Carter;
using Carter.ModelBinding;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ADB.Blogger.Application.Endpoints;

public class PostRoutes : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        const string postsRoute = "/api/v1/posts";
        // Set up API group for adding new posts

        var posts = app.MapGroup(postsRoute);


        // Map the Get All
        posts.MapGet("/", async (
        ApplicationDbContext dbCtx,
        IActionsStrategyResolver actionStrategyResolver,
            string? searchTerm,
            int index = 0,
            int pageSize = 20) =>
        {
            var actionLinksGenerator = actionStrategyResolver.ResolveStrategy();
            try
            {
                ListContext listContext = new ListContext
                {
                    PageSize = pageSize,
                    Index = index,
                    SearchTerm = searchTerm
                };


                // Check for query term and use if available
                IQueryable<Post> postsQuery = SetupQuery(dbCtx, searchTerm);

                // get the results ordered by Tag name, only return the top 'n' maximum
                IQueryable<PostViewModel> queryResult = SetupDataRequest(index, pageSize, postsQuery);

                List<PostViewModel> resourceData = await queryResult.ToListAsync();

                CreateResourceLinks(resourceData, actionLinksGenerator);

                var callResult = new BloggerResult<List<PostViewModel>>(
                    actionLinksGenerator.GenerateActionsForPostRoute()
                    , resourceData);

                return TypedResults.Ok(callResult);
            }
            catch (Exception ex)
            {
                return Results.StatusCode(500);
            }
        })
            .WithName("GetPosts");

        // Map the "Get One"
        posts.MapGet("/{id}", async (
            Guid id,
            ApplicationDbContext dbCtx,
            IActionsStrategyResolver actionsStrategyResolver) =>
        {
            var actionLinksGenerator = actionsStrategyResolver.ResolveStrategy();
            try
            {
                var result = await dbCtx.Posts.FindAsync(id);

                if (result == null)
                {
                    return Results.NotFound();
                }
                await dbCtx.Entry(result)
                .Reference(p => p.CreatedBy)
                .LoadAsync();

                return Results.Ok(new BloggerResult<PostViewModel>(
                    actionLinksGenerator.GenerateActionsForPostResource()
                    , PostViewModel.FromDto(result))
                    );
            }
            catch (Exception ex)
            {
                return Results.StatusCode(500);
            }
        })
            .WithName("GetSinglePost");

        // Map the new post
        posts.MapPost("/", async (
            HttpContext ctx, [FromBody]
            PostViewModel newPost,
            ApplicationDbContext dbCtx) =>
        {
            try
            {
                if (newPost == null)
                {
                    return Results.BadRequest();
                }

                var result = ctx.Request.Validate(newPost);
                if (!result.IsValid)
                {
                    return Results.UnprocessableEntity(result.GetFormattedErrors());
                }

                var postDto = newPost.ToDto();
                dbCtx.Add(postDto);
                int postsWritten = await dbCtx.SaveChangesAsync();
                return TypedResults.Ok(postDto);

            }
            catch (Exception ex)
            {
                return Results.UnprocessableEntity(ex.Message);
            }
        })
            .RequireAuthorization("create")
            .WithName("CreatePost");

        // Map the update post
        posts.MapPut("/{id}", async (Guid Id, HttpContext ctx, PostViewModel updatedPost, ApplicationDbContext dbCtx) =>
        {
            try
            {
                var post = await dbCtx.Posts.FindAsync(Id);
                if (post == null)
                {
                    return Results.NotFound();
                }

                var result = ctx.Request.Validate(updatedPost);
                if (!result.IsValid)
                {
                    return Results.UnprocessableEntity(result.GetFormattedErrors());
                }
                post.Title = updatedPost.Title;
                post.Body = updatedPost.Body;
                await dbCtx.SaveChangesAsync();

                await dbCtx.Entry(post)
                    .Reference(p => p.CreatedBy)
                    .LoadAsync();
                return TypedResults.Ok(post);

            }
            catch (Exception ex)
            {
                return Results.UnprocessableEntity(ex.Message);
            }
        })
            .RequireAuthorization("update")
            .WithName("UpdatePost");

        // Map the delete post
        posts.MapDelete("/{Id}", async (Guid Id, ApplicationDbContext dbCtx) =>
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

        })
            .RequireAuthorization("delete")
            .WithName("DeletePost");
    }

    private static IQueryable<PostViewModel> SetupDataRequest(int index, int pageSize, IQueryable<Post> postsQuery)
    {
        return postsQuery
            .OrderByDescending(p => p.CreatedAt)
            .ThenBy(p => p.Title)
            .Skip(index * pageSize)
            .Take(pageSize)
            .Select(p => PostViewModel.FromDto(p));
    }

    private static void CreateResourceLinks(List<PostViewModel> resourceData, IActionStrategy actionLinksGenerator)
    {
        foreach (var post in resourceData)
        {
            post.Actions = actionLinksGenerator.GenerateActionsForPostResource();
        }
    }

    private static IQueryable<Post> SetupQuery(ApplicationDbContext dbCtx, string? searchTerm)
    {
        IQueryable<Post> postsQuery = dbCtx.Posts.Include(p => p.CreatedBy);
        if (!string.IsNullOrEmpty(searchTerm))
        {
            postsQuery = postsQuery.Where(p =>
                p.Title.Contains(searchTerm) || (p.Tags != null && p.Tags.Select(t => t.Name).Contains(searchTerm))
            );
        }

        return postsQuery;
    }
}
