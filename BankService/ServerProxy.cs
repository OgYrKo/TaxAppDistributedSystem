using BankService.Classes;
using BankService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BankService
{
    internal class ServerProxy : ClientBase<IBankToServer>, IBankToServer
    {
        public ServerProxy() : base("ServerEndpoint") { }

        public bool AddData(List<(string, byte[])> files)
        {
            return Channel.AddData(files);
        }
    }
}
