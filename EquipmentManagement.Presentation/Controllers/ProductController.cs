using EquipmentManagement.Application.CQRS.SiteSide.Places.Command;
using EquipmentManagement.Application.CQRS.SiteSide.Product.Command;
using EquipmentManagement.Application.CQRS.SiteSide.Product.Query;
using EquipmentManagement.Domain.DTO.SiteSide.Product;
using EquipmentManagement.Presentation.HttpManager;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.Presentation.Controllers;

public class ProductController : SiteBaseController
{
    #region List Of Products

    public async Task<IActionResult> FilterProduct(FilterProductDTO filter,
                                                   CancellationToken cancellation = default)
    {
        return View(await Mediator.Send(new FilterProductQuery()
        {
            CategoryId = filter.CategoryId,
            CategoryTitle = filter.CategoryTitle,
            PlaceId = filter.PlaceId,
            PlaceTitle = filter.PlaceTitle,
            ProductTitle = filter.ProductTitle,
        },
        cancellation));
    }

    #endregion

    #region Create Product

    [HttpGet]
    public async Task<IActionResult> CreateProduct(ulong? PlaceId,
                                                   ulong? CategoryId,
                                                   CancellationToken cancellationToken = default)
    {
        #region View Bags

        ViewBag.places = await Mediator.Send(new SelectListOfPlacesQuery()
        { }, cancellationToken);

        ViewBag.Categories = await Mediator.Send(new SelectListOfCategoriesQuery()
        { }, cancellationToken);

        #endregion

        return View(new CreateProductDTO()
        {
            PlaceId = PlaceId,
            CategoryId = CategoryId,
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProduct(CreateProductDTO createProduct,
                                                   CancellationToken cancellationToken = default)
    {
        #region Add Product

        if (ModelState.IsValid)
        {
            var res = await Mediator.Send(new CreateProductCommand()
            {
                BarCode = createProduct.BarCode,
                Description = createProduct.Description,
                EntityCount = createProduct.EntityCount,
                CategoryId = createProduct.CategoryId,
                ProductTitle = createProduct.ProductTitle,
                PlaceId = createProduct.PlaceId,
            },
            cancellationToken
            );

            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(FilterProduct));
            }
        }

        #endregion

        #region View Bags

        ViewBag.places = await Mediator.Send(new SelectListOfPlacesQuery()
        { }, cancellationToken);

        ViewBag.Categories = await Mediator.Send(new SelectListOfCategoriesQuery()
        { }, cancellationToken);

        #endregion

        return View(createProduct);
    }

    #endregion

    #region Product Detail

    [HttpGet]
    public async Task<IActionResult> ProductDetail(ProductDetailQuery query , 
                                                   CancellationToken cancellationToken = default)
    {
        return View(await Mediator.Send(query , cancellationToken));
    }

    #endregion

    #region Delete Product

    public async Task<IActionResult> DeleteProduct(DeleteProductCommand command,
                                               CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(command, cancellationToken);
        if (res) return JsonResponseStatus.Success();

        return JsonResponseStatus.Error();
    }

    #endregion
}
