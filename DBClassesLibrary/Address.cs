using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Address
    {
        public Address()
        {
            CounterpartyAddresses = new HashSet<Counterparty>();
            CounterpartyLegals = new HashSet<Counterparty>();
            Landplots = new HashSet<Landplot>();
            Realproperties = new HashSet<Realproperty>();
        }

        public int Citykey { get; set; }
        public int Streetkey { get; set; }

        public virtual City CitykeyNavigation { get; set; }
        public virtual Street StreetkeyNavigation { get; set; }
        public virtual ICollection<Counterparty> CounterpartyAddresses { get; set; }
        public virtual ICollection<Counterparty> CounterpartyLegals { get; set; }
        public virtual ICollection<Landplot> Landplots { get; set; }
        public virtual ICollection<Realproperty> Realproperties { get; set; }
    }
}
