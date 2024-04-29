using DBClassesLibrary;
using System.ServiceModel;
using System.Collections.Generic;

namespace InterfacesLibrary
{
    [ServiceContract]
    public interface INace : ISetUser
    {
        [OperationContract]
        List<Naceclass> GetAllClasses();
        [OperationContract]
        Naceclass GetClass(short Classkey);

        [OperationContract]
        string AddClass(Naceclass naceclass);//(short Classkey, short Groupkey, short Chapterkey, string Class)

        [OperationContract]
        string UpdateClass(Naceclass naceclass);

        [OperationContract]
        string DeleteClass(List<short> Classkeys);



        [OperationContract]
        List<Nacegroup> GetAllGroups();
        [OperationContract]
        Nacegroup GetGroup(short Groupkey);

        [OperationContract]
        string AddGroup(Nacegroup nacegroup);

        [OperationContract]
        string UpdateGroup(Nacegroup nacegroup);

        [OperationContract]
        string DeleteGroup(List<short> Groupkeys);



        [OperationContract]
        List<Nacechapter> GetAllChapters();
        [OperationContract]
        Nacechapter GetChapter(short Chapterkey);

        [OperationContract]
        string AddChapter(Nacechapter nacechapter);

        [OperationContract]
        string UpdateChapter(Nacechapter nacechapter);

        [OperationContract]
        string DeleteChapter(List<short> Chapterkeys);



        [OperationContract]
        List<Nacesection> GetAllSections();
        [OperationContract]
        Nacesection GetSection(char Sectionkey);

        [OperationContract]
        string AddSection(Nacesection nacesection);

        [OperationContract]
        string UpdateSection(Nacesection nacesection);

        [OperationContract]
        string DeleteSection(List<char> Sectionkey);
    }
}
