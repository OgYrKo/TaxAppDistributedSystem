using System;
using System.Collections.Generic;

namespace DBClassesLibrary
{
    public partial class User
    {
        public uint? Usesysid { get; set; }
        public string Usename { get; set; }
        public string Rolname { get; set; }
        public string Passwd { get; set; }
    }
}
