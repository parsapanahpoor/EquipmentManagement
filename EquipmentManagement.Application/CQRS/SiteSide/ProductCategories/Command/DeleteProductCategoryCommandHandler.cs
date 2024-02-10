
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Application.Utilities.Security;
using EquipmentManagement.Domain.IRepositories.ProductCategory;

namespace EquipmentManagement.Application.CQRS.SiteSide.ProductCategories.Command;

public record DeleteProductCategoryCommandHandler : IRequestHandler<DeleteProductCategoryCommand, bool>
{
    #region Ctor

    private readonly IProductCategoryCommandRepository _productCategoryCommandRepository;
    private readonly IProductCategoryQueryRepository _productCategoryQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCategoryCommandHandler(IProductCategoryCommandRepository productCategoryCommandRepository,
                                             IProductCategoryQueryRepository productCategoryQueryRepository,
                                             IUnitOfWork unitOfWork)
    {
        _productCategoryCommandRepository = productCategoryCommandRepository;
        _productCategoryQueryRepository = productCategoryQueryRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<bool> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
    {
        //Get Product Category By Id 
        var category = await _productCategoryQueryRepository.GetByIdAsync(cancellationToken, request.ProductCategoryId);
        if (category == null) return false;

        //Edit Fields
        category.IsDelete = true;

        _productCategoryCommandRepository.Update(category);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
