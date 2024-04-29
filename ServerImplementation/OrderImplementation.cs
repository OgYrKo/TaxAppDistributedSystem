using BankServiceInterfaces;
using CoreWCF;
using CounterpartyInfo;
using DBClassesLibrary;
using InterfacesLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace ServerImplementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class OrderImplementation : SetUserTemplate, IOrder
    {
        string UsernameMail { get; set; }
        IBankService Channel;

        public OrdersFromFile AddOrders(OrdersFromFile ordersFromFile)
        {
            OrdersFromFile newOrdersFromFile = AddOrdersToBD(ordersFromFile);
            if (newOrdersFromFile == null)
            {
                FileStorage.Instance.DeleteFromMain(ordersFromFile.FileName);
                return null;
            }
            else
            {
                FileStorage.Instance.SaveToFile(newOrdersFromFile, ordersFromFile.FileName);
                return newOrdersFromFile;
            }
        }

        private OrdersFromFile AddOrdersToBD(OrdersFromFile ordersFromFile)
        {
            try
            {
                List<Counterpartyorder> orders = ProcessCounterpartyOrders(ordersFromFile.Orders);
                if(orders.Count == 0) { return null; }
                ordersFromFile.Orders = orders;
                return ordersFromFile;
            }
            catch
            {
                return null;
            }

        }

        private List<Counterpartyorder> ProcessCounterpartyOrders(List<Counterpartyorder> orders)
        {
            using (var context = new TSNAPContext(UsernameDB, PasswordDB))
            {
                List<Counterpartyorder> newOrders = new List<Counterpartyorder>();
                foreach (var order in orders)
                {
                    var existingCounterparty = context.Counterparties.FirstOrDefault(c => c.Itn == order.Itn);
                    if (existingCounterparty != null)
                    {
                        context.Database.ExecuteSqlRaw("INSERT INTO CounterpartyOrder (ITN, Purpose, Amount, Date) VALUES ({0}, {1}, {2}, {3})",
                            existingCounterparty.Itn, 
                            order.Purpose,
                            order.Amount, 
                            order.Date);

                    }
                    else
                    {
                        newOrders.Add(order);
                    }
                }
                return newOrders;
            }
        }

        public List<string> GetActiveFilesOrders()
        {
            string fileName = UsernameMail.Replace("@", "_");
            DateTime lastExecutionTime = FileStorage.Instance.LoadLastExecutionTime(fileName);
            DateTime currentExecutionTime = DateTime.Now.AddMinutes(-2);//что бы сообщения доходили за 2 минуты
            List<(string, byte[])> files = Channel.GetDatabyPeriod(lastExecutionTime, currentExecutionTime);
            if (files != null)
            {
                FileStorage.Instance.StoreFiles(files);
            }
            FileStorage.Instance.SaveLastExecutionDate(fileName, currentExecutionTime);
            return FileStorage.Instance.FileNames;
        }

        public OrdersFromFile GetFile(string name)
        {
            return FileStorage.Instance.ReadFromFile<OrdersFromFile>(name);
        }

        public string Login(string usernameMail, string passwordMail,string usernameDB,string passwordDB)
        {
            if (Channel == null) SetChannel();

            UsernameMail = usernameMail;
            UsernameDB= usernameDB;
            PasswordDB = passwordDB;
            try
            {
                return Channel.SetConnection(usernameMail, passwordMail);
            }
            catch
            {
                return "Відсутній зв'язок з поштовим сервісом";
            }
        }

        private void SetChannel()
        {
            Uri address = new Uri("http://localhost:9875/BankService");
            System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding();
            binding.MaxReceivedMessageSize = 1000000;
            System.ServiceModel.EndpointAddress endpoint = new System.ServiceModel.EndpointAddress(address);
            ChannelFactory<IBankService> factory = new ChannelFactory<IBankService>(binding, endpoint);
            Channel = factory.CreateChannel();
        }

    }
}
