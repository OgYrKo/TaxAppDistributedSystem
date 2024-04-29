using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Realpropertyfull
    {
        public int? Propertytypekey { get; set; }
        public int? Streetkey { get; set; }
        public int? Citykey { get; set; }
        public int? Realpropertykey { get; set; }
        public bool? Purpose { get; set; }
        public short? Area { get; set; }
        public bool? View { get; set; }
        public string Housenum { get; set; }
        public string Extrainformation { get; set; }
        public string Realpropertyname { get; set; }
        public int[] Counterpartykeys { get; set; }
        public string Counterpartynames { get; set; }
        public DateTime? Contractorspropertydate { get; set; }
        public DateTime? Dateoflastchangedsquareinrealproperty { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Type { get; set; }
    }
}
