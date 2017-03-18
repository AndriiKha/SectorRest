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
                session = NHibernateConf.CreateNewSession;
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
                session = NHibernateConf.CreateNewSession;
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
        public object GetObj_Name(string name)
        {
            object result = default(T);
            try
            {
                session = NHibernateConf.CreateNewSession;
                using (session.BeginTransaction())
                {
                    if (typeof(T) == typeof(Products))
                    {
                        result = (from p in session.Query<Products>()
                                  where p.PrName.Equals(name)
                                  select p).FirstOrDefault();
                    }
                    if (typeof(T) == typeof(Category))
                    {
                        result = (from p in session.Query<Category>()
                                            where p.CtName.Equals(name)
                                            select p).FirstOrDefault();
                    }
                    if (typeof(T) == typeof(Tabs))
                    {
                        result = (from p in session.Query<Tabs>()
                                            where p.TbName.Equals(name)
                                            select p).FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }
        public bool isExist<T>(T _obj)
        {
            bool res = false;
            try
            {
                session = NHibernateConf.CreateNewSession;
                using (session.BeginTransaction())
                {
                    if (typeof(T) == typeof(Tabs))
                    {
                        var obj = (from p in session.Query<Tabs>()
                                   where p.TbName.Equals((_obj as Tabs).TbName)
                                   select p).FirstOrDefault();
                        res = obj != null;
                    }
                    else if (typeof(T) == typeof(Category))
                    {
                        var obj = (from p in session.Query<Category>()
                                   where p.CtName.Equals((_obj as Category).CtName)
                                   select p).FirstOrDefault();
                        res = obj != null;
                    }
                    else if (typeof(T) == typeof(Products))
                    {
                        var obj = (from p in session.Query<Products>()
                                   where p.PrName.Equals((_obj as Products).PrName)
                                   select p).FirstOrDefault();
                        res = obj != null;
                    }


                }

            }
            catch (Exception exc)
            {
                return res;
            }
            return res;
        }

        public bool SaveOrUpdate(T obj)
        {
            if (obj == null) return false;
            try
            {
                session = NHibernateConf.CreateNewSession;
                using (session.BeginTransaction())
                {
                    int RECID = (obj as BaseModel).RECID;
                    T item = session.Get<T>(RECID);
                    if (item == null)
                    {
                        item = obj;
                        //if (!isExist(item))
                        session.Save(item);
                        session.Transaction.Commit();
                    }
                    else {
                        if (obj is Products)
                        {
                            Products product = session.Get<Products>(RECID);
                            CRUDHelper.CopyProduct(ref product, obj as Products);
                            session.SaveOrUpdate(product);
                        }
                        else if (obj is Images)
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
        public bool SaveOrUpdate(List<T> obj)
        {
            try
            {
                session = NHibernateConf.CreateNewSession;
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
