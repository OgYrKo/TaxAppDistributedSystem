using DBClassesLibrary;
using RouterInterfaces;
using RouterLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client.Classes
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
            throw new NotImplementedException();
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
