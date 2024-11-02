using EquipmentManagement.Domain.Entities.OrganizationRequest.AbolitionRequest;
using System.Web.Mvc;

namespace EquipmentManagement.Application.Utilities;

public class GetFilteredAbolitionRequest
{
    public IEnumerable<SelectListItem> GetFilteredAbolitionRequestStates()
    {
        return Enum.GetValues(typeof(AbolitionRequestState))
            .Cast<AbolitionRequestState>()
            .Where(state => state != AbolitionRequestState.WaitingForManagerRespons &&
                            state != AbolitionRequestState.WaitingForProductsCollectorRespons)
            .Select(state => new SelectListItem
            {
                Value = ((int)state).ToString(),
                Text = state.GetDisplayName() 
            });
    }
}
