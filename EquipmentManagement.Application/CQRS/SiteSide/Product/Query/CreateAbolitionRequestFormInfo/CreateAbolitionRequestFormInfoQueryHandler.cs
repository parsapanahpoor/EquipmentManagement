using EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;
using EquipmentManagement.Domain.IRepositories.Product;
using EquipmentManagement.Domain.IRepositories.User;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Query.CreateAbolitionRequestFormInfo;

public record CreateAbolitionRequestFormInfoQueryHandler(
    IUserQueryRepository UserQueryRepository,
    IProductQueryRepository ProductQueryRepository) :
    IRequestHandler<CreateAbolitionRequestFormInfoQuery, CreateAbolitionRequestFormInfoDto>
{
    public async Task<CreateAbolitionRequestFormInfoDto> Handle(CreateAbolitionRequestFormInfoQuery request, CancellationToken cancellationToken)
    => new CreateAbolitionRequestFormInfoDto()
    {
        Employee = await UserQueryRepository.GetByIdAsync(cancellationToken , request.UserId),
        Product = await ProductQueryRepository.GetByIdAsync (cancellationToken , request.ProductId),
        ListOfUsers = await UserQueryRepository.ListOfUsers(cancellationToken)
    };
}
