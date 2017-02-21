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
        private ISession session;
        private UniversalCRUD<Products> prod_crud;
        public ProductEntry()
        {
            session = NHibernateConf.Session;
            prod_crud = new UniversalCRUD<Products>();
        }
        public Products GetProduct(int recid)
        {
            Products product = null;
            try
            {
                using (session.BeginTransaction())
                {
                    product = (from p in session.Query<Products>()
                               where p.RECID == recid
                               select p).FirstOrDefault();
                }
            }
            catch (Exception exc)
            {
                return product;
            }
            return product;
        }
    }
}
