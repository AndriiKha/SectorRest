using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.Entry.Interfaces
{
    public interface IUniversalCRUD<T>
    {
        bool Delete<T>(int id);
        bool SaveOrUpdate(T obj);
        T GetObj_ID(int id);
    }
}
