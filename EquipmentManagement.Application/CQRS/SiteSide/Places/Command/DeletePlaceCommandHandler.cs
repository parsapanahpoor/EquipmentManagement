
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.IRepositories.Place;

namespace EquipmentManagement.Application.CQRS.SiteSide.Places.Command;

public record DeletePlaceCommandHandler : IRequestHandler<DeletePlaceCommand, bool>
{
    #region Ctor

    private readonly IPlacesQueryRepository _placesQueryRepository;
    private readonly IPlacesCommandRepository _placesCommandRepository;
    private IUnitOfWork _unitOfWork;

    public DeletePlaceCommandHandler(IPlacesQueryRepository placesQueryRepository ,
                                     IPlacesCommandRepository placesCommandRepository , 
                                     IUnitOfWork unitOfWork)
    {
        _placesCommandRepository = placesCommandRepository;
        _placesQueryRepository = placesQueryRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<bool> Handle(DeletePlaceCommand request, CancellationToken cancellationToken)
    {
        //Get Place
        var place = await _placesQueryRepository.GetByIdAsync(cancellationToken , request.placeId) ;
        if (place == null) return false ;

        //Delete Main Place
        place.IsDelete = true;

        _placesCommandRepository.Update(place) ;

        //Get Sub Places
        var subPlaces = await _placesQueryRepository.GetSubPlacesByPlaceParentId(place.Id , cancellationToken);

        foreach (var subplace in subPlaces)
        {
            //Delete Sub Place
            subplace.IsDelete = true;

            _placesCommandRepository.Update(subplace);
        }

        await _unitOfWork.SaveChangesAsync() ;

        return true;
    }
}
