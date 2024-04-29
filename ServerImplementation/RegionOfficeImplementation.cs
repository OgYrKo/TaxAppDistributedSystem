using CoreWCF;
using DBClassesLibrary;
using RouterInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerImplementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class RegionOfficeImplementation : GraphicImplementation, IRegionOffice
    {
        public void SetUser(string usernameDB, string passwordDB, string OfficeName)=>SetUser(usernameDB, passwordDB);
         
        public List<Income> GetDailyIncomes(string OfficeName) => GetDailyIncomes();

        public List<Tax> GetDailyTaxes(string OfficeName) => GetDailyTaxes();

        public List<Quarterincome> GetQuarterIncomes(string OfficeName)=>GetQuarterIncomes();

        public List<Quartertax> GetQuarterTaxes(string OfficeName) => GetQuarterTaxes();

        public List<Debt> GetDebts(string OfficeName)
        {
            throw new NotImplementedException();
        }
        public string SetConnectionString(string login, string password)
        {
            try
            {
                using (var context = new TSNAPContext(login, password))
                {
                    var response = context.Users.Where(value => value.Usename == login).Select(value => context.get_group_name(value.Usename)).ToList();
                    string group_name = response[0];
                    return group_name;
                }

            }
            catch
            {
                return "-1";
            }
        }
    }
}
