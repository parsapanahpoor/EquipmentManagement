using EquipmentManagement.Domain.DTO.SiteSide.PropertyInquiry;

namespace EquipmentManagement.Application.CQRS.SiteSide.PropertyInquiry.Query;

public record FilterPropertyInquiryDetail_BadgesCountQuery : IRequest<FilterPropertiesInquiry_BadgesCountDTO>
{
    #region properties

    public ulong PlaceId { get; set; }

    public ulong InquiryId { get; set; }

    #endregion
}
