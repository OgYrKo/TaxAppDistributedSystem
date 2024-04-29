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
    public class Nace : SetUserTemplate, INace
    {
        public string AddChapter(Nacechapter nacechapter)//(short Chapterkey, char Sectionkey, string Chapter)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Nacechapters.Add(nacechapter);
                    context.SaveChanges();
                    return $"Розділ КВЕД доданий";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string AddClass(Naceclass naceclass)//(short Classkey, short Groupkey, short Chapterkey, string Class)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Naceclasses.Add(naceclass);
                    context.SaveChanges();
                    return $"Клас КВЕД доданий";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string AddGroup(Nacegroup nacegroup)//(short Groupkey, short Chapterkey, string Grouptext)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Nacegroups.Add(nacegroup);
                    context.SaveChanges();
                    return $"Група КВЕД додана";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string AddSection(Nacesection nacesection)//(char Sectionkey, string Section)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Nacesections.Add(nacesection);
                    context.SaveChanges();
                    return $"Секція КВЕД додана";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string DeleteChapter(List<short> Chapterkeys)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    int counter = 0;
                    var Nacechapters = context.Nacechapters;
                    var toDelete = Nacechapters.Where(value => Chapterkeys.Contains(value.Chapterkey));
                    foreach (var value in toDelete)
                    {
                        Nacechapters.Remove(value);
                        counter++;
                    }
                    context.SaveChanges();
                    return $"Видалено {counter} розділів КВЕД";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.InnerException.Message.ToString();
            }
        }

        public string DeleteClass(List<short> Classkeys)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    int counter = 0;
                    var Naceclasses = context.Naceclasses;
                    var toDelete = Naceclasses.Where(value => Classkeys.Contains(value.Classkey));
                    foreach (var value in toDelete)
                    {
                        Naceclasses.Remove(value);
                        counter++;
                    }
                    context.SaveChanges();
                    return $"Видалено {counter} класів КВЕД";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.InnerException.Message.ToString();
            }
        }

        public string DeleteGroup(List<short> Groupkeys)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    int counter = 0;
                    var Nacegroups = context.Nacegroups;
                    var toDelete = Nacegroups.Where(value => Groupkeys.Contains(value.Groupkey));
                    foreach (var value in toDelete)
                    {
                        Nacegroups.Remove(value);
                        counter++;
                    }
                    context.SaveChanges();
                    return $"Видалено {counter} груп КВЕД";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.InnerException.Message.ToString();
            }
        }

        public string DeleteSection(List<char> Sectionkey)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    int counter = 0;
                    var Nacesections = context.Nacesections;
                    var toDelete = Nacesections.Where(value => Sectionkey.Contains(value.Sectionkey));
                    foreach (var value in toDelete)
                    {
                        Nacesections.Remove(value);
                        counter++;
                    }
                    context.SaveChanges();
                    return $"Видалено {counter} секцій КВЕД";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.InnerException.Message.ToString();
            }
        }

        public List<Nacechapter> GetAllChapters()
        {
            using (var context = new TSNAPContext())
            {
                List<Nacechapter> Nacechapters;
                Nacechapters = context.Nacechapters.ToList();
                return Nacechapters;
            }
        }

        public List<Naceclass> GetAllClasses()
        {
            using (var context = new TSNAPContext())
            {
                List<Naceclass> Naceclasses;
                Naceclasses = context.Naceclasses.ToList();
                return Naceclasses;
            }
        }

        public List<Nacegroup> GetAllGroups()
        {
            using (var context = new TSNAPContext())
            {
                List<Nacegroup> Nacegroups;
                Nacegroups = context.Nacegroups.ToList();
                return Nacegroups;
            }
        }

        public List<Nacesection> GetAllSections()
        {
            using (var context = new TSNAPContext())
            {
                List<Nacesection> Nacesections;
                Nacesections = context.Nacesections.ToList();
                return Nacesections;
            }
        }

        public Nacechapter GetChapter(short Chapterkey)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var Nacechapters = context.Nacechapters;
                    return Nacechapters.AsQueryable().FirstOrDefault(x => x.Chapterkey == Chapterkey);
                }
            }
            catch
            {
                return null;
            }
        }

        public Naceclass GetClass(short Classkey)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var Naceclasses = context.Naceclasses;
                    return Naceclasses.AsQueryable().FirstOrDefault(x => x.Classkey == Classkey);
                }
            }
            catch
            {
                return null;
            }
        }

        public Nacegroup GetGroup(short Groupkey)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var Nacegroups = context.Nacegroups;
                    return Nacegroups.AsQueryable().FirstOrDefault(x => x.Groupkey == Groupkey);
                }
            }
            catch
            {
                return null;
            }
        }

        public Nacesection GetSection(char Sectionkey)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var Nacesections = context.Nacesections;
                    return Nacesections.AsQueryable().FirstOrDefault(x => x.Sectionkey == Sectionkey);
                }
            }
            catch
            {
                return null;
            }
        }

        public string UpdateChapter(Nacechapter nacechapter)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Nacechapters.Update(nacechapter);
                    context.SaveChanges();
                    return $"Розділ КВЕД {nacechapter.Chapterkey} оновлено";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string UpdateClass(Naceclass naceclass)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Naceclasses.Update(naceclass);
                    context.SaveChanges();
                    return $"Клас КВЕД {naceclass.Chapterkey}.{naceclass.Groupkey}.{naceclass.Classkey} оновлено";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string UpdateGroup(Nacegroup nacegroup)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Nacegroups.Update(nacegroup);
                    context.SaveChanges();
                    return $"Групу КВЕД {nacegroup.Chapterkey}.{nacegroup.Groupkey} оновлено";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string UpdateSection(Nacesection nacesection)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Nacesections.Update(nacesection);
                    context.SaveChanges();
                    return $"Секцію КВЕД {nacesection.Sectionkey} оновлено";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
