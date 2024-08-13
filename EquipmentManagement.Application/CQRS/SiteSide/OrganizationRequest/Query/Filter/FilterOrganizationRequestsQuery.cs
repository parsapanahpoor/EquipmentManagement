﻿using EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Query.Filter;

public class FilterOrganizationRequestsQuery :
    IRequest<FilterOrganizationRequestsDto>
{
    public FilterOrganizationRequestsDto? Filter { get; set; }
}
