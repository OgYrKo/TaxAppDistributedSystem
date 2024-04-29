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
    public class CounterpartyProperties : SetUserTemplate, ICounterpartyProperties
    {
        public string AddBenefit(Benefit benefit)//(int Key, string Benefit)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Benefits.Add(benefit);
                    context.SaveChanges();
                    return $"Нова льгота додана";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string AddCounterpartygroup(Counterpartygroup counterpartygroup)//(int Key, string Name)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Counterpartygroups.Add(counterpartygroup);
                    context.SaveChanges();
                    return $"Нова група створена";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string AddCounterpartytype(Counterpartytype counterpartytype)//(int Key, string Type)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Counterpartytypes.Add(counterpartytype);
                    context.SaveChanges();
                    return $"Новий тип контрагента додано";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string AddBenefitToCounterparty(Counterpartyandbenefit counterpartyandbenefit)//(int Counterpartykey, int Benefitskey, DateTime Date)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Counterpartyandbenefits.Add(counterpartyandbenefit);
                    context.SaveChanges();
                    return $"Льгота {counterpartyandbenefit.Benefitskey} додана до {counterpartyandbenefit.Counterpartykey}";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string DeleteBenefits(List<int> Keys)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    int counter = 0;
                    var Benefits = context.Benefits;
                    var toDelete = Benefits.Where(value => Keys.Contains(value.Benefitskey));
                    foreach (var value in toDelete)
                    {
                        Benefits.Remove(value);
                        counter++;
                    }
                    context.SaveChanges();
                    return $"Видалено {counter} льгот";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.InnerException.Message.ToString();
            }
        }

        public string DeleteCounterpartygroups(List<int> Keys)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    int counter = 0;
                    var Counterpartygroups = context.Counterpartygroups;
                    var toDelete = Counterpartygroups.Where(value => Keys.Contains(value.Counterpartygroupkey));
                    foreach (var value in toDelete)
                    {
                        Counterpartygroups.Remove(value);
                        counter++;
                    }
                    context.SaveChanges();
                    return $"Видалено {counter} груп";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.InnerException.Message.ToString();
            }
        }

        public string DeleteCounterpartytypes(List<int> Keys)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    int counter = 0;
                    var Counterpartytypes = context.Counterpartytypes;
                    var toDelete = Counterpartytypes.Where(value => Keys.Contains(value.Counterpartytypekey));
                    foreach (var value in toDelete)
                    {
                        Counterpartytypes.Remove(value);
                        counter++;
                    }
                    context.SaveChanges();
                    return $"Видалено {counter} типів";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.InnerException.Message.ToString();
            }
        }

        public List<Benefit> GetAllBenefits()
        {
            using (var context = new TSNAPContext())
            {
                List<Benefit> Benefits;
                Benefits = context.Benefits.ToList();
                return Benefits;
            }
        }

        public List<Counterpartygroup> GetAllCounterpartygroups()
        {
            using (var context = new TSNAPContext())
            {
                List<Counterpartygroup> Counterpartygroups;
                Counterpartygroups = context.Counterpartygroups.ToList();
                return Counterpartygroups;
            }
        }

        public List<Counterpartytype> GetAlllCounterpartytypes()
        {
            using (var context = new TSNAPContext())
            {
                List<Counterpartytype> Counterpartytypes;
                Counterpartytypes = context.Counterpartytypes.ToList();
                return Counterpartytypes;
            }
        }

        public Benefit GetBenefit(int Key)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var Benefits = context.Benefits;
                    return Benefits.AsQueryable().FirstOrDefault(x => x.Benefitskey == Key);
                }
            }
            catch
            {
                return null;
            }
        }

        public Counterpartygroup GetCounterpartygroup(int Key)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var Counterpartygroups = context.Counterpartygroups;
                    return Counterpartygroups.AsQueryable().FirstOrDefault(x => x.Counterpartygroupkey == Key);
                }
            }
            catch
            {
                return null;
            }
        }

        public Counterpartytype GetCounterpartytype(int Key)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var Counterpartytypes = context.Counterpartytypes;
                    return Counterpartytypes.AsQueryable().FirstOrDefault(x => x.Counterpartytypekey == Key);
                }
            }
            catch
            {
                return null;
            }
        }

        public string UpdateBenefit(Benefit benefit)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Benefits.Update(benefit);
                    context.SaveChanges();
                    return $"Льготу {benefit.Benefitskey} оновлено";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string UpdateCounterpartygroup(Counterpartygroup counterpartygroup)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Counterpartygroups.Update(counterpartygroup);
                    context.SaveChanges();
                    return $"Групу {counterpartygroup.Counterpartygroupkey} оновлено";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string UpdateCounterpartytype(Counterpartytype counterpartytype)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Counterpartytypes.Update(counterpartytype);
                    context.SaveChanges();
                    return $"Тип {counterpartytype.Counterpartytypekey} оновлено";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
