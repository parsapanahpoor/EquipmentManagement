using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.Presentation.ViewComponents
{
    public class SideBarViewComponent : ViewComponent
    {
        #region Ctor

        public SideBarViewComponent()
        {

        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("SideBar");
        }
    }
}
