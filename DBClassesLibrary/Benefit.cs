using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Benefit
    {
        public Benefit()
        {
            Counterpartyandbenefits = new HashSet<Counterpartyandbenefit>();
        }

        public int Benefitskey { get; set; }
        public string Benefit1 { get; set; }

        public virtual ICollection<Counterpartyandbenefit> Counterpartyandbenefits { get; set; }
    }
}
