using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Olimp.Models;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AnyRoleAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
{
    private readonly string[] _roles;

    public AnyRoleAuthorizeAttribute(params string[] roles)
    {
        _roles = roles;
    }

    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        foreach (var role in _roles)
        {
            if (context.HttpContext.User.IsInRole(role))
            {
                return Task.CompletedTask;
            }
        }

        context.Result = new ForbidResult();
        return Task.CompletedTask;
    }
}