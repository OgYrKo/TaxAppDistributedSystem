using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Specialpurposeland
    {
        public int Key { get; set; }
        public int Specialpurposechapter { get; set; }
        public int Specialpurposesubgroup { get; set; }
        public int Landplotkey { get; set; }
        public DateTime Specialpurposelanddate { get; set; }

        public virtual Landplot LandplotkeyNavigation { get; set; }
    }
}
