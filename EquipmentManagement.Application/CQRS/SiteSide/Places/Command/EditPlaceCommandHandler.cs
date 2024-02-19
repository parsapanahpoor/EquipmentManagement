
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.IRepositories.Place;

namespace EquipmentManagement.Application.CQRS.SiteSide.Places.Command;

public record EditPlaceCommandHandler : IRequestHandler<EditPlaceCommand, bool>
{
    #region Ctor 

    private readonly IPlacesCommandRepository _placeCommandRepository;
    private readonly IPlacesQueryRepository _placeQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditPlaceCommandHandler(IPlacesCommandRepository placeCommandRepository ,
                                   IPlacesQueryRepository placeQueryRepository ,
                                   IUnitOfWork unitOfWork)
    {
        _placeCommandRepository = placeCommandRepository;
        _placeQueryRepository = placeQueryRepository;
        _unitOfWork = unitOfWork;   
    }

    #endregion

    public async Task<bool> Handle(EditPlaceCommand request, CancellationToken cancellationToken)
    {
        //Get Place by Id 
        var place = await _placeQueryRepository.GetByIdAsync(cancellationToken , request.PlaceId);
        if (place == null) return false;

        place.PlaceTitle = request.PlaceTitle;

        _placeCommandRepository.Update(place);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
