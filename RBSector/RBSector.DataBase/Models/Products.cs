using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;

namespace RBSector.DataBase.Models
{
    public class Products
    {
        public Products()
        {
            //Ordersproducts = new List<Ordersproducts>();
        }
        public virtual int PrRecid { get; set; }
        public virtual string PrName { get; set; }
        public virtual Images Images { get; set; }
        public virtual Category Category { get; set; }
        public virtual Tabs Tabs { get; set; }
        public virtual Ingredients Ingredients { get; set; }
        public virtual decimal PrPrice { get; set; }
        public virtual IList<Ordersproducts> Ordersproducts { get; set; }
    }
    public class ProductsMap : ClassMapping<Products>
    {
        public ProductsMap()
        {
            Id(x => x.PrRecid, map => { map.Column("PR_RECID"); map.Generator(Generators.Identity); });
            Property(x => x.PrName, map => { map.Column("PR_Name"); map.NotNullable(true); });
            Property(x => x.PrPrice, map => { map.Column("PR_Price"); map.NotNullable(true); });
            ManyToOne(x => x.Images, map =>
            {
                map.Column("PR_IDImage");
                map.NotNullable(false);
                map.Cascade(Cascade.None);
            });

            ManyToOne(x => x.Category, map =>
            {
                map.Column("PR_IDCategory");
                map.PropertyRef("CtRecid");
                map.Cascade(Cascade.None);
            });

            ManyToOne(x => x.Tabs, map => { map.Column("PR_IDTab"); map.Cascade(Cascade.None); });

            ManyToOne(x => x.Ingredients, map =>
            {
                map.Column("PR_IDIngredients");
                map.NotNullable(true);
                map.Cascade(Cascade.None);
            });

            Bag<Ordersproducts>(x => x.Ordersproducts, colmap => { colmap.Key(x => x.Column("ORD_PR_IDProduct")); colmap.Inverse(true); colmap.Lazy(CollectionLazy.NoLazy);}, map => { map.OneToMany(); });
            //Bag<Ordersproducts>(x => x.Ordersproducts,cp=> { },cr=>cr.OneToMany(y=>y.Class(typeof(Ordersproducts))));
        }
    }
}
