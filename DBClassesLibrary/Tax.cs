using System;
using System.Collections.Generic;

namespace DBClassesLibrary
{
    public partial class Tax
    {
        public DateTime? Date { get; set; }
        public decimal? Taxday { get; set; }
        public decimal? Totaltax { get; set; }
    }
}
