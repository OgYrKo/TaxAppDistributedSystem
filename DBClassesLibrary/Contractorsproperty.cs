using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Contractorsproperty
    {
        public int Contractorspropertykey { get; set; }
        public int Realpropertykey { get; set; }
        public int Counterpartykey { get; set; }
        public DateTime Contractorspropertydate { get; set; }
        public float? Share { get; set; }

        public virtual Counterparty CounterpartykeyNavigation { get; set; }
        public virtual Realproperty RealpropertykeyNavigation { get; set; }
    }
}
