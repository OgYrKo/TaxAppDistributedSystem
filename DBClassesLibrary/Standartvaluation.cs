using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Standartvaluation
    {
        public int Standartvaluationkey { get; set; }
        public int Landplotkey { get; set; }
        public decimal Standartvaluation1 { get; set; }
        public DateTime Standartvaluationdate { get; set; }

        public virtual Landplot LandplotkeyNavigation { get; set; }
    }
}
