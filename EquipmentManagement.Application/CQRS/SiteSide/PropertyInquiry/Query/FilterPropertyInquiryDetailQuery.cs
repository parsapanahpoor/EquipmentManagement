﻿using EquipmentManagement.Domain.DTO.SiteSide.PropertyInquiry;

namespace EquipmentManagement.Application.CQRS.SiteSide.PropertyInquiry.Query;

public class FilterPropertyInquiryDetailQuery : IRequest<List<FilterInquiryDetail>>
{
    #region properties

    public FilterInquiryDetailDTO FilterInquiryDetailDTO { get; set; }

    #endregion
}
