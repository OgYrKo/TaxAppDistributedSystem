using CoreWCF;
using DBClassesLibrary;
using EntityFramework.Exceptions.Common;
using InterfacesLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ServerImplementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class PropertyFunc : SetUserTemplate, IPropertyFunc
    {
        public string AddProperty(Realproperty realproperty)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Realproperties.Add(realproperty);
                    context.SaveChanges();
                    return $" Об'єкт нерухомості {realproperty.Realpropertykey} додано";//TODO not sure 
                }
            }
            catch (UniqueConstraintException e)
            {
                return e.InnerException.Message;
            }
            catch (ReferenceConstraintException e)
            {
                return e.InnerException.Message;
            }
            catch (ConstraintException e)
            {
                return e.InnerException.Message;
            }
            catch (CannotInsertNullException e)
            {
                return e.InnerException.Message;
            }
            catch (DbUpdateException e)
            {
                return e.InnerException.Message;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public int GetNextIdForRealProperty()
        {
            using (var context = new TSNAPContext())
            {
                int maxId = context.Realproperties.Max(model => model.Realpropertykey);
                return (maxId + 1);
            }
        }

        public string DeleteProperties(List<int> Keys)
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
            catch(Exception e)
            {
                return e.InnerException.Message.ToString();
            }
            
        }

        public List<Realproperty> GetAll()
        {
            
            using (var context = new TSNAPContext())
            {
                List<Realproperty> Realproperties;
                Realproperties = context.Realproperties.ToList();
                return Realproperties;
            }
        }

        public List<Realproperty> GetAllWithFilter(int? Key, bool? Purpose, List<int> Types, 
                                                    List<short> Areas, bool? View, List<int> Cities,
                                                    List<int> Streets, string Housenum, string Extrainformation, int sort_num)
        {
            using (var context = new TSNAPContext())
            {
                var Realproperties = context.Realproperties;
                IQueryable<Realproperty> FilterProperty = Realproperties;
                if (Key != null)
                {
                    IQueryable<Realproperty> realproperties = FilterProperty.Where(value => value.Realpropertykey.ToString().Contains(Key.ToString()));
                    FilterProperty = realproperties;
                }

                if (Purpose != null)
                {
                    IQueryable<Realproperty> realproperties = FilterProperty.Where(value => value.Purpose.Equals(Purpose));
                    FilterProperty = realproperties;
                }
                if (Types != null)
                {
                    IQueryable<Realproperty> realproperties = FilterProperty.Where(value => Types.Contains(value.Propertytypekey));
                    FilterProperty = realproperties;
                }
                    
                if (Areas != null)
                {
                    IQueryable<Realproperty> realproperties = FilterProperty.Where(value => Areas.Contains(value.Area));
                    FilterProperty = realproperties;
                }
                    
                if (View != null)
                {
                    IQueryable<Realproperty> realproperties = FilterProperty.Where(value => value.View.Equals(View));
                    FilterProperty = realproperties;
                }
                    
                if (Cities != null)
                {
                    IQueryable<Realproperty> realproperties = FilterProperty.Where(value => Cities.Contains(value.Citykey));
                    FilterProperty = realproperties;
                }
                    
                if (Streets != null)
                {
                    IQueryable<Realproperty> realproperties = FilterProperty.Where(value => Streets.Contains(value.Streetkey));
                    FilterProperty = realproperties;
                }
                    
                if (Housenum != null)
                {
                    IQueryable<Realproperty> realproperties = FilterProperty.Where(value => value.Housenum.Contains(Housenum));
                    FilterProperty = realproperties;
                }
                    
                if (Extrainformation != null)
                {
                    IQueryable<Realproperty> realproperties = FilterProperty.Where(value => value.Extrainformation.Contains(Extrainformation));
                    FilterProperty = realproperties;
                }
                    

                //var OrderRealproperties = FilterProperty.OrderBy(realproperty => realproperty.Key);
                //switch (sort_num)
                //{

                //    //desc
                //    case -1:
                //        OrderRealproperties = FilterProperty.OrderByDescending(realproperty => realproperty.Key);
                //        break;
                //    case -2:
                //        OrderRealproperties = FilterProperty.OrderByDescending(realproperty => realproperty.Purpose);
                //        break;
                //    case -3:
                //        OrderRealproperties = FilterProperty.OrderByDescending(realproperty => realproperty.Type);
                //        break;
                //    case -4:
                //        OrderRealproperties = FilterProperty.OrderByDescending(realproperty => realproperty.Area);
                //        break;
                //    case -5:
                //        OrderRealproperties = FilterProperty.OrderByDescending(realproperty => realproperty.View);
                //        break;
                //    case -6:
                //        OrderRealproperties = FilterProperty.OrderByDescending(realproperty => realproperty.City);
                //        break;
                //    case -7:
                //        OrderRealproperties = FilterProperty.OrderByDescending(realproperty => realproperty.Street);
                //        break;
                //    case -8:
                //        OrderRealproperties = FilterProperty.OrderByDescending(realproperty => realproperty.Housenum);
                //        break;
                //    case -9:
                //        OrderRealproperties = FilterProperty.OrderByDescending(realproperty => realproperty.Extrainformation);
                //        break;


                //        //asc
                //    case 1:
                //        OrderRealproperties = FilterProperty.OrderBy(realproperty => realproperty.Key);
                //        break;
                //    case 2:
                //        OrderRealproperties = FilterProperty.OrderBy(realproperty => realproperty.Purpose);
                //        break;
                //    case 3:
                //        OrderRealproperties = FilterProperty.OrderBy(realproperty => realproperty.Type);
                //        break;
                //    case 4:
                //        OrderRealproperties = FilterProperty.OrderBy(realproperty => realproperty.Area);
                //        break;
                //    case 5:
                //        OrderRealproperties = FilterProperty.OrderBy(realproperty => realproperty.View);
                //        break;
                //    case 6:
                //        OrderRealproperties = FilterProperty.OrderBy(realproperty => realproperty.City);
                //        break;
                //    case 7:
                //        OrderRealproperties = FilterProperty.OrderBy(realproperty => realproperty.Street);
                //        break;
                //    case 8:
                //        OrderRealproperties = FilterProperty.OrderBy(realproperty => realproperty.Housenum);
                //        break;
                //    case 9:
                //        OrderRealproperties = FilterProperty.OrderBy(realproperty => realproperty.Extrainformation);
                //        break;
                //}
                    

                return FilterProperty.ToList();
            }
        }

        public Realproperty GetProperty(int Key)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var Realproperties = context.Realproperties;
                    return Realproperties.AsQueryable().FirstOrDefault(x => x.Realpropertykey == Key);
                }
            }
            catch
            {
                return null;
            }
        }

        public string UpdateProperty(Realproperty realproperty)//int Key, bool Purpose, int Type, short Area, bool View, int City, int Street, string Housenum, string Extrainformation)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Realproperties.Update(realproperty);
                    context.SaveChanges();
                    return $" Об'єкт нерухомості {realproperty.Realpropertykey} оновлено";//TODO not sure 
                }
            }
            
            catch(UniqueConstraintException e)
            {
                return e.InnerException.Message;
            }
            catch (ReferenceConstraintException e)
            {
                return e.InnerException.Message;
            }
            catch (ConstraintException e)
            {
                return e.InnerException.Message;
            }
            catch (CannotInsertNullException e)
            {
                return e.InnerException.Message;
            }
            catch (DbUpdateException e)
            {
                return e.InnerException.Message;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
