using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using WebClient.Infrastructure;
using System.Security.Claims;
using WebClient.Services.User;
using WebClient.Pages;
using Microsoft.JSInterop;

namespace WebClient;

/// <summary>
/// Class Program.
/// </summary>
public class Program
{
    /// <summary>
    /// Defines the entry point of the application.
    /// </summary>
    /// <param name="args">The arguments.</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddTransient(sp => new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7256")
        });

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        builder.Services.AddScoped<UserService>();
        builder.Services.AddAntDesign();

        builder.Services.AddOptions();
        builder.Services.AddAuthorizationCore();

        builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationStateProvider>();
        builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");

            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }

    public class TokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        public TokenAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }


        //¬озварщает куки аутентифицированного (зарегистрированного) пользовател€,
        //или возвращает анонимного пользовател€
        public override async Task<AuthenticationState>GetAuthenticationStateAsync()
        {
            AuthenticationState CreateAnonymous()
            {
                var anonymousIdentity = new ClaimsIdentity();
                var anonymousPrincipal = new ClaimsPrincipal(anonymousIdentity);
                return new AuthenticationState(anonymousPrincipal);

            }
            

            var token = await _localStorageService.GetAsync<SecurityToken>(nameof(SecurityToken));

            if (token == null)
            {
                return CreateAnonymous();
            }

            if (string.IsNullOrEmpty(token.AccessToken) || token.ExpiredAt < DateTime.UtcNow)
            {
                return CreateAnonymous();
            }

            // Create real User state
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Country, "Russia"),
                new Claim(ClaimTypes.Name, token.UserName),
                new Claim(ClaimTypes.Expired, token.ExpiredAt.ToLongDateString()),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "User")
            };

            var identity = new ClaimsIdentity(claims, "Token");
            var principal= new ClaimsPrincipal(identity);
            return new AuthenticationState(principal);
                

        }
    }
}


