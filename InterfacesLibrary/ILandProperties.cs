using DBClassesLibrary;
using System.ServiceModel;
using System.Collections.Generic;

namespace InterfacesLibrary
{
    [ServiceContract]
    public interface ILandProperties : ISetUser
    {
        [OperationContract]
        List<Specialpurposeland> GetAllSpecialpurposelands();
        [OperationContract]
        List<Squarelandplot> GetAllSquarelandplots();
        [OperationContract]
        List<Standartvaluation> GetAllStandartvaluations();
        [OperationContract]
        List<Monetaryvaluation> GetAllMonetaryValuations();
        [OperationContract]
        int GetNextIdForSpecialpurposeland();
        [OperationContract]
        int GetNextIdForSquarelandplot();
        [OperationContract]
        int GetNextIdForStandartvaluation();
        [OperationContract]
        int GetNextIdForMonetaryvaluation();

        [OperationContract]
        string AddSquarelandplot(Squarelandplot squarelandplot);//(int Key, int Landplotkey, float Square, DateTime Date);
        [OperationContract]
        string AddStandartvaluation(Standartvaluation standartvaluation);// (int Key, int Landplotkey, decimal Standartvaluation, DateTime Date);
        [OperationContract]
        string AddMonetaryvaluation(Monetaryvaluation monetaryvaluation);// (int Key, int Landplotkey, decimal Monetaryvaluation, DateTime Date);
        [OperationContract]
        string AddSpecialpurposeland(Specialpurposeland specialpurposeland);// (int Key, int Specialpurposechapter, int Specialpurposesubgroup, int Landplotkey, DateTime Date);
        [OperationContract]
        List<Ownershiptype> GetAllOwnershiptype();
        [OperationContract]
        Ownershiptype GetOwnershiptype(int Key);
        [OperationContract]
        string AddOwnershiptype(Ownershiptype ownershiptype);// (int Key, string Name);
        [OperationContract]
        string UpdateOwnershiptype(Ownershiptype ownershiptype);// (int Key, string Name);
        [OperationContract]
        string DeleteOwnershiptype(List<int> Keys);

    }
}
