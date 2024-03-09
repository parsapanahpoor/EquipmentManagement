using EquipmentManagement.Domain.DTO.Common;
using System.ComponentModel.DataAnnotations;
namespace EquipmentManagement.Domain.DTO.SiteSide.PropertyInquiry;

public class FilterSystemPropertyInquiriesDTO : BasePaging<Domain.Entities.PropertyInquiry.PropertyInquiry>
{
    #region properties

    [RegularExpression(@"^\d{4}\/(0?[1-9]|1[012])\/(0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = "The entered date is not valid")]
    public string? StartTime { get; set; }

    [RegularExpression(@"^\d{4}\/(0?[1-9]|1[012])\/(0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = "The entered date is not valid")]
    public string? EndTime { get; set; }

    #endregion
}
