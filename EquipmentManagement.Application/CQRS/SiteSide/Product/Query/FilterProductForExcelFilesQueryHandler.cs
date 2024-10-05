using EquipmentManagement.Domain.DTO.SiteSide.Product;
using EquipmentManagement.Domain.IRepositories.Product;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Query;

public record FilterProductForExcelFilesQueryHandler : IRequestHandler<FilterProductForExcelFilesQuery, List<FilterProductForExcelFilesDTO>>
{
    #region Ctor

    private readonly IProductQueryRepository _productQueryRepository;

    public FilterProductForExcelFilesQueryHandler(IProductQueryRepository productQueryRepository)
    {
        _productQueryRepository = productQueryRepository;
    }

    #endregion

    public async Task<List<FilterProductForExcelFilesDTO>> Handle(FilterProductForExcelFilesQuery request, CancellationToken cancellationToken)
    {
        return await _productQueryRepository.FilterProductsForExcelFiles(request.filter);
    }
}
