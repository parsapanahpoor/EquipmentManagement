﻿using EquipmentManagement.Application.CQRS.SiteSide.Product.Command;
using EquipmentManagement.Application.CQRS.SiteSide.Product.Command.CreateAbolitionRequest;
using EquipmentManagement.Application.CQRS.SiteSide.Product.Command.CreateRepairRequest;
using EquipmentManagement.Application.CQRS.SiteSide.Product.Command.EditProduct;
using EquipmentManagement.Application.CQRS.SiteSide.Product.Query;
using EquipmentManagement.Application.CQRS.SiteSide.Product.Query.CreateAbolitionRequestFormInfo;
using EquipmentManagement.Application.CQRS.SiteSide.Product.Query.CreateRepairRequestFormInfo;
using EquipmentManagement.Application.CQRS.SiteSide.Product.Query.EditProduct;
using EquipmentManagement.Application.CQRS.SiteSide.Product.Query.FiltreProductAbolitionRequest;
using EquipmentManagement.Application.CQRS.SiteSide.Product.Query.FiltreProductRepairRequest;
using EquipmentManagement.Application.Extensions;
using EquipmentManagement.Application.Security;
using EquipmentManagement.Domain.DTO.SiteSide.Product;
using EquipmentManagement.Presentation.HttpManager;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.Presentation.Controllers;

[PermissionChecker("ManageAccount")]
public class ProductController : SiteBaseController
{
    #region Product

    #region List Of Products

    public async Task<IActionResult> FilterProduct(FilterProductDTO filter,
                                                   CancellationToken cancellation = default)
    {
        return View(await Mediator.Send(new FilterProductQuery()
        {
            filter = filter
        }));
    }

    #endregion

    #region List Of Products For Excel Files 

