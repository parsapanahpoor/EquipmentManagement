using EquipmentManagement.Application.CQRS.SiteSide.ProductCategories.Command;
using EquipmentManagement.Application.CQRS.SiteSide.ProductCategories.Query;
using EquipmentManagement.Application.Security;
using EquipmentManagement.Domain.DTO.SiteSide.ProductCategory;
using EquipmentManagement.Presentation.HttpManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
namespace EquipmentManagement.Presentation.Controllers;

[PermissionChecker("ManageProductCategory")]
public class ShopProductCategoryController : SiteBaseController
{
    #region Filter Product Categories

    [PermissionChecker("FilterProductCategory")]
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

    [PermissionChecker("AddProductCategory")]
    [HttpGet]
    public IActionResult AddProductCategory()
    {
        return View();
    }

    [PermissionChecker("AddProductCategory")]
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

    #region Edit Product Category

    [PermissionChecker("EditProductCategory")]
    [HttpGet]
    public async Task<IActionResult> EditProductCategory(ulong productCategoryId , 
                                                         CancellationToken cancellationToken = default)
    {
        #region Get Product Category

        GetProductCategoryQuery query = new()
        {
            ProductCategoryId = productCategoryId
        };

        var category = await Mediator.Send(query , cancellationToken);
        if (category == null) return NotFound();

        #endregion

        return View(category);
    }

    [PermissionChecker("EditProductCategory")]
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProductCategory(EditProductCategoryDTO model,
                                                         CancellationToken cancellationToken = default)
    {
        #region Edit Product Category

        if (ModelState.IsValid)
        {
            EditProductCategoryCommand command = new()
            {
                ProductCategoryId = model.CategoryId,
                Title = model.Title
            };

            var res = await Mediator.Send(command , cancellationToken);
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

    #region Delete Product Category

    public async Task<IActionResult> DeleteProductCategory(ulong productCategoryId , 
                                                           CancellationToken cancellationToken)
    {
        DeleteProductCategoryCommand command = new()
        {
            ProductCategoryId = productCategoryId
        };

        var res = await Mediator.Send(command , cancellationToken);
        if (res) return JsonResponseStatus.Success();

        return JsonResponseStatus.Error();
    }

    #endregion
}
