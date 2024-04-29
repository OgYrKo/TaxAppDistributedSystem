using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBClassesLibrary;

namespace Client.Classes
{
    internal class SpecialPurposeNumChapter:Specialpurposechapter
    {
        public int Key { get; set; }

        public SpecialPurposeNumChapter(int key, Specialpurposechapter specialpurposechapter)
        {
            Key = key;
            Sectionkey = specialpurposechapter.Sectionkey;
            Chapterkey = specialpurposechapter.Chapterkey;
            Chapter = specialpurposechapter.Chapter;
        }
    }
}
