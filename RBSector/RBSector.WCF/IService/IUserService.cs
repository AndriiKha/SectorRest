using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.WCF.IService
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        bool isLogIn(string login, string password);
    }
}
