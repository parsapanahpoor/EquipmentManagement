
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.IRepositories.OranizationRequest;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Command.Edit;

public record EditOrganizationRequestCommandHandler : IRequestHandler<EditOrganizationRequestCommand, bool>
{
    #region Ctor

    private readonly IOrganziationRequestCommandRepository _organizationRequestCommandRepository;
    private readonly IOrganziationRequestQueryRepository _organizationRequestQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditOrganizationRequestCommandHandler(IOrganziationRequestCommandRepository organizationRequestCommandRepository,
        IOrganziationRequestQueryRepository organizationRequestQueryRepository,
        IUnitOfWork unitOfWork)
    {
        _organizationRequestQueryRepository = organizationRequestQueryRepository;
        _organizationRequestCommandRepository = organizationRequestCommandRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<bool> Handle(EditOrganizationRequestCommand request, CancellationToken cancellationToken)
    {
        //Get Organization Request by Id 
        var organizationRequest = await _organizationRequestQueryRepository.GetByIdAsync(cancellationToken,
            request.data.Id!.Value);
        if (organizationRequest == null) 
            return false;

        organizationRequest.RequestType = request.data.RequestType;
        organizationRequest.IsActive = request.data.IsActive;

        _organizationRequestCommandRepository.Update(organizationRequest);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
