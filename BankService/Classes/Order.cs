using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BankService.Classes
{
    [XmlRoot("NewDataSet")]
    public class OrdersFromFile
    {
        [XmlElement(IsNullable = true)]
        public string FileName { get; set; }

        [XmlElement(IsNullable = true)]
        public DateTime? Date { get; set; }

        [XmlElement("Table")]
        public List<Order> Orders { get; set; }
    }

    public class Order
    {
        [XmlElement("FDAT")]
        public DateTime FDAT { get; set; }

        [XmlElement("NMK")]
        public string NMK { get; set; }

        [XmlElement("IBAN")]
        public string IBAN { get; set; }

        [XmlElement("NMS")]
        public string NMS { get; set; }

        [XmlElement("OSTF")]
        public double OSTF { get; set; }

        [XmlElement("S")]
        public double S { get; set; }

        [XmlElement("ND")]
        public string ND { get; set; }

        [XmlElement("DK")]
        public double DK { get; set; }

        [XmlElement("REF")]
        public double REF { get; set; }

        [XmlElement("TT")]
        public string TT { get; set; }

        [XmlElement("NAZN")]
        public string NAZN { get; set; }

        [XmlElement("NAZN1")]
        public string NAZN1 { get; set; }

        [XmlElement("NAZN2")]
        public string NAZN2 { get; set; }

        [XmlElement("dapp")]
        public DateTime Dapp { get; set; }

        [XmlElement("DATP")]
        public DateTime DATP { get; set; }

        [XmlElement("IBANK")]
        public string IBANK { get; set; }

        [XmlElement("NB")]
        public string NB { get; set; }

        [XmlElement("NAMK")]
        public string NAMK { get; set; }

        [XmlElement("OKPO")]
        public string OKPO { get; set; }

        [XmlElement("UNAM_A")]
        public string UNAM_A { get; set; }

        [XmlElement("UID_A")]
        public string UID_A { get; set; }

        [XmlElement("UNAM_B")]
        public string UNAM_B { get; set; }

        [XmlElement("UID_B")]
        public string UID_B { get; set; }

        [XmlElement("ADMZNE")]
        public string ADMZNE { get; set; }

        [XmlElement("REFNB")]
        public string REFNB { get; set; }

        [XmlElement("TP")]
        public string TP { get; set; }

        [XmlElement("CTGY")]
        public string CTGY { get; set; }

        [XmlElement("CTGYDTLS")]
        public string CTGYDTLS { get; set; }

        [XmlElement("CERTID")]
        public string CERTID { get; set; }

        [XmlElement("TAXAMT")]
        public double TAXAMT { get; set; }

        [XmlElement("ADDTLINF")]
        public string ADDTLINF { get; set; }
    }
}
