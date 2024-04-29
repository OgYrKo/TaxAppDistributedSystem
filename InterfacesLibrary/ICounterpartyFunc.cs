using DBClassesLibrary;
using System.ServiceModel;
using System;
using System.Collections.Generic;

namespace InterfacesLibrary
{
    [ServiceContract]
    public interface ICounterpartyFunc : ISetUser
    {
        [OperationContract]
        int GetNextIdForCounterparty();
        [OperationContract]
        List<Counterparty> GetAllCounterparties();
        [OperationContract]
        List<Counterparty> GetAllCounterpartiesWithFilter(int? Key, string Name, List<int> Counterpartygroups, string Itn,
                                                                    List<int> Selectionaddresses, List<int> Types, List<short> Classkeys,
                                                                    List<short> Groupkeys, List<short> Chapterkeys, List<short> Flattaxgroups,
                                                                    (decimal, decimal)? Bid, List<int?> Cities, List<int?> Streets, string Housenum,
                                                                    List<int?> Legalcities, List<int?> Legalstreets, string Legalhousenum,
                                                                    string Phonenum, string Email, string Extrainformation, int sort_num);
        [OperationContract]
        Counterparty GetCounterparty(int Key);
        [OperationContract]
        string AddCounterparty(Counterparty counterparty);
        [OperationContract]
        string UpdateCounterparty(Counterparty counterparty);
        [OperationContract]
        string DeleteCounterparty(List<int> Keys);


        [OperationContract]
        string AddContractorsland(Contractorsland contractorsland);
        [OperationContract]
        List<Contractorsland> GetAllContractorslandWithFilter(int? Key, List<int> Landplotkey, List<int> Counterpartykey, (DateTime, DateTime)? Date, bool? Withouttax, (float, float)? Share, int sort_num);
        [OperationContract]
        string AddContractorsproperty(Contractorsproperty contractorsproperty);
        [OperationContract]
        List<Contractorsproperty> GetAllContractorspropertyWithFilter(int? Key, List<int> Propertykey, List<int> Counterpartykey, (DateTime, DateTime)? Date, (float, float)? Share, int sort_num);
    }
}
