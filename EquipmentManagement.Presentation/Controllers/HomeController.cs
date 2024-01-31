using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EquipmentManagement.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
