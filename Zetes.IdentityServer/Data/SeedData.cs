using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace Zetes.IdentityServer.Data
{
    public static class SeedData
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        public async static Task<bool> InitialiseUsersAndRoles(IServiceProvider serviceProvider)
        {
            bool seedSuccessful = true;
            try
            {
                var context = serviceProvider.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
                var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
                var config = serviceProvider.GetService<IConfiguration>();

                Dictionary<string, string[]> roleClaims = new Dictionary<string, string[]>()
                {
                    ["Admin"] = new string[] { "UserManagement", "Portal", "ClaimManagement", "RoleManagement", "Free", "WorkItem" },
                    ["Portal"] = new string[] { "Portal", "Free" },
                    ["Free"] = new string[] { "Portal", "Free" },
                };

                List<Claim> claims = new List<Claim>();
                List<IdentityRole> roles = new List<IdentityRole>();

                foreach (var roleClaim in roleClaims)
                {
                    if (!roleManager.RoleExistsAsync(roleClaim.Key).GetAwaiter().GetResult())
                    {
                        var identityRole = new IdentityRole(roleClaim.Key);
                        var result = await roleManager.CreateAsync(identityRole);
                        foreach (var claim in roleClaim.Value)
                        {
                            Claim claimToAdd;
                            if (!claims.Any(x => x.Type == claim))
                            {
                                claimToAdd = new Claim(claim, claim);
                                claims.Add(claimToAdd);
                            }
                            else
                            {
                                claimToAdd = claims.Single(x => x.Type == claim);
                            }
                            await roleManager.AddClaimAsync(identityRole, claimToAdd);
                        }
                    }
                }

                if (!context.Users.Any())
                {
                    var adminUser = new IdentityUser("admin");
                    string password = (string)config.GetValue(typeof(string), "Seed:UserPassword");
                    var createdUser = userManager.CreateAsync(adminUser, password).GetAwaiter().GetResult();
                    var roleAdded =  userManager.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
                    var claimAdded = userManager.AddClaimAsync(adminUser, new Claim("ZetesAdmin", "ZetesAdmin")).GetAwaiter().GetResult();
                }
            }
            catch (Exception e)
            {
                _log.Error(e);
                seedSuccessful = false;
            }

            return seedSuccessful;
        }

        public async static Task<bool> InitialiseApiAndIdentityResources(IServiceProvider serviceProvider)
        {
            bool successfullySeeded = true;
            try
            {
                var context = serviceProvider.GetRequiredService<ConfigurationDbContext>();
                var config = serviceProvider.GetService<IConfiguration>();
                await context.Database.MigrateAsync();

                if (!context.ApiScopes.Any())
                {
                    foreach (var apiScope in ApiScopeDefinition.GetApiScopes())
                    {
                        context.ApiScopes.Add(apiScope.ToEntity());
                    }
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in IdentityResourceDefinitions.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in ApiResourceDefinitions.GetApis())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.Clients.Any())
                {
                    foreach (var client in ClientDefinitions.GetClients(config))
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                successfullySeeded = false;
            }
            return successfullySeeded;
        }
    }
}
