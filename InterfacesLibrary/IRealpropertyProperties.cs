using DBClassesLibrary;
using System.ServiceModel;
using System.Collections.Generic;

namespace InterfacesLibrary
{
    [ServiceContract]
    public interface IRealpropertyProperties : ISetUser
    {
        [OperationContract]
        string AddPropertysquare(Propertysquare propertysquare);
        [OperationContract]
        List<Propertytype> GetAllPropertytypes();
        [OperationContract]
        List<Propertytype> GetAllPropertytypesWithFilter(int? Key, List<string> Types, int sort_num);
        [OperationContract]
        Propertytype GetPropertytype(int Key);
        [OperationContract]
        string AddPropertytype(Propertytype propertytype);

        [OperationContract]
        string UpdatePropertytype(Propertytype propertytype);

        [OperationContract]
        string DeletePropertytypes(List<int> Keys);
    }
}
