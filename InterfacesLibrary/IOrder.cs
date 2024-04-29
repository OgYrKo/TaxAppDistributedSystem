using System.Collections.Generic;
using System.ServiceModel;
using CounterpartyInfo;

namespace InterfacesLibrary
{
    [ServiceContract]
    public interface IOrder : ISetUser
    {
        [OperationContract]
        List<string> GetActiveFilesOrders();

        [OperationContract]
        OrdersFromFile GetFile(string name);

        [OperationContract]
        OrdersFromFile AddOrders(OrdersFromFile ordersFromFile);

        [OperationContract]
        string Login(string usernameMail, string passwordMail, string usernameDB, string passwordDB);
    }
}
