using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BankService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("BankService host");
                ServiceHost serviceHost = new ServiceHost(typeof(BankServiceImplementation));
                serviceHost.Open();
                Console.WriteLine("Started");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
