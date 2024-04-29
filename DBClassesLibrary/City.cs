using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class City
    {
        public City()
        {
            Addresses = new HashSet<Address>();
            Selectionaddresses = new HashSet<Selectionaddress>();
        }

        public int Citykey { get; set; }
        public string City1 { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Selectionaddress> Selectionaddresses { get; set; }
    }
}