    public async Task<IActionResult> FilterProductForExcelFiles(FilterProductForExcelFilesDTO filter,
                                                                CancellationToken cancellation = default)
    {
        return View(await Mediator.Send(new FilterProductForExcelFilesQuery()
        {
            filter = filter
        }));
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
                EntityCount = 1,
                CategoryId = createProduct.CategoryId,
                ProductTitle = createProduct.ProductTitle,
                PlaceId = createProduct.PlaceId,
                RepositoryCode = createProduct.RepositoryCode,
                UserId = User.GetUserId(),
                BrandName = createProduct.BrandName,
                InvoiceDateTime = createProduct.InvoiceDateTime,
                InvoiceImage = createProduct.InvoiceImage,
                InvoiceNumber = createProduct.InvoiceNumber
            },cancellationToken);

            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(FilterProduct));
            }
        }

        #endregion

        #region View Bags

        if (!createProduct.PlaceId.HasValue)
            TempData[ErrorMessage] = "مکان کالا باید انتخاب شود.";

        if (!createProduct.CategoryId.HasValue)
            TempData[ErrorMessage] = "دسته بندی کالا باید انتخاب شود.";

        ViewBag.places = await Mediator.Send(new SelectListOfPlacesQuery()
        { }, cancellationToken);

        ViewBag.Categories = await Mediator.Send(new SelectListOfCategoriesQuery()
        { }, cancellationToken);

        #endregion

        return View(createProduct);
    }

    #endregion

    #region Edit Product

    [HttpGet]
    public async Task<IActionResult> EditProduct(EditProductQuery query,
                                                 CancellationToken cancellationToken = default)
    {
        #region View Bags

        ViewBag.places = await Mediator.Send(new SelectListOfPlacesQuery()
        { }, cancellationToken);

        ViewBag.Categories = await Mediator.Send(new SelectListOfCategoriesQuery()
        { }, cancellationToken);

        #endregion

        return View(await Mediator.Send(query, cancellationToken));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProduct(EditProductDTO editProduct,
                                                 CancellationToken cancellationToken = default)
    {
        #region Edit Product

        var res = await Mediator.Send(new EditProductCommand()
        {
            EditProductDTO = editProduct,
            UserId = User.GetUserId(),
        }, cancellationToken);

        if (res)
        {
            TempData[SuccessMessage] = "ویرایش محصول باموفقیت انجام شده است.";
            return RedirectToAction(nameof(FilterProduct));
        }

        #endregion

        #region View Bags

        if (editProduct.PlaceId == 0)
            TempData[ErrorMessage] = "مکان کالا باید انتخاب شود.";

        if (editProduct.CategoryId == 0)
            TempData[ErrorMessage] = "دسته بندی کالا باید انتخاب شود.";

        ViewBag.places = await Mediator.Send(new SelectListOfPlacesQuery()
        { }, cancellationToken);

        ViewBag.Categories = await Mediator.Send(new SelectListOfCategoriesQuery()
        { }, cancellationToken);

        #endregion

        TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد";
        return View(editProduct);
    }

    #endregion

    #region Product Detail

    [HttpGet]
    public async Task<IActionResult> ProductDetail(ProductDetailQuery query,
                                                   CancellationToken cancellationToken = default)
    {
        return View(await Mediator.Send(query, cancellationToken));
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

    #endregion

    #region Organization Request

    #region Create Repair Request

    [HttpGet]
    public async Task<IActionResult> CreateRepairRequest(ulong productId,
        CancellationToken cancellationToken)
    => View(await Mediator.Send(new CreateRepairRequestFormInfoQuery(
        productId,
        User.GetUserId()),
        cancellationToken));

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateRepairRequest(ulong productId,
        ulong expertUserId,
        string? description,
        CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            var result = await Mediator.Send(new CreateRepairRequestCommand(
                productId,
                expertUserId,
                User.GetUserId(),
                description)
             , cancellationToken);

            switch (result)
            {
                case CreateRepairRequestCommandRespons.Success:
                    TempData[SuccessMessage] = "درخواست باموفقیت انجام شده است .";
                    return RedirectToAction("Landing", "Home");

                case CreateRepairRequestCommandRespons.Failure:
                    TempData[ErrorMessage] = "عملیات باشکست مواجه شده است.";
                    break;

                case CreateRepairRequestCommandRespons.DosentConfig:
                    TempData[ErrorMessage] = "درحال حاضر ایجاد درخواست در دسترس نمی باشد .";
                    return RedirectToAction(nameof(FilterProduct));

                default:
                    break;
            }
        }

        TempData[ErrorMessage] = "عملیات باشکست مواجه شده است.";
        return View(await Mediator.Send(new CreateRepairRequestFormInfoQuery(
            productId,
            User.GetUserId()),
            cancellationToken));
    }

    #endregion

    #region Filtre Product Repair Request 

    [HttpGet]
    public async Task<IActionResult> FiltreProductRepairRequest(FiltreProductRepairRequestDto filter,
        CancellationToken cancellationToken)
        => View(await Mediator.Send(
            new FiltreProductRepairRequestQuery(filter),
            cancellationToken));

    #endregion

    #region Filter Abolition Request

    [HttpGet]
    public async Task<IActionResult> FiltreProductAbolitionRequest(FiltreProductAbolitionRequestDto filter,
        CancellationToken cancellationToken)
        => View(await Mediator.Send(
            new FiltreProductAbolitionRequestQuery(filter),
            cancellationToken));

    #endregion

    #region Create Abolition Request

    [HttpGet]
    public async Task<IActionResult> CreateAbolitionRequest(ulong productId,
        CancellationToken cancellationToken)
        => View(await Mediator.Send(new CreateAbolitionRequestFormInfoQuery(
            productId,
            User.GetUserId()),
            cancellationToken));

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAbolitionRequest(ulong productId,
        ulong expertUserId,
        string? description,
        CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            var result = await Mediator.Send(new CreateAbolitionRequestCommand(
                productId,
                expertUserId,
                User.GetUserId(),
                description)
             , cancellationToken);

            switch (result)
            {
                case CreateAbolitionRequestCommandRespons.Success:
                    TempData[SuccessMessage] = "درخواست باموفقیت انجام شده است .";
                    return RedirectToAction("Landing", "Home");

                case CreateAbolitionRequestCommandRespons.Failure:
                    TempData[ErrorMessage] = "عملیات باشکست مواجه شده است.";
                    break;

                case CreateAbolitionRequestCommandRespons.DosentConfig:
                    TempData[ErrorMessage] = "درحال حاضر ایجاد درخواست در دسترس نمی باشد .";
                    return RedirectToAction(nameof(FilterProduct));

                default:
                    break;
            }
        }

        TempData[ErrorMessage] = "عملیات باشکست مواجه شده است.";
        return View(await Mediator.Send(new CreateAbolitionRequestFormInfoQuery(
            productId,
            User.GetUserId()),
            cancellationToken));
    }

    #endregion

    #endregion
}
