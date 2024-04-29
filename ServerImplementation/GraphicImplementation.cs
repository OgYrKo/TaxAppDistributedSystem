using System.Collections.Generic;
using DBClassesLibrary;
using System.Linq;
using InterfacesLibrary;
using CoreWCF;

namespace ServerImplementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class GraphicImplementation: SetUserTemplate, IGraphics
    {
        public List<Income> GetDailyIncomes()
        {
            using (var context = new TSNAPContext(UsernameDB, PasswordDB))
            {
                return context.Incomes.ToList();
            }
        }

        public List<Tax> GetDailyTaxes()
        {
            using (var context = new TSNAPContext(UsernameDB, PasswordDB))
            {
                return context.Taxes.ToList();
            }
        }

        public List<Quarterincome> GetQuarterIncomes()
        {
            using (var context = new TSNAPContext(UsernameDB, PasswordDB))
            {
                return context.Quarterincomes.ToList();
            }
        }

        public List<Quartertax> GetQuarterTaxes()
        {
            using (var context = new TSNAPContext(UsernameDB, PasswordDB))
            {
                return context.Quartertaxes.ToList();
            }
        }
    }
}
