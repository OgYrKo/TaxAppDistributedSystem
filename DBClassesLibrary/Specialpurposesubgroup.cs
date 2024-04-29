using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Specialpurposesubgroup
    {
        public short Groupkey { get; set; }
        public short Chapterkey { get; set; }
        public string Subgrouptext { get; set; }
        public float? Bidinside { get; set; }
        public float? Bidoutside { get; set; }

        public virtual Specialpurposechapter ChapterkeyNavigation { get; set; }
    }
}
