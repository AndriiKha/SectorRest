using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.WCF.IService
{
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        string GetAllProducts();
        [OperationContract]
        string GetProduct(int id);

    }
}
