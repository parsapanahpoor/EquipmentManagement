using EquipmentManagement.Domain.DTO.SiteSide.Product;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Query;

public class FilterProductForExcelFilesQuery : IRequest<List<FilterProductForExcelFilesDTO>>
{
    #region properties

    public FilterProductForExcelFilesDTO filter { get; set; }

    #endregion
}