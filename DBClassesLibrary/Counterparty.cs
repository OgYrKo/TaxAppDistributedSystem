using System;
using System.Collections.Generic;



namespace DBClassesLibrary
{
    public partial class Counterparty
    {
        public Counterparty()
        {
            Contractorslands = new HashSet<Contractorsland>();
            Contractorsproperties = new HashSet<Contractorsproperty>();
            Counterpartyandbenefits = new HashSet<Counterpartyandbenefit>();
            Counterpartyorders = new HashSet<Counterpartyorder>();
            Counterpartytaxes = new HashSet<Counterpartytax>();
        }

        public int Counterpartykey { get; set; }
        public string Counterpartyname { get; set; }
        public int Counterpartygroupkey { get; set; }
        public string Itn { get; set; }
        public int Selectionaddresskey { get; set; }
        public int Counterpartytypekey { get; set; }
        public short Classkey { get; set; }
        public short Groupkey { get; set; }
        public short Chapterkey { get; set; }
        public short Flattaxgroup { get; set; }
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

        public virtual Address Address { get; set; }
        public virtual Counterpartygroup CounterpartygroupkeyNavigation { get; set; }
        public virtual Counterpartytype CounterpartytypekeyNavigation { get; set; }
        public virtual Address Legal { get; set; }
        public virtual Naceclass Naceclass { get; set; }
        public virtual Selectionaddress SelectionaddresskeyNavigation { get; set; }
        public virtual ICollection<Contractorsland> Contractorslands { get; set; }
        public virtual ICollection<Contractorsproperty> Contractorsproperties { get; set; }
        public virtual ICollection<Counterpartyandbenefit> Counterpartyandbenefits { get; set; }
        public virtual ICollection<Counterpartyorder> Counterpartyorders { get; set; }
        public virtual ICollection<Counterpartytax> Counterpartytaxes { get; set; }
    }
}
