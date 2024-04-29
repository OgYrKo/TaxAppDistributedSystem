using CoreWCF;
using DBClassesLibrary;
using InterfacesLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace ServerImplementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class LandFunc : SetUserTemplate,ILandFunc
    {
        
        public int GetNextIdForLandplot()
        {
            using (var context = new TSNAPContext(UsernameDB,PasswordDB))
            {
                int maxId = context.Landplots.Max(model => model.Landplotkey);
                return (maxId + 1);
            }
        }

        public string AddLand(Landplot landplot)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Landplots.Add(landplot);
                    context.SaveChanges();
                    return $"Земельна ділянка додана";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string DeleteLand(List<int> Keys)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    int counter = 0;
                    var Landplots = context.Landplots;
                    var toDelete = Landplots.Where(value => Keys.Contains(value.Landplotkey));
                    foreach (var value in toDelete)
                    {
                        Landplots.Remove(value);
                        counter++;
                    }
                    context.SaveChanges();
                    return $"Видалено {counter} земельних ділянок";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.InnerException.Message.ToString();
            }
        }

        public List<Landplot> GetAllLands()
        {
            using (var context = new TSNAPContext(UsernameDB, PasswordDB))
            {
                List<Landplot> Landplots;
                Landplots = context.Landplots.ToList();
                return Landplots;
            }
        }

        public List<Landplot> GetAllLandsWithFilter(int? Key, string Cadastralnumber, List<short> Areas, List<int> Cities, List<int> Streets, 
                                              string Housenum, bool? Location, List<int> Ownershiptypes, string Extrainformation, int sort_num)
        {
            using (var context = new TSNAPContext())
            {
                var Landplots = context.Landplots;
                IQueryable<Landplot> FilterLandplot = Landplots;
                if (Key != null)
                {
                    IQueryable<Landplot> landplots = FilterLandplot.Where(value => value.Landplotkey.ToString().Contains(Key.ToString()));
                    FilterLandplot = landplots;
                }

                if (Cadastralnumber != null)
                {
                    IQueryable<Landplot> landplots = FilterLandplot.Where(value => value.Cadastralnumber.Contains(Cadastralnumber));
                    FilterLandplot = landplots;
                }
                if (Areas != null)
                {
                    IQueryable<Landplot> landplots = FilterLandplot.Where(value => Areas.Contains(value.Area));
                    FilterLandplot = landplots;
                }

                if (Cities != null)
                {
                    IQueryable<Landplot> landplots = FilterLandplot.Where(value => Cities.Contains(value.Citykey));
                    FilterLandplot = landplots;
                }

                if (Streets != null)
                {
                    IQueryable<Landplot> landplots = FilterLandplot.Where(value => Streets.Contains(value.Streetkey));
                    FilterLandplot = landplots;
                }

                if (Housenum != null)
                {
                    IQueryable<Landplot> landplots = FilterLandplot.Where(value => value.Housenum.Contains(Housenum));
                    FilterLandplot = landplots;
                }
                if (Housenum != null)
                {
                    IQueryable<Landplot> landplots = FilterLandplot.Where(value => value.Location.Equals(Location));
                    FilterLandplot = landplots;
                }

                if (Ownershiptypes != null)
                {
                    IQueryable<Landplot> landplots = FilterLandplot.Where(value => Ownershiptypes.Contains(value.Ownershiptypekey));
                    FilterLandplot = landplots;
                }

                if (Extrainformation != null)
                {
                    IQueryable<Landplot> landplots = FilterLandplot.Where(value => value.Extrainformation.Contains(Extrainformation));
                    FilterLandplot = landplots;
                }


                //var OrderLandplots = FilterLandplot.OrderBy(realproperty => realproperty.Key);
                //switch (sort_num)
                //{

                //    //desc
                //    case -1:
                //        OrderLandplots = FilterLandplot.OrderByDescending(realproperty => realproperty.Key);
                //        break;
                //    case -2:
                //        OrderLandplots = FilterLandplot.OrderByDescending(realproperty => realproperty.Cadastralnumber);
                //        break;
                //    case -3:
                //        OrderLandplots = FilterLandplot.OrderByDescending(realproperty => realproperty.Area);
                //        break;
                //    case -4:
                //        OrderLandplots = FilterLandplot.OrderByDescending(realproperty => realproperty.City);
                //        break;
                //    case -5:
                //        OrderLandplots = FilterLandplot.OrderByDescending(realproperty => realproperty.Street);
                //        break;
                //    case -6:
                //        OrderLandplots = FilterLandplot.OrderByDescending(realproperty => realproperty.Housenum);
                //        break;
                //    case -7:
                //        OrderLandplots = FilterLandplot.OrderByDescending(realproperty => realproperty.Location);
                //        break;
                //    case -8:
                //        OrderLandplots = FilterLandplot.OrderByDescending(realproperty => realproperty.Ownershiptype);
                //        break;
                //    case -9:
                //        OrderLandplots = FilterLandplot.OrderByDescending(realproperty => realproperty.Extrainformation);
                //        break;


                //    //asc
                //    case 1:
                //        OrderLandplots = FilterLandplot.OrderBy(realproperty => realproperty.Key);
                //        break;
                //    case 2:
                //        OrderLandplots = FilterLandplot.OrderBy(realproperty => realproperty.Cadastralnumber);
                //        break;
                //    case 3:
                //        OrderLandplots = FilterLandplot.OrderBy(realproperty => realproperty.Area);
                //        break;
                //    case 4:
                //        OrderLandplots = FilterLandplot.OrderBy(realproperty => realproperty.City);
                //        break;
                //    case 5:
                //        OrderLandplots = FilterLandplot.OrderBy(realproperty => realproperty.Street);
                //        break;
                //    case 6:
                //        OrderLandplots = FilterLandplot.OrderBy(realproperty => realproperty.Housenum);
                //        break;
                //    case 7:
                //        OrderLandplots = FilterLandplot.OrderBy(realproperty => realproperty.Location);
                //        break;
                //    case 8:
                //        OrderLandplots = FilterLandplot.OrderBy(realproperty => realproperty.Ownershiptype);
                //        break;
                //    case 9:
                //        OrderLandplots = FilterLandplot.OrderBy(realproperty => realproperty.Extrainformation);
                //        break;
                //}


                return FilterLandplot.ToList();
            }
        }

        public Landplot GetLand(int Key)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var Landplots = context.Landplots;
                    return Landplots.AsQueryable().FirstOrDefault(x => x.Landplotkey == Key);
                }
            }
            catch
            {
                return null;
            }
        }

        public string UpdateLand(Landplot landplot)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Landplots.Update(landplot);
                    context.SaveChanges();
                    return $"Земельну ділянку {landplot.Landplotkey} оновлено";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string ChargeTaxesToAll()
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Database.ExecuteSqlRaw("CALL charge_taxes_to_all();");
                    return "1";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public string ChargeTaxesByLand(int landplotkey)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Database.ExecuteSqlRaw("CALL charge_taxes_by_land({0});", landplotkey);
                    return "1";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public string ChargeTaxesByCounterparty(int counterpartykey)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Database.ExecuteSqlRaw("CALL charge_taxes_by_counterparty({0});", counterpartykey);
                    return "1";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public string ChargeTaxes(int landplotkey,int counterpartykey)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Database.ExecuteSqlRaw("CALL charge_taxes({0},{1});", landplotkey,counterpartykey);
                    return "1";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public string UpdateLandplotOwners(int landplotkey, List<Owner>owners)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    string str = "ARRAY[";
                    for(int i=0;i<owners.Count;i++)
                    {
                        if (i > 0) str += ",";
                        Owner owner = owners[i];
                        str += "("+ Convert.ToString(owner.Counterpartykey) + "," +
                            (owner.Share).ToString(new CultureInfo("en-us", false)) + "," +
                            Convert.ToString(owner.Withouttax) +  ")::owners";
                    }
                    str+= "]";
                    context.Database.ExecuteSqlRaw("CALL update_landplot_owners({0},"+str+");", landplotkey);
                    return $"Змінено {owners.Count} власників";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
