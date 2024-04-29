using DBClassesLibrary;
using System.ServiceModel;
using System.Collections.Generic;

namespace InterfacesLibrary
{
    [ServiceContract]
    public interface ILandFunc: ISetUser
    {
        [OperationContract]
        int GetNextIdForLandplot();
        [OperationContract]
        List<Landplot> GetAllLands();
        [OperationContract]
        List<Landplot> GetAllLandsWithFilter(int? Key, string Cadastralnumber, List<short> Area, List<int> City, List<int> Street, 
                                       string Housenum, bool? Location, List<int> Ownershiptype, string Extrainformation, int sort_num);
        [OperationContract]
        Landplot GetLand(int Key);
        [OperationContract]
        string AddLand(Landplot landplot);//(int Key, string Cadastralnumber, short Area, int City, int Street, string Housenum, bool Location, int Ownershiptype, string Extrainformation);
        [OperationContract]
        string UpdateLand(Landplot landplot);//(int Key, string Cadastralnumber, short Area, int City, int Street, string Housenum, bool Location, int Ownershiptype, string Extrainformation);
        [OperationContract]
        string DeleteLand(List<int> Keys);
        [OperationContract]
        string ChargeTaxesToAll();
        [OperationContract]
        string ChargeTaxesByLand(int landplotkey);
        [OperationContract]
        string ChargeTaxesByCounterparty(int counterpartykey);
        [OperationContract]
        string ChargeTaxes(int landplotkey, int counterpartykey);
        [OperationContract]
        string UpdateLandplotOwners(int landplotkey, List<Owner> owners);
    }
}
