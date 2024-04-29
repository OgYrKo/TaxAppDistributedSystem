using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Counterpartyandbenefit
    {
        public int Counterpartykey { get; set; }
        public int Benefitskey { get; set; }
        public DateTime Counterpartyandbenefitsdate { get; set; }

        public virtual Benefit BenefitskeyNavigation { get; set; }
        public virtual Counterparty CounterpartykeyNavigation { get; set; }
    }
}
