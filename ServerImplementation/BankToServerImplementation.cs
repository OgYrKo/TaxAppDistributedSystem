using System.Collections.Generic;
using BankServiceInterfaces;

namespace ServerImplementation
{
    public class BankToServerImplementation : IBankToServer
    {
        public bool AddData(List<(string, byte[])> files)
        {
            try
            {
                FileStorage.Instance.StoreFiles(files);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
