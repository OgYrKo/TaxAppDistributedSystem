using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Counterpartygroup
    {
        public Counterpartygroup()
        {
            Counterparties = new HashSet<Counterparty>();
        }

        public int Counterpartygroupkey { get; set; }
        public string Counterpartygroup1 { get; set; }

        public virtual ICollection<Counterparty> Counterparties { get; set; }
    }
}
