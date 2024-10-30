namespace EquipmentManagement.Domain.DTO.SiteSide.ProductLog;

public record CreateProductLogDto(
    ulong UserId , 
    ulong ProductId , 
    ulong? PlaceId , 
    string? Description);
