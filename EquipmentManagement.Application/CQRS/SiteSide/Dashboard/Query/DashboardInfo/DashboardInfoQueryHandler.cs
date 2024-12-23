﻿using EquipmentManagement.Domain.DTO.SiteSide.Dashboard;
using EquipmentManagement.Domain.IRepositories.OranizationRequest;
using EquipmentManagement.Domain.IRepositories.ProductLog;
using System.Collections.Generic;

namespace EquipmentManagement.Application.CQRS.SiteSide.Dashboard.Query.DashboardInfo;

public record DashboardInfoQueryHandler(
    IOrganziationRequestQueryRepository organziationRequestQueryRepository , 
    IProductLogQueryRepository productLogQueryRepository) :
    IRequestHandler<DashboardInfoQuery, DashboardDto>
{
    public async Task<DashboardDto> Handle(
        DashboardInfoQuery request,
        CancellationToken cancellationToken)
    {
        var repairRequests = new List<RepairRequestDto>();
        repairRequests.AddRange(await organziationRequestQueryRepository.FillRepairRequestDto(request.UserId, cancellationToken));
        repairRequests.AddRange(await organziationRequestQueryRepository.GetLastestNewRequestAsDecisinorsForCurrentUser(request.UserId, cancellationToken));

        var abolitionRequests = new List<AbolitionRequestDto>();
        abolitionRequests.AddRange(await organziationRequestQueryRepository.FillAbolitionRequestDto(request.UserId, cancellationToken));
        abolitionRequests.AddRange(await organziationRequestQueryRepository.GetNotifForDecisinor(request.UserId, cancellationToken));

        var productLog = new List<Domain.Entities.ProductLog.ProductLog>();
        productLog.AddRange(await productLogQueryRepository.GetLastestProductLogs(cancellationToken));

        return new DashboardDto()
        {
            RepairRequest = repairRequests.DistinctBy(p=> p.RepairRequestId).ToList(),
            AbolitionRequest = abolitionRequests.DistinctBy(p=> p.AbolitionRequestId).ToList(),
            ProductLogs = productLog
        };
    }
}

