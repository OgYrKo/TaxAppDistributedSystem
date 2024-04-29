using DBClassesLibrary;
using System.Collections.Generic;
using System.ServiceModel;

namespace InterfacesLibrary
{
    [ServiceContract]
    public interface IGraphics : ISetUser
    {
        [OperationContract]
        List<Income> GetDailyIncomes();

        [OperationContract]
        List<Tax> GetDailyTaxes();

        [OperationContract]
        List<Quarterincome> GetQuarterIncomes();

        [OperationContract]
        List<Quartertax> GetQuarterTaxes();
    }
}
