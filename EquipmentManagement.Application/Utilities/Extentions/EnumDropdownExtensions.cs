using EquipmentManagement.Domain.DTO.Common;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

public static class EnumDropdownExtensions
{
    public static List<DropdownItem> ToDropdownItems<TEnum>() where TEnum : struct, Enum
    {
        return Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .Select(e => new DropdownItem
            {
                Id = e.ToString(),
                Name = e.GetDisplayName()
            })
            .ToList();
    }

    public static string GetDisplayName<TEnum>(this TEnum value) where TEnum : Enum
    {
        var field = typeof(TEnum).GetField(value.ToString());
        var attr = field?.GetCustomAttribute<DisplayAttribute>();
        return attr?.Name ?? value.ToString();
    }
}
