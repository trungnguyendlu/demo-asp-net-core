using Demo.Data;
using Microsoft.AspNetCore.Authorization;

namespace Demo.Infrastructure.Authorize
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(FunctionType permission)
        {
            Permission = permission;
        }

        public FunctionType Permission { get; }
    }
}