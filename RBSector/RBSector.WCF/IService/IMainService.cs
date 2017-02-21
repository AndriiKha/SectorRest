using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.WCF.IService
{
    [ServiceContract]
    public interface IMainService
    {
        [OperationContract]
        bool SaveResult(string json, string deleted);
        [OperationContract]
        bool SaveOrder(string json);
    }
}
