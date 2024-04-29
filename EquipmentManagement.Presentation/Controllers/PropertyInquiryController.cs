using EquipmentManagement.Application.CQRS.SiteSide.Product.Query;
using EquipmentManagement.Application.CQRS.SiteSide.PropertyInquiry.Command;
using EquipmentManagement.Application.CQRS.SiteSide.PropertyInquiry.Query;
using EquipmentManagement.Application.Extensions;
using EquipmentManagement.Domain.DTO.SiteSide.PropertyInquiry;
using EquipmentManagement.Domain.Entities.Places;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
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
    public async Task<IActionResult> AddNewExcelFile(CancellationToken cancellationToken = default)
    {
        #region View Bags

        ViewBag.places = await Mediator.Send(new SelectListOfPlacesQuery()
        { }, cancellationToken);

        ViewBag.Categories = await Mediator.Send(new SelectListOfCategoriesQuery()
        { }, cancellationToken);

        #endregion

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
                PlaceId = model.PlaceId,
            } , 
            cancellationToken);

            if (res.ResState == AddNewExcelFileForPropertyInquiryResultState.Success)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(FilterInquiryDetail), new { PropertyInquiryId = res.PropertyInquiryId, PlaceId = model.PlaceId }) ;
            }
        }

        #region View Bags

        ViewBag.places = await Mediator.Send(new SelectListOfPlacesQuery()
        { }, cancellationToken);

        ViewBag.Categories = await Mediator.Send(new SelectListOfCategoriesQuery()
        { }, cancellationToken);

        #endregion

        TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
        return View(model);
    }

    #endregion

    #region Filter Inquiry Detail 

    public async Task<IActionResult> FilterInquiryDetail(FilterInquiryDetailDTO model ,
                                                         CancellationToken cancellation = default)
    {
        ViewBag.FilterInquiryMembers = model;
        ViewBag.badgesCount = await Mediator.Send(new FilterPropertyInquiryDetail_BadgesCountQuery()
        {
            PlaceId = model.PlaceId,
            InquiryId = model.PropertyInquiryId
        }) ;

        return View(await Mediator.Send(new FilterPropertyInquiryDetailQuery()
        {
            FilterInquiryDetailDTO = model,
        }));
    }

    #endregion
}
