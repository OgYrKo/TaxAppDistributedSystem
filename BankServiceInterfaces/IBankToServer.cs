using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BankServiceInterfaces
{
    [ServiceContract]
    public interface IBankToServer
    {
        [OperationContract]
        bool AddData(List<(string, byte[])> files);
    }
}
