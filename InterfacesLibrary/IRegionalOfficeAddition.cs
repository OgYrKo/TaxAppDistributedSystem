using System.ServiceModel;

namespace InterfacesLibrary
{
    [ServiceContract]
    public interface IRegionalOfficeAddition : ISetUser
    {
        bool AddRegionalOffice(string regionName, string address, string binding);
    }
}
