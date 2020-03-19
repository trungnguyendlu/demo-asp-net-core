using Demo.Infrastructure.Helper;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Authorize
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly ISessionHelper _sessionHelper;

        public PermissionHandler(ISessionHelper sessionHelper)
        {
            _sessionHelper = sessionHelper;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            return Task.Run(() =>
            {
                if (_sessionHelper.HasPermission(requirement.Permission))
                {
                    context.Succeed(requirement);
                }
            });            
        }
    }
}
