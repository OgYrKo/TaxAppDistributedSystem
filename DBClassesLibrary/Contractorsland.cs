using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Contractorsland
    {
        public int Contractorslandkey { get; set; }
        public int Landplotkey { get; set; }
        public int Counterpartykey { get; set; }
        public DateTime Contractorslanddate { get; set; }
        public bool? Withouttax { get; set; }
        public float? Share { get; set; }

        public virtual Counterparty CounterpartykeyNavigation { get; set; }
        public virtual Landplot LandplotkeyNavigation { get; set; }
    }
}
