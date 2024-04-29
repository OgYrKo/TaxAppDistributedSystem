using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Ownershiptype
    {
        public Ownershiptype()
        {
            Landplots = new HashSet<Landplot>();
        }

        public int Ownershiptypekey { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Landplot> Landplots { get; set; }
    }
}
