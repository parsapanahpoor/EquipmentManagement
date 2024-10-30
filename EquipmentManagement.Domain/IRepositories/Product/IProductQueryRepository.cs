using EquipmentManagement.Domain.DTO.SiteSide.Product;

namespace EquipmentManagement.Domain.IRepositories.Product;

public interface IProductQueryRepository
{
    #region Site Side

    Task<FiltreProductRepairRequestDto> FiltreProductRepairRequest(
        FiltreProductRepairRequestDto filter,
        CancellationToken cancellationToken);

    Task<FilterProductDTO> FilterProducts(FilterProductDTO filter);

    Task<List<FilterProductForExcelFilesDTO>> FilterProductsForExcelFiles(FilterProductForExcelFilesDTO filter);

    Task<bool> IsExistAny_Product_ByBarCode(string barCode,CancellationToken cancellationToken);

    Task<bool> IsExistAny_Product_ByBarCode(string barCode,
                                                         ulong productId,
                                                         CancellationToken cancellationToken);

    Task<ProductDetailDTO?> FillProductDetailDTO(ulong productId, CancellationToken cancellationToken);

    Task<Domain.Entities.Product.Product> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);

    Task<bool> IsExist_Product_ByRfId(string rfId, CancellationToken cancellationToken);

    Task<Domain.Entities.Product.Product?> GetProduct_Product_ByRfId(string rfId, CancellationToken cancellationToken);

    Task<EditProductDTO?> Fill_EditProductDTO(ulong productId,
                                              CancellationToken cancellationToken);

    #endregion
}
