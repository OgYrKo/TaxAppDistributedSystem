using InterfacesLibrary;
using DBClassesLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using CoreWCF;

namespace ServerImplementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class FullData : SetUserTemplate, IFullData
    {
        public List<Debt> GetAllDebts()
        {
            using (var context = new TSNAPContext(UsernameDB,PasswordDB))
            {
                List<Debt> debts;
                debts = context.Debts.OrderByDescending(value => value.Duty).ToList();
                return debts;
            }
        }

        public Addressfull GetAddress(int CityKey, int StreetKey)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var Addresses = context.Addressfulls;
                    return Addresses.AsQueryable().FirstOrDefault(x => x.Citykey == CityKey && x.Streetkey == StreetKey);
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Realpropertyfull> GetAllRealProperties()
        {
            using (var context = new TSNAPContext())
            {
                List<Realpropertyfull> Realproperties;
                Realproperties = context.Realpropertyfulls.ToList();
                return Realproperties;
            }
        }

        public List<Contractorslandfull> GetAllContractorslandfull()
        {
            using (var context = new TSNAPContext())
            {
                List<Contractorslandfull> contractorslands;
                contractorslands = context.Contractorslandfulls.ToList();
                return contractorslands;
            }
        }
        public string AddContractorslandfull(Contractorslandfull contractorslandfull)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Contractorslandfulls.Add(contractorslandfull);
                    context.SaveChanges();
                    return $"Власника {contractorslandfull.Contractorslandkey} додано";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public List<Addressfull> GetAllAddresses()
        {
            using (var context = new TSNAPContext())
            {
                List<Addressfull> Addresses;
                Addresses = context.Addressfulls.ToList();
                return Addresses;
            }
        }
        public List<Contractorslandfull> GetAllContractorslandfullWithFilter(List<int?> Counterpartykeys, List<int?> Contractorslandkeys, List<int?> Landplotkeys)
        {
            using (var context = new TSNAPContext())
            {
                var Contractorslandfulls = context.Contractorslandfulls;
                IQueryable<Contractorslandfull> Filter = Contractorslandfulls;
                if (Counterpartykeys != null)
                {
                    IQueryable<Contractorslandfull> contractorslandfulls = Filter.Where(value => Counterpartykeys.Contains(value.Counterpartykey));
                    Filter = contractorslandfulls;
                }

                if (Contractorslandkeys != null)
                {
                    IQueryable<Contractorslandfull> contractorslandfulls = Filter.Where(value => Contractorslandkeys.Contains(value.Contractorslandkey));
                    Filter = contractorslandfulls;
                }

                if (Landplotkeys != null)
                {
                    IQueryable<Contractorslandfull> contractorslandfulls = Filter.Where(value => Landplotkeys.Contains(value.Landplotkey));
                    Filter = contractorslandfulls;
                }


                return Filter.OrderByDescending(value => value.Contractorslanddate).ToList();
            }
        }
        public List<Addressfull> GetAllAddressesWithFilter(List<int?> Citykeys, List<int?> Streetkeys, string City, string Street)
        {
            using (var context = new TSNAPContext())
            {
                var FullAddresses = context.Addressfulls;
                IQueryable<Addressfull> FilterAddresses = FullAddresses;
                if (Streetkeys != null)
                {
                    IQueryable<Addressfull> streets = FilterAddresses.Where(value => Streetkeys.Contains(value.Streetkey));
                    FilterAddresses = streets;
                }

                if (Citykeys != null)
                {
                    IQueryable<Addressfull> streets = FilterAddresses.Where(value => Citykeys.Contains(value.Citykey));
                    FilterAddresses = streets;
                }

                if (City != null)
                {
                    IQueryable<Addressfull> streets = FilterAddresses.Where(value => value.City.Contains(City));
                    FilterAddresses = streets;
                }
                if (Street != null)
                {
                    IQueryable<Addressfull> streets = FilterAddresses.Where(value => value.Street.Contains(Street));
                    FilterAddresses = streets;
                }


                return FilterAddresses.OrderBy(value => value.City).ToList();
            }
        }

        public List<Counterpartyfull> GetAllCounterparties()
        {
            using (var context = new TSNAPContext())
            {
                List<Counterpartyfull> Counterparties;
                Counterparties = context.Counterpartyfulls.ToList();
                return Counterparties;
            }
        }

        public List<Counterpartyfull> GetAllCounterpartiesWithFilter(int? Key, string Name, List<int> Counterpartygroups, string Itn, List<int> Selectionaddresses, List<int> Types, List<short> Classkeys, List<short> Groupkeys, List<short> Chapterkeys, List<short> Flattaxgroups, (decimal, decimal)? Bid, List<int?> Cities, List<int?> Streets, string Housenum, List<int?> Legalcities, List<int?> Legalstreets, string Legalhousenum, string Phonenum, string Email, string Extrainformation)
        {
            throw new NotImplementedException();
        }

        public List<Landplotfull> GetAllLands()
        {
            using (var context = new TSNAPContext(UsernameDB,PasswordDB))
            {
                List<Landplotfull> Lands;
                Lands = context.Landplotfulls.ToList();
                return Lands;
            }
        }

        public List<Landplotfull> GetAllLandsWithFilter(int? Key, string Cadastralnumber, List<short> Area, List<int> City, List<int> Street, string Housenum, bool? Location, List<int> Ownershiptype, string Extrainformation, int sort_num)
        {
            throw new NotImplementedException();
        }

        public List<Realpropertyfull> GetAllRealPropertiesWithFilter(int? Key, bool? Purpose, List<int> Types, List<short> Areas, bool? View, List<int> Cities, List<int> Streets, string Housenum, string Extrainformation, int sort_num)
        {
            throw new NotImplementedException();
        }

        public Counterpartyfull GetCounterparty(int Key)
        {
            throw new NotImplementedException();
        }

        public Landplotfull GetLand(int Key)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var Landplotfulls = context.Landplotfulls;
                    return Landplotfulls.AsQueryable().FirstOrDefault(x => x.Landplotkey == Key);
                }
            }
            catch
            {
                return null;
            }
        }

        public int GetNextIdForRealProperty()
        {
            throw new NotImplementedException();
        }

        public Realpropertyfull GetProperty(int Key)
        {
            throw new NotImplementedException();
        }
    }
}
