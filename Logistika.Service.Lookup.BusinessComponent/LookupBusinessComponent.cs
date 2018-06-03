using Logistika.Service.Common.Entities.Lookup;
using Logistika.Service.Lookup.BusinessComponentInterface;
using Logistika.Service.Lookup.DataAccessInterface;
using System.Collections.Generic;

namespace Logistika.Service.Lookup.BusinessComponent
{
    public class LookupBusinessComponent : ILookupBusinessComponent
    {
        ILookupDataAccess _instance = null;

        public LookupBusinessComponent(ILookupDataAccess Instance)
        {
            _instance = Instance;
        }

        public IList<DropdownData> GetAllDivisions(int? clientId)
        {
          return _instance.GetAllDivisions(clientId);
        }

        public IList<DropdownData> GetAllJobs()
        {
            return _instance.GetAllJobs();
        }

        public IList<DropdownData> GetAOCProcedureByPickSlip()
        {
            return _instance.GetAOCProcedureByPickSlip();
        }

        public IList<DropdownData> GetAutoIncludeProductInserts(int? clientPK)
        {
            return _instance.GetAutoIncludeProductInserts(clientPK);
        }

        public IList<DropdownData> GetAutoIncludeScopes()
        {
            return _instance.GetAutoIncludeScopes();
        }

        public IList<DropdownData> GetBrandsByClientPK(int clientPK)
        {
            return _instance.GetBrandsByClientPK(clientPK);
        }

        public IList<DropdownData>  GetClients()
        {
            return _instance.GetClients();
        }

        public IList<DropdownData> GetClientsByProjectID(int ProjectPK)
        {
            return _instance.GetClientsByProjectID(ProjectPK);
        }

        public IList<DropdownData> GetDataSetType()
        {
            return _instance.GetDataSetType();
        }

        public IList<DropdownData> GetDateFormats()
        {
            return _instance.GetDateFormats();
        }

        public IList<DropdownData> GetDeliveryMethods()
        {
            return _instance.GetDeliveryMethods();
        }

        public IList<DropdownData> GetFileExtensions()
        {
            return _instance.GetFileExtensions();
        }

        public IList<DropdownData> GetFrameworkPrinters()
        {
            return _instance.GetFrameworkPrinters();
        }

        public IList<DropdownData> GetFrameworkProcedureTypes()
        {
            return _instance.GetFrameworkProcedureTypes();
        }

        public IList<DropdownData> GetFrameworkStoredProcedure(string code, string clientFK)
        {
            return _instance.GetFrameworkStoredProcedure(code, clientFK);
        }

        public IList<DropdownData> GetFrameworkURLByType(string type)
        {
            return _instance.GetFrameworkURLByType(type);
        }

        public IList<DropdownData> GetImportOrderAdjustQuantityAlgorithm()
        {
            return _instance.GetImportOrderAdjustQuantityAlgorithm();
        }

        public IList<DropdownData> GetImportRecordTypes()
        {
            return _instance.GetImportRecordTypes();
        }

        public IList<DropdownData> GetItemDrugSchedules()
        {
            return _instance.GetItemDrugSchedules();
        }

        public IList<DropdownData> GetJobItems(string jobPks, char delmiter)
        {
            return _instance.GetJobItems(jobPks, delmiter);
        }

        public IList<DropdownData> GetJobsByClientPk(int ClientFk)
        {
            return _instance.GetJobsByClientPk(ClientFk);
        }

        public IList<DropdownData> GetJobsByProjectPK(int ProjectPK)
        {
            return _instance.GetJobsByProjectPK(ProjectPK);
        }

        public IList<DropdownData> GetJobsByProjectWithOmsFileNameOrOrderValidationSP(int ProjectPK)
        {
            return _instance.GetJobsByProjectWithOmsFileNameOrOrderValidationSP(ProjectPK);
        }

        public IList<DropdownData> GetLetterPeriodStartCodes()
        {
            return _instance.GetLetterPeriodStartCodes();
        }

        public IList<DropdownData> GetLetterTypes()
        {
            return _instance.GetLetterTypes();
        }

        public IList<DropdownData> GetOptInActivity()
        {
            return _instance.GetOptInActivity();
        }

        public IList<DropdownData> GetOptoutOptInCommunicationType()
        {
            return _instance.GetOptoutOptInCommunicationType();
        }

        public IList<DropdownData> GetOrderStreamRuleTypes()
        {
            return _instance.GetOrderStreamRuleTypes();
        }

        public IList<DropdownData> GetOrderValidationActionDropDown()
        {

            return _instance.GetOrderValidationActionDropDown();
        }

        public IList<DropdownData> GetOrderValidationRuleDropdown()
        {
            return _instance.GetOrderValidationRuleDropdown();
        }

        public IList<DropdownData> GetProductsByClient(int? ClientFk)
        {
            return _instance.GetProductsByClient(ClientFk);
        }

        public IList<DropdownData> GetProductsByProject(int projectPK)
        {
            return _instance.GetProductsByProject(projectPK);
        }

        public IList<DropdownData> GetProductTypes()
        {
            return _instance.GetProductTypes();
        }

        public IList<DropdownData> GetProfessionalDesignations()
        {
            return _instance.GetProfessionalDesignations();
        }

        public IList<DropdownData> GetProgramTypes()
        {
            return _instance.GetProgramTypes();
        }

        public IList<DropdownData> GetProjects()
        {
            return _instance.GetProjects();
        }

        public IList<DropdownData> GetProjectsByDivisionId(int divisionId)
        {
            return _instance.GetProjectsByDivisionId(divisionId);
        }

        public IList<DropdownData> GetQNASet()
        {
            return _instance.GetQNASet();
        }

        public IList<DropdownData> GetShipmentCarriers()
        {
            return _instance.GetShipmentCarriers();
        }

        public IList<DropdownData> GetShipmentCarrierServices()
        {
            return _instance.GetShipmentCarrierServices();
        }

        public IList<DropdownData> GetSignedByType()
        {
            return _instance.GetSignedByType();
        }

        public IList<DropdownData> GetStates()
        {
            return _instance.GetStates();
        }

        public IList<DropdownData> GetStatusByType(string code)
        {
            return _instance.GetStatusByType(code);
        }

        public IList<DropdownData> GetSVLAOCClients()
        {
            return _instance.GetSVLAOCClients();
        }

        public IList<DropdownData> GetSVLAOCJobsByClientPk(int ClientFk)
        {
            return _instance.GetSVLAOCJobsByClientPk(ClientFk);
        }

        public IList<DropdownData> GetSVLSRFClients()
        {
            return _instance.GetSVLAOCClients();
        }

        public IList<DropdownData> GetSVLSRFJobsByClientPk(int ClientFk)
        {
            return _instance.GetSVLSRFJobsByClientPk(ClientFk);
        }

        public IList<DropdownData> GetUnitOfMeasures()
        {
            return _instance.GetSVLAOCClients();
        }

        public IList<DropdownData> GetTitle()
        {
            return _instance.GetTitle();
        }

        public IList<DropdownData> GetProjects(int ClientFk)
        {
            return _instance.GetProjects(ClientFk);
        }


        public IList<SignatureVerficationModificationType> GetSignatureVerficationQuestionTypeModificationType()
        {
            return _instance.GetSignatureVerficationQuestionTypeModificationType();
        }
    }
}
