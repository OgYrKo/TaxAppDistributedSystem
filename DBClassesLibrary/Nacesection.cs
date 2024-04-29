using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Nacesection
    {
        public Nacesection()
        {
            Nacechapters = new HashSet<Nacechapter>();
        }

        public char Sectionkey { get; set; }
        public string Section { get; set; }

        public virtual ICollection<Nacechapter> Nacechapters { get; set; }
    }
}
