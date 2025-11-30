using EquipmentManagement.Application.Service;
using EquipmentManagement.Domain.DTO.SiteSide.Role;
using EquipmentManagement.Domain.Entities.Account;
using EquipmentManagement.Domain.IRepositories.Role;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.CQRS.SiteSide.Role.Query;

public record GetPDFCustomEmployeeQueryHandler(ISender? _mediator, IPDFService PDFService) : IRequestHandler<GetPDFCustomEmployeeQuery, byte[]>
{
    #region Ctor



    #endregion

    public async Task<byte[]> Handle(GetPDFCustomEmployeeQuery request, CancellationToken cancellationToken)
    {
        var Text = await _mediator.Send(new LoadTemplateEmployeeQuery(request.List));
        var result =await PDFService.GeneratePdf(Guid.NewGuid().ToString() + ".pdf", Text);
        return result;

    }
}
