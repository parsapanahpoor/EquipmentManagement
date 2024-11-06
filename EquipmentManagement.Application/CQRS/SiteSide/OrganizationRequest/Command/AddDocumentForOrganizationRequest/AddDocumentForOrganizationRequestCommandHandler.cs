using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Application.Extensions;
using EquipmentManagement.Application.Generators;
using EquipmentManagement.Application.Security;
using EquipmentManagement.Application.StaticTools;
using EquipmentManagement.Domain.Entities.OrganizationRequest;
using EquipmentManagement.Domain.IRepositories.OranizationRequest;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Command.AddDocumentForOrganizationRequest;

public record AddDocumentForOrganizationRequestCommandHandler(
    IOrganziationRequestQueryRepository organizationQueryRepository , 
    IOrganziationRequestCommandRepository organiziationRequestCommandRepository , 
    IUnitOfWork UnitOfWork) :
    IRequestHandler<AddDocumentForOrganizationRequestCommand, bool>
{
    public async Task<bool> Handle(AddDocumentForOrganizationRequestCommand request, CancellationToken cancellationToken)
    {
        if (request.OrganizationRequestType == RequestType.Repair && 
            await organizationQueryRepository.GetRepairRequestById(request.RequestId, cancellationToken) == null)
            return false;

        if (request.OrganizationRequestType == RequestType.Abolition && 
            await organizationQueryRepository.GetAbolitionRequestById(request.RequestId, cancellationToken) == null)
            return false;

        var organizationRequestDocument = new OrganizationRequestDocumentEntity()
        {
            Description = request.Description,
            EmployeeUserId = request.UserId,
            OrganizationRequestType = request.OrganizationRequestType,
            RequestId = request.RequestId
        };

        #region File Image Uploader

        if (request.ImageFile != null && request.ImageFile.IsImage())
        {
            var imageName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(request.ImageFile.FileName);
            request.ImageFile.AddImageToServer(imageName, FilePaths.OrganizationRequestDocumentPathServer, 270, 270, FilePaths.OrganizationRequestDocumentPathThumbServer);
            organizationRequestDocument.Image = imageName;
        }

        #endregion

        await organiziationRequestCommandRepository.Add_OrganizationRequestDocument(organizationRequestDocument , cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}