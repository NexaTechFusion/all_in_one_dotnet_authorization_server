﻿using OpenIddict.Abstractions;

namespace AIO.AuthServer;

public class ClientsSeeder
{
    private readonly IServiceProvider _serviceProvider;

        public ClientsSeeder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task AddScopes()
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();

            var apiScope = await manager.FindByNameAsync("api1");

            if (apiScope != null)
            {
                await manager.DeleteAsync(apiScope);
            }

            await manager.CreateAsync(new OpenIddictScopeDescriptor
            {
                DisplayName = "Api scope",
                Name = "api1",
                Resources =
                {
                    "resource_server_1"
                }
            });
        }

        public async Task AddClients()
        {
            await using var scope = _serviceProvider.CreateAsyncScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureCreatedAsync();

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            var client = await manager.FindByClientIdAsync("web-client");
            if (client != null)
            {
                await manager.DeleteAsync(client);
            }

            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "web-client",
                ClientSecret = "901564A5-E7FE-42CB-B10D-61EF6A8F3654",
                ConsentType = OpenIddictConstants.ConsentTypes.Explicit,
                DisplayName = "Swagger client application",
                RedirectUris =
                {
                    new Uri("https://localhost:7002/swagger/oauth2-redirect.html")
                },
                PostLogoutRedirectUris =
                {
                    new Uri("https://localhost:7002/resources")
                },
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Authorization,
                    OpenIddictConstants.Permissions.Endpoints.Logout,
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                    OpenIddictConstants.Permissions.ResponseTypes.Code,
                    OpenIddictConstants.Permissions.Scopes.Email,
                    OpenIddictConstants.Permissions.Scopes.Profile,
                    OpenIddictConstants.Permissions.Scopes.Roles,
                   $"{OpenIddictConstants.Permissions.Prefixes.Scope}api1"
                },
                //Requirements =
                //{
                //    Requirements.Features.ProofKeyForCodeExchange
                //}
            });

            var reactClient = await manager.FindByClientIdAsync("react-client");
            if (reactClient != null)
            {
                await manager.DeleteAsync(reactClient);
            }

            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "react-client",
                ClientSecret = "901564A5-E7FE-42CB-B10D-61EF6A8F3654",
                ConsentType = OpenIddictConstants.ConsentTypes.Explicit,
                DisplayName = "React client application",
                RedirectUris =
                {
                    new Uri("http://localhost:3000/oauth/callback")
                },
                PostLogoutRedirectUris =
                {
                    new Uri("http://localhost:3000/")
                },
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Authorization,
                    OpenIddictConstants.Permissions.Endpoints.Logout,
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                    OpenIddictConstants.Permissions.ResponseTypes.Code,
                    OpenIddictConstants.Permissions.Scopes.Email,
                    OpenIddictConstants.Permissions.Scopes.Profile,
                    OpenIddictConstants.Permissions.Scopes.Roles,
                   $"{OpenIddictConstants.Permissions.Prefixes.Scope}api1"
                },
                //Requirements =
                //{
                //    Requirements.Features.ProofKeyForCodeExchange
                //}
            });
        }
        public async Task AddOidcDebuggerClient()
        {
            await using var scope = _serviceProvider.CreateAsyncScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureCreatedAsync();

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            var client = await manager.FindByClientIdAsync("oidc-debugger");
            if (client != null)
            {
                await manager.DeleteAsync(client);
            }

            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "oidc-debugger",
                ClientSecret = "901564A5-E7FE-42CB-B10D-61EF6A8F3654",
                ConsentType = OpenIddictConstants.ConsentTypes.Explicit,
                DisplayName = "Postman client application",
                RedirectUris =
                {
                    new Uri("https://oidcdebugger.com/debug")
                },
                PostLogoutRedirectUris =
                {
                    new Uri("https://oauth.pstmn.io/v1/callback")
                },
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Authorization,
                    OpenIddictConstants.Permissions.Endpoints.Logout,
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                    OpenIddictConstants.Permissions.ResponseTypes.Code,
                    OpenIddictConstants.Permissions.Scopes.Email,
                    OpenIddictConstants.Permissions.Scopes.Profile,
                    OpenIddictConstants.Permissions.Scopes.Roles,
                    $"{OpenIddictConstants.Permissions.Prefixes.Scope}api1"
                },
                //Requirements =
                //{
                //    Requirements.Features.ProofKeyForCodeExchange
                //}
            });
        }
}