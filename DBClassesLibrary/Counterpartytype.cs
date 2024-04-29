using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Counterpartytype
    {
        public Counterpartytype()
        {
            Counterparties = new HashSet<Counterparty>();
        }

        public int Counterpartytypekey { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Counterparty> Counterparties { get; set; }
    }
}
