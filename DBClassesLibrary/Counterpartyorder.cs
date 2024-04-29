using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DBClassesLibrary
{
    public partial class Counterpartyorder
    {
        [XmlIgnore]
        public int Key { get; set; }
        [XmlElement("REF")]
        public string Itn { get; set; }
        [XmlElement("NAZN")]
        public string Purpose { get; set; }
        [XmlElement("S")]
        public decimal Amount { get; set; }
        [XmlElement("FDAT")]
        public DateTime Date { get; set; }
        [XmlIgnore]
        public virtual Counterparty ItnNavigation { get; set; }
    }
}
