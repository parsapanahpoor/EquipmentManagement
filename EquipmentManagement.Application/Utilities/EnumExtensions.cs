using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EquipmentManagement.Application.Utilities;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()
                        .GetMember(enumValue.ToString())
                        .FirstOrDefault()?
                        .GetCustomAttribute<DisplayAttribute>()?
                        .GetName() ?? enumValue.ToString();
    }
}