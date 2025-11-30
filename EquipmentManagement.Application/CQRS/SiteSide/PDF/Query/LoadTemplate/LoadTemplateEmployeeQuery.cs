using EquipmentManagement.Application.CQRS.SiteSide.SelfService.Query.ReceiveFoodListReceipt;
using EquipmentManagement.Domain.DTO.SiteSide.Role;
namespace EquipmentManagement.Application.CQRS.SiteSide.Role.Query;

public record LoadTemplateEmployeeQuery(List<ReceiveFoodListReceiptDto> List) : IRequest<string>;
