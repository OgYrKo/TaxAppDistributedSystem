using System.Collections.Generic;
using DBClassesLibrary;
using System.ServiceModel;

namespace InterfacesLibrary
{
    [ServiceContract]
    public interface ISettings : ISetUser
    {
        [OperationContract]
        List<User> GetAllUsers();
        [OperationContract]
        User GetUser(uint id);
        [OperationContract]
        User GetUserByName(string login);
        [OperationContract]
        string AddUser(User user);
        [OperationContract]
        string DeleteUser(string login);
        [OperationContract]
        string UpdateUser(User user);
        [OperationContract]
        string SetConnectionString(string login, string password);
    }
}
