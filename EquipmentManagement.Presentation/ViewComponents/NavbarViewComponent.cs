using EquipmentManagement.Application.Extensions;
using EquipmentManagement.Domain.IRepositories.User;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.Presentation.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IUserQueryRepository _userQueryRepository;

        public NavbarViewComponent(IUserQueryRepository userQueryRepository)
        {
            _userQueryRepository  = userQueryRepository;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken = default)
        {
            var user = await _userQueryRepository.GetByIdAsync(cancellationToken , User.GetUserId());

            return View("Navbar", user);
        }
    }
}
