using EquipmentManagement.Application.Extensions;
using EquipmentManagement.Domain.IRepositories.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EquipmentManagement.Application.Security
{
    public class PermissionChecker : AuthorizeAttribute, IAuthorizationFilter
    {
        #region Ctor

        private readonly string _permission;

        public PermissionChecker(string permission)
        {
            _permission = permission;
        }

        #endregion

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var _permissionService = (IRoleQueryRepository)context.HttpContext.RequestServices.GetService(typeof(IRoleQueryRepository));

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = context.HttpContext.User.GetUserId();

                if (!_permissionService.HasUserPermission(userId, _permission).Result)
                {
                    context.Result = new RedirectResult("/");
                }
            }
            else
            {
                context.Result = new RedirectResult("/");
            }
        }
    }
}
