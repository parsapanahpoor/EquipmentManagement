using EquipmentManagement.Application.CQRS.SiteSide.Places.Command;
using EquipmentManagement.Application.CQRS.SiteSide.Places.Query;
using EquipmentManagement.Application.CQRS.SiteSide.ProductCategories.Command;
using EquipmentManagement.Application.Security;
using EquipmentManagement.Domain.DTO.SiteSide.FilterPlaces;
using EquipmentManagement.Domain.DTO.SiteSide.Places;
using EquipmentManagement.Domain.Entities.ProductCategory;
using EquipmentManagement.Presentation.HttpManager;
using Microsoft.AspNetCore.Mvc;
namespace EquipmentManagement.Presentation.Controllers;

[PermissionChecker("ManagePlaces")]
public class PlacesController : SiteBaseController 
{
    #region Filter Places 

    [PermissionChecker("FilterPlaces")]
    public async Task<IActionResult> FilterPlaces(FilterPlacesDTO filter , 
                                                  CancellationToken cancellation)
    {
        var model = await Mediator.Send(new FilterPlacesQuery()
        {
            Filter = filter,    
        } , 
        cancellation);

        return View(model);
    }

    #endregion

    #region Create Place

    [PermissionChecker("CreatePlace")]
    [HttpGet]
    public IActionResult CreatePlace(ulong? parentId)
    {
        return View(new CreatePlaceDTO()
        {
            ParentId = parentId
        });
    }

    [PermissionChecker("CreatePlace")]
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePlace(CreatePlaceDTO createPlace , 
                                                 CancellationToken cancellationToken)
    {
        #region Create Place

        if (ModelState.IsValid)
        {
            var res = await Mediator.Send(new CreatePlaceCommand()
            {
                ParentId = createPlace.ParentId,    
                Title = createPlace.Title,
            } , 
            cancellationToken);

            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(FilterPlaces), new { parentId = createPlace.ParentId });
            }
        }

        #endregion

        TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
        return View(createPlace);
    }

    #endregion

    #region Edit Place

    [PermissionChecker("EditPlace")]
    [HttpGet]
    public async Task<IActionResult> EditPlace(EditPlaceQuery place , 
                                               CancellationToken cancellationToken)
    {
        var model = await Mediator.Send(place , cancellationToken);
        if (model == null) return NotFound();

        return View(model);
    }

    [PermissionChecker("EditPlace")]
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditPlace(EditPlaceDTO place,
                                               CancellationToken cancellation)
    {
        #region Edit Place 

        if (ModelState.IsValid) 
        {
            var res = await Mediator.Send(new EditPlaceCommand()
            {
                PlaceId = place.PlaceId,    
                PlaceTitle = place.PlaceTitle
            } , 
            cancellation);

            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(FilterPlaces), new { parentId = place.ParentId }) ;
            }
        }

        #endregion

        TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
        return View(place);
    }

    #endregion

    #region Delete Place

    public async Task<IActionResult> DeletePlace(DeletePlaceCommand command , 
                                                 CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(command, cancellationToken);
        if (res) return JsonResponseStatus.Success();

        return JsonResponseStatus.Error();
    }

    #endregion
}
