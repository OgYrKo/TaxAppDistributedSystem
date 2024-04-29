using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace BankServiceInterfaces
{
    [ServiceContract]
    public interface IBankService
    {
        [OperationContract]
        string SetConnection(string login, string password);

        [OperationContract]
        List<(string, byte[])> GetDatabyPeriod(DateTime startDate, DateTime endDate);
    }
}
