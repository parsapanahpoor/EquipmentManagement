using EquipmentManagement.Application.StaticTools;
using EquipmentManagement.Domain.Entities.Users;
using System.Security.Claims;
using System.Security.Principal;
public static class BoolExtensions
{
   
    public static string ConvertToPersian(this bool? user)
    {
        return user.HasValue ? user.Value ? "دارد" : "ندارد" : "ندارد";
    }
}

