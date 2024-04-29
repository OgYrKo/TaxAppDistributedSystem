using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Naceclass
    {
        public Naceclass()
        {
            Counterparties = new HashSet<Counterparty>();
        }

        public short Classkey { get; set; }
        public short Groupkey { get; set; }
        public short Chapterkey { get; set; }
        public string Class { get; set; }

        public virtual Nacegroup Nacegroup { get; set; }
        public virtual ICollection<Counterparty> Counterparties { get; set; }
    }
}
