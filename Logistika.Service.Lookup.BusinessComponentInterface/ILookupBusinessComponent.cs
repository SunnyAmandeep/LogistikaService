using Logistika.Service.Common.Entities.Lookup;
using System.Collections.Generic;

namespace Logistika.Service.Lookup.BusinessComponentInterface
{
    public interface ILookupBusinessComponent
    {
        IList<DropdownData> GetAllDivisions(int? clientId);
        IList<DropdownData> GetAllJobs();
        //IList<SignatureVerficationSRFAOCVerificationQuestionTypeObject> GetAOCModificationTypes();
        IList<DropdownData> GetAOCProcedureByPickSlip();
        //IList<DropdownData> GetAutoCompleteDataFromRepository(string ProcedureName, System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<string, object>> Parameters, string ValuePropertyName, string TextPropertyName);
        IList<DropdownData> GetAutoIncludeProductInserts(int? clientPK);
        IList<DropdownData> GetAutoIncludeScopes();
        IList<DropdownData> GetBrandsByClientPK(int clientPK);
        //InventoryFulfilmentThresholdRequestobject GetClientDivisionProjectByJobPk(int jobPK);
        IList<DropdownData> GetClients();
        IList<DropdownData> GetClientsByProjectID(int ProjectPK);
        IList<DropdownData> GetDataSetType();
        IList<DropdownData> GetDateFormats();
        IList<DropdownData> GetDeliveryMethods();
        IList<DropdownData> GetFileExtensions();
        IList<DropdownData> GetFrameworkPrinters();
        IList<DropdownData> GetFrameworkProcedureTypes();
        IList<DropdownData> GetFrameworkStoredProcedure(string code, string clientFK);
        IList<DropdownData> GetFrameworkURLByType(string type);
        //IList<ImportFieldObject> GetImportFields();
        IList<DropdownData> GetImportOrderAdjustQuantityAlgorithm();
        IList<DropdownData> GetImportRecordTypes();
        IList<DropdownData> GetItemDrugSchedules();
        IList<DropdownData> GetJobItems(string jobPks, char delmiter);
        IList<DropdownData> GetJobsByClientPk(int ClientFk);
        IList<DropdownData> GetJobsByProjectPK(int ProjectPK);
        IList<DropdownData> GetJobsByProjectWithOmsFileNameOrOrderValidationSP(int ProjectPK);
        //IList<ItemJobMappingDataObject> GetJobsDescriptionByClientPk(int ClientFk);
        IList<DropdownData> GetLetterPeriodStartCodes();
        IList<DropdownData> GetLetterTypes();
        IList<DropdownData> GetOptInActivity();
        IList<DropdownData> GetOptoutOptInCommunicationType();
        IList<DropdownData> GetOrderStreamRuleTypes();
        IList<DropdownData> GetOrderValidationActionDropDown();
        //IList<OrderValidationAction> GetOrderValidationActions();
        IList<DropdownData> GetOrderValidationRuleDropdown();
        IList<DropdownData> GetProductsByClient(int? ClientFk);
        IList<DropdownData> GetProductsByProject(int projectPK);
        IList<DropdownData> GetProductTypes();
        IList<DropdownData> GetProfessionalDesignations();
        IList<DropdownData> GetProgramTypes();
        IList<DropdownData> GetProjects();
        IList<DropdownData> GetProjectsByDivisionId(int divisionId);
        IList<DropdownData> GetQNASet();
        IList<DropdownData> GetShipmentCarriers();
        IList<DropdownData> GetShipmentCarrierServices();
        //SignatureVerificationEntryObject GetSignatureVerificationAOCFormURLByJobNumber(string jobNumber);
        //SignatureVerificationEntryObject GetSignatureVerificationSRFFormURLByJobNumber(string jobNumber);
        IList<DropdownData> GetSignedByType();
        IList<DropdownData> GetStates();
        IList<DropdownData> GetStatusByType(string code);
        IList<DropdownData> GetSVLAOCClients();
        IList<DropdownData> GetSVLAOCJobsByClientPk(int ClientFk);
        //IList<SignatureVerficationSRFAOCVerificationQuestionTypeObject> GetSVLAOCVerificationQuestionTypes(string type);
        //IList<SignatureVerficationSRFAOCVerificationQuestionTypeObject> GetSVLSRFAOCModificationTypes();
        IList<DropdownData> GetSVLSRFClients();
        IList<DropdownData> GetSVLSRFJobsByClientPk(int ClientFk);
        //IList<SignatureVerficationSRFAOCVerificationQuestionTypeObject> GetSVLSRFModificationTypes();
        //IList<SignatureVerficationSRFAOCVerificationQuestionTypeObject> GetSVLSRFVerificationQuestionTypes(string type);

        IList<DropdownData> GetUnitOfMeasures();

        IList<DropdownData> GetTitle();
        IList<DropdownData> GetProjects(int ClientFk);

        IList<SignatureVerficationModificationType> GetSignatureVerficationQuestionTypeModificationType();
        
    }
}
