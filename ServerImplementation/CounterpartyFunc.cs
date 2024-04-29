using CoreWCF;
using DBClassesLibrary;
using InterfacesLibrary;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ServerImplementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CounterpartyFunc : SetUserTemplate, ICounterpartyFunc
    {
        public int GetNextIdForCounterparty()
        {
            using (var context = new TSNAPContext())
            {
                int maxId = context.Counterparties.Max(model => model.Counterpartykey);
                return (maxId + 1);
            }
        }

        public string AddContractorsland(Contractorsland contractorsland)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Contractorslands.Add(contractorsland);
                    context.SaveChanges();
                    return $"Власник земельної ділянки доданий";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public string AddContractorsproperty(Contractorsproperty contractorsproperty)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Contractorsproperties.Add(contractorsproperty);
                    context.SaveChanges();
                    return $"Власник об'єкту нерухомості доданий";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string AddCounterparty(Counterparty counterparty)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Counterparties.Add(counterparty);
                    context.SaveChanges();
                    return $"Контрагент {counterparty.Counterpartykey} додано";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string DeleteCounterparty(List<int> Keys)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    int counter = 0;
                    var Counterparties = context.Counterparties;
                    var toDelete = Counterparties.Where(value => Keys.Contains(value.Counterpartykey));
                    foreach (var value in toDelete)
                    {
                        Counterparties.Remove(value);
                        counter++;
                    }
                    context.SaveChanges();
                    return $"Видалено {counter} контрагентів";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.InnerException.Message.ToString();
            }
        }

        public List<Counterparty> GetAllCounterparties()
        {
            using (var context = new TSNAPContext())
            {
                List<Counterparty> Counterparties;
                Counterparties = context.Counterparties.ToList();
                return Counterparties;
            }
        }
        public List<Counterparty> GetAllCounterpartiesWithFilter(int? Key, string Name, List<int> Counterpartygroups, string Itn,
                                                                    List<int> Selectionaddresses, List<int> Types, List<short> Classkeys,
                                                                    List<short> Groupkeys, List<short> Chapterkeys, List<short> Flattaxgroups,
                                                                    (decimal, decimal)? Bid, List<int?> Cities, List<int?> Streets, string Housenum,
                                                                    List<int?> Legalcities, List<int?> Legalstreets, string Legalhousenum,
                                                                    string Phonenum, string Email, string Extrainformation, int sort_num)
        {
            using (var context = new TSNAPContext())
            {
                var Counterparties = context.Counterparties;
                IQueryable<Counterparty> FilterCounterparties = Counterparties;
                if (Key != null)
                {
                    IQueryable<Counterparty> counterparties = FilterCounterparties.Where(value => value.Counterpartykey.ToString().Contains(Key.ToString()));
                    FilterCounterparties = counterparties;
                }

                if (Name != null)
                {
                    IQueryable<Counterparty> counterparties = FilterCounterparties.Where(value => value.Counterpartyname.Contains(Name));
                    FilterCounterparties = counterparties;
                }
                if (Counterpartygroups != null)
                {
                    IQueryable<Counterparty> counterparties = FilterCounterparties.Where(value => Counterpartygroups.Contains(value.Counterpartygroupkey));
                    FilterCounterparties = counterparties;
                }

                if (Itn != null)
                {
                    IQueryable<Counterparty> counterparties = FilterCounterparties.Where(value => value.Itn.Contains(Itn));
                    FilterCounterparties = counterparties;
                }

                if (Selectionaddresses != null)
                {
                    IQueryable<Counterparty> counterparties = FilterCounterparties.Where(value => Selectionaddresses.Contains(value.Selectionaddresskey));
                    FilterCounterparties = counterparties;
                }

                if (Types != null)
                {
                    IQueryable<Counterparty> counterparties = FilterCounterparties.Where(value => Types.Contains(value.Counterpartytypekey));
                    FilterCounterparties = counterparties;
                }

                if (Classkeys != null)
                {
                    IQueryable<Counterparty> counterparties = FilterCounterparties.Where(value => Classkeys.Contains(value.Classkey));
                    FilterCounterparties = counterparties;
                }

                if (Groupkeys != null)
                {
                    IQueryable<Counterparty> counterparties = FilterCounterparties.Where(value => Groupkeys.Contains(value.Groupkey));
                    FilterCounterparties = counterparties;
                }

                if (Chapterkeys != null)
                {
                    IQueryable<Counterparty> counterparties = FilterCounterparties.Where(value => Chapterkeys.Contains(value.Chapterkey));
                    FilterCounterparties = counterparties;
                }
                if (Flattaxgroups != null)
                {
                    IQueryable<Counterparty> counterparties = FilterCounterparties.Where(value => Flattaxgroups.Contains(value.Flattaxgroup));
                    FilterCounterparties = counterparties;
                }
                if (Bid != null)
                {
                    IQueryable<Counterparty> counterparties = FilterCounterparties.Where(value => Bid.Value.Item1 <= value.Duty && value.Duty <= Bid.Value.Item2);
                    FilterCounterparties = counterparties;
                }
                if (Cities != null)
                {
                    IQueryable<Counterparty> counterparties = FilterCounterparties.Where(value => Cities.Contains(value.Counterpartycitykey));
                    FilterCounterparties = counterparties;
                }
                if (Streets != null)
                {
                    IQueryable<Counterparty> counterparties = FilterCounterparties.Where(value => Streets.Contains(value.Streetkey));
                    FilterCounterparties = counterparties;
                }
                if (Housenum != null)
                {
                    IQueryable<Counterparty> counterparties = FilterCounterparties.Where(value => value.Housenum.Contains(Housenum));
                    FilterCounterparties = counterparties;
                }
                if (Legalcities != null)
                {
                    IQueryable<Counterparty> counterparties = FilterCounterparties.Where(value => Legalcities.Contains(value.Legalcity));
                    FilterCounterparties = counterparties;
                }
                if (Legalstreets != null)
                {
                    IQueryable<Counterparty> counterparties = FilterCounterparties.Where(value => Legalstreets.Contains(value.Legalstreet));
                    FilterCounterparties = counterparties;
                }
                if (Legalhousenum != null)
                {
                    IQueryable<Counterparty> counterparties = FilterCounterparties.Where(value => value.Legalhousenum.Contains(Legalhousenum));
                    FilterCounterparties = counterparties;
                }
                if (Phonenum != null)
                {
                    IQueryable<Counterparty> counterparties = FilterCounterparties.Where(value => value.Phonenum.Contains(Phonenum));
                    FilterCounterparties = counterparties;
                }
                if (Email != null)
                {
                    IQueryable<Counterparty> counterparties = FilterCounterparties.Where(value => value.Email.Contains(Email));
                    FilterCounterparties = counterparties;
                }
                if (Extrainformation != null)
                {
                    IQueryable<Counterparty> counterparties = FilterCounterparties.Where(value => value.Extrainformation.Contains(Extrainformation));
                    FilterCounterparties = counterparties;
                }

                //var OrderCounterparties = FilterCounterparties.OrderBy(Counterparty => Counterparty.Key);
                //switch (sort_num)
                //{

                //    //desc
                //    case -1:
                //        OrderCounterparties = FilterCounterparties.OrderByDescending(Counterparty => Counterparty.Key);
                //        break;
                //    case -2:
                //        OrderCounterparties = FilterCounterparties.OrderByDescending(Counterparty => Counterparty.Name);
                //        break;
                //    case -3:
                //        OrderCounterparties = FilterCounterparties.OrderByDescending(Counterparty => Counterparty.Counterpartygroup);
                //        break;
                //    case -4:
                //        OrderCounterparties = FilterCounterparties.OrderByDescending(Counterparty => Counterparty.Itn);
                //        break;
                //    case -5:
                //        OrderCounterparties = FilterCounterparties.OrderByDescending(Counterparty => Counterparty.Selectionaddress);
                //        break;
                //    case -6:
                //        OrderCounterparties = FilterCounterparties.OrderByDescending(Counterparty => Counterparty.Type);
                //        break;
                //    case -7:
                //        OrderCounterparties = FilterCounterparties.OrderByDescending(Counterparty => Counterparty.Classkey);
                //        break;
                //    case -8:
                //        OrderCounterparties = FilterCounterparties.OrderByDescending(Counterparty => Counterparty.Groupkey);
                //        break;
                //    case -9:
                //        OrderCounterparties = FilterCounterparties.OrderByDescending(Counterparty => Counterparty.Chapterkey);
                //        break;
                //    case -10:
                //        OrderCounterparties = FilterCounterparties.OrderByDescending(Counterparty => Counterparty.Flattaxgroup);
                //        break;
                //    case -11:
                //        OrderCounterparties = FilterCounterparties.OrderByDescending(Counterparty => Counterparty.Bid);
                //        break;
                //    case -12:
                //        OrderCounterparties = FilterCounterparties.OrderByDescending(Counterparty => Counterparty.City);
                //        break;
                //    case -13:
                //        OrderCounterparties = FilterCounterparties.OrderByDescending(Counterparty => Counterparty.Street);
                //        break;
                //    case -14:
                //        OrderCounterparties = FilterCounterparties.OrderByDescending(Counterparty => Counterparty.Housenum);
                //        break;
                //    case -15:
                //        OrderCounterparties = FilterCounterparties.OrderByDescending(Counterparty => Counterparty.Legalcity);
                //        break;
                //    case -16:
                //        OrderCounterparties = FilterCounterparties.OrderByDescending(Counterparty => Counterparty.Legalstreet);
                //        break;
                //    case -17:
                //        OrderCounterparties = FilterCounterparties.OrderByDescending(Counterparty => Counterparty.Legalhousenum);
                //        break;
                //    case -18:
                //        OrderCounterparties = FilterCounterparties.OrderByDescending(Counterparty => Counterparty.Phonenum);
                //        break;
                //    case -19:
                //        OrderCounterparties = FilterCounterparties.OrderByDescending(Counterparty => Counterparty.Email);
                //        break;
                //    case -20:
                //        OrderCounterparties = FilterCounterparties.OrderByDescending(Counterparty => Counterparty.Extrainformation);
                //        break;


                //    //asc
                //    case 1:
                //        OrderCounterparties = FilterCounterparties.OrderBy(Counterparty => Counterparty.Key);
                //        break;
                //    case 2:
                //        OrderCounterparties = FilterCounterparties.OrderBy(Counterparty => Counterparty.Name);
                //        break;
                //    case 3:
                //        OrderCounterparties = FilterCounterparties.OrderBy(Counterparty => Counterparty.Counterpartygroup);
                //        break;
                //    case 4:
                //        OrderCounterparties = FilterCounterparties.OrderBy(Counterparty => Counterparty.Itn);
                //        break;
                //    case 5:
                //        OrderCounterparties = FilterCounterparties.OrderBy(Counterparty => Counterparty.Selectionaddress);
                //        break;
                //    case 6:
                //        OrderCounterparties = FilterCounterparties.OrderBy(Counterparty => Counterparty.Type);
                //        break;
                //    case 7:
                //        OrderCounterparties = FilterCounterparties.OrderBy(Counterparty => Counterparty.Classkey);
                //        break;
                //    case 8:
                //        OrderCounterparties = FilterCounterparties.OrderBy(Counterparty => Counterparty.Groupkey);
                //        break;
                //    case 9:
                //        OrderCounterparties = FilterCounterparties.OrderBy(Counterparty => Counterparty.Chapterkey);
                //        break;
                //    case 10:
                //        OrderCounterparties = FilterCounterparties.OrderBy(Counterparty => Counterparty.Flattaxgroup);
                //        break;
                //    case 11:
                //        OrderCounterparties = FilterCounterparties.OrderBy(Counterparty => Counterparty.Bid);
                //        break;
                //    case 12:
                //        OrderCounterparties = FilterCounterparties.OrderBy(Counterparty => Counterparty.City);
                //        break;
                //    case 13:
                //        OrderCounterparties = FilterCounterparties.OrderBy(Counterparty => Counterparty.Street);
                //        break;
                //    case 14:
                //        OrderCounterparties = FilterCounterparties.OrderBy(Counterparty => Counterparty.Housenum);
                //        break;
                //    case 15:
                //        OrderCounterparties = FilterCounterparties.OrderBy(Counterparty => Counterparty.Legalcity);
                //        break;
                //    case 16:
                //        OrderCounterparties = FilterCounterparties.OrderBy(Counterparty => Counterparty.Legalstreet);
                //        break;
                //    case 17:
                //        OrderCounterparties = FilterCounterparties.OrderBy(Counterparty => Counterparty.Legalhousenum);
                //        break;
                //    case 18:
                //        OrderCounterparties = FilterCounterparties.OrderBy(Counterparty => Counterparty.Phonenum);
                //        break;
                //    case 19:
                //        OrderCounterparties = FilterCounterparties.OrderBy(Counterparty => Counterparty.Email);
                //        break;
                //    case 20:
                //        OrderCounterparties = FilterCounterparties.OrderBy(Counterparty => Counterparty.Extrainformation);
                //        break;
                //}


                return FilterCounterparties.ToList();
            }
        }

        public List<Contractorsland> GetAllContractorslandWithFilter(int? Key, List<int> Landplotkey, List<int> Counterpartykey, (DateTime, DateTime)? Date, bool? Withouttax, (float, float)? Share, int sort_num)
        {
            using (var context = new TSNAPContext())
            {
                var Contractorslands = context.Contractorslands;
                IQueryable<Contractorsland> FilterContractorsland = Contractorslands;
                if (Key != null)
                {
                    IQueryable<Contractorsland> contractorslands = FilterContractorsland.Where(value => value.Contractorslandkey.ToString().Contains(Key.ToString()));
                    FilterContractorsland = contractorslands;
                }
                if (Landplotkey != null)
                {
                    IQueryable<Contractorsland> contractorslands = FilterContractorsland.Where(value => Landplotkey.Contains(value.Landplotkey));
                    FilterContractorsland = contractorslands;
                }
                if (Counterpartykey != null)
                {
                    IQueryable<Contractorsland> contractorslands = FilterContractorsland.Where(value => Counterpartykey.Contains(value.Counterpartykey));
                    FilterContractorsland = contractorslands;
                }
                if (Date != null)
                {
                    IQueryable<Contractorsland> contractorslands = FilterContractorsland.Where(value => Date.Value.Item1 <= value.Contractorslanddate && value.Contractorslanddate <= Date.Value.Item2);
                    FilterContractorsland = contractorslands;
                }
                if (Withouttax != null)
                {
                    IQueryable<Contractorsland> contractorslands = FilterContractorsland.Where(value => value.Withouttax.Equals(value));
                    FilterContractorsland = contractorslands;
                }
                if (Share != null)
                {
                    IQueryable<Contractorsland> contractorslands = FilterContractorsland.Where(value => Share.Value.Item1 <= value.Share && value.Share <= Share.Value.Item2);
                    FilterContractorsland = contractorslands;
                }


                //var OrderContractorslands = FilterContractorsland.OrderBy(Contractorsland => Contractorsland.Key);
                //switch (sort_num)
                //{

                //    //desc
                //    case -1:
                //        OrderContractorslands = FilterContractorsland.OrderByDescending(Contractorsland => Contractorsland.Key);
                //        break;
                //    case -2:
                //        OrderContractorslands = FilterContractorsland.OrderByDescending(Contractorsland => Contractorsland.Landplotkey);
                //        break;
                //    case -3:
                //        OrderContractorslands = FilterContractorsland.OrderByDescending(Contractorsland => Contractorsland.Counterpartykey);
                //        break;
                //    case -4:
                //        OrderContractorslands = FilterContractorsland.OrderByDescending(Contractorsland => Contractorsland.Date);
                //        break;
                //    case -5:
                //        OrderContractorslands = FilterContractorsland.OrderByDescending(Contractorsland => Contractorsland.Withouttax);
                //        break;
                //    case -6:
                //        OrderContractorslands = FilterContractorsland.OrderByDescending(Contractorsland => Contractorsland.Share);
                //        break;

                //    //asc
                //    case 1:
                //        OrderContractorslands = FilterContractorsland.OrderBy(Contractorsland => Contractorsland.Key);
                //        break;
                //    case 2:
                //        OrderContractorslands = FilterContractorsland.OrderBy(Contractorsland => Contractorsland.Landplotkey);
                //        break;
                //    case 3:
                //        OrderContractorslands = FilterContractorsland.OrderBy(Contractorsland => Contractorsland.Counterpartykey);
                //        break;
                //    case 4:
                //        OrderContractorslands = FilterContractorsland.OrderBy(Contractorsland => Contractorsland.Date);
                //        break;
                //    case 5:
                //        OrderContractorslands = FilterContractorsland.OrderBy(Contractorsland => Contractorsland.Withouttax);
                //        break;
                //    case 6:
                //        OrderContractorslands = FilterContractorsland.OrderBy(Contractorsland => Contractorsland.Share);
                //        break;

                //}
                return FilterContractorsland.ToList();
            }
        }

        public List<Contractorsproperty> GetAllContractorspropertyWithFilter(int? Key, List<int> Propertykey, List<int> Counterpartykey, (DateTime, DateTime)? Date, (float, float)? Share, int sort_num)
        {
            using (var context = new TSNAPContext())
            {
                var Contractorsproperties = context.Contractorsproperties;
                IQueryable<Contractorsproperty> FilterContractorsproperties = Contractorsproperties;
                if (Key != null)
                {
                    IQueryable<Contractorsproperty> contractorsproperty = FilterContractorsproperties.Where(value => value.Contractorspropertykey.ToString().Contains(Key.ToString()));
                    FilterContractorsproperties = contractorsproperty;
                }
                if (Propertykey != null)
                {
                    IQueryable<Contractorsproperty> contractorsproperty = FilterContractorsproperties.Where(value => Propertykey.Contains(value.Realpropertykey));
                    FilterContractorsproperties = contractorsproperty;
                }
                if (Counterpartykey != null)
                {
                    IQueryable<Contractorsproperty> contractorsproperty = FilterContractorsproperties.Where(value => Counterpartykey.Contains(value.Counterpartykey));
                    FilterContractorsproperties = contractorsproperty;
                }
                if (Date != null)
                {
                    IQueryable<Contractorsproperty> contractorsproperty = FilterContractorsproperties.Where(value => Date.Value.Item1 <= value.Contractorspropertydate && value.Contractorspropertydate <= Date.Value.Item2);
                    FilterContractorsproperties = contractorsproperty;
                }
                if (Share != null)
                {
                    IQueryable<Contractorsproperty> contractorsproperty = FilterContractorsproperties.Where(value => Share.Value.Item1 <= value.Share && value.Share <= Share.Value.Item2);
                    FilterContractorsproperties = contractorsproperty;
                }


                //var OrderContractorsproperties = FilterContractorsproperties.OrderBy(Contractorsproperty => Contractorsproperty.Key);
                //switch (sort_num)
                //{

                //    //desc
                //    case -1:
                //        OrderContractorsproperties = FilterContractorsproperties.OrderByDescending(Contractorsproperty => Contractorsproperty.Key);
                //        break;
                //    case -2:
                //        OrderContractorsproperties = FilterContractorsproperties.OrderByDescending(Contractorsproperty => Contractorsproperty.Propertykey);
                //        break;
                //    case -3:
                //        OrderContractorsproperties = FilterContractorsproperties.OrderByDescending(Contractorsproperty => Contractorsproperty.Counterpartykey);
                //        break;
                //    case -4:
                //        OrderContractorsproperties = FilterContractorsproperties.OrderByDescending(Contractorsproperty => Contractorsproperty.Date);
                //        break;
                //    case -5:
                //        OrderContractorsproperties = FilterContractorsproperties.OrderByDescending(Contractorsproperty => Contractorsproperty.Share);
                //        break;

                //    //asc
                //    case 1:
                //        OrderContractorsproperties = FilterContractorsproperties.OrderBy(Contractorsproperty => Contractorsproperty.Key);
                //        break;
                //    case 2:
                //        OrderContractorsproperties = FilterContractorsproperties.OrderBy(Contractorsproperty => Contractorsproperty.Propertykey);
                //        break;
                //    case 3:
                //        OrderContractorsproperties = FilterContractorsproperties.OrderBy(Contractorsproperty => Contractorsproperty.Counterpartykey);
                //        break;
                //    case 4:
                //        OrderContractorsproperties = FilterContractorsproperties.OrderBy(Contractorsproperty => Contractorsproperty.Date);
                //        break;
                //    case 5:
                //        OrderContractorsproperties = FilterContractorsproperties.OrderBy(Contractorsproperty => Contractorsproperty.Share);
                //        break;

                //}
                return FilterContractorsproperties.ToList();
            }
        }

        public Counterparty GetCounterparty(int Key)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var Counterparties = context.Counterparties;
                    return Counterparties.AsQueryable().FirstOrDefault(x => x.Counterpartykey == Key);
                }
            }
            catch
            {
                return null;
            }
        }

        public string UpdateCounterparty(Counterparty counterparty)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Counterparties.Update(counterparty);
                    context.SaveChanges();
                    return $"Контрагента {counterparty.Counterpartykey} оновлено";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
