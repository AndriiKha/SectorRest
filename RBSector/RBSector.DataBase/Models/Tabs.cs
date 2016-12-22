using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;

namespace RBSector.DataBase.Models
{
    public class Tabs
    {
        public Tabs()
        {
            Category = new List<Category>();
            Products = new List<Products>();
        }
        public virtual int TbRecid { get; set; }
        public virtual string TbName { get; set; }
        public virtual IList<Category> Category { get; set; }
        public virtual IList<Products> Products { get; set; }
    }
    public class TabsMap : ClassMapping<Tabs>
    {

        public TabsMap()
        {
            Id(x => x.TbRecid, map => { map.Column("TB_RECID"); map.Generator(Generators.Identity); });
            Property(x => x.TbName, map => { map.Column("TB_Name"); map.NotNullable(true); });
            Bag(x => x.Category, colmap => { colmap.Key(x => x.Column("CT_IDTab")); colmap.Inverse(true); }, map => { map.OneToMany(); });
            Bag(x => x.Products, colmap => { colmap.Key(x => x.Column("PR_IDTab")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }

    }
}
