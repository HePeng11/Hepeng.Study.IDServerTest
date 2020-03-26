using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hepeng.Study.IDServerTest.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hepeng.Study.IDServerTest
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            InMemoryConfiguration.Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var idb = services.AddIdentityServer()
              //����Tokenǩ����Ҫһ�Թ�Կ��˽Կ������IdentityServerΪ�������ṩ��һ��AddDeveloperSigningCredential()��������������Ǹ㶨����£���Ĭ�ϴ浽Ӳ���С�
              //���л�����������ʱ�����ǵ�ʹ�������˾���֤�飬����Ϊʹ��AddSigningCredential()����
              .AddDeveloperSigningCredential()
              .AddInMemoryIdentityResources(InMemoryConfiguration.GetIdentityResources())
              //.AddTestUsers(InMemoryConfiguration.GetUsers().ToList())
              .AddProfileService<ProfileService>()
              .AddInMemoryClients(InMemoryConfiguration.GetClients())
              .AddInMemoryApiResources(InMemoryConfiguration.GetApiResources());

            #region

            //Inject the classes we just created
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion
            // for QuickStart-UI
            services.AddMvc(o =>
            {
                o.EnableEndpointRouting = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            // for QuickStart-UI
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
