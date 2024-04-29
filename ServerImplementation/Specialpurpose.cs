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
    public class Specialpurpose : SetUserTemplate, ISpecialpurpose
    {
        public string AddChapter(Specialpurposechapter specialpurposechapter)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Specialpurposechapters.Add(specialpurposechapter);
                    context.SaveChanges();
                    return $"Розділ КВЦПЗ доданий";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string AddSection(Specialpurposesection specialpurposesection)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Specialpurposesections.Add(specialpurposesection);
                    context.SaveChanges();
                    return $"Секція КВЦПЗ додана";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string AddSubGroup(Specialpurposesubgroup specialpurposesubgroup)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Specialpurposesubgroups.Add(specialpurposesubgroup);
                    context.SaveChanges();
                    return $"Підрозділ КВЦПЗ доданий";//TODO not sure 
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
                    var Specialpurposechapters = context.Specialpurposechapters;
                    var toDelete = Specialpurposechapters.Where(value => Chapterkeys.Contains(value.Chapterkey));
                    foreach (var value in toDelete)
                    {
                        Specialpurposechapters.Remove(value);
                        counter++;
                    }
                    context.SaveChanges();
                    return $"Видалено {counter} розділів";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.InnerException.Message.ToString();
            }
        }

        public string DeleteSection(List<char> Sectionkeys)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    int counter = 0;
                    var Specialpurposesections = context.Specialpurposesections;
                    var toDelete = Specialpurposesections.Where(value => Sectionkeys.Contains(value.Sectionkey));
                    foreach (var value in toDelete)
                    {
                        Specialpurposesections.Remove(value);
                        counter++;
                    }
                    context.SaveChanges();
                    return $"Видалено {counter} секцій";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.InnerException.Message.ToString();
            }
        }

        public string DeleteSubGroup(List<(short, short)> SubGroupAndChapterKeys)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    int counter = 0;
                    var Specialpurposesubgroups = context.Specialpurposesubgroups;
                    
                    foreach (var SubGroupAndChapterKey in SubGroupAndChapterKeys)
                    {
                        var toDelete = Specialpurposesubgroups.Where(value => SubGroupAndChapterKey.Item1.Equals(value.Groupkey) && SubGroupAndChapterKey.Item2.Equals(value.Chapterkey));
                        if (toDelete.Count() > 0)
                        {
                            Specialpurposesubgroups.Remove(toDelete.First());
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

        public List<Specialpurposechapter> GetAllChapters()
        {
            using (var context = new TSNAPContext())
            {
                List<Specialpurposechapter> Specialpurposechapters;
                Specialpurposechapters = context.Specialpurposechapters.OrderBy(value => value.Chapterkey).OrderBy(value => value.Sectionkey).ToList();
                return Specialpurposechapters;
            }
        }

        public List<Specialpurposesection> GetAllSections()
        {
            using (var context = new TSNAPContext())
            {
                List<Specialpurposesection> Specialpurposesections;
                Specialpurposesections = context.Specialpurposesections.OrderBy(value => value.Sectionkey).ToList();
                return Specialpurposesections;
            }
        }

        public List<Specialpurposesubgroup> GetAllSubGroups()
        {
            using (var context = new TSNAPContext())
            {
                List<Specialpurposesubgroup> Specialpurposesubgroups;
                Specialpurposesubgroups = context.Specialpurposesubgroups.OrderBy(value => value.Groupkey).OrderBy(value => value.Chapterkey).ToList();
                return Specialpurposesubgroups;
            }
        }

        public List<Specialpurposechapter> GetAllChaptersWithFilter(List<short> Chapterkeys, List<char> Sectionkeys, string Chapter)
        {
            using (var context = new TSNAPContext())
            {
                var Specialpurposechapters = context.Specialpurposechapters;
                IQueryable<Specialpurposechapter> FilterData = Specialpurposechapters;
                if (Chapterkeys != null)
                {
                    IQueryable<Specialpurposechapter> specialpurposechapters = FilterData.Where(value => Chapterkeys.Contains(value.Chapterkey));
                    FilterData = specialpurposechapters;
                }

                if (Sectionkeys != null)
                {
                    IQueryable<Specialpurposechapter> specialpurposechapters = FilterData.Where(value => Sectionkeys.Contains(value.Sectionkey));
                    FilterData = specialpurposechapters;
                }

                if (Chapter != null)
                {
                    IQueryable<Specialpurposechapter> specialpurposechapters = FilterData.Where(value => value.Chapter.Contains(Chapter));
                    FilterData = specialpurposechapters;
                }


                return FilterData.OrderBy(value => value.Sectionkey).OrderBy(value => value.Chapterkey).ToList();
            }
        }

        public List<Specialpurposesection> GetAllSectionsWithFilter(List<char> Sectionkeys, string Section)
        {
            using (var context = new TSNAPContext())
            {
                var Specialpurposesections = context.Specialpurposesections;
                IQueryable<Specialpurposesection> FilterData = Specialpurposesections;
                if (Section != null)
                {
                    IQueryable<Specialpurposesection> specialpurposechapters = FilterData.Where(value => value.Section.Contains(Section));
                    FilterData = specialpurposechapters;
                }

                if (Sectionkeys != null)
                {
                    IQueryable<Specialpurposesection> specialpurposechapters = FilterData.Where(value => Sectionkeys.Contains(value.Sectionkey));
                    FilterData = specialpurposechapters;
                }


                return FilterData.OrderBy(value => value.Sectionkey).ToList();
            }     
        }

        public List<Specialpurposesubgroup> GetAllSubGroupsWithFilter(List<short> Chapterkeys, List<short> Groupkeys, string Subgrouptext)
        {

            using (var context = new TSNAPContext())
            {
                var Specialpurposesubgroups = context.Specialpurposesubgroups;
                IQueryable<Specialpurposesubgroup> FilterData = Specialpurposesubgroups;
                if (Chapterkeys != null)
                {
                    IQueryable<Specialpurposesubgroup> specialpurposechapters = FilterData.Where(value => Chapterkeys.Contains(value.Chapterkey));
                    FilterData = specialpurposechapters;
                }

                if (Groupkeys != null)
                {
                    IQueryable<Specialpurposesubgroup> specialpurposechapters = FilterData.Where(value => Groupkeys.Contains(value.Groupkey));
                    FilterData = specialpurposechapters;
                }

                if (Subgrouptext != null)
                {
                    IQueryable<Specialpurposesubgroup> specialpurposechapters = FilterData.Where(value => value.Subgrouptext.Contains(Subgrouptext));
                    FilterData = specialpurposechapters;
                }

                return FilterData.OrderBy(value => value.Groupkey).OrderBy(value => value.Chapterkey).ToList();
            }
        }

        public Specialpurposechapter GetChapter(short Chapterkey)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var Specialpurposechapters = context.Specialpurposechapters;
                    return Specialpurposechapters.AsQueryable().FirstOrDefault(x => x.Chapterkey == Chapterkey);
                }
            }
            catch
            {
                return null;
            }
        }

        public Specialpurposesection GetSection(char Sectionkey)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var Specialpurposesections = context.Specialpurposesections;
                    return Specialpurposesections.AsQueryable().FirstOrDefault(x => x.Sectionkey == Sectionkey);
                }
            }
            catch
            {
                return null;
            }
        }

        public Specialpurposesubgroup GetSubGroup(short SubGroupkey, short Chapterkey)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var Specialpurposesubgroups = context.Specialpurposesubgroups;
                    return Specialpurposesubgroups.AsQueryable().FirstOrDefault(x => x.Groupkey == SubGroupkey && x.Chapterkey == Chapterkey);
                }
            }
            catch
            {
                return null;
            }
        }

        public string UpdateChapter(Specialpurposechapter specialpurposechapter)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Specialpurposechapters.Update(specialpurposechapter);
                    context.SaveChanges();
                    return $"Розділ {specialpurposechapter.Chapterkey} оновлено";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string UpdateSection(Specialpurposesection specialpurposesection)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Specialpurposesections.Update(specialpurposesection);
                    context.SaveChanges();
                    return $"Секцію {specialpurposesection.Sectionkey} оновлено";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string UpdateSubGroup(Specialpurposesubgroup specialpurposesubgroup)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    context.Specialpurposesubgroups.Update(specialpurposesubgroup);
                    context.SaveChanges();
                    return $"Підрозділ {specialpurposesubgroup.Groupkey} оновлено";//TODO not sure 
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
