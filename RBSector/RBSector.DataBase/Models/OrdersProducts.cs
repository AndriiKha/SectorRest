using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;

namespace RBSector.DataBase.Models
{
    public class Ordersproducts
    {
        public Ordersproducts() { }
        public virtual int OrdPrRecid { get; set; }
        public virtual Products Products { get; set; }
        public virtual Orders Orders { get; set; }
        public virtual int? OrdPrCount { get; set; }
    }
    public class OrdersproductsMap : ClassMapping<Ordersproducts>
    {

        public OrdersproductsMap()
        {
            Id(x => x.OrdPrRecid, map => { map.Column("ORD_PR_RECID"); map.Generator(Generators.Identity); });
            Property(x => x.OrdPrCount, map => map.Column("ORD_PR_Count"));
            ManyToOne(x => x.Products, map =>
            {
                map.Column("ORD_PR_IDProduct");
                map.PropertyRef("PrRecid");
                map.Cascade(Cascade.None);
            });

            ManyToOne(x => x.Orders, map =>
            {
                map.Column("ORD_PR_IDOrder");
                map.PropertyRef("OrdRecid");
                map.Cascade(Cascade.None);
            });

        }
    }
}
