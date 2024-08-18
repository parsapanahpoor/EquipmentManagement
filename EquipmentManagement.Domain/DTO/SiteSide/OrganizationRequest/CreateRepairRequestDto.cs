namespace EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;

public record CreateRepairRequestFormInfoDto
{
    public Entities.Users.User? Employee { get; set; }
    public Entities.Product.Product? Product { get; set; }
    public List<Entities.Users.User>? ListOfUsers { get; set; }
}
