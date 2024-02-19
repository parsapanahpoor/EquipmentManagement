using EquipmentManagement.Domain.DTO.SiteSide.Places;
using EquipmentManagement.Domain.IRepositories.Place;

namespace EquipmentManagement.Application.CQRS.SiteSide.Places.Query;

public record EditPlaceQueryHandler : IRequestHandler<EditPlaceQuery, EditPlaceDTO>
{
    #region Ctor

    private readonly IPlacesQueryRepository _placesQueryRepository;

    public EditPlaceQueryHandler(IPlacesQueryRepository placesQueryRepository)
    {
        _placesQueryRepository = placesQueryRepository;
    }

    #endregion

    public async Task<EditPlaceDTO?> Handle(EditPlaceQuery request, CancellationToken cancellationToken)
    {
        //Get Place 
        var place = await _placesQueryRepository.GetByIdAsync(cancellationToken , request.placeId);
        if (place == null) return null;

        return new EditPlaceDTO()
        {
            PlaceId = place.Id,
            PlaceTitle = place.PlaceTitle,
            ParentId = place.ParentId,
        };
    }
}
