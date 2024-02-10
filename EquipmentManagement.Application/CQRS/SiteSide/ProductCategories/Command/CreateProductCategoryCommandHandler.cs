
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.Entities.ProductCategory;
using EquipmentManagement.Domain.IRepositories.ProductCategory;

namespace EquipmentManagement.Application.CQRS.SiteSide.ProductCategories.Command;

public record CreateProductCategoryCommandHandler : IRequestHandler<CreateProductCategoryCommand, bool>
{
    #region Ctor

    private readonly IProductCategoryCommandRepository _productCategoryCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCategoryCommandHandler(IProductCategoryCommandRepository productCategoryCommandRepository , 
                                               IUnitOfWork unitOfWork)
    {
        _productCategoryCommandRepository = productCategoryCommandRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<bool> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        ProductCategory productCategory = new ProductCategory()
        {
            CategoryTitle = request.Title,
            BusinessKey = new Random().Next(10000, 999999).ToString(),
        };

        await _productCategoryCommandRepository.AddAsync(productCategory, cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
