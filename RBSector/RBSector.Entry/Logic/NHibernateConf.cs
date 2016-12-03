using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using RBSector.DataBase.Models;
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
            var mapping = GetMappings();
            myConf.AddDeserializedMapping(mapping, "NHSchemaTest");
            SchemaMetadataUpdater.QuoteTableAndColumns(myConf);
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

        public static HbmMapping GetMappings()
        {
            ModelMapper mapper = new ModelMapper();

            mapper.AddMapping<UsersdataMap>();
            mapper.AddMapping<OrdersMap>();
            mapper.AddMapping<CategoryMap>();
            mapper.AddMapping<ImagesMap>();
            mapper.AddMapping<IngredientsMap>();
            mapper.AddMapping<OrdersproductsMap>();
            mapper.AddMapping<ProductsMap>();
            mapper.AddMapping<TabsMap>();

            HbmMapping mapping = mapper.CompileMappingFor(new[] { typeof(Usersdata), typeof(Orders), typeof(Category),
            typeof(Images), typeof(Ingredients), typeof(Ordersproducts), typeof(Products), typeof(Tabs)});
            return mapping;
        }
    }
}
