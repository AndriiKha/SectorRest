using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using RBSector.DataBase.Interfaces;
using RBSector.DataBase.Tools;

namespace RBSector.DataBase.Models
{
    public class Products: BaseModel
    {
        public Products()
        {
            Ordersproducts = new List<Ordersproducts>();
        }
        public virtual int PrRecid { get; set; }
        public virtual string PrName { get; set; }
        public virtual Images Images { get; set; }
        public virtual Category Category { get; set; }
        public virtual Tabs Tabs { get; set; }
        public virtual Ingredients Ingredients { get; set; }
        public virtual decimal PrPrice { get; set; }
        public virtual IList<Ordersproducts> Ordersproducts { get; set; }

        public virtual string Serialize
        {
            get
            {
                return SendDataType.ConvertToString(
                    "\"PrRecid\":" + "\"" + PrRecid + "\"",
                    "\"PrName\":" + "\"" + PrName + "\"",
                    "\"PrPrice\":" + "\"" + PrPrice + "\"",
                    "\"Images\":" + "\"" + (Images != null ? Images.ImRecid : default(int)) + "\"",
                    "\"Category\":" + "\"" + Category.CtRecid + "\"",
                    "\"Tabs\":" + "\"" + Tabs.TbRecid + "\"",
                    "\"Ingredients\":" + "\"" + (Ingredients != null? Ingredients.IgRecid : -1)+ "\"",
                    "\"Ordersproducts\":{" + Ordersproducts.Select(x => x.OrdPrRecid.ToString()).ToList<string>().JSonRecid() + "}"
                    );
            }
        }
        public virtual string SerializeWithComponents
        {
            get
            {
                return SendDataType.ConvertToString(
                    "\"PrRecid\":" + "\"" + PrRecid + "\"",
                    "\"PrName\":" + "\"" + PrName + "\"",
                    "\"PrPrice\":" + "\"" + PrPrice + "\"",
                    "\"Images\":" + "\"" + Images.ImRecid + "\"",
                    "\"Category\":" + "\"" + Category.CtRecid + "\"",
                    "\"Tabs\":" + "\"" + Tabs.TbRecid + "\"",
                    "\"Ingredients\":" + "\"" + (Ingredients != null ? Ingredients.IgRecid : -1) + "\"",
                    SendDataType.Componets<Ordersproducts>(Ordersproducts)
                    );
            }
        }
    }
    public class ProductsMap : ClassMapping<Products>
    {
        public ProductsMap()
        {
            Table("Products");
            Id(x => x.PrRecid, map => { map.Column("PR_RECID"); map.Generator(Generators.Identity); });
            Property(x => x.PrName, map => { map.Column("PR_Name"); map.NotNullable(true); });
            Property(x => x.PrPrice, map => { map.Column("PR_Price"); map.NotNullable(true); });
            ManyToOne(x => x.Images, map =>
            {
                map.Column("PR_IDImage");
               // map.NotNullable(false);
            });

            ManyToOne(x => x.Category, map =>
            {
                map.Column("PR_IDCategory");
               // map.PropertyRef("CtRecid");
            });

            ManyToOne(x => x.Tabs, map => { map.Column("PR_IDTab");});

            ManyToOne(x => x.Ingredients, map =>
            {
                map.Column("PR_IDIngredients");
               // map.NotNullable(true);
            });

            Bag<Ordersproducts>(x => x.Ordersproducts, colmap => { colmap.Key(x => x.Column("ORD_PR_IDProduct")); colmap.Inverse(true);}, map => { map.OneToMany(); });
            //Bag<Ordersproducts>(x => x.Ordersproducts,cp=> { },cr=>cr.OneToMany(y=>y.Class(typeof(Ordersproducts))));
        }
    }
}
