using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.IRepositories.OranizationRequest;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Command.Delete;

public record DeleteOrganizationRequestCommandHandler :
    IRequestHandler<DeleteOrganizationRequestCommand, bool>
{
    #region Ctor

    private readonly IOrganziationRequestCommandRepository _organizationRequestCommandRepository;
    private readonly IOrganziationRequestQueryRepository _organizationRequestQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrganizationRequestCommandHandler(IOrganziationRequestCommandRepository organizationRequestCommandRepository,
        IOrganziationRequestQueryRepository organizationRequestQueryRepository,
        IUnitOfWork unitOfWork)
    {
        _organizationRequestQueryRepository = organizationRequestQueryRepository;
        _organizationRequestCommandRepository = organizationRequestCommandRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<bool> Handle(DeleteOrganizationRequestCommand request, CancellationToken cancellationToken)
    {
        //Get Organization Request by Id 
        var organizationRequest = await _organizationRequestQueryRepository.GetByIdAsync(cancellationToken,
            request.OrganizationRequestId);
        if (organizationRequest == null) return false;

        organizationRequest.IsDelete = true;

        _organizationRequestCommandRepository.Update(organizationRequest);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
