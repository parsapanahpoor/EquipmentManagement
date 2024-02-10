
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Application.Utilities.Security;
using EquipmentManagement.Domain.IRepositories.ProductCategory;

namespace EquipmentManagement.Application.CQRS.SiteSide.ProductCategories.Command;

public record EditProductCategoryCommandHandler : IRequestHandler<EditProductCategoryCommand, bool>
{
    #region Ctor

    private readonly IProductCategoryCommandRepository _productCategoryCommandRepository;
    private readonly IProductCategoryQueryRepository _productCategoryQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditProductCategoryCommandHandler(IProductCategoryCommandRepository productCategoryCommandRepository,
                                             IProductCategoryQueryRepository productCategoryQueryRepository ,
                                             IUnitOfWork unitOfWork) 
    {
        _productCategoryCommandRepository = productCategoryCommandRepository;
        _productCategoryQueryRepository = productCategoryQueryRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<bool> Handle(EditProductCategoryCommand request, CancellationToken cancellationToken)
    {
        //Get Product Category By Id 
        var category = await _productCategoryQueryRepository.GetByIdAsync(cancellationToken , request.ProductCategoryId);
        if (category == null) return false;

        //Edit Fields
        category.CategoryTitle = request.Title.SanitizeText();

        _productCategoryCommandRepository.Update(category);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
