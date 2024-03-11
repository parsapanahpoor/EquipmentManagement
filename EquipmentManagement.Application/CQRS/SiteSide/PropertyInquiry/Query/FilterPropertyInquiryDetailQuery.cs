using EquipmentManagement.Domain.DTO.SiteSide.PropertyInquiry;

namespace EquipmentManagement.Application.CQRS.SiteSide.PropertyInquiry.Query;

public class FilterPropertyInquiryDetailQuery : IRequest<FilterInquiryDetailDTO>
{
    #region properties

    public FilterInquiryDetailDTO FilterInquiryDetailDTO { get; set; }

    #endregion
}
