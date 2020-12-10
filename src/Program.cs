using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PSI_FRONT.Services;

namespace PSI_FRONT
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            //builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorizationCore();
            services.AddScoped<UserService>();
            services.AddScoped<AuthenticationStateProvider>(
                provider => provider.GetRequiredService<UserService>()
            );
            services.AddScoped<IUserService, UserService>(
                provider => provider.GetRequiredService<UserService>()
            );
            services.AddScoped<IProductService, ProductService>();

#if DEBUG
            services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri("http://localhost:8181")}
            );
#else
            services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri("http://34.73.241.32:8181")}
            );
#endif
        }
    }
}
