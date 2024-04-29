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
    public class LandProperties : SetUserTemplate, ILandProperties
    {
        public List<Monetaryvaluation> GetAllMonetaryValuations()
        {
            using (var context = new TSNAPContext())
            {
                List<Monetaryvaluation> Monetaryvaluations;
                Monetaryvaluations = context.Monetaryvaluations.ToList();
                return Monetaryvaluations;
            }
        }

        public List<Standartvaluation> GetAllStandartvaluations()
        {
            using (var context = new TSNAPContext())
            {
                List<Standartvaluation> Standartvaluations;
                Standartvaluations = context.Standartvaluations.ToList();
                return Standartvaluations;
            }
        }

        public List<Squarelandplot> GetAllSquarelandplots()
        {
            using (var context = new TSNAPContext())
            {
                List<Squarelandplot> Squarelandplots;
                Squarelandplots = context.Squarelandplots.ToList();
                return Squarelandplots;
            }
        }

        public List<Specialpurposeland> GetAllSpecialpurposelands()
        {
            using (var context = new TSNAPContext())
            {
                List<Specialpurposeland> Specialpurposelands;
                Specialpurposelands = context.Specialpurposelands.ToList();
                return Specialpurposelands;
            }
        }





        public int GetNextIdForMonetaryvaluation()
        {
            using (var context = new TSNAPContext())
            {
                int maxId = context.Monetaryvaluations.Max(model => model.Monetaryvaluationkey);
                return (maxId + 1);
            }
        }

        public int GetNextIdForStandartvaluation()
        {
            using (var context = new TSNAPContext())
            {
                int maxId = context.Standartvaluations.Max(model => model.Standartvaluationkey);
                return (maxId + 1);
            }
        }

        public int GetNextIdForSquarelandplot()
        {
            using (var context = new TSNAPContext())
            {
                int maxId = context.Squarelandplots.Max(model => model.Squarelandplotkey);
                return (maxId + 1);
            }
        }
        public int GetNextIdForSpecialpurposeland()
        {
            using (var context = new TSNAPContext())
            {
                int maxId = context.Specialpurposelands.Max(model => model.Key);
                return (maxId + 1);
            }
        }

        public string AddMonetaryvaluation(Monetaryvaluation monetaryvaluation)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Monetaryvaluations.Add(monetaryvaluation);
                    context.SaveChanges();
                    return $"РГО доданий";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string AddOwnershiptype(Ownershiptype ownershiptype)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Ownershiptypes.Add(ownershiptype);
                    context.SaveChanges();
                    return $"Форма власності додана";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string AddSpecialpurposeland(Specialpurposeland specialpurposeland)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Specialpurposelands.Add(specialpurposeland);
                    context.SaveChanges();
                    return $"Цільове призначення земельної ділянки додано";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string AddSquarelandplot(Squarelandplot squarelandplot)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Squarelandplots.Add(squarelandplot);
                    context.SaveChanges();
                    return $"Площу земельної ділянки записано";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string AddStandartvaluation(Standartvaluation standartvaluation)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Standartvaluations.Add(standartvaluation);
                    context.SaveChanges();
                    return $"НГО додано";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string DeleteOwnershiptype(List<int> Keys)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    int counter = 0;
                    var Ownershiptypes = context.Ownershiptypes;
                    var toDelete = Ownershiptypes.Where(value => Keys.Contains(value.Ownershiptypekey));
                    foreach (var value in toDelete)
                    {
                        Ownershiptypes.Remove(value);
                        counter++;
                    }
                    context.SaveChanges();
                    return $"Видалено {counter} форму власності";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.InnerException.Message.ToString();
            }
        }

        public List<Ownershiptype> GetAllOwnershiptype()
        {
            using (var context = new TSNAPContext())
            {
                List<Ownershiptype> Ownershiptypes;
                Ownershiptypes = context.Ownershiptypes.ToList();
                return Ownershiptypes;
            }
        }

        public Ownershiptype GetOwnershiptype(int Key)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var Ownershiptypes = context.Ownershiptypes;
                    return Ownershiptypes.AsQueryable().FirstOrDefault(x => x.Ownershiptypekey == Key);
                }
            }
            catch
            {
                return null;
            }
        }

        public string UpdateOwnershiptype(Ownershiptype ownershiptype)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Ownershiptypes.Update(ownershiptype);
                    context.SaveChanges();
                    return $"Форму власності {ownershiptype.Ownershiptypekey} оновлено";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
