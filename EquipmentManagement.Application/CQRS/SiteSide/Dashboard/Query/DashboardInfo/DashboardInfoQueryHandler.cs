using EquipmentManagement.Domain.DTO.SiteSide.Dashboard;
using EquipmentManagement.Domain.IRepositories.OranizationRequest;

namespace EquipmentManagement.Application.CQRS.SiteSide.Dashboard.Query.DashboardInfo;

public record DashboardInfoQueryHandler(
    IOrganziationRequestQueryRepository organziationRequestQueryRepository) :
    IRequestHandler<DashboardInfoQuery, DashboardDto>
{
    public async Task<DashboardDto> Handle(DashboardInfoQuery request, 
        CancellationToken cancellationToken)
    => new DashboardDto()
    {
        RepairRequest = await organziationRequestQueryRepository.FillRepairRequestDto(request.UserId, cancellationToken)
    };

}

