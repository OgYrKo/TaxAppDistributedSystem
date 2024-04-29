using System;
using System.ServiceModel;
using System.ServiceModel.Routing;

namespace RouterHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("RoutingService");
                ServiceHost serviceHost = new ServiceHost(typeof(RoutingService));
                serviceHost.Open();
                ServiceHost serviceHost1 = new ServiceHost(typeof(RouterHostImplementation));
                serviceHost1.Open();
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
