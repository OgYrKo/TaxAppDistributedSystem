using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Nacegroup
    {
        public Nacegroup()
        {
            Naceclasses = new HashSet<Naceclass>();
        }

        public short Groupkey { get; set; }
        public short Chapterkey { get; set; }
        public string Grouptext { get; set; }

        public virtual Nacechapter ChapterkeyNavigation { get; set; }
        public virtual ICollection<Naceclass> Naceclasses { get; set; }
    }
}
