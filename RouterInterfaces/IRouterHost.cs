using RouterLib;
using System.Collections.Generic;
using System.ServiceModel;

namespace RouterInterfaces
{
    [ServiceContract]
    public interface IRouterHost
    {
        [OperationContract]
        string AddRegionOffice(string regionName, string regiionOfficeAddress, string regiionOfficebinding);

        [OperationContract]
        string EditRegionOffice(string regionName, string regiionOfficeAddress, string regiionOfficebinding);

        [OperationContract]
        string DeleteRegionOffice(string regionName);

        [OperationContract]
        List<RegionOffice> GetRegionOffices();

        [OperationContract]
        string CheckUser(string username, string password);
    }
}
