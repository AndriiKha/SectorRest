using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.Entry.Logic
{
    public class NHibernateBase
    {
        private static Configuration Configuration { get; set; }
        private static ISession session = null;
        private static IStatelessSession statelessSession = null;

        public static ISessionFactory SessionFactory { get; set; }

        public static ISession Session
        {
            get
            {
                if (session == null)
                    session = SessionFactory.OpenSession();
                return session;
            }
        }

        public static IStatelessSession StatelessSession
        {
            get
            {
                if (statelessSession == null)
                    statelessSession = SessionFactory.OpenStatelessSession();
                return statelessSession;
            }
        }

        public static Configuration ConfigureNHibernate()
        {
            Configuration = new Configuration();
            Configuration.Configure();
            return Configuration;
        }

        public void Initialize()
        {
            Configuration = ConfigureNHibernate();
            SessionFactory = Configuration.BuildSessionFactory();
            new SchemaExport(Configuration);
        }
    }
}
