using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Specialpurposesection
    {
        public Specialpurposesection()
        {
            Specialpurposechapters = new HashSet<Specialpurposechapter>();
        }

        public char Sectionkey { get; set; }
        public string Section { get; set; }

        public virtual ICollection<Specialpurposechapter> Specialpurposechapters { get; set; }
    }
}
