﻿
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.IRepositories.Place;
using EquipmentManagement.Domain.IRepositories.Product;
using EquipmentManagement.Domain.IRepositories.ProductCategory;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Command.EditProduct;

public record EditProductCommandHandler : IRequestHandler<EditProductCommand, bool>
{
    #region Ctor

    private readonly IProductCommandRepository _commandRepository;
    private readonly IProductQueryRepository _queryRepository;
    private readonly IProductCategoryQueryRepository _categoryQueryRepository;
    private readonly IPlacesQueryRepository _placesQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditProductCommandHandler(IProductCommandRepository commandRepository,
                                       IProductQueryRepository queryRepository,
                                       IProductCategoryQueryRepository categoryQueryRepository,
                                       IPlacesQueryRepository placesQueryRepository,
                                       IUnitOfWork unitOfWork)
    {
        _categoryQueryRepository = categoryQueryRepository;
        _commandRepository = commandRepository;
        _queryRepository = queryRepository;
        _placesQueryRepository = placesQueryRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<bool> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        //Get Old product by id 
        var product = await _queryRepository.GetByIdAsync(cancellationToken , request.EditProductDTO.ProductId);
        if (product == null) return false;

        //Check Valid BarCod  
        if (await _queryRepository.IsExistAny_Product_ByBarCode(request.EditProductDTO.BarCode , product.Id, cancellationToken))
        {
            return false;
        }

        //Check Category
        if (!await _categoryQueryRepository.IsExistAny_Category_ByCategoryId(request.EditProductDTO.CategoryId, cancellationToken))
        {
            return false;
        }

        //Check Place
        if (!await _placesQueryRepository.IsExistAny_Place_ById(request.EditProductDTO.PlaceId, cancellationToken))
        {
            return false;
        }

        //Update Prodperties
        product.PlaceId = request.EditProductDTO.PlaceId;
        product.UpdateDate = DateTime.Now;
        product.ProductCategoryId = request.EditProductDTO.CategoryId;
        product.BarCode = request.EditProductDTO.BarCode;
        product.Description = request.EditProductDTO.Description;
        product.ProductTitle = request.EditProductDTO.ProductTitle;
        product.RepostiroyCode = request.EditProductDTO.RepositoryCode;

        //Update Product
        _commandRepository.Update(product);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
