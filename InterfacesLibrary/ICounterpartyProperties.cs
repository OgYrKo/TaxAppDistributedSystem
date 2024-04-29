using DBClassesLibrary;
using System.ServiceModel;
using System.Collections.Generic;

namespace InterfacesLibrary
{
    [ServiceContract]
    public interface ICounterpartyProperties : ISetUser
    {
        [OperationContract]
        List<Counterpartygroup> GetAllCounterpartygroups();
        [OperationContract]
        Counterpartygroup GetCounterpartygroup(int Key);
        [OperationContract]
        string AddCounterpartygroup(Counterpartygroup counterpartygroup);
        [OperationContract]
        string UpdateCounterpartygroup(Counterpartygroup counterpartygroup);
        [OperationContract]
        string DeleteCounterpartygroups(List<int> Keys);


        [OperationContract]
        List<Counterpartytype> GetAlllCounterpartytypes();
        [OperationContract]
        Counterpartytype GetCounterpartytype(int Key);
        [OperationContract]
        string AddCounterpartytype(Counterpartytype counterpartytype);
        [OperationContract]
        string UpdateCounterpartytype(Counterpartytype counterpartytype);
        [OperationContract]
        string DeleteCounterpartytypes(List<int> Keys);


        [OperationContract]
        List<Benefit> GetAllBenefits();
        [OperationContract]
        Benefit GetBenefit(int Key);
        [OperationContract]
        string AddBenefit(Benefit benefit);
        [OperationContract]
        string UpdateBenefit(Benefit benefit);
        [OperationContract]
        string DeleteBenefits(List<int> Keys);


        [OperationContract]
        string AddBenefitToCounterparty(Counterpartyandbenefit counterpartyandbenefit);
    }
}
