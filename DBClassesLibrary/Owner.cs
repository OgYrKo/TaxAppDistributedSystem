using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClassesLibrary
{
    public class Owner
    {
        public int Counterpartykey { get; set; }
        public double Share { get; set; }
        public bool Withouttax { get; set; }
    }
}
