using EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;
using EquipmentManagement.Domain.IRepositories.Product;
using EquipmentManagement.Domain.IRepositories.User;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Query.CreateRepairRequestFormInfo;

public record CreateRepairRequestFormInfoQueryHandler(
    IUserQueryRepository UserQueryRepository,
    IProductQueryRepository ProductQueryRepository) :
    IRequestHandler<CreateRepairRequestFormInfoQuery, CreateRepairRequestFormInfoDto>
{
    public async Task<CreateRepairRequestFormInfoDto> Handle(CreateRepairRequestFormInfoQuery request, CancellationToken cancellationToken)
    => new CreateRepairRequestFormInfoDto()
    {
        Employee = await UserQueryRepository.GetByIdAsync(cancellationToken , request.UserId),
        Product = await ProductQueryRepository.GetByIdAsync (cancellationToken , request.ProductId),
        ListOfUsers = await UserQueryRepository.ListOfUsers(cancellationToken)
    };
}
