
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Application.Utilities.Security;
using EquipmentManagement.Domain.IRepositories.Place;
using EquipmentManagement.Domain.IRepositories.Product;
using EquipmentManagement.Domain.IRepositories.ProductCategory;
using EquipmentManagement.Domain.IRepositories.ProductLog;
using Org.BouncyCastle.Bcpg;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Command;

public record CreateProductCommandHandler : IRequestHandler<CreateProductCommand, bool>
{
    #region Ctor

    private readonly IProductCommandRepository _commandRepository;
    private readonly IProductQueryRepository _queryRepository;
    private readonly IProductCategoryQueryRepository _categoryQueryRepository;
    private readonly IPlacesQueryRepository _placesQueryRepository;
    private readonly IProductLogCommandRepository _productLogCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IProductCommandRepository commandRepository,
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

    public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        #region Check Valid BarCod 

        if (await _queryRepository.IsExistAny_Product_ByBarCode(request.BarCode, cancellationToken))
        {
            return false;
        }

        #endregion

        #region Check Category

        if (!request.CategoryId.HasValue) return false;

        if (!await _categoryQueryRepository.IsExistAny_Category_ByCategoryId(request.CategoryId.Value, cancellationToken))
        {
            return false;
        }

        #endregion

        #region Check Place

        if (!request.PlaceId.HasValue) return false;

        if (!await _placesQueryRepository.IsExistAny_Place_ById(request.PlaceId.Value, cancellationToken))
            return false;

        #endregion

        #region Fill Product 

        Domain.Entities.Product.Product product = new()
        {
            PlaceId = request.PlaceId.Value,
            CreateDate = DateTime.Now,
            ProductCategoryId = request.CategoryId.Value,
            BarCode = request.BarCode.SanitizeText(),
            Description = request.Description.SanitizeText(),
            EntityCount = request.EntityCount,
            IsDelete = false,
            ProductTitle = request.ProductTitle.SanitizeText(),
            RepostiroyCode = request.RepositoryCode.SanitizeText(),
        };

        #endregion

        //Add Product To Data Base
        await _commandRepository.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        //Add Log for initialed product
        var productLog = new Domain.Entities.ProductLog.ProductLog()
        { 
            UserId = request.UserId,
            Description = "ثبت کالا برای اولین بار",
            PlaceId = request.PlaceId.Value,
            ProductId= product.Id,
            CreateDate= DateTime.Now,
        };
        await _commandRepository.AddProductLog(productLog, cancellationToken);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
