using NHibernate;
using NHibernate.Linq;
using RBSector.DataBase.Models;
using RBSector.DataBase.Tools;
using RBSector.Entry.Interfaces;
using RBSector.Entry.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.Entry.Entry
{
    public class UniversalCRUD<T> : IUniversalCRUD<T>
    {
        private ISession session;
        public UniversalCRUD()
        {
            session = NHibernateConf.Session;
        }
        public bool Delete<T>(int id)
        {
            try
            {
                using (session.BeginTransaction())
                {
                    T item = session.Get<T>(id);
                    if (item != null)
                    {
                        session.Delete(item);
                        session.Transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                string i = ex.Message;
                return false;
            }
            return true;
        }
        public T GetObj_ID(int id)
        {
            T result = default(T);
            try
            {
                using (session.BeginTransaction())
                {
                    result = session.Get<T>(id);
                }
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }

        public bool SaveOrUpdate(T obj)
        {
            if (obj == null) return false;
            try
            {
                using (session.BeginTransaction())
                {
                    int RECID = (obj as BaseModel).RECID;

                    T item = session.Get<T>(RECID);
                    if (item == null)
                    {
                        item = obj;
                        session.Save(item);
                    }
                    else {
                        if (obj is Products)
                        {
                            Products product = session.Get<Products>(RECID);
                            CRUDHelper.CopyProduct(ref product, obj as Products);
                            session.SaveOrUpdate(product);
                        }else if (obj is Images)
                        {
                            Images image = session.Get<Images>(RECID);
                            CRUDHelper.CopyImages(ref image, obj as Images);
                            session.SaveOrUpdate(image);
                        }
                        else {
                            item = obj;
                            session.SaveOrUpdate(item);
                            //session.Update(obj);
                        }
                    }
                    session.Transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                string i = ex.Message;
                return false;
            }
            return true;
        }
        public bool SaveOrUpdate(List<T> obj)
        {
            try
            {
                using (session.BeginTransaction())
                {
                    foreach (var ob in obj)
                    {
                        session.SaveOrUpdate(ob);
                    }
                    session.Transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                string i = ex.Message;
                return false;
            }
            return true;
        }
    }
}
