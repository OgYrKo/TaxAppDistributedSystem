using DBClassesLibrary;
using System.ServiceModel;
using System.Collections.Generic;

namespace InterfacesLibrary
{
    [ServiceContract]
    public interface ISpecialpurpose : ISetUser
    {
        [OperationContract]
        List<Specialpurposechapter> GetAllChaptersWithFilter(List<short> Chapterkeys, List<char> Sectionkeys, string Chapter);
        [OperationContract]
        List<Specialpurposesection> GetAllSectionsWithFilter(List<char> Sectionkeys, string Section);
        [OperationContract]
        List<Specialpurposesubgroup> GetAllSubGroupsWithFilter(List<short> Chapterkeys, List<short> Groupkeys, string Subgrouptext);
        [OperationContract]
        List<Specialpurposesubgroup> GetAllSubGroups();
        [OperationContract]
        Specialpurposesubgroup GetSubGroup(short SubGroupkey, short Chapterkey);
        [OperationContract]
        string AddSubGroup(Specialpurposesubgroup specialpurposesubgroup);//(short Groupkey, short Chapterkey, string Subgrouptext);
        [OperationContract]
        string UpdateSubGroup(Specialpurposesubgroup specialpurposesubgroup);// (short Groupkey, short Chapterkey, string Subgrouptext);
        [OperationContract]
        string DeleteSubGroup(List<(short, short)> SubGroupAndChapterKeys); //(short SubGroupkey, short Chapterkey);


        [OperationContract]
        List<Specialpurposechapter> GetAllChapters();
        [OperationContract]
        Specialpurposechapter GetChapter(short Chapterkey);
        [OperationContract]
        string AddChapter(Specialpurposechapter specialpurposechapter);//(short Chapterkey, char Sectionkey, string Chapter);
        [OperationContract]
        string UpdateChapter(Specialpurposechapter specialpurposechapter); //(short Chapterkey, char Sectionkey, string Chapter);
        [OperationContract]
        string DeleteChapter(List<short> Chapterkeys);


        [OperationContract]
        List<Specialpurposesection> GetAllSections();
        [OperationContract]
        Specialpurposesection GetSection(char Sectionkey);
        [OperationContract]
        string AddSection(Specialpurposesection specialpurposesection);//(char Sectionkey, string Section);
        [OperationContract]
        string UpdateSection(Specialpurposesection specialpurposesection);//(char Sectionkey, string Section);
        [OperationContract]
        string DeleteSection(List<char> Sectionkeys);
    }
}
