using System;
using System.Runtime.Serialization;

namespace RouterLib
{
    [DataContract]
    public class RegionOffice
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string URI { get; set; }
    }
}
