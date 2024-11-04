using System.Reflection;
using Cloud.Auth;
using Cloud.DAL.Database;
using Cloud.DAL.Extension;
using Cloud.Extension;
using Cloud.Filter;
using Cloud.Middleware;
using Cloud.Service.Extension;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Cloud;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
        {
            option.LoginPath = null;
            option.AccessDeniedPath = null;
            option.Events = new CustomCookieAuthenticationEvents();
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("default", policyBuilder => policyBuilder.RequireClaim(CustomClaimTypes.Identifier));

            foreach (var policy in Policies.GetPolicies())
                options.AddPolicy(policy, policyBuilder => policyBuilder.RequirePolicy(policy));

            options.DefaultPolicy = options.GetPolicy("default")!;
        });

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddRepository();
        builder.Services.AddAutoMapperCollection();

        builder.Services.AddCloudService();
        builder.Services.AddValidatorRegistry();
        
        builder.Services.BackgroundServiceCollection();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddControllers(options =>
        {
            var i = 0;
            options.Filters.Add<ValidateModelStateActionFilter>();
        });

        var app = builder.Build();

        await app.UseDatabaseAsync(false);
        await app.UseAuthorizationAsync();

        app.UseStaticFiles();

        app.UseCors(corsPolicyBuilder =>
        {
            corsPolicyBuilder.WithOrigins();
            corsPolicyBuilder.AllowAnyHeader();
            corsPolicyBuilder.AllowAnyMethod();
            corsPolicyBuilder.AllowCredentials();
        });

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // await using (var scope = app.Services.CreateAsyncScope())
        // {
        //     var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        //     await dbContext.Database.MigrateAsync();
        // }

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();
    }
}