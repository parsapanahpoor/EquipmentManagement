using EquipmentManagement.Application.CQRS.SiteSide.ProductCategories.Command;
using EquipmentManagement.Application.CQRS.SiteSide.ProductCategories.Query;
using EquipmentManagement.Domain.DTO.SiteSide.ProductCategory;
using Microsoft.AspNetCore.Mvc;
namespace EquipmentManagement.Presentation.Controllers;

public class ShopProductCategoryController : SiteBaseController
{
    #region Filter Product Categories

    [HttpGet]
    public async Task<IActionResult> FilterProductCategory(FilterProductCategories filter,
                                                           CancellationToken token = default)
    {
        FilterProductCatgeoriesQuery query = new FilterProductCatgeoriesQuery()
        {
            Title = filter.Title,
        };

        return View(await Mediator.Send(query, token));
    }

    #endregion

    #region Add Product Category

    [HttpGet]
    public IActionResult AddProductCategory()
    {
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddProductCategory(CreateProductCategoryDTO model ,
                                                        CancellationToken cancellation)
    {
        #region Create Category

        if (ModelState.IsValid)
        {
            CreateProductCategoryCommand command = new()
            {
                Title = model.Title,
            };

            var res = await Mediator.Send(command, cancellation);
            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(FilterProductCategory));
            }
        }

        #endregion

        TempData[ErrorMessage] = "عملیات باشکست مواجه شده است.";
        return View(model);
    }

    #endregion
}
