using CoreWCF;
using DBClassesLibrary;
using InterfacesLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ServerImplementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class RealpropertyProperties: SetUserTemplate, IRealpropertyProperties
    {
        public string AddPropertysquare(Propertysquare propertysquare)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Propertysquares.Add(propertysquare);
                    context.SaveChanges();
                    return $"Інформацію о площі об'єкту нерухомості {propertysquare.Propertysquarekey} додано";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public List<Propertytype> GetAllPropertytypes() 
        {
            using (var context = new TSNAPContext())
            {
                List<Propertytype> Propertytypes;
                Propertytypes = context.Propertytypes.ToList();
                return Propertytypes;
            }
        }
        public List<Propertytype> GetAllPropertytypesWithFilter(int? Key,List<string> Types, int sort_num)
        {
            using (var context = new TSNAPContext())
            {
                var Propertytypes = context.Propertytypes;
                IQueryable<Propertytype> FilterProperty = Propertytypes;
                if (Key != null)
                {
                    IQueryable<Propertytype> propertytypes = FilterProperty.Where(value => value.Propertytypekey.ToString().Contains(Key.ToString()));
                    FilterProperty = propertytypes;
                }
                if (Types != null)
                {
                    IQueryable<Propertytype> propertytypes = FilterProperty.Where(value => Types.Contains(value.Type));
                    FilterProperty = propertytypes;
                }


                //var OrderPropertytypes = FilterProperty.OrderBy(Propertytype => Propertytype.Key);
                //switch (sort_num)
                //{

                //    //desc
                //    case -1:
                //        OrderPropertytypes = FilterProperty.OrderByDescending(Propertytype => Propertytype.Key);
                //        break;
                //    case -2:
                //        OrderPropertytypes = FilterProperty.OrderByDescending(Propertytype => Propertytype.Type);
                //        break;
                    
                //    //asc
                //    case 1:
                //        OrderPropertytypes = FilterProperty.OrderBy(Propertytype => Propertytype.Key);
                //        break;
                //    case 2:
                //        OrderPropertytypes = FilterProperty.OrderBy(Propertytype => Propertytype.Type);
                //        break;
                    
                //}


                return FilterProperty.ToList();
            }
        }
        public Propertytype GetPropertytype(int Key)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var Propertytypes = context.Propertytypes;
                    return Propertytypes.AsQueryable().FirstOrDefault(x => x.Propertytypekey == Key);
                }
            }
            catch
            {
                return null;
            }
        }

        public string AddPropertytype(Propertytype propertytype)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Propertytypes.Add(propertytype);
                    context.SaveChanges();
                    return $" Об'єкт нерухомості {propertytype.Propertytypekey} додано";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string UpdatePropertytype(Propertytype propertytype)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Propertytypes.Update(propertytype);
                    context.SaveChanges();
                    return $" Об'єкт нерухомості {propertytype.Propertytypekey} оновлено";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string DeletePropertytypes(List<int> Keys)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    int counter = 0;
                    var Realproperties = context.Realproperties;
                    var toDelete = Realproperties.Where(value => Keys.Contains(value.Realpropertykey));
                    foreach (var value in toDelete)
                    {
                        Realproperties.Remove(value);
                        counter++;
                    }
                    context.SaveChanges();
                    return $"Видалено {counter} об'єктів нерухомості";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.InnerException.Message.ToString();
            }
        }
    }
}
