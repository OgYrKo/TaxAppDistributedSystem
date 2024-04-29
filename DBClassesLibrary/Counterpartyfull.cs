using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Counterpartyfull
    {
        public int? Counterpartykey { get; set; }
        public char? Sectionkey { get; set; }
        public short? Chapterkey { get; set; }
        public short? Groupkey { get; set; }
        public short? Classkey { get; set; }
        public int? Counterpartytypekey { get; set; }
        public int? Counterpartygroupkey { get; set; }
        public int? Citykey { get; set; }
        public int? Selectionaddresskey { get; set; }
        public string Counterpartyname { get; set; }
        public string Itn { get; set; }
        public short? Flattaxgroup { get; set; }
        public decimal? Duty { get; set; }
        public int? Counterpartycitykey { get; set; }
        public int? Streetkey { get; set; }
        public string Housenum { get; set; }
        public int? Legalcity { get; set; }
        public int? Legalstreet { get; set; }
        public string Legalhousenum { get; set; }
        public string Phonenum { get; set; }
        public string Email { get; set; }
        public string Extrainformation { get; set; }
        public string City { get; set; }
        public string Counterpartygroup { get; set; }
        public string Type { get; set; }
        public string Class { get; set; }
        public string Grouptext { get; set; }
        public string Chapter { get; set; }
        public string Section { get; set; }
        public int[] Benefitskeys { get; set; }
        public string Benefits { get; set; }
    }
}
