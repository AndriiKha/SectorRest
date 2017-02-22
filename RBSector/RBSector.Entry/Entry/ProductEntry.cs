using NHibernate;
using NHibernate.Linq;
using RBSector.DataBase.Models;
using RBSector.Entry.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.Entry.Entry
{
    public class ProductEntry
    {
        private UniversalCRUD<Products> prod_crud;
        public ProductEntry()
        {
            prod_crud = new UniversalCRUD<Products>();
        }
        public Products GetProduct(int recid)
        {
            Products product = null;
            try
            {
                product = prod_crud.GetObj_ID(recid);
            }
            catch (Exception exc)
            {
                return product;
            }
            return product;
        }
    }
}
