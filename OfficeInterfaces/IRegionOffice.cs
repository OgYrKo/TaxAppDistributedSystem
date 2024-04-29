using CounterpartyInfo;
using System.Collections.Generic;
using System.ServiceModel;

namespace OfficeInterfaces
{
    [ServiceContract]
    public interface IRegionOffice
    {
        //[OperationContract]
        //void SendStatus(DocumentInfo documentInfo, DocumentStatus documentStatus);

        //[OperationContract]
        //int GetTax(string OfficeName, int groupNum, DateTime start, DateTime end);

        [OperationContract]
        bool AddFee(List<OrdersFromFile> currencies);
    }
}
