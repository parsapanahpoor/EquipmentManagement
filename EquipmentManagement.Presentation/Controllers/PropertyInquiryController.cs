using EquipmentManagement.Application.CQRS.SiteSide.PropertyInquiry.Command;
using EquipmentManagement.Application.CQRS.SiteSide.PropertyInquiry.Query;
using EquipmentManagement.Application.Extensions;
using EquipmentManagement.Domain.DTO.SiteSide.PropertyInquiry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace EquipmentManagement.Presentation.Controllers;

[Authorize]
public class PropertyInquiryController : SiteBaseController
{
    #region List Of System Inquiries

    public async Task<IActionResult> ListOfSystemInquiries(FilterSystemPropertyInquiriesDTO filter ,
                                                           CancellationToken cancellation = default)
    {
        return View(await Mediator.Send(new FilterSystemPropertyInquiriesQuery()
        {
            Filter = filter
        } , 
        cancellation));
    }

    #endregion

    #region Add New Excel File 

    [HttpGet]
    public IActionResult AddNewExcelFile()
    {
        return View();
    }

    [HttpPost , ValidateAntiForgeryToken]
    public async Task<IActionResult> AddNewExcelFile(AddNewExcelFileDTO model , 
                                                     CancellationToken cancellationToken =default)
    {
        if (ModelState.IsValid)
        {
            var res = await Mediator.Send(new AddNewExcelFileForPropertyInquiryCommand()
            {
                Model = model,
                UserId = User.GetUserId(),
            } , 
            cancellationToken);

            if (res.ResState == AddNewExcelFileForPropertyInquiryResultState.Success)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(FilterInquiryDetail) , new { PropertyInquiryId = res.PropertyInquiryId });
            }
        }

        TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
        return View(model);
    }

    #endregion

    #region Filter Inquiry Detail 

    public async Task<IActionResult> FilterInquiryDetail(FilterInquiryDetailDTO model ,
                                                         CancellationToken cancellation = default)
    {
        return View(await Mediator.Send(new FilterPropertyInquiryDetailQuery()
        {
            FilterInquiryDetailDTO = model,
        },
        cancellation));
    }

    #endregion
}
