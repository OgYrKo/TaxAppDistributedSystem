using RouterInterfaces;
using RouterLib;
using System.Collections.Generic;
using System.ServiceModel;

namespace MainOfficeClient.Classes
{
    public class RouterProxy : ClientBase<IRouterHost>, IRouterHost
    {
        public RouterProxy() : base("RouterEndpoint") { }

        public string AddRegionOffice(string regionName, string regiionOfficeAddress, string regiionOfficebinding)
        {
            return Channel.AddRegionOffice(regionName, regiionOfficeAddress, regiionOfficebinding);
        }

        public string CheckUser(string username, string password)
        {
            return Channel.CheckUser(username, password);
        }

        public string DeleteRegionOffice(string regionName)
        {
            return Channel.DeleteRegionOffice(regionName);
        }

        public string EditRegionOffice(string regionName, string regiionOfficeAddress, string regiionOfficebinding)
        {
            return Channel.EditRegionOffice(regionName, regiionOfficeAddress, regiionOfficebinding);
        }

        public List<RegionOffice> GetRegionOffices()
        {
            return Channel.GetRegionOffices();
        }
    }
}
