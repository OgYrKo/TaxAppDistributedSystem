using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Realproperty
    {
        public Realproperty()
        {
            Contractorsproperties = new HashSet<Contractorsproperty>();
            Propertysquares = new HashSet<Propertysquare>();
        }

        public int Realpropertykey { get; set; }
        public bool Purpose { get; set; }
        public int Propertytypekey { get; set; }
        public short Area { get; set; }
        public bool View { get; set; }
        public int Citykey { get; set; }
        public int Streetkey { get; set; }
        public string Housenum { get; set; }
        public string Extrainformation { get; set; }
        public string Realpropertyname { get; set; }

        public virtual Address Address { get; set; }
        public virtual Propertytype PropertytypekeyNavigation { get; set; }
        public virtual ICollection<Contractorsproperty> Contractorsproperties { get; set; }
        public virtual ICollection<Propertysquare> Propertysquares { get; set; }
    }
}
