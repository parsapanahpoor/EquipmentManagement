using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.Entities.OrganizationRequest;
using EquipmentManagement.Domain.IRepositories.OranizationRequest;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Command.Create;

public class OrganizationRequestCreateCommandHandler(
    IOrganziationRequestCommandRepository _organizationRequestCommandRepository,
    IOrganziationRequestQueryRepository _organziationRequestQueryRepository,
    IUnitOfWork _unitOfWork) :
    IRequestHandler<OrganizationRequestCreateCommand, bool>
{
    public async Task<bool> Handle(OrganizationRequestCreateCommand request, CancellationToken cancellationToken)
    {
        if (await _organziationRequestQueryRepository.IsExistAnyOrganizationRequestByRequestType(request.data.RequestType,
            cancellationToken))
            return false;

        await _organizationRequestCommandRepository.AddAsync(new OrganziationRequestEntity()
        {
            IsActive = request.data.IsActive,
            RequestType = request.data.RequestType,
        }, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}