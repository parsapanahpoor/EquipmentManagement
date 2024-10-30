
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.IRepositories.Product;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Command;

public record DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    #region Ctor

    private readonly IProductQueryRepository _productQueryRepository;
    private readonly IProductCommandRepository _productCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IProductQueryRepository productQueryRepository ,
                                       IProductCommandRepository productCommandRepository ,
                                       IUnitOfWork unitOfWork)
    {
        _productCommandRepository = productCommandRepository;
        _productQueryRepository = productQueryRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        //Get Product By Id 
        var product = await _productQueryRepository.GetByIdAsync(cancellationToken , request.ProductId);
        if (product == null) return false;

        product.IsDelete = true;

        //Update Product 
        _productCommandRepository.Update(product);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
