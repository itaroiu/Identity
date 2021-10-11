using Identity.Shared.Models;
using Identity.Shared.Services;
using Identity.Shared.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using Microsoft.Extensions.Http;
using System.Threading.Tasks;
using Identity.Shared.ViewModels.Shared;
using Identity.Shared.ViewModels.Account;

namespace Identity.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IdentityStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<IdentityStateProvider>());
            builder.Services.AddHttpClient<IAuthService, AccountService>("IdentityHttpClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            builder.Services.AddTransient<IMainLayoutViewModel, MainLayoutViewModel>();
            builder.Services.AddTransient<INavMenuViewModel, NavMenuViewModel>();
            builder.Services.AddTransient<ILoginViewModel, LoginViewModel>();
            builder.Services.AddTransient<IRegisterAccountViewModel, RegisterAccountViewModel>();
            builder.Services.AddTransient<IAccountsViewModel, AccountsViewModel>();
            builder.Services.AddTransient<IUpdateAccountViewModel, UpdateAccountViewModel>();

            await builder.Build().RunAsync();
        }
    }
}
