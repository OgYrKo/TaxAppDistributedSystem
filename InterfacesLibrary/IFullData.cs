using DBClassesLibrary;
using System.ServiceModel;
using System.Collections.Generic;

namespace InterfacesLibrary
{
    [ServiceContract]
    public interface IFullData : ISetUser
    {
        [OperationContract]
        List<Debt> GetAllDebts();
        [OperationContract]
        List<Contractorslandfull> GetAllContractorslandfullWithFilter(List<int?> Counterpartykeys, List<int?> Contractorslandkeys, List<int?> Landplotkeys);
        [OperationContract]
        string AddContractorslandfull(Contractorslandfull contractorslandfull);
        [OperationContract]
        List<Contractorslandfull> GetAllContractorslandfull();

        [OperationContract]
        List<Addressfull> GetAllAddresses();
        [OperationContract]
        List<Addressfull> GetAllAddressesWithFilter(List<int?> Citykey, List<int?> Streetkey, string City, string Street);
        [OperationContract]
        Addressfull GetAddress(int CityKey, int StreetKey);
        //[OperationContract]
        //string AddAddress(Address address);
        //[OperationContract]
        //string UpdateAddress(Address address);
        //[OperationContract]
        //string DeleteAddresses(List<(int, int)> Keys);


        [OperationContract]
        List<Counterpartyfull> GetAllCounterparties();
        [OperationContract]
        List<Counterpartyfull> GetAllCounterpartiesWithFilter(int? Key, string Name, List<int> Counterpartygroups, string Itn,
                                                                    List<int> Selectionaddresses, List<int> Types, List<short> Classkeys,
                                                                    List<short> Groupkeys, List<short> Chapterkeys, List<short> Flattaxgroups,
                                                                    (decimal, decimal)? Bid, List<int?> Cities, List<int?> Streets, string Housenum,
                                                                    List<int?> Legalcities, List<int?> Legalstreets, string Legalhousenum,
                                                                    string Phonenum, string Email, string Extrainformation);
        [OperationContract]
        Counterpartyfull GetCounterparty(int Key);
        //[OperationContract]
        //string AddCounterparty(Counterparty counterparty);
        //[OperationContract]
        //string UpdateCounterparty(Counterparty counterparty);


        [OperationContract]
        List<Landplotfull> GetAllLands();
        [OperationContract]
        List<Landplotfull> GetAllLandsWithFilter(int? Key, string Cadastralnumber, List<short> Area, List<int> City, List<int> Street,
                                       string Housenum, bool? Location, List<int> Ownershiptype, string Extrainformation, int sort_num);
        [OperationContract]
        Landplotfull GetLand(int Key);
        //[OperationContract]
        //string AddLand(Landplot landplot);
        //[OperationContract]
        //string UpdateLand(Landplot landplot);


        [OperationContract]
        List<Realpropertyfull> GetAllRealProperties();
        [OperationContract]
        List<Realpropertyfull> GetAllRealPropertiesWithFilter(int? Key, bool? Purpose, List<int> Types,
                                                    List<short> Areas, bool? View, List<int> Cities,
                                                    List<int> Streets, string Housenum, string Extrainformation, int sort_num);
        [OperationContract]
        int GetNextIdForRealProperty();
        [OperationContract]
        Realpropertyfull GetProperty(int Key);

        //[OperationContract]
        //string AddProperty(Realproperty realproperty);

        //[OperationContract]
        //string UpdateProperty(Realproperty realproperty);


    }
}
