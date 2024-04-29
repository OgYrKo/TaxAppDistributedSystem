using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace BankService.Interfaces
{
    [ServiceContract]
    public interface IBankService
    {
        [OperationContract]
        string SetConnection(string login,string password);

        [OperationContract]
        List<(string, byte[])> GetDatabyPeriod(DateTime startDate, DateTime endDate);
    }
}
