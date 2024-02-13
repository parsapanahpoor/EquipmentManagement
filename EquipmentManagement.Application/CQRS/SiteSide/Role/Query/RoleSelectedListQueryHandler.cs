using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.IRepositories.Role;

namespace EquipmentManagement.Application.CQRS.SiteSide.Role.Query;

internal class RoleSelectedListQueryHandler : IRequestHandler<RoleSelectedListQuery, List<SelectListViewModel>>
{
    #region Ctor

    private readonly IRoleQueryRepository _roleQueryRepository;

    public RoleSelectedListQueryHandler(IRoleQueryRepository roleQueryRepository)
    {
        _roleQueryRepository = roleQueryRepository;
    }

    #endregion

    public async Task<List<SelectListViewModel>> Handle(RoleSelectedListQuery request, CancellationToken cancellationToken)
    {
        return await _roleQueryRepository.GetSelectRolesList(cancellationToken);
    }
}
