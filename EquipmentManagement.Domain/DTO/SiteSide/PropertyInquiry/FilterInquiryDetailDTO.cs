using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.Entities.PropertyInquiry;

namespace EquipmentManagement.Domain.DTO.SiteSide.PropertyInquiry;

public class FilterInquiryDetailDTO : BasePaging<FilterInquiryDetail>
{
    #region properties

    public ulong PropertyInquiryId { get; set; }

    public string? PlaceName{ get; set; }

    public string? CategoryName { get; set; }

    #endregion
}

public class FilterInquiryDetail
{
    #region properties

    public ulong InquiryDetailId { get; set; }

    public ulong PropertyId { get; set; }

    public string PropertyTitle { get; set; }

    public string RfId { get; set; }

    public string CategoryTitle { get; set; }

    public string PlaceTitle { get; set; }

    #endregion
}
