using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Landplotfull
    {
        public int? Landplotkey { get; set; }
        public int? Streetkey { get; set; }
        public int? Citykey { get; set; }
        public char? Sectionkey { get; set; }
        public int? Chapterkey { get; set; }
        public int? Groupkey { get; set; }
        public int? Ownershiptypekey { get; set; }
        public string Cadastralnumber { get; set; }
        public short? Area { get; set; }
        public string Housenum { get; set; }
        public bool Location { get; set; }
        public string Extrainformation { get; set; }
        public string Landplotname { get; set; }
        public float? Square { get; set; }
        public decimal? Standartvaluation { get; set; }
        public decimal? Monetaryvaluation { get; set; }
        public string Type { get; set; }
        public string Subgrouptext { get; set; }
        public float? Bidinside { get; set; }
        public float? Bidoutside { get; set; }
        public string Chapter { get; set; }
        public string Section { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int[] Counterpartykeys { get; set; }
        public string Counterpartynames { get; set; }
        public bool Withouttax { get; set; }
    }
}
