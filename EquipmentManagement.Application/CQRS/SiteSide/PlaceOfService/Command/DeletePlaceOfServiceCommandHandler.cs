
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.IRepositories.PlaceOfService;

namespace EquipmentManagement.Application.CQRS.SiteSide.PlaceOfServices.Command;

public record DeletePlaceOfServiceCommandHandler : IRequestHandler<DeletePlaceOfServiceCommand, bool>
{
    #region Ctor

    private readonly IPlaceOfServicesQueryRepository _PlaceOfServicesQueryRepository;
    private readonly IPlaceOfServicesCommandRepository _PlaceOfServicesCommandRepository;
    private IUnitOfWork _unitOfWork;

    public DeletePlaceOfServiceCommandHandler(IPlaceOfServicesQueryRepository PlaceOfServicesQueryRepository ,
                                     IPlaceOfServicesCommandRepository PlaceOfServicesCommandRepository , 
                                     IUnitOfWork unitOfWork)
    {
        _PlaceOfServicesCommandRepository = PlaceOfServicesCommandRepository;
        _PlaceOfServicesQueryRepository = PlaceOfServicesQueryRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<bool> Handle(DeletePlaceOfServiceCommand request, CancellationToken cancellationToken)
    {
        //Get PlaceOfService
        var PlaceOfService = await _PlaceOfServicesQueryRepository.GetByIdAsync(cancellationToken , request.PlaceOfServiceId) ;
        if (PlaceOfService == null) return false ;

        //Delete Main PlaceOfService
        PlaceOfService.IsDelete = true;

        _PlaceOfServicesCommandRepository.Update(PlaceOfService) ;

        //Get Sub PlaceOfServices
        var subPlaceOfServices = await _PlaceOfServicesQueryRepository.GetSubPlaceOfServicesByPlaceOfServiceParentId(PlaceOfService.Id , cancellationToken);

        foreach (var subPlaceOfService in subPlaceOfServices)
        {
            //Delete Sub PlaceOfService
            subPlaceOfService.IsDelete = true;

            _PlaceOfServicesCommandRepository.Update(subPlaceOfService);
        }

        await _unitOfWork.SaveChangesAsync() ;

        return true;
    }
}
