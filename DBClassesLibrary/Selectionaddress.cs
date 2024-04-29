using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Selectionaddress
    {
        public Selectionaddress()
        {
            Counterparties = new HashSet<Counterparty>();
        }

        public int Selectionaddresskey { get; set; }
        public int Citykey { get; set; }

        public virtual City CitykeyNavigation { get; set; }
        public virtual ICollection<Counterparty> Counterparties { get; set; }
    }
}
