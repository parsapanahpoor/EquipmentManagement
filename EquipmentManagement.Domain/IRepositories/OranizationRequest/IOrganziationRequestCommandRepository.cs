﻿using EquipmentManagement.Domain.Entities.OrganizationRequest;
using EquipmentManagement.Domain.Entities.OrganizationRequest.AbolitionRequest;

namespace EquipmentManagement.Domain.IRepositories.OranizationRequest;

public interface IOrganziationRequestCommandRepository
{
    Task AddAsync(OrganziationRequestEntity entity,
        CancellationToken cancellationToken);

    Task Add_RequestDecisionMaker(RequestDecisionMaker model,
        CancellationToken cancellationToken);

    Task Add_ExpertVisitorRequest(ExpertVisitorOpinionEntity data,
        CancellationToken cancellationToken);

    Task Add_ExpertVisitorRequest(ExpertVisitorOpinionForAbolitionRequestEntity data,
        CancellationToken cancellationToken);

    Task Add_DecisionRepairRequestRespons(DecisionRepairRequestRespons data,
        CancellationToken cancellationToken);

    Task Add_DecisionAbolitionRequestRespons(DecisionAbolitionRequestRespons data,
        CancellationToken cancellationToken);

    Task Add_RepairRequest(RepairRequest data,
        CancellationToken cancellationToken);

    Task Add_AbolitionRequest(AbolitionRequest data,
       CancellationToken cancellationToken);

    Task Add_OrganizationRequestDocument(OrganizationRequestDocumentEntity data,
       CancellationToken cancellationToken);

    void Update(OrganziationRequestEntity entity);
    void Delete_RequestDecisionMaker(RequestDecisionMaker model);
    void Update_ExpertVisitorResponse(ExpertVisitorOpinionEntity data);
    void Update_ExpertVisitorResponse(ExpertVisitorOpinionForAbolitionRequestEntity data);
    void Update_DecisionRepairRequestRespons(DecisionRepairRequestRespons data);
    void Update_DecisionAbolitionRequestRespons(DecisionAbolitionRequestRespons data);
    void Update_RepairRequest(RepairRequest data);
    void Update_AbolitionRequest(AbolitionRequest data);
}
