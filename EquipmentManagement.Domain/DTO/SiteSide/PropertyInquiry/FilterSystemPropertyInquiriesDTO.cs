using EquipmentManagement.Domain.DTO.Common;
using System.ComponentModel.DataAnnotations;
namespace EquipmentManagement.Domain.DTO.SiteSide.PropertyInquiry;

public class FilterSystemPropertyInquiriesDTO : BasePaging<PropertyInquiryDTO>
{
    #region properties

    [RegularExpression(@"^\d{4}\/(0?[1-9]|1[012])\/(0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = "The entered date is not valid")]
    public string? StartTime { get; set; }

    [RegularExpression(@"^\d{4}\/(0?[1-9]|1[012])\/(0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = "The entered date is not valid")]
    public string? EndTime { get; set; }

    #endregion
}

public class PropertyInquiryDTO
{
    #region properties

    public ulong InquiryId { get; set; }

    public ulong UserId { get; set; }

    public string Username { get; set; }

    public string ExcelFile { get; set; }

    public DateTime CreateDate { get; set; }

    #endregion
}