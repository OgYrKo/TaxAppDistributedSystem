using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Squarelandplot
    {
        public int Squarelandplotkey { get; set; }
        public int Landplotkey { get; set; }
        public float Square { get; set; }
        public DateTime Squarelandplotdate { get; set; }

        public virtual Landplot LandplotkeyNavigation { get; set; }
    }
}
