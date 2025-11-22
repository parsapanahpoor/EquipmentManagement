
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.IRepositories.PlaceOfService;

namespace EquipmentManagement.Application.CQRS.SiteSide.PlaceOfServices.Command;

public record EditPlaceOfServiceCommandHandler : IRequestHandler<EditPlaceOfServiceCommand, bool>
{
    #region Ctor 

    private readonly IPlaceOfServicesCommandRepository _PlaceOfServiceCommandRepository;
    private readonly IPlaceOfServicesQueryRepository _PlaceOfServiceQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditPlaceOfServiceCommandHandler(IPlaceOfServicesCommandRepository PlaceOfServiceCommandRepository ,
                                   IPlaceOfServicesQueryRepository PlaceOfServiceQueryRepository ,
                                   IUnitOfWork unitOfWork)
    {
        _PlaceOfServiceCommandRepository = PlaceOfServiceCommandRepository;
        _PlaceOfServiceQueryRepository = PlaceOfServiceQueryRepository;
        _unitOfWork = unitOfWork;   
    }

    #endregion

    public async Task<bool> Handle(EditPlaceOfServiceCommand request, CancellationToken cancellationToken)
    {
        //Get PlaceOfService by Id 
        var PlaceOfService = await _PlaceOfServiceQueryRepository.GetByIdAsync(cancellationToken , request.PlaceOfServiceId);
        if (PlaceOfService == null) return false;

        PlaceOfService.Title = request.PlaceOfServiceTitle;

        _PlaceOfServiceCommandRepository.Update(PlaceOfService);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
