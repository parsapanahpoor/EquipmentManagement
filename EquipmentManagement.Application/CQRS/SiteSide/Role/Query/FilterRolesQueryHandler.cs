using EquipmentManagement.Domain.DTO.SiteSide.Role;
using EquipmentManagement.Domain.IRepositories.Role;

namespace EquipmentManagement.Application.CQRS.SiteSide.Role.Query;

public record FilterRolesQueryHandler : IRequestHandler<FilterRolesQuery, FilterRolesDTO>
{
    #region Ctor

    private readonly IRoleQueryRepository _roleQueryRepository;

    public FilterRolesQueryHandler(IRoleQueryRepository roleQueryRepository)
    {
        _roleQueryRepository = roleQueryRepository;
    }

    #endregion

    public async Task<FilterRolesDTO> Handle(FilterRolesQuery request, CancellationToken cancellationToken)
    {
        return await _roleQueryRepository.FilterRoles(new FilterRolesDTO()
        {
            RoleTitle = request.RoleTitle,
        },
        cancellationToken
        );
    }
}
