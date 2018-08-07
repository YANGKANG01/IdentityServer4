using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class Config
    {
        /// <summary>
        /// 定义api信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }
        /// <summary>
        /// 定义客户端凭据
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
               new Client
               {
                    ClientId  =  "client",
                    //没有交互式用户，使用clientid / secret进行身份验证，不需要密码
                    AllowedGrantTypes  =  GrantTypes.ClientCredentials,
                    //秘密认证
                    ClientSecrets  =
                    {
                       new Secret("secret".Sha256())
                    },
                    //客户端可以访问的范围
                    AllowedScopes  =  {"api1"}
                },
                 new Client
                 {
                   ClientId = "ro.client",
                   //使用用户名密码访问
                   AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                   ClientSecrets =
                    {
                       new Secret("secret".Sha256())
                    },
                   AllowedScopes = { "api1" }
                 },
                new Client
                {
                  ClientId = "mvc",
                  ClientName = "MVC Client",
                  AllowedGrantTypes = GrantTypes.Implicit,
                  //关闭是否返回身份信息界面
                  RequireConsent=false,
                  // 登录后重定向地址
                  RedirectUris = { "http://localhost:5002/signin-oidc" },
                  //重定向到注销后地址
                  PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                  //运行访问的资源
                  AllowedScopes = new List<string>
                  {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                  }
               }
            };
        }
        /// <summary>
        /// 定义系统中的资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
        /// <summary>
        /// 测试用户信息
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
              new TestUser
               {
                  SubjectId = "1",
                  Username = "alice",
                  Password = "password",
                  //自定义返回数据
                Claims = new []
                {
                    new Claim("name", "Alice"),
                    new Claim("website", "https://alice.com")
                }
               },
              new TestUser
               {
                  SubjectId = "2",
                  Username = "bob",
                  Password = "password"
               }
            };
        }
    }
}
