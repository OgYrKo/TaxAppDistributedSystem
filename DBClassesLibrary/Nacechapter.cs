using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Nacechapter
    {
        public Nacechapter()
        {
            Nacegroups = new HashSet<Nacegroup>();
        }

        public short Chapterkey { get; set; }
        public char Sectionkey { get; set; }
        public string Chapter { get; set; }

        public virtual Nacesection SectionkeyNavigation { get; set; }
        public virtual ICollection<Nacegroup> Nacegroups { get; set; }
    }
}
