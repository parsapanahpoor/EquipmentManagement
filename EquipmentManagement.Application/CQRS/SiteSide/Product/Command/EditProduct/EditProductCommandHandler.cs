
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Application.Convertors;
using EquipmentManagement.Application.Extensions;
using EquipmentManagement.Application.Generators;
using EquipmentManagement.Application.Security;
using EquipmentManagement.Application.StaticTools;
using EquipmentManagement.Domain.IRepositories.Place;
using EquipmentManagement.Domain.IRepositories.Product;
using EquipmentManagement.Domain.IRepositories.ProductCategory;
using EquipmentManagement.Domain.IRepositories.ProductLog;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Command.EditProduct;

public record EditProductCommandHandler : IRequestHandler<EditProductCommand, bool>
{
    #region Ctor

    private readonly IProductCommandRepository _commandRepository;
    private readonly IProductQueryRepository _queryRepository;
    private readonly IProductCategoryQueryRepository _categoryQueryRepository;
    private readonly IPlacesQueryRepository _placesQueryRepository;
    private readonly IProductLogCommandRepository _productLogCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditProductCommandHandler(IProductCommandRepository commandRepository,
                                       IProductQueryRepository queryRepository,
                                       IProductCategoryQueryRepository categoryQueryRepository,
                                       IPlacesQueryRepository placesQueryRepository,
                                       IProductLogCommandRepository productLogCommandRepository,
                                       IUnitOfWork unitOfWork)
    {
        _categoryQueryRepository = categoryQueryRepository;
        _commandRepository = commandRepository;
        _queryRepository = queryRepository;
        _placesQueryRepository = placesQueryRepository;
        _productLogCommandRepository = productLogCommandRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<bool> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        //Get Old product by id 
        var product = await _queryRepository.GetByIdAsync(cancellationToken, request.EditProductDTO.ProductId);
        if (product == null) return false;

        //Check Valid BarCod  
        if (await _queryRepository.IsExistAny_Product_ByBarCode(request.EditProductDTO.BarCode, product.Id, cancellationToken))
            return false;

        //Check Category
        if (!await _categoryQueryRepository.IsExistAny_Category_ByCategoryId(request.EditProductDTO.CategoryId, cancellationToken))
            return false;

        //Check Place
        if (!await _placesQueryRepository.IsExistAny_Place_ById(request.EditProductDTO.PlaceId, cancellationToken))
            return false;

        //Update Prodperties
        product.PlaceId = request.EditProductDTO.PlaceId;
        product.UpdateDate = DateTime.Now;
        product.ProductCategoryId = request.EditProductDTO.CategoryId;
        product.BarCode = request.EditProductDTO.BarCode;
        product.Description = request.EditProductDTO.Description;
        product.ProductTitle = request.EditProductDTO.ProductTitle;
        product.RepostiroyCode = request.EditProductDTO.RepositoryCode;
        product.Brand = request.EditProductDTO.BrandName;
        product.InvoiceNumber = request.EditProductDTO.InvoiceNumber;
        product.InvoiceDateTime = !string.IsNullOrEmpty(request.EditProductDTO.InvoiceDateTime) ?
                                   request.EditProductDTO.InvoiceDateTime.ToMiladiDateTime() : 
                                   null;

        product.InvoiceUniqueNumber = (product.InvoiceNumber.HasValue && product.InvoiceDateTime.HasValue) ?
                                       $"{request.EditProductDTO.InvoiceNumber}" +
                                       $"{product.InvoiceDateTime.Value.Year}" +
                                       $"{product.InvoiceDateTime.Value.Month}" +
                                       $"{product.InvoiceDateTime.Value.Day}" :
                                       null;

        #region Invoice Image

        if (request.EditProductDTO.InvoiceImage != null && request.EditProductDTO.InvoiceImage.IsImage())
        {
            if (!string.IsNullOrEmpty(product.InvoiceImage))
            {
                product.InvoiceImage.DeleteImage(FilePaths.InvoiceImagePathServer, FilePaths.InvoiceImagePathThumbServer);
            }

            var imageName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(request.EditProductDTO.InvoiceImage.FileName);
            request.EditProductDTO.InvoiceImage.AddImageToServer(imageName, FilePaths.InvoiceImagePathServer, 270, 270, FilePaths.InvoiceImagePathThumbServer);
            product.InvoiceImage = imageName;
        }

        #endregion

        //Add Product Log
        var productLog = new Domain.Entities.ProductLog.ProductLog()
        {
            UserId = request.UserId!.Value,
            ProductId = request.EditProductDTO.ProductId,
            PlaceId = request.EditProductDTO.PlaceId,
            Description = "ویرایش کالا"
        };

        await _commandRepository.AddProductLog(productLog,
            cancellationToken);

        //Update Product
        _commandRepository.Update(product);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
