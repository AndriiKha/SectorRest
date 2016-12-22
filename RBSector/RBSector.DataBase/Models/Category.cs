using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;

namespace RBSector.DataBase.Models
{
    public class Category
    {
        public Category()
        {
            Products = new List<Products>();
        }
        public virtual int CtRecid { get; set; }
        public virtual Tabs Tabs { get; set; }
        public virtual string CtName { get; set; }
        public virtual IList<Products> Products { get; set; }
    }
    public class CategoryMap : ClassMapping<Category>
    {

        public CategoryMap()
        {
            Id(x => x.CtRecid, map => { map.Column("CT_RECID"); map.Generator(Generators.Identity); });
            Property(x => x.CtName, map => { map.Column("CT_Name"); map.NotNullable(true); });
            ManyToOne(x => x.Tabs, map => { map.Column("CT_IDTab"); map.Cascade(Cascade.None); });

            Bag(x => x.Products, colmap => { colmap.Key(x => x.Column("PR_IDCategory")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
