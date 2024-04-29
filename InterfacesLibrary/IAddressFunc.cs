using DBClassesLibrary;
using System.Collections.Generic;
using System.ServiceModel;
//using System.ServiceModel;

namespace InterfacesLibrary
{
    [ServiceContract]
    public interface IAddressFunc : ISetUser
    {
        [OperationContract]
        List<City> GetAllCities();
        [OperationContract]
        City GetCity(int Key);
        [OperationContract]
        string AddCity(City city);// (int Key, string City);
        [OperationContract]
        string UpdateCity(City city); //(int Key, string City);
        [OperationContract]
        string DeleteCities(List<int> Keys);



        [OperationContract]
        List<Address> GetAllAddresses();
        [OperationContract]
        Address GetAddress(int CityKey, int StreetKey);
        [OperationContract]
        string AddAddress(Address address);// (int Citykey, int Streetkey);
        [OperationContract]
        string UpdateAddress(Address address); //(int Citykey, int Streetkey);
        [OperationContract]
        string DeleteAddresses(List<(int,int)> Keys); 



        [OperationContract]
        List<Street> GetAllStreets();
        [OperationContract]
        List<Street> GetAllStreetsWithFilter(List<int> keys, string street, int sort_num);
        [OperationContract]
        Street GetStreet(int Key);
        [OperationContract]
        string AddStreet(Street street);// (int Key, string Street);
        [OperationContract]
        string UpdateStreet(Street street);// (int Key, string Street);
        [OperationContract]
        string DeleteStreets(List<int> Keys);


        [OperationContract]
        List<Selectionaddress> GetAllSelectionaddresses();
        [OperationContract]
        Selectionaddress GetSelectionaddress(int Sectionkey);
        [OperationContract]
        string AddSelectionaddresses(List<int> Citykeys);
        [OperationContract]
        string DeleteSelectionaddresses(List<int> Sectionkeys);
    }
}
