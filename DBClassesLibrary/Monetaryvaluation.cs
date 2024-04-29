using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Monetaryvaluation
    {
        public int Monetaryvaluationkey { get; set; }
        public int Landplotkey { get; set; }
        public decimal Monetaryvaluation1 { get; set; }
        public DateTime Monetaryvaluationdate { get; set; }

        public virtual Landplot LandplotkeyNavigation { get; set; }
    }
}
