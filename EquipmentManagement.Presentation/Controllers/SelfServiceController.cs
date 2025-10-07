using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.Presentation.Controllers;

public class SelfServiceController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
