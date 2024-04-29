using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Propertysquare
    {
        public int Propertysquarekey { get; set; }
        public int Realpropertykey { get; set; }
        public float Square { get; set; }
        public DateTime Propertysquaredate { get; set; }

        public virtual Realproperty RealpropertykeyNavigation { get; set; }
    }
}
