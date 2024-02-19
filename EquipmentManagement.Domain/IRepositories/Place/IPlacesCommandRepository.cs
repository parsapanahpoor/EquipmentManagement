namespace EquipmentManagement.Domain.IRepositories.Place;

public interface IPlacesCommandRepository
{
    Task AddAsync(Domain.Entities.Places.Place place, CancellationToken cancellationToken);

    void Update(Entities.Places.Place place);
}
