using DBClassesLibrary;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CounterpartyInfo
{

    [XmlRoot("NewDataSet")]
    public class OrdersFromFile
    {
        [XmlIgnore]
        public string FileName { get; set; }

        [XmlIgnore]
        public DateTime Date { get; set; }

        [XmlElement("Table")]
        public List<Counterpartyorder> Orders { get; set; }
    }
}
