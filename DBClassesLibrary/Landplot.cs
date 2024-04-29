using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Landplot
    {
        public Landplot()
        {
            Contractorslands = new HashSet<Contractorsland>();
            Monetaryvaluations = new HashSet<Monetaryvaluation>();
            Specialpurposelands = new HashSet<Specialpurposeland>();
            Squarelandplots = new HashSet<Squarelandplot>();
            Standartvaluations = new HashSet<Standartvaluation>();
        }

        public int Landplotkey { get; set; }
        public string Cadastralnumber { get; set; }
        public short Area { get; set; }
        public int Citykey { get; set; }
        public int Streetkey { get; set; }
        public string Housenum { get; set; }
        public bool Location { get; set; }
        public int Ownershiptypekey { get; set; }
        public string Extrainformation { get; set; }
        public string Landplotname { get; set; }

        public virtual Address Address { get; set; }
        public virtual Ownershiptype OwnershiptypekeyNavigation { get; set; }
        public virtual ICollection<Contractorsland> Contractorslands { get; set; }
        public virtual ICollection<Monetaryvaluation> Monetaryvaluations { get; set; }
        public virtual ICollection<Specialpurposeland> Specialpurposelands { get; set; }
        public virtual ICollection<Squarelandplot> Squarelandplots { get; set; }
        public virtual ICollection<Standartvaluation> Standartvaluations { get; set; }
    }
}
