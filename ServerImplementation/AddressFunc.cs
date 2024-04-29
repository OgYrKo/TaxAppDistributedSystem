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
    public class AddressFunc : SetUserTemplate, IAddressFunc
    {
        public string AddAddress(Address address)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Addresses.Add(address);
                    context.SaveChanges();
                    return $"Нова адреса додана";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string AddCity(City city)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Cities.Add(city);
                    context.SaveChanges();
                    return $"Місто додано";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string AddSelectionaddresses(List<int> Citykeys)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var Selectionaddresses = context.Selectionaddresses;
                    foreach (var CityKey in Citykeys)
                        Selectionaddresses.Add(new Selectionaddress() {
                                                                           Selectionaddresskey = Selectionaddresses.Max(model => model.Selectionaddresskey) +1,
                                                                           Citykey = CityKey    
                                                                       });
                    context.SaveChanges();
                    return $"Адресу відбору додано";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string AddStreet(Street street)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Streets.Add(street);
                    context.SaveChanges();
                    return $"Вулицю додано";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Keys">
        /// Keys.Item1 - City key; 
        /// Keys.Item2 - Street Key
        /// </param>
        /// <returns></returns>
        public string DeleteAddresses(List<(int, int)> Keys)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    int counter = 0;
                    var Addresses = context.Addresses;

                    foreach (var AddressKey in Keys)
                    {
                        var toDelete = Addresses.Where(value => AddressKey.Item1.Equals(value.Citykey) && AddressKey.Item2.Equals(value.Streetkey));
                        if (toDelete.Count() > 0)
                        {
                            Addresses.Remove(toDelete.First());
                            counter++;
                        }
                    }
                    context.SaveChanges();
                    return $"Видалено {counter} підгруп";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.InnerException.Message.ToString();
            }
        }

        public string DeleteCities(List<int> Keys)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    int counter = 0;
                    var Cities = context.Cities;
                    var toDelete = Cities.Where(value => Keys.Contains(value.Citykey));
                    foreach (var value in toDelete)
                    {
                        Cities.Remove(value);
                        counter++;
                    }
                    context.SaveChanges();
                    return $"Видалено {counter} міст";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.InnerException.Message.ToString();
            }
        }

        public string DeleteSelectionaddresses(List<int> Selectionkeys)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    int counter = 0;
                    var Selectionaddresses = context.Selectionaddresses;
                    var toDelete = Selectionaddresses.Where(value => Selectionkeys.Contains(value.Selectionaddresskey));
                    foreach (var value in toDelete)
                    {
                        Selectionaddresses.Remove(value);
                        counter++;
                    }
                    context.SaveChanges();
                    return $"Видалено {counter} адрес відбору";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.InnerException.Message.ToString();
            }
        }

        public string DeleteStreets(List<int> Keys)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    int counter = 0;
                    var Streets = context.Streets;
                    var toDelete = Streets.Where(value => Keys.Contains(value.Streetkey));
                    foreach (var value in toDelete)
                    {
                        Streets.Remove(value);
                        counter++;
                    }
                    context.SaveChanges();
                    return $"Видалено {counter} вулиць";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.InnerException.Message.ToString();
            }
        }

        public Address GetAddress(int CityKey, int StreetKey)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var Addresses = context.Addresses;
                    return Addresses.AsQueryable().FirstOrDefault(x => x.Citykey == CityKey && x.Streetkey == StreetKey);
                }
            }
            catch
            {
                return null;
            }
        }

        //TODO addresses with join?
        public List<Address> GetAllAddresses()
        {
            using (var context = new TSNAPContext())
            {
                List<Address> Addresses;
                Addresses = context.Addresses.ToList();
                return Addresses;
            }
        }

        public List<Street> GetAllStreetsWithFilter(List<int> keys, string street, int sort_num)
        {
            using (var context = new TSNAPContext())
            {
                var Streets = context.Streets;
                IQueryable<Street> FilterCounterparties = Streets;
                if (keys != null)
                {
                    IQueryable<Street> streets = FilterCounterparties.Where(value => keys.Contains(value.Streetkey));
                    FilterCounterparties = streets;
                }

                if (street != null)
                {
                    IQueryable<Street> streets = FilterCounterparties.Where(value => value.Street1.Contains(street));
                    FilterCounterparties = streets;
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
                //}


                return FilterCounterparties.ToList();
            }
        }

        public List<City> GetAllCities()
        {
            using (var context = new TSNAPContext())
            {
                List<City> Cities;
                Cities = context.Cities.OrderBy(value => value.City1).ToList();
                return Cities;
            }
        }

        public List<Selectionaddress> GetAllSelectionaddresses()
        {
            using (var context = new TSNAPContext())
            {
                List<Selectionaddress> selectionaddresses;
                selectionaddresses = context.Selectionaddresses.ToList();
                return selectionaddresses;
            }
        }

        public List<Street> GetAllStreets()
        {
            using (var context = new TSNAPContext())
            {
                List<Street> Streets;
                Streets = context.Streets.ToList();
                return Streets;
            }
        }

        public City GetCity(int Key)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var Cities = context.Cities;
                    return Cities.AsQueryable().FirstOrDefault(x => x.Citykey == Key);
                }
            }
            catch
            {
                return null;
            }
        }

        public Selectionaddress GetSelectionaddress(int Selectionkey)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var Selectionaddresses = context.Selectionaddresses;
                    return Selectionaddresses.AsQueryable().FirstOrDefault(x => x.Selectionaddresskey == Selectionkey);
                }
            }
            catch
            {
                return null;
            }
        }

        public Street GetStreet(int Key)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var Streets = context.Streets;
                    return Streets.AsQueryable().FirstOrDefault(x => x.Streetkey == Key);
                }
            }
            catch
            {
                return null;
            }
        }

        public string UpdateAddress(Address address)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Addresses.Update(address);
                    context.SaveChanges();
                    return $"Адресу оновлено";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string UpdateCity(City city)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Cities.Update(city);
                    context.SaveChanges();
                    return $"Місто {city.Citykey} оновлено";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string UpdateStreet(Street street)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Streets.Update(street);
                    context.SaveChanges();
                    return $"Вулицю {street.Streetkey} оновлено";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
