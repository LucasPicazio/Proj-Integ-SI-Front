using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PSI_FRONT.Helpers;
using PSI_FRONT.Services;

namespace PSI_FRONT
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            ConfigureServices(builder.Services, builder.Configuration.GetValue<string>("BaseURL"));

            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services, string baseAddress)
        {
            services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri(baseAddress)}
            );
            services.AddAuthorizationCore();
            services.AddScoped<UserService>();
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IDisplayMessage, DisplayMessage>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IUserService, UserService>(
                provider => provider.GetRequiredService<UserService>()
            );
            services.AddScoped<AuthenticationStateProvider>(
                provider => provider.GetRequiredService<UserService>()
            );
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICartService, CartService>();

        }
    }
}
