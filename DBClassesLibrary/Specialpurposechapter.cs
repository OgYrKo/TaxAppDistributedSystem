using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Specialpurposechapter
    {
        public Specialpurposechapter()
        {
            Specialpurposesubgroups = new HashSet<Specialpurposesubgroup>();
        }

        public short Chapterkey { get; set; }
        public char Sectionkey { get; set; }
        public string Chapter { get; set; }

        public virtual Specialpurposesection SectionkeyNavigation { get; set; }
        public virtual ICollection<Specialpurposesubgroup> Specialpurposesubgroups { get; set; }
    }
}
