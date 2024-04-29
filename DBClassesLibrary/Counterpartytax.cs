using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClassesLibrary
{
    public partial class Counterpartytax
    {
        public int Key { get; set; }
        public string Itn { get; set; }
        public string Purpose { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public virtual Counterparty ItnNavigation { get; set; }
    }
}
