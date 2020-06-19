using Domain.Authorization.Enums;
using Domain.Authorization.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Authorization
{
    public static class AuthorizationPolicyCollectionExtensions
    {
        public static void RegisterAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationPolicies.AdministratorPolicy, policy =>
                    policy.RequireRole($"{RoleTypes.Administrator}"));
                options.AddPolicy(AuthorizationPolicies.AdministratorPolicy, policy =>
                    policy.RequireClaim("Scope", $"{ScopeTypes.Administrator}"));

                options.AddPolicy(AuthorizationPolicies.ReadPolicy, policy =>
                    policy.RequireRole(
                        $"{RoleTypes.Administrator}",
                        $"{RoleTypes.Read}"));
                options.AddPolicy(AuthorizationPolicies.ReadPolicy, policy =>
                    policy.RequireClaim("Scope", $"{ScopeTypes.Default}", $"{ScopeTypes.Administrator}"));
            });
        }
    }
}
