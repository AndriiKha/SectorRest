using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.Entry.Interfaces
{
    public interface IUniversalCRUD<T>
    {
        bool Delete(T obj);
        bool SaveOrUpdate(T obj);
        string Get(int? id);
    }
}
