namespace EquipmentManagement.Domain.DTO.SiteSide.OrganizationChart;

public record OrganizationChartEntryModel
{
    public ulong? OrganizationChartId { get; set; }
    public string? Title { get; set; }
    public ulong? ParentId { get; set; }
    public string? Description { get; set; }
}
