using Microsoft.AspNetCore.Http;

namespace EquipmentManagement.Domain.DTO.SiteSide.PropertyInquiry;

public record AddNewExcelFileDTO
{
    #region properties

    public IFormFile ExcelFile { get; set; }

    #endregion
}
