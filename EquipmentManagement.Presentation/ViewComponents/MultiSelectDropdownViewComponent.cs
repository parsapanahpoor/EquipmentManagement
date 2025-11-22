using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using EquipmentManagement.Domain.DTO.Common;

namespace EquipmentManagement.Presentation.ViewComponents;

public class MultiSelectDropdownViewComponent : ViewComponent
{
    // حالت معمولی با لیست آماده
    public async Task<IViewComponentResult> InvokeAsync(
        string id,
        List<DropdownItem> items,
        List<string> selectedIds = null,
        string placeholder = "انتخاب کنید",
        bool IsMultiple = true,
        bool IsHasDefult = false)
    {
        ViewData["Id"] = id;
        ViewData["IsMultiple"] = IsMultiple;
        ViewData["IsHasDefult"] = IsHasDefult;
        ViewData["Items"] = items;
        ViewData["SelectedIds"] = selectedIds ?? new List<string>();
        ViewData["Placeholder"] = placeholder;

        return View("/Views/Shared/Components/MultiSelectDropdown/MultiSelectDropdownView.cshtml");
    }



    private string GetDisplayName<TEnum>(TEnum value) where TEnum : struct, Enum
    {
        var field = typeof(TEnum).GetField(value.ToString());
        var attr = field?.GetCustomAttributes(typeof(DisplayAttribute), false)
                        .Cast<DisplayAttribute>()
                        .FirstOrDefault();
        return attr?.Name ?? value.ToString();
    }
}
