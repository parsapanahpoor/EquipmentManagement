using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.IRepositories.OranizationRequest;
using EquipmentManagement.Domain.IRepositories.Product;
using EquipmentManagement.Domain.IRepositories.User;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Command.CreateRepairRequest;

public record CreateRepairRequestCommandHandler(
    IUserQueryRepository userQueryRepository,
    IProductQueryRepository productQueryRepository,
    IOrganziationRequestQueryRepository organziationRequestQueryRepository , 
    IOrganziationRequestCommandRepository organziationRequestCommandRepository , 
    IUnitOfWork unitOfWork
    ) :
    IRequestHandler<CreateRepairRequestCommand, CreateRepairRequestCommandRespons>
{
    public async Task<CreateRepairRequestCommandRespons> Handle(CreateRepairRequestCommand request, CancellationToken cancellationToken)
    {
        //Check That is exist any configuration 
        var requestConfiguration = await organziationRequestQueryRepository.GetFirstConfiguration_ForRepairRequest(cancellationToken);
        if (requestConfiguration == null) 
            return CreateRepairRequestCommandRespons.DosentConfig;

        //Check That Exist Any  Desicion For This Request 
        if (!await organziationRequestQueryRepository.IExistAny_DesicionMaker_ForRequest(requestConfiguration.Id ,  
            cancellationToken))
            return CreateRepairRequestCommandRespons.DosentConfig;

        var repairRequest = new Domain.Entities.OrganizationRequest.RepairRequest()
        {
            Description = request.Description,
            EmployeeUserId = request.EmployeeId,
            ProductId = request.ProductId,
            ExpertVisitorRepairRequestState = Domain.Entities.OrganizationRequest.RepairRequestState.WaitingForRespons,
            DesicionMakersRepairRequestState = Domain.Entities.OrganizationRequest.RepairRequestState.WaitingForRespons,
            IsNeedToOutSource = false,
        };

        await organziationRequestCommandRepository.Add_RepairRequest(repairRequest , cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await organziationRequestCommandRepository.Add_ExpertVisitorRequest(new Domain.Entities.OrganizationRequest.ExpertVisitorOpinionEntity()
        {
            ExpertUserId = request.ExpertUserId,
            Description = null , 
            RepairRequestId = repairRequest.Id , 
            ResponsType = Domain.Entities.OrganizationRequest.ExpertVisitorResponsType.WaitingForRespons , 
        } , cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        //Can Send SMS To The Expert

        return CreateRepairRequestCommandRespons.Success;
    }
}
