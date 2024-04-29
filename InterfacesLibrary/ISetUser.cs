using System.ServiceModel;

namespace InterfacesLibrary
{
    [ServiceContract]
    public interface ISetUser
    {
        [OperationContract]
        void SetUser(string usernameDB, string passwordDB);
    }
}
