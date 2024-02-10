using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.Presentation.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        #region Ctor

    

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View("Header");
        }
    }
}
