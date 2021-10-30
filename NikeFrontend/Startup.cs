using Blazored.SessionStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NikeFrontend.Data;
using NikeFrontend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NikeFrontend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddBlazoredSessionStorage();
            services.AddBlazoredToast();
            //API Service--->//

            services.AddTransient<ProductService>();
            services.AddTransient<ProductCategoryService>();
            services.AddTransient<TeamMemberService>();

            //<---API Service//
            services.AddTransient<ValidateHeaderHandler>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddHttpClient();
            services.AddHttpClient("local", client =>
            {
                client.BaseAddress = new Uri(Configuration.GetValue<string>("LocalHostAPI"));
            });
            services.AddHttpClient("KSC", client =>
            {
                client.BaseAddress = new Uri(Configuration.GetValue<string>("KeyboardSlingerAPI"));

            });
            services.AddHttpClient("KSC_auth", client =>
            {
                client.BaseAddress = new Uri(Configuration.GetValue<string>("KeyboardSlingerAPI"));

            })
                .AddHttpMessageHandler<ValidateHeaderHandler>();
            services.AddHttpClient<IUserService, UserService>(x =>
            {
                x.BaseAddress = new Uri(Configuration.GetValue<string>("KeyboardSlingerAPI"));
            });
            
            
            services.AddSingleton<HttpClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
