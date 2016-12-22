using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.WCF.IService
{
    [ServiceContract]
    public interface ITabsService
    {
        [OperationContract]
        bool AddTabs(string name);

        [OperationContract]
        string GetAllTabs();

        [OperationContract]
        string GetTab(string name);
    }
}
