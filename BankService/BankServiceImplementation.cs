
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MailKit;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Data.OleDb;
using System.Data;
using BankService.Interfaces;
using BankService.Classes;
using System.ServiceModel;

namespace BankService
{
    //TODO check by other threads
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class BankServiceImplementation : IBankService
    {
        public string Username { private get; set; }
        public string Password { private get; set; }
        public string Host { private get; set; }
        private int Port { get; set; }
        private string DateFileName { get => "lastExecutionTime.txt"; }
        private ImapClient Client;

        public BankServiceImplementation()
        {
            SetDefaultData();
        }

        ~BankServiceImplementation()
        {
            if (Client != null) Client.Disconnect(true);
        }

        public List<(string, byte[])> GetDatabyPeriod(DateTime startDate, DateTime endDate)
        {
            Console.WriteLine("Have Get request");
            List<OrdersFromFile> list = Read(startDate, endDate);
            if (list.Count == 0) return null;
            List<(string, byte[])> files = new List<(string, byte[])>();
            foreach (var item in list)
            {
                byte[] file = SerializeToXmlBytes(item);
                string fileName = item.FileName;
                files.Add((fileName, file));
            }
            return files;
        }

        public List<OrdersFromFile> Read(DateTime startDate, DateTime endDate)
        {
            var inbox = Client.Inbox;

            IList<UniqueId> messageIds = GetMailIdsByPeriod(Client, startDate, endDate);

            List<OrdersFromFile> counterparties = new List<OrdersFromFile>();
            foreach (var matchingUid in messageIds)
            {
                var message = inbox.GetMessage(matchingUid);
                if (message.Date < startDate || message.Date > endDate) continue;
                if (!message.Attachments.Any()) continue;// Проверка на наличие вложений

                foreach (var attachment in message.Attachments)
                {
                    string dbfFileName = attachment.ContentDisposition?.FileName ?? "untitled";
                    if (Path.GetExtension(dbfFileName).ToLower() != ".dbf") continue;

                    SaveFile(attachment, dbfFileName);
                    string xmlFileName = Path.GetFileNameWithoutExtension(dbfFileName) + ".xml";
                    try
                    {
                        ConvertFromDbfToXml(dbfFileName, xmlFileName);
                        OrdersFromFile OrdersFromFile = ConvertFromXMLData(xmlFileName);
                        DateTimeOffset mesageDate = message.Date;
                        OrdersFromFile.Date = mesageDate.DateTime;
                        OrdersFromFile.FileName = Path.GetFileNameWithoutExtension(dbfFileName)+$"_{mesageDate.DateTime.ToString("yyyyMMddHHmmss")}";
                        OrdersFromFile.Orders=SetCredits(OrdersFromFile.Orders);
                        counterparties.Add(OrdersFromFile);
                        Console.WriteLine("Data send");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        File.Delete(xmlFileName); // Удаление временного файла
                        File.Delete(dbfFileName); // Удаление временного файла
                    }

                }
            }
            return counterparties;
        }

        private List<Order> SetCredits(List<Order> orders)
        {
            foreach (var order in orders)
            {
                order.S /= 100;
            }

            return orders;
        }

        private void SetDefaultData()
        {
            Host = "imap.gmail.com";
            Port = 993;
        }

        private IList<UniqueId> GetMailIdsByPeriod(ImapClient client, DateTime startDate, DateTime endDate)
        {
            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);
            var query = SearchQuery.DeliveredAfter(startDate)
                           .And(SearchQuery.DeliveredBefore(endDate.AddDays(1)));
            return inbox.Search(query);
        }

        private bool SaveFile(MimeEntity attachment, string fileName)
        {
            using (var stream = File.Create(fileName))
            {
                if (attachment is MimePart mimePart)
                {
                    mimePart.Content.DecodeTo(stream);
                    return true;
                }
            }
            return false;
        }

        private OrdersFromFile ConvertFromXMLData(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(OrdersFromFile));
            OrdersFromFile OrdersFromFile;
            using (StreamReader stringReader = new StreamReader(fileName))
            {
                OrdersFromFile = (OrdersFromFile)serializer.Deserialize(stringReader);
            }
            return OrdersFromFile;
        }

        public void ConvertFromDbfToXml(string dbfFileName, string xmlFileName)
        {
            string fullPath = Path.GetFullPath(dbfFileName);
            string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={System.IO.Path.GetDirectoryName(fullPath)};Extended Properties=dBase IV;";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                // Загрузка данных из .dbf
                //TODO check where
                string query = $"SELECT * FROM {dbfFileName} WHERE S > 0";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet, "Table");

                        // Сохранение данных в XML
                        dataSet.WriteXml(xmlFileName, XmlWriteMode.WriteSchema);
                    }
                }

                connection.Close();
            }
        }

        private OrdersFromFile Deserialize(string responseContent)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(OrdersFromFile));
            OrdersFromFile OrdersFromFile;
            using (StringReader stringReader = new StringReader(responseContent))
            {
                OrdersFromFile = (OrdersFromFile)serializer.Deserialize(stringReader);
            }
            return OrdersFromFile;
        }

        private void SetClientConnection()
        {
            if (Client != null) Client.Disconnect(true);
            Client = new ImapClient();
            try
            {
            Client.Connect(Host, Port, SecureSocketOptions.SslOnConnect);
            }
            catch
            {
                throw new Exception("Банковський сервіс немає з'єднання з інтернетом");
            }

            try
            {
            Client.Authenticate(Username, Password);
            }
            catch
            {
                throw new Exception("Введені дані недійсні");
            }
        }

        private byte[] SerializeToXmlBytes<T>(T obj)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(memoryStream, obj);
                return memoryStream.ToArray();
            }
        }

        private DateTime LoadLastExecutionTime()
        {
            // Путь к файлу, где будет храниться значение lastExecutionTime
            string filePath = DateFileName;

            // Проверяем, существует ли файл
            if (File.Exists(filePath))
            {
                // Читаем значение lastExecutionTime из файла
                string dateString = File.ReadAllText(filePath);
                if (DateTime.TryParse(dateString, out DateTime lastExecutionTime))
                {
                    return lastExecutionTime;
                }
            }

            // Значение lastExecutionTime по умолчанию при первом запуске
            return DateTime.Now.AddDays(-1).Date;
        }

        private void SaveLastExecutionTime(DateTime lastExecutionTime)
        {
            // Путь к файлу, где будет храниться значение lastExecutionTime
            string filePath = DateFileName;

            // Записываем значение lastExecutionTime в файл
            File.WriteAllText(filePath, lastExecutionTime.ToString());
        }

        public string SetConnection(string login, string password)
        {
            try
            {
                Username = login;
                Password = password;
                SetClientConnection();
            }
            catch (Exception ex) 
            { 
                return ex.Message; 
            }
            return "OK";
        }
    }
}
