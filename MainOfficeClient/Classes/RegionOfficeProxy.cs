﻿using DBClassesLibrary;
using RouterInterfaces;
using System.Collections.Generic;
using System.ServiceModel;

namespace MainOfficeClient.Classes
{
    internal class RegionOfficeProxy : ClientBase<IRegionOffice>, IRegionOffice
    {
        public RegionOfficeProxy() : base("FromMainCoordinationEndpoint") { }

        public List<Income> GetDailyIncomes(string OfficeName)=>Channel.GetDailyIncomes(OfficeName);

        public List<Tax> GetDailyTaxes(string OfficeName)=> Channel.GetDailyTaxes(OfficeName);

        public List<Debt> GetDebts(string OfficeName)=>Channel.GetDebts(OfficeName);

        public List<Quarterincome> GetQuarterIncomes(string OfficeName) => Channel.GetQuarterIncomes(OfficeName);

        public List<Quartertax> GetQuarterTaxes(string OfficeName)=>Channel.GetQuarterTaxes(OfficeName);

        public string SetConnectionString(string login, string password)
        {
            throw new System.NotImplementedException();
        }

        public void SetUser(string usernameDB, string passwordDB, string OfficeName)=>Channel.SetUser(usernameDB, passwordDB, OfficeName);
    }
}
