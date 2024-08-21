using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Application.Utilities.Security;
using EquipmentManagement.Domain.Entities.OrganizationRequest;
using EquipmentManagement.Domain.IRepositories.OranizationRequest;
using EquipmentManagement.Domain.IRepositories.OrganizationChart;
using EquipmentManagement.Domain.IRepositories.User;

namespace EquipmentManagement.Application.CQRS.SiteSide.RepairRequest.Command.NewFolder;

public record ChangeRepairRequestStateCommand(
        ulong userId,
        ulong repairRequestId,
        RepairRequestState requestState,
        bool outSource,
        string description) :
    IRequest<bool>;

public record ChangeRepairRequestStateCommandHandler(
    IUserQueryRepository UserQueryRepository,
    IOrganziationRequestQueryRepository OrganziationRequestQueryRepository,
    IOrganziationRequestCommandRepository OrganziationRequestCommandRepository,
    IOrganizationChartQueryRepository OrganziationChartQueryRepository,
    IUnitOfWork UnitOfWork) :
    IRequestHandler<ChangeRepairRequestStateCommand, bool>
{
    public async Task<bool> Handle(ChangeRepairRequestStateCommand request,
        CancellationToken cancellationToken)
    {
        //Get Reuqest By Id 
        var repairRequest = await OrganziationRequestQueryRepository.GetRepairRequestById(request.repairRequestId,
            cancellationToken);

        if (repairRequest == null)
            return false;

        //Check That is current user in visit expert of this request 
        var expertOpinion = await OrganziationRequestQueryRepository.Get_ExpertOpinion_ByRepairRequestId(request.repairRequestId,
            cancellationToken);

        if (expertOpinion == null ||
            expertOpinion.ResponsType == ExpertVisitorResponsType.Reject)
            return false;

        if (expertOpinion.ResponsType == ExpertVisitorResponsType.WaitingForRespons)
        {
            if (expertOpinion.ExpertUserId != request.userId)
                return false;

            //Change Expert Visitor Opinion 
            expertOpinion.UpdateDate = DateTime.Now;
            expertOpinion.Description = request.description.SanitizeText();
            expertOpinion.ResponsType = request.requestState == RepairRequestState.Accepted ?
                ExpertVisitorResponsType.Accepted :
                ExpertVisitorResponsType.Reject;

            OrganziationRequestCommandRepository.Update_ExpertVisitorResponse(expertOpinion);

            if (expertOpinion.ResponsType == ExpertVisitorResponsType.Accepted)
            {
                repairRequest.ExpertVisitorRepairRequestState = RepairRequestState.Accepted;
                repairRequest.IsNeedToOutSource = request.outSource;
                OrganziationRequestCommandRepository.Update_RepairRequest(repairRequest);
            }
            if (expertOpinion.ResponsType == ExpertVisitorResponsType.Reject)
            {
                repairRequest.ExpertVisitorRepairRequestState = RepairRequestState.Reject;
                repairRequest.IsNeedToOutSource = request.outSource;
                OrganziationRequestCommandRepository.Update_RepairRequest(repairRequest);
            }

            //Add Record for Repair Request Decisioners
            var userSelectedOrganizationChart = await OrganziationChartQueryRepository.Get_RepairRequestDesiciners(cancellationToken);
            if (userSelectedOrganizationChart != null && userSelectedOrganizationChart.Any())
            {
                foreach (var desicionoresUserId in userSelectedOrganizationChart)
                {
                    await OrganziationRequestCommandRepository.Add_DecisionRepairRequestRespons(
                        new DecisionRepairRequestRespons()
                        {
                            EmployeeUserId = desicionoresUserId.UserId,
                            OrganizationChartId = desicionoresUserId.OrganizationChartAggregateId,
                            RejectDescription = null,
                            IsDelete = false,
                            Response = DecisionRepairRespons.WaitingForRespons,
                            Sign = 1234,
                            RepariRequestId = request.repairRequestId,
                        }, cancellationToken);
                }
            }

            await UnitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        if (expertOpinion.ResponsType == ExpertVisitorResponsType.Accepted)
        {
            var decisionsResponses = await OrganziationRequestQueryRepository
                .Get_DecisionRepairRequestRespons_ByRequestIdAndUserId(request.repairRequestId,
                request.userId,
                cancellationToken);

            if (decisionsResponses is null || !decisionsResponses.Any())
                return false;

            //Update Decision Reponse
            if (decisionsResponses.Any(p => p.EmployeeUserId == request.userId && p.Response == DecisionRepairRespons.WaitingForRespons))
            {
                foreach (var decisionsRespons in decisionsResponses.Where(p => p.EmployeeUserId == request.userId && p.Response == DecisionRepairRespons.WaitingForRespons))
                {
                    decisionsRespons.UpdateDate = DateTime.Now;
                    decisionsRespons.RejectDescription = request.description;
                    decisionsRespons.Response = request.requestState == 
                        RepairRequestState.Accepted ?
                        DecisionRepairRespons.Accepted :
                        DecisionRepairRespons.Reject;

                    OrganziationRequestCommandRepository.Update_DecisionRepairRequestRespons(decisionsRespons);
                }
            }

            if (request.requestState == RepairRequestState.Accepted)
            {
                if (!await OrganziationRequestQueryRepository.IsRequestNotBeFinished(request.repairRequestId,
                    cancellationToken))
                {
                    repairRequest.DesicionMakersRepairRequestState = RepairRequestState.Accepted;
                    OrganziationRequestCommandRepository.Update_RepairRequest(repairRequest);
                }
            }
            if (request.requestState == RepairRequestState.Reject)
            {
                repairRequest.DesicionMakersRepairRequestState = RepairRequestState.Reject;
                OrganziationRequestCommandRepository.Update_RepairRequest(repairRequest);
            }

            await UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        return true;
    }
}
