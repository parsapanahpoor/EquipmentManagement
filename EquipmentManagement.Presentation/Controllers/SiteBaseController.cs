using MediatR;
using Microsoft.AspNetCore.Mvc;
using EquipmentManagement.Presentation.Filter;
namespace EquipmentManagement.Presentation.Controllers;

[CatchExceptionFilter]
public abstract class SiteBaseController : Controller
{
    public static string SuccessMessage = "SuccessMessage";
    public static string ErrorMessage = "ErrorMessage";
    public static string InfoMessage = "InfoMessage";
    public static string WarningMessage = "WarningMessage";

    private ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
