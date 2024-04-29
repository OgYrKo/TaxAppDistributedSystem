using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Propertytype
    {
        public Propertytype()
        {
            Realproperties = new HashSet<Realproperty>();
        }

        public int Propertytypekey { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Realproperty> Realproperties { get; set; }
    }
}
