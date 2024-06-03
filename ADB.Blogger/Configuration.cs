﻿using ADB.Blogger.Application.Strategies;
using ADB.Blogger.Infrastructure.Identity;
using ADB.Blogger.Infrastructure.Persistence;
using Carter;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace ADB.Blogger
{
    public static class Configuration
    {
        public static async void RegisterServices(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "open", policy =>
                {
                    policy
                        .WithOrigins("https://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();

                });
            });

            builder.Services.AddAuthorizationBuilder()
                .AddPolicy("create", policy => policy.RequireRole("Admin"))
                .AddPolicy("update", policy => policy.RequireRole("Admin"))
                .AddPolicy("delete", policy => policy.RequireRole("Admin"));


            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("adbBlogger")));
            builder.Services
                .AddIdentityApiEndpoints<ApplicationUser>()
                   .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddCarter();

            builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

            builder.Services.AddScoped<IActionStrategy, AdminActionsStrategy>();
            builder.Services.AddScoped<IActionStrategy, UserActionsStrategy>();
            builder.Services.AddSingleton<IActionsStrategyResolver, ActionsStrategyResolver>();


            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();



        }

        private static async Task SeedRolesAndAccount(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            IServiceProvider servicesProvider = scope.ServiceProvider;

            var roleManager = servicesProvider.GetService<RoleManager<IdentityRole>>();
            var userManager = servicesProvider.GetService<UserManager<ApplicationUser>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            var admin = await userManager.FindByEmailAsync("andy.bullivent@gmail.com");
            if (admin == null)
            {
                await userManager.CreateAsync(new ApplicationUser()
                {
                    Email = "andy.bullivent@gmail.com",
                    FirstName = "Andy",
                    Surname = "Bullivent",
                    UserName = "andy.bullivent@gmail.com"
                });

                admin = await userManager.FindByEmailAsync("andy.bullivent@gmail.com");
            }

            if (!await userManager.IsInRoleAsync(admin, "Admin"))
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }

        public static void RegisterMiddlewares(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("open");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapIdentityApi<ApplicationUser>();
            app.MapCarter();

            app.UseCookiePolicy(new CookiePolicyOptions()
            {
                HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.SameAsRequest,
                MinimumSameSitePolicy = SameSiteMode.None
            });

            app.UseHttpsRedirection();

            app.SeedRolesAndAccount().Wait();
        }
    }
}