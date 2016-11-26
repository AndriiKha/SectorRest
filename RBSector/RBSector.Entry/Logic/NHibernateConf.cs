using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.Entry.Logic
{
    public class NHibernateConf
    {
        private Configuration myConf;
        private ISessionFactory mySessionFactory;
        private static ISession mySession;
        private NHibernateConf()
        {
            myConf = new Configuration();
            myConf.Configure();
            mySessionFactory = myConf.BuildSessionFactory();
            mySession = mySessionFactory.OpenSession();
        }
        public static ISession Session
        {
            get
            {
                if (mySession == null)
                {
                    new NHibernateConf();
                }
                return mySession;
            }
        }
    }
}
