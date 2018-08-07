using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServer4
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // 使用内存存储，密钥，客户端和资源来配置身份服务器。
            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddInMemoryApiResources(Config.GetApiResources())//添加api资源
                    .AddInMemoryClients(Config.GetClients())//添加客户端
                    .AddTestUsers(Config.GetUsers()) //添加测试用户
                    .AddInMemoryIdentityResources(Config.GetIdentityResources());//添加对OpenID Connect的支持

            //注册mvc服务
            services.AddMvc();
            //添加Google登录
            services.AddAuthentication()
                    .AddGoogle("Google", options =>
                    {
                        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                        options.ClientId = "434483408261-55tc8n0cs4ff1fe21ea8df2o443v2iuc.apps.googleusercontent.com";
                        options.ClientSecret = "3gcoTrEDPPJ0ukn_aYYT6PWo";
                    });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //添加到HTTP管道中。
            app.UseIdentityServer();
            //
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
