using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Street
    {
        public Street()
        {
            Addresses = new HashSet<Address>();
        }

        public int Streetkey { get; set; }
        public string Street1 { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
