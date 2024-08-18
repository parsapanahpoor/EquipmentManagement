using EquipmentManagement.Domain.DTO.SiteSide.Dashboard;
using EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;
using EquipmentManagement.Domain.Entities.OrganizationRequest;

namespace EquipmentManagement.Domain.IRepositories.OranizationRequest;

public interface IOrganziationRequestQueryRepository
{
    Task<FilterOrganizationRequestsDto> FilterOrganziationRequest(FilterOrganizationRequestsDto filter,
        CancellationToken cancellationToken);

    Task<bool> IsExistAnyOrganizationRequestByRequestType(RequestType requestType,
        CancellationToken cancellationToken);

    Task<OrganizationRequestEntryModel?> FillOrganizationRequestEntryModel(ulong organizationRequestId,
      CancellationToken cancellation);

    Task<OrganziationRequestEntity> GetByIdAsync(CancellationToken cancellationToken, 
        params object[] ids);

    Task<List<RequestDecisionMaker>> Get_RequestDecisionMaker_ByRequestId(ulong requestId,
     CancellationToken cancellationToken);

    Task<List<ulong>?> Get_OrganizationChartsIds_ByRequestId(ulong requestId,
       CancellationToken cancellation);

    Task<bool> IExistAny_DesicionMaker_ForRequest(ulong organziationRequestId,
        CancellationToken cancellationToken);

    Task<List<RepairRequestDto>> FillRepairRequestDto(ulong userId,
        CancellationToken cancellationToken);

    Task<RepairRequest?> GetRepairRequestById(ulong repairReuqestId,
        CancellationToken cancellationToken);

    Task<ExpertVisitorOpinionEntity?> Get_ExpertOpinion_ByRepairRequestId(ulong repairRequestId,
        CancellationToken cancellationToken);

    Task<DecisionRepairRequestRespons> Get_DecisionRepairRequestRespons_ByRequestIdAndUserId(
        ulong requestId,
        ulong userId ,
        CancellationToken cancellationToken);

    Task<bool> IsRequestNotBeFinished(ulong repairRequestId,
        CancellationToken cancellationToken);

    Task<bool> IsExistAnyConfiguration_ForRepairRequest(CancellationToken cancellationToken);
    Task<OrganziationRequestEntity?> GetFirstConfiguration_ForRepairRequest(CancellationToken cancellationToken);
}
