using System.Collections.Generic;
using DBClassesLibrary;
using System.ServiceModel;

namespace InterfacesLibrary
{
    [ServiceContract]
    public interface IPropertyFunc : ISetUser
    {
        [OperationContract]
        List<Realproperty> GetAll();
        [OperationContract]
        List<Realproperty> GetAllWithFilter(int? Key, bool? Purpose, List<int> Types,
                                                    List<short> Areas, bool? View, List<int> Cities,
                                                    List<int> Streets, string Housenum, string Extrainformation, int sort_num);
        [OperationContract]
        int GetNextIdForRealProperty();
        [OperationContract]
        Realproperty GetProperty(int Key);

        [OperationContract]
        string AddProperty(Realproperty realproperty);

        [OperationContract]
        string UpdateProperty(Realproperty realproperty);//int Key, bool Purpose, int Type, short Area, bool View, int City, int Street, string Housenum, string Extrainformation);

        [OperationContract]
        string DeleteProperties(List<int> Key);
    }
}
