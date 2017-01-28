using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using RBSector.DataBase.Tools;

namespace RBSector.DataBase.Models
{
    public class Orders: BaseModel
    {
        public Orders()
        {
            Ordersproducts = new List<Ordersproducts>();
        }
        public virtual int OrdRecid { get; set; }
        public virtual Usersdata Usersdata { get; set; }
        public virtual DateTime OrdOrderdate { get; set; }
        public virtual decimal OrdPricecost { get; set; }
        public virtual decimal OrdGetmoney { get; set; }
        public virtual IList<Ordersproducts> Ordersproducts { get; set; }
        public virtual string Serialize
        {
            get
            {
                return SendDataType.ConvertToString(
                    "\"OrdRecid\":" + "\"" + OrdRecid + "\"",
                    "\"OrdOrderdate\":" + "\"" + OrdOrderdate + "\"",
                    "\"OrdPricecost\":" + "\"" + OrdPricecost + "\"",
                    "\"OrdGetmoney\":" + "\"" + OrdGetmoney + "\"",
                    "\"Usersdata\":" + "\"" + Usersdata.UsrRecid + "\"",
                    "\"Ordersproducts\":{" + Ordersproducts.Select(x => x.OrdPrRecid.ToString()).ToList<string>().JSonRecid() + "}"
                    );
            }
        }
        public virtual string SerializeWithComponents
        {
            get
            {
                return SendDataType.ConvertToString(
                   "\"OrdRecid\":" + "\"" + OrdRecid + "\"",
                   "\"OrdOrderdate\":" + "\"" + OrdOrderdate + "\"",
                   "\"OrdPricecost\":" + "\"" + OrdPricecost + "\"",
                   "\"OrdGetmoney\":" + "\"" + OrdGetmoney + "\"",
                   "\"Usersdata\":" + "\"" + Usersdata.UsrRecid + "\"",
                   SendDataType.Componets<Ordersproducts>(Ordersproducts)
               );
            }
        }
    }
    public class OrdersMap : ClassMapping<Orders>
    {

        public OrdersMap()
        {
            Table("Orders");
            Id(x => x.OrdRecid, map => { map.Column("ORD_RECID"); map.Generator(Generators.Identity); });
            Property(x => x.OrdOrderdate, map => { map.Column("ORD_OrderDate"); map.NotNullable(true); });
            Property(x => x.OrdPricecost, map => { map.Column("ORD_PriceCost"); map.NotNullable(true); });
            Property(x => x.OrdGetmoney, map => { map.Column("ORD_GetMoney"); map.NotNullable(true); });
            ManyToOne(x => x.Usersdata, map => { map.Column("ORD_UserID"); map.Cascade(Cascade.None); });

            Bag(x => x.Ordersproducts, colmap => { colmap.Key(x => x.Column("ORD_PR_IDOrder")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
