using RouterInterfaces;
using RouterLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Reflection;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Xml;
using System.Runtime.Remoting.Channels;

namespace RouterHost
{
    public class RouterHostImplementation : IRouterHost
    {
        private string Contract = "/IRegionOffice";

        public string AddRegionOffice(string regionName, string address, string binding)
        {
            try
            {
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                Configuration config = ConfigurationManager.OpenExeConfiguration(executingAssembly.Location);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(config.FilePath);

                // Поиск узла endpoint с именем, содержащим имя региона
                XmlNode endpointNode = xmlDoc.SelectSingleNode($"/configuration/system.serviceModel/client/endpoint[@name='{regionName}Endpoint']");
                if (endpointNode != null) throw new Exception("Офіс з таким ім'ям вже існує");

                // Добавление нового endpoint в секцию client
                XmlNode clientNode = xmlDoc.SelectSingleNode("/configuration/system.serviceModel/client");
                XmlElement newEndpointNode = xmlDoc.CreateElement("endpoint");
                newEndpointNode.SetAttribute("binding", binding);
                newEndpointNode.SetAttribute("contract", "*");
                newEndpointNode.SetAttribute("address", address+Contract);
                newEndpointNode.SetAttribute("name", $"{regionName}Endpoint");
                clientNode.AppendChild(newEndpointNode);

                // Добавление нового filter в секцию filters
                XmlNode filtersNode = xmlDoc.SelectSingleNode("/configuration/system.serviceModel/routing/filters");
                XmlElement newFilterNode = xmlDoc.CreateElement("filter");
                newFilterNode.SetAttribute("name", $"{regionName}Pattern");
                newFilterNode.SetAttribute("filterType", "XPath");
                newFilterNode.SetAttribute("filterData", $"boolean(//*[local-name()= 'OfficeName']/text() = '{regionName}')");
                filtersNode.AppendChild(newFilterNode);

                XmlElement newFilterNode1 = xmlDoc.CreateElement("filter");
                newFilterNode1.SetAttribute("name", $"Filter_{regionName}");
                newFilterNode1.SetAttribute("filterType", "And");
                newFilterNode1.SetAttribute("filter1", $"{regionName}Pattern");
                newFilterNode1.SetAttribute("filter2", "ToRegionOfficeEndpoint");
                filtersNode.AppendChild(newFilterNode1);



                // Добавление нового filter в секцию filterTables
                XmlNode filterTablesNode = xmlDoc.SelectSingleNode("/configuration/system.serviceModel/routing/filterTables");
                XmlElement newFilterTableNode = xmlDoc.CreateElement("add");
                newFilterTableNode.SetAttribute("filterName", $"Filter_{regionName}");
                newFilterTableNode.SetAttribute("endpointName", $"{regionName}Endpoint");
                filterTablesNode.FirstChild.AppendChild(newFilterTableNode);

                // Сохранение изменений в файле конфигурации
                xmlDoc.Save(config.FilePath);
                ConfigurationManager.RefreshSection("system.serviceModel");
                return "OK";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        public string EditRegionOffice(string regionName, string regionOfficeAddress, string regionOfficeBinding)
        {
            try
            {
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                Configuration config = ConfigurationManager.OpenExeConfiguration(executingAssembly.Location);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(config.FilePath);

                // Поиск узла endpoint с именем, содержащим имя региона
                XmlNode endpointNode = xmlDoc.SelectSingleNode($"/configuration/system.serviceModel/client/endpoint[@name='{regionName}Endpoint']");

                if (endpointNode != null)
                {
                    // Изменение атрибутов узла endpoint
                    endpointNode.Attributes["address"].Value = regionOfficeAddress+ Contract;
                    endpointNode.Attributes["binding"].Value = regionOfficeBinding;
                }

                // Сохранение изменений в файле конфигурации
                xmlDoc.Save(config.FilePath);
                ConfigurationManager.RefreshSection("system.serviceModel");
                return "OK";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        public string DeleteRegionOffice(string regionName)
        {
            try
            {
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                Configuration config = ConfigurationManager.OpenExeConfiguration(executingAssembly.Location);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(config.FilePath);

                // Удаление узла endpoint с именем, содержащим имя региона
                XmlNode endpointNode = xmlDoc.SelectSingleNode($"/configuration/system.serviceModel/client/endpoint[@name='{regionName}Endpoint']");


                XmlNode clientNode = xmlDoc.SelectSingleNode("/configuration/system.serviceModel/client");
                clientNode.RemoveChild(endpointNode);


                // Удаление связанных фильтров и фильтр-таблиц
                XmlNode filtersNode = xmlDoc.SelectSingleNode("/configuration/system.serviceModel/routing/filters");

                // Удаление фильтра с именем, содержащим имя региона
                XmlNode filterNode1 = filtersNode.SelectSingleNode($"filter[@name='Filter_{regionName}']");
                XmlNode filterNode2 = filtersNode.SelectSingleNode($"filter[@name='{regionName}Pattern']");

                filtersNode.RemoveChild(filterNode1);
                filtersNode.RemoveChild(filterNode2);

                XmlNode filterTablesNode = xmlDoc.SelectSingleNode("/configuration/system.serviceModel/routing/filterTables/filterTable[@name='routingRules']");
                // Удаление фильтра с именем, содержащим имя региона
                XmlNode filterTableNode = filterTablesNode.SelectSingleNode($"add[@endpointName='{regionName}Endpoint']");

                filterTablesNode.RemoveChild(filterTableNode);

                // Сохранение изменений в файле конфигурации
                xmlDoc.Save(config.FilePath);
                ConfigurationManager.RefreshSection("system.serviceModel");
                return "OK";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        public List<RegionOffice> GetRegionOffices()
        {
            List<RegionOffice> regionOffices = new List<RegionOffice>();

            try
            {
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                Configuration config = ConfigurationManager.OpenExeConfiguration(executingAssembly.Location);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(config.FilePath);

                // Получение всех узлов endpoint в секции client
                XmlNodeList endpointNodes = xmlDoc.SelectNodes("/configuration/system.serviceModel/client/endpoint");

                foreach (XmlNode endpointNode in endpointNodes)
                {
                    string endpointName = endpointNode.Attributes["name"].Value;
                    string regionName = endpointName.Substring(0, endpointName.Length - "Endpoint".Length);
                    string addressWithContract = endpointNode.Attributes["address"].Value;
                    string address = addressWithContract.Substring(0,addressWithContract.Length-Contract.Length);

                    RegionOffice regionOffice = new RegionOffice
                    {
                        Name = regionName,
                        URI = address,
                    };

                    regionOffices.Add(regionOffice);
                }
            }
            catch
            {
                return null;
            }

            return regionOffices;
        }

        public string CheckUser(string username, string password)
        {
            List<RegionOffice> regionOffices = GetRegionOffices();
            int regionCounter = 0;
            foreach(RegionOffice regionOffice in regionOffices)
            {
                try
                {
                    Uri address = new Uri(regionOffice.URI + "/IRegionOffice");
                    BasicHttpBinding binding = new BasicHttpBinding();
                    binding.MaxReceivedMessageSize = 1000000;
                    EndpointAddress endpoint = new EndpointAddress(address);
                    ChannelFactory<IRegionOffice> factory = new ChannelFactory<IRegionOffice>(binding, endpoint);
                    IRegionOffice channel = factory.CreateChannel();
                    string answer;
                    if (!(answer = channel.SetConnectionString(username, password)).Equals("-1"))
                        return answer;
                }
                catch { regionCounter++; }
            }
            if (regionCounter== regionOffices.Count) return "Жоден сервер не відповідає!";
            return "Логін або пароль невірні!";
        }
    }
}
