using EquipmentManagement.Application.StaticTools;
using EquipmentManagement.Domain.Entities.Users;
using System.Security.Claims;
using System.Security.Principal;

namespace EquipmentManagement.Application.Extensions
{
    public static class UserExtensions
    {
        public static ulong GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var data = claimsPrincipal.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier);

            return ulong.Parse(data.Value);
        }

        public static ulong GetUserId(this IPrincipal principal)
        {
            var user = (ClaimsPrincipal)principal;

            return user.GetUserId();
        }

        public static string GetUserFullName(this User user)
        {
            return $"{user.FirstName} {user.LastName}";
        }

        public static string GetUserAvatar(this User user)
        {
            if (!string.IsNullOrEmpty(user.Avatar))
            {
                return Path.Combine(FilePaths.UserAvatarPathThumb, user.Avatar);
            }

            return FilePaths.DefaultUserAvatar;
        }
    }
}
