using DBClassesLibrary;
using InterfacesLibrary;
using System.Collections.Generic;
using System.ServiceModel;

namespace RouterInterfaces
{
    [ServiceContract]
    public interface IRegionOffice
    {
        [OperationContract] 
        List<Debt> GetDebts(string OfficeName);

        [OperationContract]
        void SetUser(string usernameDB, string passwordDB, string OfficeName);

        [OperationContract]
        List<Income> GetDailyIncomes(string OfficeName);

        [OperationContract]
        List<Tax> GetDailyTaxes(string OfficeName);

        [OperationContract]
        List<Quarterincome> GetQuarterIncomes(string OfficeName);

        [OperationContract]
        List<Quartertax> GetQuarterTaxes(string OfficeName);

        [OperationContract]
        string SetConnectionString(string login, string password);
    }
}
