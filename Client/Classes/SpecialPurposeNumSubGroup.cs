using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBClassesLibrary;

namespace Client.Classes
{
    internal class SpecialPurposeNumSubGroup : Specialpurposesubgroup
    {
        public int Key { get; set; }

        public SpecialPurposeNumSubGroup(int key, Specialpurposesubgroup specialpurposesubgroup)
        {
            Key = key;
            Groupkey = specialpurposesubgroup.Groupkey;
            Chapterkey = specialpurposesubgroup.Chapterkey;
            Subgrouptext = specialpurposesubgroup.Subgrouptext;
        }
    }
}
